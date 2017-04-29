/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				PermissionPrincipal.cs                                    			*
 *      Description:																*
 *				 方法权限检测认证类							    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2008年5月12日														*
 *      History:																	*
 ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Diagnostics;
using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 方法属性权限检测类
    /// </summary>
    public class PermissionPrincipal:IPrincipal
    {
        private IIdentity _identity;
        private string[] _roles;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_IID"></param>
        public PermissionPrincipal(IIdentity _IID)
        {
            _identity = _IID;
            _roles = new string[1]{"check"};
        }

        /// <summary>
        /// 检测角色资料
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return Check_PopedomTypeAttaible();
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }


        private bool Check_PopedomTypeAttaible()
        {
            //System.Web.HttpResponse rp = System.Web.HttpContext.Current.Response;
            //rp.Write("执行方法名称!");
            //rp.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
            //rp.Write("<br>");
            StackTrace stack = new StackTrace();

            foreach (StackFrame sframe in stack.GetFrames())
            {
                //rp.Write(sframe.GetMethod().Name);
                //rp.Write("<br>");
                foreach (PopedomTypeAttaible var in sframe.GetMethod().GetCustomAttributes(typeof(PopedomTypeAttaible), true))
                {
                    //rp.Write(var.PType.ToString());
                    //rp.Write("<br>");
                    //rp.Write("无权限!");
                    //rp.End();
                    //return false;
                    FrameWorkPermission.CheckPermissionVoid(var.PType);
                }
                //rp.Write("------");
                //rp.Write("<br>");
            }
            //rp.End();
            return true;
        }
    }

    /// <summary>
    /// 权限方法属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PopedomTypeAttaible : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="PT"></param>
        public PopedomTypeAttaible(PopedomType PT)
        {
            _PType = PT;
        }

        /// <summary>
        /// 权限类型
        /// </summary>
        private PopedomType _PType;
        /// <summary>
        /// 权限类型
        /// </summary>
        public PopedomType PType
        {
            get {
                return _PType;
            }
        }
        
    }
}
