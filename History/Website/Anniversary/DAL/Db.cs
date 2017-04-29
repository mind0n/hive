using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using DAL.Entities;
using Joy.Core;

namespace DAL
{
	public class Db
	{
		private const string STR_Anniversarymdb = "Anniversary.mdb";

		#region Static Fields

		public static string BasePath;
		public static string DBFile;
		public static Db Instance = new Db();
		public readonly static ReaderWriterLock Lock = new ReaderWriterLock();
		private static bool isDbNew;

		public static bool IsDbNew
		{
			get { return Db.isDbNew; }
		}

		#endregion

		public Category RootCategory;
		public UserCollection Users;
		public ViewCollection Views;
		public CategoryViewMappingCollection CVMappings;

		public Db()
		{
		}

		static Db()
		{
			BasePath = AppDomain.CurrentDomain.BaseDirectory;
			DBFile = string.Concat(BasePath, STR_Anniversarymdb);
			LoadDb();
		}

		private static void InitializeDb()
		{
			Instance.RootCategory = new Category();
			Instance.Users = new UserCollection();
			Instance.Views = new ViewCollection();
			Instance.CVMappings = new CategoryViewMappingCollection();
		}

		public void Initialize()
		{
			RootCategory.Initialize();
			CVMappings.Initialize();
		}

		public static void LoadDb()
		{
			if (!File.Exists(DBFile))
			{
				Lock.AcquireReaderLock(Int32.MaxValue);
				isDbNew = true;
				Db.InitializeDb();
				Lock.ReleaseReaderLock();
			}
			else
			{
				Lock.AcquireWriterLock(Int32.MaxValue);
				isDbNew = false;
				string content = File.ReadAllText(DBFile);
				Instance = content.FromXml<Db>();
				Instance.Initialize();
				Lock.ReleaseWriterLock();
			}
		}

		public static void SaveDb()
		{
			Lock.AcquireReaderLock(Int32.MaxValue);
			Instance.ToXml(DBFile);
			Lock.ReleaseReaderLock();
		}

		public static void LockReadAndWrite()
		{
			Lock.AcquireWriterLock(Int32.MaxValue);
		}

		public static void LockWrite()
		{
			Lock.AcquireReaderLock(Int32.MaxValue);
		}

		public static void ReleaseWrite()
		{
			Lock.ReleaseReaderLock();
		}

		public static void ReleaseReadAndWrite()
		{
			Lock.ReleaseWriterLock();
		}
	}
}
