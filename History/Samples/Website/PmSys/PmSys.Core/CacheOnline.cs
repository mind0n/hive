/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				CacheOnline.cs	                                        			*
 *      Description:																*
 *				 用户在线类   													    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PmSys
{
    /// <summary>
    /// 用户在线类
    /// </summary>
    /// <typeparam name="TKey">Key</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public class CacheOnline<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : OnlineUser<TKey>, new()
    {

        //会员列表
        Dictionary<string, LinkedListNode<TValue>> _MemberUserList;
        //所人数列表
        Dictionary<TKey, LinkedListNode<TValue>> _AllUserList;
        //所有会员列表排序
        LinkedList<TValue> _TValueLink;
        /// <summary>
        /// Cache锁
        /// </summary>
        protected ReaderWriterLock _CacheDataRwl = new ReaderWriterLock();
        //定时器
        System.Timers.Timer _UpdateTimer;
        //用户登陆超时设置(毫秒)
        double _TimeOutMilliseconds;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="LoginTimeOutMinute">用户登陆超时设置(分钟)</param>
        public CacheOnline(int LoginTimeOutMinute)
        {
            if (LoginTimeOutMinute == 0)
                LoginTimeOutMinute = 30;
            _MemberUserList = new Dictionary<string, LinkedListNode<TValue>>();
            _AllUserList = new Dictionary<TKey, LinkedListNode<TValue>>();
            _TValueLink = new LinkedList<TValue>();
            _TimeOutMilliseconds = 1000 * 60 * LoginTimeOutMinute;
            _UpdateTimer = new System.Timers.Timer();
            _UpdateTimer.AutoReset = true;
            _UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(ClearTimeOutUser);
            _UpdateTimer.Interval = _TimeOutMilliseconds;
            _UpdateTimer.Start();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CacheOnline() : this(30) { }

        /// <summary>
        /// 清除到期在线用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTimeOutUser(object sender, System.Timers.ElapsedEventArgs e)
        {
            double NextRunTime = _TimeOutMilliseconds;
            DateTime _CheckTime = e.SignalTime.AddMilliseconds(_TimeOutMilliseconds * -1);
            _UpdateTimer.Stop();
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);
                if (_TValueLink.Count > 0)
                {
                    LinkedListNode<TValue> UserNode = _TValueLink.Last;
                    TimeSpan ts = UserNode.Value.U_LastTime - _CheckTime;
                    if (ts.TotalMilliseconds <= 0)
                    {
                        if (UserNode.Previous != null)
                        {
                            ts = UserNode.Previous.Value.U_LastTime - _CheckTime;
                            if (ts.TotalMilliseconds <= 0)
                                NextRunTime = 1;
                            else
                                NextRunTime = ts.TotalMilliseconds;
                        }
                        Remove(UserNode);
                    }
                    else
                    {
                        NextRunTime = ts.TotalMilliseconds;
                    }
                }
                _UpdateTimer.Interval = NextRunTime;
            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
            _UpdateTimer.Start();
        }

        /// <summary>
        /// 获取所有用户列表(IEnumerable)
        /// </summary>
        public IEnumerable<TValue> GetListIEnumerable
        {
            get
            {
                _CacheDataRwl.AcquireReaderLock(Timeout.Infinite);
                foreach (TValue var in _TValueLink)
                {
                    yield return var;
                }
                _CacheDataRwl.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// 获取在线用户列表(List)
        /// </summary>
        public List<TValue> GetList
        {
            get
            {
                _CacheDataRwl.AcquireReaderLock(Timeout.Infinite);
                List<TValue> lst = new List<TValue>();
                foreach (TValue var in _TValueLink)
                {
                    lst.Add(var);
                }
                _CacheDataRwl.ReleaseReaderLock();
                return lst;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="U_Name">用户名</param>
        /// <returns></returns>
        public TValue GetValue(string U_Name)
        {
            TValue value = default(TValue);
            LinkedListNode<TValue> NodeUser;
            try
            {
                _CacheDataRwl.AcquireReaderLock(Timeout.Infinite);
                if (_MemberUserList.TryGetValue(U_Name, out NodeUser))
                {
                    value = NodeUser.Value;
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseReaderLock();
            }
            return value;
        }

        #region "插入用户"
        /// <summary>
        /// 插入游客
        /// </summary>
        /// <param name="key"></param>
        public void InsertUser(TKey key)
        {
            InsertUser(key, new TValue());
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void InsertUser(TKey key, TValue value)
        {
            LinkedListNode<TValue> OldUserNode_A;
            value.U_Guid = key;
            LinkedListNode<TValue> UserNode = new LinkedListNode<TValue>(value);
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);


                if (_AllUserList.TryGetValue(key, out OldUserNode_A))
                {
                    OldUserNode_A.Value = UserNode.Value;
                    ReNewUser(OldUserNode_A);
                }
                else
                {
                    if (value.U_Type)
                    {
                        if (!_MemberUserList.TryGetValue(value.U_Name, out OldUserNode_A))
                        {
                            _MemberUserList.Add(value.U_Name, UserNode);
                            _AllUserList.Add(key, UserNode);
                            ReNewUser(UserNode);
                        }
                        else
                        {
                            OldUserNode_A.Value = UserNode.Value;
                            ReNewUser(OldUserNode_A);
                        }
                    }
                    else
                    {
                        _AllUserList.Add(key, UserNode);
                        ReNewUser(UserNode);
                    }
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
        }
        #endregion

        /// <summary>
        /// 更新用户请求信息
        /// </summary>
        /// <param name="key"></param>
        public void Access(TKey key)
        {
            LinkedListNode<TValue> OutNodeUser;
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);

                if (_AllUserList.TryGetValue(key, out OutNodeUser))
                {
                    ReNewUser(OutNodeUser);
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 访问请求 更新key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="U_LastUrl"></param>
        public void Access(TKey key, string U_LastUrl)
        {
            LinkedListNode<TValue> OutNodeUser;
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);

                if (_AllUserList.TryGetValue(key, out OutNodeUser))
                {
                    ReNewUser(OutNodeUser, U_LastUrl);
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
        }


        /// <summary>
        /// 重建用户列表排序
        /// </summary>
        /// <param name="UserNode"></param>
        private void ReNewUser(LinkedListNode<TValue> UserNode)
        {
            if (UserNode.List != null)
            {
                UserNode.List.Remove(UserNode);
            }
            _TValueLink.AddFirst(UserNode);
            TimeSpan ts = DateTime.Now - UserNode.Value.U_StartTime;
            UserNode.Value.U_OnlineSeconds = ts.TotalSeconds;
            UserNode.Value.U_LastTime = DateTime.Now;


        }

        /// <summary>
        /// 重建
        /// </summary>
        /// <param name="UserNode"></param>
        /// <param name="U_LastUrl"></param>
        private void ReNewUser(LinkedListNode<TValue> UserNode, string U_LastUrl)
        {
            if (UserNode.List != null)
            {
                UserNode.List.Remove(UserNode);
            }
            _TValueLink.AddFirst(UserNode);
            TimeSpan ts = DateTime.Now - UserNode.Value.U_StartTime;
            UserNode.Value.U_OnlineSeconds = ts.TotalSeconds;
            UserNode.Value.U_LastTime = DateTime.Now;
            UserNode.Value.U_LastUrl = U_LastUrl;
        }

        /// <summary>
        /// 根据Key移除用户
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key)
        {
            LinkedListNode<TValue> UserNode;
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);
                if (_AllUserList.TryGetValue(key, out UserNode))
                {
                    Remove(UserNode);
                }

            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 根据用户名移除用户
        /// </summary>
        /// <param name="U_Name"></param>
        public void RemoveUserName(string U_Name)
        {
            LinkedListNode<TValue> UserNode;
            try
            {
                _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);
                if (_MemberUserList.TryGetValue(U_Name, out UserNode))
                {
                    Remove(UserNode);
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 移除用户资料
        /// </summary>
        /// <param name="UserNode"></param>
        private void Remove(LinkedListNode<TValue> UserNode)
        {
            if (UserNode.List != null)
            {
                _AllUserList.Remove(UserNode.Value.U_Guid);
                if (UserNode.Value.U_Type)
                    _MemberUserList.Remove(UserNode.Value.U_Name);
                UserNode.List.Remove(UserNode);
            }
        }

        /// <summary>
        /// 检测Key是否在线
        /// </summary>
        /// <param name="key">用户标识</param>
        /// <returns>True在线 False不在线</returns>
        public bool CheckKeyOnline(TKey key)
        {
            bool rBool = false;
            try
            {
                _CacheDataRwl.AcquireReaderLock(Timeout.Infinite);
                if (_AllUserList.ContainsKey(key))
                {
                    rBool = true;
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseReaderLock();
            }
            return rBool;
        }

        /// <summary>
        /// 根据用户名检测是否在线
        /// </summary>
        /// <param name="U_Name"></param>
        /// <returns></returns>
        public bool CheckMemberOnline(string U_Name)
        {
            bool rBool = false;
            try
            {
                _CacheDataRwl.AcquireReaderLock(Timeout.Infinite);
                if (_MemberUserList.ContainsKey(U_Name))
                {
                    rBool = true;
                }
            }
            finally
            {
                _CacheDataRwl.ReleaseReaderLock();
            }
            return rBool;
        }

        /// <summary>
        /// 所有用户总数
        /// </summary>
        public int AllCount
        {
            get
            {
                return _AllUserList.Count;
            }
        }

        /// <summary>
        /// 会员总数
        /// </summary>
        public int MemberCount
        {
            get
            {
                return _MemberUserList.Count;
            }
        }

        /// <summary>
        /// 清除所有在线人数
        /// </summary>
        public void Clear()
        {
            _CacheDataRwl.AcquireWriterLock(Timeout.Infinite);
            _MemberUserList.Clear();
            _AllUserList.Clear();
            _TValueLink.Clear();
            _CacheDataRwl.ReleaseWriterLock();
        }
    }

    /// <summary>
    /// 用户基础类
    /// </summary>
    public class OnlineUser<TKey>
    {
        #region "Private Variables"
        private TKey _U_Guid;
        private string _U_Name = "游客";
        private DateTime _U_StartTime = DateTime.Now;
        private DateTime _U_LastTime = DateTime.Now;
        private string _U_LastIP = Common.GetIPAddress();
        private bool _U_Type = false;
        private string _U_LastUrl;
        private double _U_OnlineSeconds;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 用户标识值
        /// </summary>
        public TKey U_Guid
        {
            get
            {
                return _U_Guid;
            }
            set
            {
                _U_Guid = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string U_Name
        {
            get
            {
                return _U_Name;
            }
            set
            {
                _U_Name = value;
            }
        }
        /// <summary>
        /// 开始访问时间
        /// </summary>
        public DateTime U_StartTime
        {
            get
            {
                return _U_StartTime;
            }
            set
            {
                _U_StartTime = value;
            }
        }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime U_LastTime
        {
            get
            {
                return _U_LastTime;
            }
            set
            {
                _U_LastTime = value;
            }
        }
        /// <summary>
        /// 是否会员(True会员False游客)
        /// </summary>
        public bool U_Type
        {
            get
            {
                return _U_Type;
            }
            set
            {
                _U_Type = value;
            }
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string U_LastIP
        {
            get
            {
                return _U_LastIP;
            }
            set
            {
                _U_LastIP = value;
            }
        }
        /// <summary>
        /// 最后请求网址
        /// </summary>
        public string U_LastUrl
        {
            get
            {
                return _U_LastUrl;
            }
            set
            {
                _U_LastUrl = value;
            }
        }
        /// <summary>
        /// 在线时长（秒）
        /// </summary>
        public double U_OnlineSeconds
        {
            get
            {
                return _U_OnlineSeconds;
            }
            set
            {
                _U_OnlineSeconds = value;
            }
        }
        #endregion

    }
}
