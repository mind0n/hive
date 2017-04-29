using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Fs;

namespace Native.Desktop
{
	[Serializable]
	public class Reminder
	{
		public Reminder()
		{
			readyToStart = false;
			InitTimer();
		}
		public delegate void VoidDlgRmdSpan(Reminder rmd, TimeSpan timeRemains);
		public delegate bool BoolDlgRmd(Reminder rmd);
		public delegate bool BoolDlgRmdBool(Reminder rmd, bool exist);
		protected delegate bool BoolDlgVoid();

		[NonSerialized]
		public BoolDlgRmd OnElapsed;
		[NonSerialized]
		public VoidDlgRmdSpan OnElapsing;
		[NonSerialized]
		public BoolDlgRmdBool OnElapsedComplete;
		protected BoolDlgVoid OnStart;

		public int SecondsEarlier = 0;

		public DateTime RemindPoint
		{
			get
			{
				return rmdPoint;
			}
		}protected DateTime rmdPoint;
		[NonSerialized]
		protected Timer ragularTmr;
		protected bool readyToStart;
		public virtual string GetRemindTimeString()
		{
			return rmdPoint.Hour + ":" + rmdPoint.Minute;
		}
		public virtual DateTime GetRemindTime()
		{
			return rmdPoint;
		}
		public virtual void SetRemindTime(DateTime dt)
		{
			if (dt != null)
			{
				rmdPoint = dt;
				readyToStart = true;
			}
		}
		public virtual void SetRemindTime(TimeSpan ts)
		{
			if (ts != null)
			{
				rmdPoint = DateTime.Now.Add(ts);
				readyToStart = true;
			}
		}
		public virtual bool Start()
		{
			if (readyToStart)
			{
				if (OnStart != null && OnStart())
				{
					//Console.WriteLine("Started");
					TimeSpan ts = rmdPoint.Subtract(DateTime.Now);
					if (ts.TotalSeconds < 0 && ts.TotalSeconds > -20)
					{
						//Console.WriteLine("Invalid reminder");
						tmr_Elapsed(this, null);
						return true;
					}
					else
					{
						if (IsRemindPointPassed())
						{
							if (!SetNextRemindPoint())
							{
								return false;
							}
						}
					}
					ragularTmr.Enabled = true;
					ragularTmr.Start();
					return true;
				}
				return false;
			}
			return false;
		}
		public virtual void Stop()
		{
			ragularTmr.Enabled = false;
			ragularTmr.Stop();
		}
		public virtual void Reset()
		{
			Stop();
			rmdPoint = DateTime.Now;
			OnElapsed = null;
			OnElapsedComplete = null;
			OnElapsing = null;
		}
		protected bool IsRemindPointPassed()
		{
			return ((TimeSpan)rmdPoint.Subtract(DateTime.Now)).TotalDays <= 0;
		}
		protected void InitTimer()
		{
			ragularTmr = new Timer();
			ragularTmr.Elapsed += new ElapsedEventHandler(ragularTmr_Elapsed);
			ragularTmr.Interval = 1000;	// 
		}
		protected virtual bool SetNextRemindPoint() { return false; }
		protected virtual bool ExclusionExists() { return true; }
		protected virtual void tmr_Elapsed(object sender, ElapsedEventArgs e)
		{
			ragularTmr.Stop();
			readyToStart = false;
			bool isExclusionExist = ExclusionExists();
			if (OnElapsed != null && !isExclusionExist)
			{
				OnElapsed(this);
			}
			if (OnElapsedComplete != null)
			{
				OnElapsedComplete(this, isExclusionExist);
			}
			if (SetNextRemindPoint())
			{
				Start();
			}
		}
		protected void ragularTmr_Elapsed(object sender, ElapsedEventArgs e)
		{
			TimeSpan ts = rmdPoint.Subtract(DateTime.Now);
			if (OnElapsing != null)
			{
				OnElapsing(this, ts);
			}
			if (ts.TotalDays <= 0)
			{
				tmr_Elapsed(sender, e);
			}
		}

	}
	[Serializable]
	public class RepeatableReminder : Reminder
	{
		public RepeatableReminder()
		{
			InfiniteLoop = true;
			CurtRepeatPos = 0;
			OnStart += OnTimerStart;
			repeats = new List<Item>();
			excludes = new List<Item>();
		}
		[Serializable]
		public class Item
		{
			public ItemType Type = ItemType.None;
			public DateTime Date;
			public TimeSpan Span;
			public int Day;
			public string WeekDay;
			public bool IsTodayMatch()
			{
				if (Type == ItemType.DayOfMonth)
				{
					if (DateTime.Now.Day == Day)
					{
						return true;
					}
				}
				else if (Type == ItemType.DayOfWeek)
				{
					if (DateTime.Now.DayOfWeek.ToString().Equals(WeekDay))
					{
						return true;
					}
				}
				else if (Type == ItemType.ExplicitDate)
				{
					int y = DateTime.Now.Year;
					int m = DateTime.Now.Month;
					int d = DateTime.Now.Day;
					if (y == Date.Year && m == Date.Month && d == Date.Day)
					{
						return true;
					}
				}
				return false;
			}
			public TimeSpan GetTimespan()
			{
				if (Type == ItemType.ExplicitDate)
				{
					TimeSpan ts = Date.Subtract(DateTime.Now);
					return ts;
				}
				else if (Type == ItemType.TimeSpan)
				{
					return Span;
				}
				else if (Type == ItemType.DayOfMonth)
				{
					DateTime dtCur = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day);
					DateTime dtNex = dtCur.AddMonths(1);
					return GetRelativeTimeSpan(dtCur, dtNex);
				}
				else if (Type == ItemType.DayOfWeek)
				{
					DateTime dtCur = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day);
					DateTime dtNex = dtCur.AddDays(7);
					return GetRelativeTimeSpan(dtCur, dtNex);
				}
				throw new Exception("Unknown item type: " + Type);
			}
			protected TimeSpan GetRelativeTimeSpan(DateTime dtCur, DateTime dtNex)
			{
				TimeSpan tsCur = dtCur.Subtract(DateTime.Now);
				if (tsCur.TotalDays < 0)
				{
					return dtNex.Subtract(DateTime.Now);
				}
				else if (tsCur.TotalDays == 0)
				{
					return new TimeSpan();
				}
				else
				{
					return tsCur;
				}
			}
			public Item(DateTime dt)
			{
				Date = dt;
				Type = ItemType.ExplicitDate;
			}
			public Item(TimeSpan ts)
			{
				Span = ts;
				Type = ItemType.TimeSpan;
			}
			public Item(int dayOfMonth)
			{
				Day = dayOfMonth;
				Type = ItemType.DayOfMonth;
			}
			public Item(string dayOfWeek)
			{
				WeekDay = dayOfWeek;
				Type = ItemType.DayOfWeek;
			}
			public Item()
			{
				Type = ItemType.TimeSpan;
				Span = new TimeSpan();
			}
		}
		public enum ItemType : int
		{
			None,
			TimeSpan,
			DayOfWeek,
			DayOfMonth,
			ExplicitDate
		}
		public string Settings;
		public bool InfiniteLoop;
		public Dictionary<string, object> ExtendedProperty = new Dictionary<string,object>();
		protected List<Item> repeats;
		protected List<Item> excludes;
		protected DateTime RemindTime;
		protected int CurtRepeatPos;
		public override string GetRemindTimeString()
		{
			DateTime dt = GetRemindTime();
			string rlt = "";
			if (dt.Hour < 10)
			{
				rlt += "0" + dt.Hour;
			}
			else
			{
				rlt += dt.Hour;
			}
			rlt += ":";
			if (dt.Minute < 10)
			{
				rlt += "0" + dt.Minute;
			}
			else
			{
				rlt += dt.Minute;
			}
			return rlt;
		}
		public override DateTime  GetRemindTime()
		{
			if (repeats.Count > 0)
			{
				Item item = repeats[0];
				DateTime dt = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, RemindTime.Hour, RemindTime.Minute, RemindTime.Second);
				return dt;
			}
			throw new Exception("Remind time missing");
		}
		public bool SetReminder(string strsettings)
		{
			// "16:37;2010/08/25;>i1000;"
			string[] settings = strsettings.Split(';');
			int l = settings.Length;
			string startTime = settings[0];
			string strexclude = settings[l - 1];
			string[] excludeList = strexclude.Split('|');
			int i;
			for (i = 1; i < l - 1; i++)
			{
				string sitem = settings[i];
				Item repeatItem = ParseItem(sitem);
				if (repeatItem.Type == ItemType.None)
				{
					InfiniteLoop = false;
				}
				repeats.Add(repeatItem);
			}
			for (i = 0; i < excludeList.Length; i++)
			{
				string sitem = excludeList[i];
				Item excludeItem = ParseItem(sitem);
				excludes.Add(excludeItem);
			}
			if (!string.IsNullOrEmpty(startTime))
			{
				RemindTime = DateTime.Parse(startTime);
			}
			else
			{
				RemindTime = DateTime.Now;
			}
			if (repeats.Count == 0)
			{
				Item repeatItem = new Item(DateTime.Now);
				repeats.Add(repeatItem);
				repeats.Add(new Item());
			}
			Settings = strsettings;
			SetNextRemindPoint();
			return readyToStart;
		}
		public override void Reset()
		{
			base.Reset();
			CurtRepeatPos = 0;
			RemindTime = DateTime.Now;
			Settings = string.Empty;
			repeats.Clear();
			excludes.Clear();
			ExtendedProperty.Clear();
			repeats.Add(new Item(RemindTime));
			repeats.Add(new Item());
		}
		protected virtual void NoFurtherRepeat() { }
		protected Item ParseItem(string stritem)
		{
			Item item = new Item();
			if (string.IsNullOrEmpty(stritem))
			{
				return item;
			}
			string body = stritem.Substring(1);
			char prefix = stritem[0];
			if (prefix == 'm')
			{
				int n;
				int.TryParse(body, out n);
				if (string.IsNullOrEmpty(body))
				{
					n = DateTime.Now.Day;
				}
				item = new Item(n);
			}
			else if (prefix == '>')
			{
				string spanBody = body.Substring(1);
				prefix = body[0];
				if (prefix == 'd')
				{
					item = new Item(TimeSpan.FromDays(Convert.ToDouble(spanBody)));
				}
				else if (prefix == 'm')
				{
					item = new Item(TimeSpan.FromMinutes(Convert.ToDouble(spanBody)));
				}
				else if (prefix == 'h')
				{
					item = new Item(TimeSpan.FromHours(Convert.ToDouble(spanBody)));
				}
				else if (prefix == 'i')
				{
					item = new Item(TimeSpan.FromMilliseconds(Convert.ToDouble(spanBody)));
				}
			}
			else if (prefix == 'w')
			{
				item = new Item(body);
			}
			else
			{
				item = new Item(DateTime.Parse(stritem));
			}
			return item;
		}
		protected bool OnTimerStart()
		{
			return true;
		}
		protected override bool ExclusionExists()
		{
			foreach (Item i in excludes)
			{
				if (i.IsTodayMatch())
				{
					return true;
				}
			}
			return false;
		}
		protected override bool SetNextRemindPoint()
		{
			int l = repeats.Count, cmpdate = 0;
			readyToStart = true;
			if (l <= 0 || CurtRepeatPos >= l)
			{
				//Console.WriteLine("No repeat item");
				readyToStart = false;
				return readyToStart;
			}
			while (cmpdate <= 0)
			{
				Item item = repeats[CurtRepeatPos];
				if (item.Type == ItemType.TimeSpan && item.Span.TotalMilliseconds == 0)
				{
					NoFurtherRepeat();
					readyToStart = false;
					break;
				}
				if (item.Type == ItemType.ExplicitDate)
				{
					rmdPoint = new DateTime(
						item.Date.Year,
						item.Date.Month,
						item.Date.Day,
						RemindTime.Hour,
						RemindTime.Minute,
						RemindTime.Second,
						RemindTime.Millisecond);
				}
				else if (item.Type == ItemType.DayOfMonth)
				{
					int year, month;
					if (rmdPoint == null)
					{
						year = DateTime.Now.Year;
						month = DateTime.Now.Month;
					}
					else
					{
						year = rmdPoint.Year;
						month = rmdPoint.Month;
					}
					DateTime dt = new DateTime(
						year,
						month,
						item.Day,
						RemindTime.Hour,
						RemindTime.Minute,
						RemindTime.Second);
					if (DateTime.Now.CompareTo(dt) <= 0)
					{
						rmdPoint = new DateTime(
							year,
							month,
							item.Day,
							RemindTime.Hour,
							RemindTime.Minute,
							RemindTime.Second);
					}
					else
					{
						rmdPoint = new DateTime(
							year,
							month + 1,
							item.Day,
							RemindTime.Hour,
							RemindTime.Minute,
							RemindTime.Second);
					}
				}
				else if (item.Type == ItemType.DayOfWeek)
				{
					//rmdPoint = DateTime.Now.g
					DateTime dt = DateTime.Now;
					for (int i = 1; i <= 7; i++)
					{
						dt = dt.AddDays(1);
						if (dt.DayOfWeek.ToString().Equals(item.WeekDay))
						{
							rmdPoint = new DateTime(dt.Year, dt.Month, dt.Day, RemindTime.Hour, RemindTime.Minute, RemindTime.Second);
						}
					}
					if (!rmdPoint.DayOfWeek.ToString().Equals(item.WeekDay))
					{
						readyToStart = false;
					}
				}
				else
				{
					rmdPoint = rmdPoint.Add(item.GetTimespan());
				}
				CurtRepeatPos++;
				if (CurtRepeatPos == repeats.Count)
				{
					CurtRepeatPos = 1;
				}
				cmpdate = rmdPoint.CompareTo(DateTime.Now);
			}
			return readyToStart;
		}
	}
	[Serializable]
	public class SavableReminder : RepeatableReminder
	{
		public SavableReminder()
		{
			//Filename = Guid.NewGuid().ToString() + ".rmd";
		}

		public delegate void RemindDeletedDelegate(SavableReminder rmd);
		public RemindDeletedDelegate OnRemindDeleted;

		public string FullFilename
		{
			get
			{
				if (!string.IsNullOrEmpty(_baseDir) && _baseDir[_baseDir.Length - 1] == '\\')
				{
					return _baseDir + Filename;
				}
				else
				{
					return _baseDir + "\\" + Filename;
				}
			}
			set
			{
				if (File.Exists(value))
				{
					int lastPos = value.LastIndexOf('\\');
					if (lastPos == 0)
					{
						Filename = value.Substring(1);
					}
					else if (lastPos < 0)
					{
						Filename = value;
					}
					else
					{
						Filename = value.Substring(lastPos + 1, value.Length - lastPos - 1);
						_baseDir = value.Substring(0, lastPos);
					}
				}
			}
		}
		public string BaseDir
		{
			get
			{
				return _baseDir;
			}
			set
			{
				_baseDir = value;
				if (!string.IsNullOrEmpty(_baseDir))
				{
					if (_baseDir[_baseDir.Length - 1] != '\\')
					{
						_baseDir += '\\';
					}
				}
			}
		}protected string _baseDir;
		public string Filename;
		public Exception Save()
		{
			return Save(FullFilename);
		}
		public Exception Save(string fullFilename)
		{
			Exception rlt = null;
			FileStream fs = null;
			try
			{
				fs = new FileStream(fullFilename, FileMode.Create, FileAccess.Write);
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, this);
				FullFilename = fullFilename;
			}
			catch (Exception err)
			{
				rlt = err;
				Exceptions.LogOnly(err);
			}
			finally
			{
				fs.Close();
			}
			return rlt;
		}
		public Exception Delete()
		{
			Exception rlt = null;
			Reset();
			if (!string.IsNullOrEmpty(Filename))
			{
				try
				{
					File.Delete(FullFilename);
					if (OnRemindDeleted != null)
					{
						OnRemindDeleted(this);
					}
				}
				catch (Exception err)
				{
					rlt = err;
					Exceptions.LogOnly(err);
				}
			}else
			{
				rlt = Exceptions.LogOnly("Note not exist.");
			}
			return rlt;
		}
		public static SavableReminder Load(string fullFilename)
		{
			SavableReminder rlt = null;
			FileStream fs = null;
			try
			{
				fs = new FileStream(fullFilename, FileMode.Open, FileAccess.Read);
				BinaryFormatter bf = new BinaryFormatter();
				object content = bf.Deserialize(fs);
				rlt = (SavableReminder)content;
				rlt.OnLoadedFromFile(fullFilename);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
			finally
			{
				fs.Close();
			}
			return rlt;
		}
		protected virtual void OnLoadedFromFile(string filename)
		{
			//Filename = filename;
			FullFilename = filename;
			InitTimer();
			OnElapsed = null;
			readyToStart = true;
		}
		protected override void NoFurtherRepeat()
		{
			Delete();
		}
	}

}
