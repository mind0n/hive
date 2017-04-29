/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				IFrameWorkCache.cs                                              			*
 *      Description:																*
 *				 �������ݽӿ�                 								    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2008��5��2��														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys
{
    /// <summary>
    /// ����ӿ�
    /// </summary>
    public interface IFrameWorkCache
    {
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="o">ֵ</param>
        void Insert(string key, object o);

        /// <summary>
        /// �Ƴ�ֵ
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// ���ֵ
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <returns>����ֵ</returns>
        object this[string key]
        {
            get;
        }

        /// <summary>
        /// ���ʹ�û�������
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// ���ʣ�໺���С
        /// </summary>
        string Remains
        {
            get;
        }
    }
}
