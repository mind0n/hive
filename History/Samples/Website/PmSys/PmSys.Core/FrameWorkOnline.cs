/*
       Copyright (C) 2008 supesoft.com,All Rights Reserved						    
       File:																		
 				FrameWorkOnline.cs                                              			*
       Description:																
 				 在线用户类
       Author:																													
 				http://www.supesoft.com												
       Finish DateTime:															
 				2008年10月12日														
       History:																	
*/
using System;
using System.Collections.Generic;
using System.Text;

using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 在线用户类
    /// </summary>
    public class FrameWorkOnline
    {
        private static IFrameWorkOnline _Online = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        static FrameWorkOnline()
        {
            IFrameWorkOnline online;
            if (Common.GetOnlineCountType == OnlineCountType.DataBase)
            {
                online = new FrameWorkOnlineDataBase();
            }
            else
            {
                online = new FrameWorkOnlineCache();
            }
            _Online = online;
        }

        /// <summary>
        /// 在线用户接口
        /// </summary>
        /// <returns>IFrameWorkOnline实现类</returns>
        public static IFrameWorkOnline Instance()
        {
            return _Online;
        }
    }
}
