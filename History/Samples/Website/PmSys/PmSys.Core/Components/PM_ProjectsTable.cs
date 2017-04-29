/************************************************************************************
 *      Copyright (C) 2012 PM,All Rights Reserved			       			        *
 *      File:																		*
 *				PM_ProjectsTable.cs                                 			    *
 *      Description:																*
 *				 应用实体类 		            								    *
 *      Author:																		*
 *      Finish DateTime:															*
 *				20012年9月4日														*
 *      History:	Generate by SP_Gen Tool 										*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PmSys.Components
{
    public class PM_ProjectsTable
    {

        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        public string DB_Option_Action_
        {
            get;
            set;
        }
        private string projectid;
        public string ProjectId
        {
            get;
            set;
        }

        private string projectno;
        public string ProjectNo
        {
            get;
            set;
        }

        private string projectname;
        public string ProjectName
        {
            get;
            set;
        }

        private int projectstatus;
        public int ProjectStatus
        {
            get;
            set;
        }

        private DateTime projectstarttime;
        public DateTime ProjectStartTime
        {
            get;
            set;
        }
        private Double projectDuration;
        public Double ProjectDuration
        {
            get;
            set;
        }
        private DateTime projectendtime;
        public DateTime ProjectEndTime
        {
            get;
            set;
        }

        private string projectbrief;
        public string ProjectBrief
        {
            get;
            set;
        }

        private string projectcomments;
        public string ProjectComments
        {
            get;
            set;
        }

        private string projecticon;
        public string ProjectIcon
        {
            get;
            set;
        }
    }

}
