using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ULib.DataSchema.Alerting
{
    [Table(Name="T1")]
    public class TableRecordEntity : Entity
    {
        [Column(Alias="ItemId", Type=ValueType.Number, IsPrimary=true)]
        public int? ItemKey;

        [Column(Alias = "GroupId", Trigger = "TriggerGroupTypeKey", IsCached = true, Verify = "true", ForeignTable = "PrefixGroup", Column = "GroupKey", Condition = "GroupID like '{0}'", Type = ValueType.Number)]
        public int? GroupKey;

        [Column(Filter="FilterMethod")]
        public string ItemName;

        [Column(Alias="ColA", IsCached = true, Verify = "true", ForeignTable="PrefixD", Column="A", Condition="x in (select d from f where g like '{0}')", Type=ValueType.Number)]
        public int? SubSystemKey;

        [Column(Alias = "ColB", IsCached = true, Verify = "true", ForeignTable = "PrefixC", Column = "B", Condition = "x like '{0}' and GroupKey={1}", ConditionField = "GroupKey", Type = ValueType.Number)]
        public int? SubKeyA;

        [Column]
        public string Region;

        [Column]
        public Guid? UKey;

        [Column(Alias = "ColC", IsCached = true, Verify = "true", ForeignTable = "PrefixB_Type", Column = "ColCKey", Condition = "x like '{0}' and GroupKey={1}", ConditionField = "GroupKey", Type = ValueType.Number)]
        public int ColCKey;

        [Column(Alias = "ColD", IsCached = true, Verify = "true", ForeignTable = "PrefixA_Type", Column = "ColDKey", Condition = "d like '{0}' and GroupTypeKey={1}", ConditionField = "GroupTypeKey", Type = ValueType.Number)]
        public int ColDKey;

        [Column(Type= ValueType.Number, Default = "1")]
        public string UStatus;

        [Column]
        public string SubRegion;

        [Column]
        public string TypeName;

        [Column(Default = "")]
        public string Message;

        [Column(Default="True")]
        public bool? IsEffective;

        [Column(Filter = "DateTimeFilter")]
        public DateTime CreateDate;

        [Column(Filter = "DateTimeFilter")]
        public DateTime ModifyDate;

        public int GroupTypeKey;

        public TableRecordEntity()
        {
        }
        public string DateTimeFilter(string name = null)
        {
            return DateTime.Now.ToString();
        }
        public void TriggerGroupTypeKey(SqlConnection connection)
        {
            string sql = string.Format("select GroupTypeKey from PrefixGroup where GroupKey={0}", GroupKey);
            //GroupTypeKey = (int)SqlHelper.ExecuteScalar(connection, System.Data.CommandType.Text, sql);
        }
        public string FilterMethod(string name = null)
        {
            List<string> list = new List<string>();
            list.Add(OriginalValues.Get("GroupId"));
            list.Add(OriginalValues.Get("ColA"));
            list.Add(OriginalValues.Get("ColB"));
            list.Add(OriginalValues.Get("ColC"));
            list.Add(OriginalValues.Get("ColD"));
            ItemName = string.Join("-", list.ToArray());
            //EntitySet.Cache.CountItem("UQ_T1", ItemName + "-1");
            return ItemName;
        }
    }
}
