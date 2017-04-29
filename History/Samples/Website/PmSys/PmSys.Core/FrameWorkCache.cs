/*
       Copyright (C) 2008 supesoft.com,All Rights Reserved						    
       File:																		
 				FrameWorkOnline.cs                                              			*
       Description:																
 				PmSys缓存类
       Author:																													
 				http://www.supesoft.com												
       Finish DateTime:															
 				2008年10月12日														
       History:																	
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys
{
    /// <summary>
    /// PmSys缓存类
    /// </summary>
    public class FrameWorkCache
    {
        private static IFrameWorkCache _FrameWorkCache = null;

        static FrameWorkCache()
        {
            _FrameWorkCache = (IFrameWorkCache)Activator.CreateInstance(Common.GetCachenamespace, Common.GetCacheclassName).Unwrap();
        }

        /// <summary>
        /// 在线用户接口
        /// </summary>
        /// <returns>IFrameWorkOnline实现类</returns>
        public static IFrameWorkCache Instance()
        {
            return _FrameWorkCache;
        }
    }
}
