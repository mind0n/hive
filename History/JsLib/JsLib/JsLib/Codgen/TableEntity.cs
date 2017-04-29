
	using System;
	using System.Collections.Generic;
	using System.Web;
	using Fs.Entities;
	using Fs.Data;
	using System.Collections;
	namespace TestWeb.Entity
	{
		public class DepartmentsEntity : TableEntity{
			public DepartmentsEntity()
			: base("Departments", Dbs.Use< DbSqlServer >("test")){
				//Custom constructor codes here
			}
			public bool Add(DictParams row){
				return true;
			}
			public bool Update(DictParams row){
				return true;
			}
			public bool Delete(ArrayList pks){
				return true;
			}
		}
		public class ManagersEntity : TableEntity{
			public ManagersEntity()
			: base("Managers", Dbs.Use< DbSqlServer >("test")){
				//Custom constructor codes here
			}
			public bool Add(DictParams row){
				return true;
			}
			public bool Update(DictParams row){
				return true;
			}
			public bool Delete(ArrayList pks){
				return true;
			}
		}
	}
	