/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				MultiListBox.cs	                                           			*
 *      Description:																*
 *				 Multi-ListBox控件 			    								    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Globalization;

[assembly: WebResource("PmSys.WebControls.Resource.moveAllLeft.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveAllLeft2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveAllRight.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveAllRight2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveLeft.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveLeft2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveRight.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.moveRight2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.sortDown.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.sortDown2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.sortUp.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.sortUp2.gif", "image/gif")]
[assembly: WebResource("PmSys.WebControls.Resource.MultiListBox.js", "text/javascript")]

namespace PmSys.WebControls
{
    /// <summary>
    /// Multi-ListBox控件
    /// </summary>
    [ToolboxData("<{0}:MultiListBox runat=\"server\"></{0}:MultiListBox")]
    [
    ParseChildren(true),
    PersistChildren(false),
    ]
    public class MultiListBox : CompositeControl, IPostBackDataHandler
    {
        #region Fields
        private MultiListBoxItem _firstListItem = new MultiListBoxItem();
        private MultiListBoxItem _secondListItem = new MultiListBoxItem();
        //private bool _stateLoaded = false;
        /// <summary>
        /// 显示行
        /// </summary>
        protected int _rows = 4;
        //private bool _marked = false;
        private string _separator;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void RaisePostDataChangedEvent()
        {

        }

        #region AddAttributesToRender
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //注册客户端操作脚本
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script language=\"javascript\">{0}", Environment.NewLine);
            sb.AppendFormat("var multiList=new MultiListBox(document.getElementById('{1}_firstListBox'),document.getElementById('{1}_secondListBox'),'{1}_');{0}", Environment.NewLine, this.ClientID);
            sb.AppendFormat("var img_AllLeft_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllLeft.gif"));
            sb.AppendFormat("var img_AllLeft_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllLeft2.gif"));
            sb.AppendFormat("var img_AllRight_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllRight.gif"));
            sb.AppendFormat("var img_AllRight_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllRight2.gif"));
            sb.AppendFormat("var img_Left_have=\"{1}\"{0};", Environment.NewLine, GetWebResourceUrl("moveLeft.gif"));
            sb.AppendFormat("var img_Left_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveLeft2.gif"));
            sb.AppendFormat("var img_Right_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveRight.gif"));
            sb.AppendFormat("var img_Right_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveRight2.gif"));
            sb.Append("</script>");

            writer.Write(sb.ToString());
        }
        #endregion

        #region RenderContents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "45%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //显示FirstListBox列表框
            RenderOptionsContents(writer, FirstListBox, "firstListBox", "multiList.transferRight()");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "10%");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");

            //间距

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //输出图标
            RenderMultiIcon(writer);
            //
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "45%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //显示SecondListBox列表框
            RenderOptionsContents(writer, SecondListBox, "secondListBox", "multiList.transferLeft()");
            writer.RenderEndTag();
            writer.RenderEndTag();


        }
        #endregion

        #region Propertity
        /// <summary>
        /// 
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        /// <summary>
        /// 获取或设置为列表项提供文本内容的数据源字段
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("获取或设置为列表项提供文本内容的数据源字段")]
        public string DataTextField
        {
            get
            {
                object objTextField = this.ViewState["DataTextField"];
                if (objTextField != null)
                {
                    return (string)objTextField;
                }
                return string.Empty;

            }
            set
            {
                this.ViewState["DataTextField"] = value;
            }
        }

        /// <summary>
        /// 获取或设置为列表项提供值的数据源字段
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("获取或设置为列表项提供值的数据源字段")]
        public string DataValueField
        {
            get
            {
                object objValueField = this.ViewState["DataValueField"];
                if (objValueField != null)
                {
                    return (string)objValueField;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataValueField"] = value;
            }

        }

        /// <summary>
        /// 获取或设置格式化字符串，该字符串用来控制如何显示绑定到列表控件的数据。（从 ListControl 继承。）
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("获取或设置格式化字符串，该字符串用来控制如何显示绑定到列表控件的数据")]
        protected string DataTextFormatString
        {
            get
            {
                object objTextFormat = this.ViewState["DataTextFormatString"];
                if (objTextFormat != null)
                {
                    return (string)objTextFormat;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataTextFormatString"] = value;
            }

        }

        /// <summary>
        /// 第一个列表框控件(源列表框)
        /// </summary>
        [Browsable(false)]
        //[NotifyParentProperty(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MultiListBoxItem FirstListBox
        {
            get
            {
                if (_firstListItem == null)
                    _firstListItem = new MultiListBoxItem();
                return _firstListItem;
            }
        }

        /// <summary>
        /// 第二个列表框控件(目标列表框)
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MultiListBoxItem SecondListBox
        {
            get
            {
                if (_secondListItem == null)
                    _secondListItem = new MultiListBoxItem();
                return _secondListItem;
            }
        }

        /// <summary>
        /// 获取或设置控件显示的宽度
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("获取或设置控件显示的宽度")]
        public new string Width
        {
            get
            {
                string val = Attributes["width"];
                if (val != null)
                {
                    return val;
                }
                return string.Empty;
            }
            set
            {
                if (value == null || value == string.Empty)
                    Attributes.Remove("width");
                else
                    Attributes["width"] = value;
            }
        }

        /// <summary>
        /// 获取或设置控件显示的高度
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("获取或设置控件显示的高度")]
        public string Heigth
        {
            get
            {
                string val = Attributes["height"];
                if (val == null)
                    return string.Empty;
                return val;
            }
            set
            {
                if (value == null || value == string.Empty)
                    Attributes.Remove("height");
                else
                    Attributes["height"] = value;
            }
        }

        /// <summary>
        /// 两个ListBox列表框中间间距
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }
        /// <summary>
        /// 获取或设置列表框控件显示个数
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("获取或设置列表框控件显示个数")]
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        /// <summary>
        /// 列表的选择模式
        /// </summary>
        [WebCategory("Behavior"), Browsable(true), DefaultValue(0), Description("列表的选择模式")]
        public ListSelectionMode SelectionMode
        {
            get
            {
                object objMode = this.ViewState["SelectionMode"];
                if (objMode != null)
                {
                    return (ListSelectionMode)objMode;
                }
                return ListSelectionMode.Single;

            }
            set
            {
                if ((value < ListSelectionMode.Single) || (value > ListSelectionMode.Multiple))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["SelectionMode"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string HFItemsAdded
        {
            get { return this.ClientID + "_ADDED"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string HFItemsRemoved
        {
            get { return this.ClientID + "_REMOVED"; }
        }
        #endregion

        #region ViewStates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Triplet triplet = (Triplet)savedState;
                base.LoadViewState(triplet.First);
                Reflector.InvokeMethod(this.FirstListBox.Items, "LoadViewState", new object[] { triplet.Second });
                Reflector.InvokeMethod(this.SecondListBox.Items, "LoadViewState", new object[] { triplet.Third });
            }
            else
            {
                base.LoadViewState(null);
            }
            //this._stateLoaded = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            if (EnableViewState == false)
                return null;
            //启用控件视图状态
            object x = base.SaveViewState();
            object y = Reflector.InvokeMethod(FirstListBox.Items, "SaveViewState", null);
            object z = Reflector.InvokeMethod(SecondListBox.Items, "SaveViewState", null);
            if ((x == null) && (y == null) && (z == null))
            {
                return null;
            }
            return new Triplet(x, y, z);
        }

        #endregion



        #region OnDataBinding
        /// <summary>
        /// 绑定数据源
        /// </summary>
        public new void DataBind()
        {
            PerformanceData(FirstListBox);
            PerformanceData(SecondListBox);
        }

        /// <summary>
        /// 将DataSource转换成指定的Items
        /// </summary>
        /// <param name="listItem"></param>
        protected void PerformanceData(MultiListBoxItem listItem)
        {
            object dataSource = listItem.DataSource;
            if (dataSource != null)
            {
                bool flag_display = false;
                bool flag_format = false;

                string propName = this.DataTextField;
                string dataValueField = this.DataValueField;
                string format = this.DataTextFormatString;

                ICollection data = dataSource as ICollection;
                if (data != null)
                {
                    listItem.Items.Capacity = listItem.Items.Count + data.Count;
                }
                if (propName.Length != 0 || dataValueField.Length != 0)
                    flag_display = true;
                if (this.DataTextFormatString.Length != 0)
                    flag_format = true;
                foreach (object obj in data)
                {
                    ListItem item = new ListItem();
                    //自定义显示TextField,ValueField
                    if (flag_display)
                    {
                        if (propName.Length > 0)
                        {
                            item.Text = DataBinder.GetPropertyValue(obj, propName).ToString();
                        }
                        if (dataValueField.Length > 0)
                        {
                            item.Value = DataBinder.GetPropertyValue(obj, dataValueField).ToString();
                        }
                    }
                    else
                    {
                        if (flag_format)
                        {
                            item.Text = string.Format(CultureInfo.CurrentCulture, format, new object[] { obj });
                        }
                        else
                        {
                            item.Text = obj.ToString();
                        }
                    }
                    listItem.Items.Add(item);
                }
            }
        }

        #endregion

        #region OnPreRender
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page != null)
                Page.RegisterRequiresPostBack(this);

            if (!Page.ClientScript.IsClientScriptBlockRegistered("MultiListBox-JavaScriptKey"))
            {
                //注册脚本
                string jsResource = Page.ClientScript.GetWebResourceUrl(typeof(MultiListBox), "PmSys.WebControls.Resource.MultiListBox.js");
                Page.ClientScript.RegisterClientScriptInclude("MultiListBox-JavaScriptKey", jsResource);

                //注册隐藏域
                Page.ClientScript.RegisterHiddenField(this.HFItemsAdded, "");

                Page.ClientScript.RegisterHiddenField(this.HFItemsRemoved, "");

            }

        }
        #endregion

        #region IPostBackDataHandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataKey"></param>
        /// <param name="postCollection"></param>
        /// <returns></returns>
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool resultValueFlag = false;
            //移除指定ListItem，并需要添加了Left ListBox列表框中
            string itemsRemoved = postCollection[this.ClientID + "_REMOVED"];
            string[] itemsRemovedCol = itemsRemoved.Split(',');
            if (itemsRemovedCol != null)
            {
                if (itemsRemovedCol.Length > 0 && itemsRemovedCol[0] != "")
                {
                    for (int i = 0; i < itemsRemovedCol.Length; i++)
                    {
                        string[] itemsRemoveItems = itemsRemovedCol[i].Split('|');
                        ListItem item = this.SecondListBox.Items.FindByValue(itemsRemoveItems[1]);
                        if (item != null)
                        {
                            this.SecondListBox.Items.Remove(item);
                        }
                        item = this.FirstListBox.Items.FindByValue(itemsRemoveItems[1]);
                        if (item == null)
                        {

                            this.FirstListBox.Items.Add(new ListItem(itemsRemoveItems[0], itemsRemoveItems[1]));
                        }
                        resultValueFlag = true;
                    }
                }
            }
            //从客户端添加指定的ListItem
            string itemsAdded = postCollection[this.ClientID + "_ADDED"];
            string[] itemsAddedCol = itemsAdded.Split(',');
            if (itemsAddedCol != null)
            {
                if (itemsAddedCol.Length > 0 && itemsAddedCol[0] != "")
                {
                    int counter = -1;
                    for (int i = 0; i < itemsAddedCol.Length; i++)
                    {
                        string[] itemsAddItems = itemsAddedCol[i].Split('|');
                        ListItem item = this.SecondListBox.Items.FindByValue(itemsAddItems[1]);
                        if (item == null)
                        {
                            this.SecondListBox.Items.Add(new ListItem(itemsAddItems[0], itemsAddItems[1]));
                            counter += 1;
                        }
                        item = this.FirstListBox.Items.FindByValue(itemsAddItems[1]);
                        if (item != null)
                        {
                            this.FirstListBox.Items.Remove(item);
                        }
                    }
                    resultValueFlag = counter > -1 ? true : false;
                }
            }

            //从客户端中移除指定的ListItem
            return resultValueFlag;
        }

        #endregion

        #region Functionality
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="multiListItem"></param>
        /// <param name="name"></param>
        /// <param name="jsDbClick"></param>
        protected void RenderOptionsContents(HtmlTextWriter writer, MultiListBoxItem multiListItem, string name, string jsDbClick)
        {
            ListItemCollection items = multiListItem.Items;
            writer.AddAttribute("name", this.ClientID + "_" + name);
            writer.AddAttribute("id", this.ClientID + "_" + name);
            writer.AddAttribute("size", this.Rows.ToString());
            writer.AddAttribute("onDblClick", jsDbClick);
            if (SelectionMode == ListSelectionMode.Multiple)
                writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
            //输出CSS定义
            if (multiListItem.StyleSheet != null)
            {
                StringBuilder sb = new StringBuilder();
                string sheetValue = multiListItem.StyleSheet.Width;
                if (sheetValue != null && sheetValue != "")
                    sb.AppendFormat("width:{0};", sheetValue);
                sheetValue = multiListItem.StyleSheet.Height;
                if (sheetValue != null && sheetValue != "")
                    sb.AppendFormat("height:{0};", sheetValue);
                writer.AddAttribute("style", sb.ToString());
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Select);
            int count = items.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ListItem item = items[i];
                    writer.AddAttribute("value", item.Value, true);
                    writer.RenderBeginTag(HtmlTextWriterTag.Option);
                    writer.Write(item.Text);
                    writer.RenderEndTag();
                }
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// 显示操作图标
        /// </summary>
        /// <param name="writer"></param>
        protected void RenderMultiIcon(HtmlTextWriter writer)
        {
            //输出Div
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            RenderImageContents(writer, "moveAllRight.gif", "multiList.transferAllLeft();", "_AllRight");//全部右移
            RenderImageContents(writer, "moveRight.gif", "multiList.transferRight();", "_Right");
            RenderImageContents(writer, "moveLeft.gif", "multiList.transferLeft();", "_Left");
            RenderImageContents(writer, "moveAllLeft.gif", "multiList.transferAllRight();", "_AllLeft");
            writer.RenderEndTag();
        }

        private string GetWebResourceUrl(string img)
        {
            return Page.ClientScript.GetWebResourceUrl(typeof(MultiListBox), "PmSys.WebControls.Resource." + img);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="imgFileName"></param>
        /// <param name="jsClickName"></param>
        /// <param name="imgClientName"></param>
        private void RenderImageContents(HtmlTextWriter writer, string imgFileName, string jsClickName, string imgClientName)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, GetWebResourceUrl(imgFileName));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + imgClientName);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID + imgClientName);
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "cursor:pointer");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, jsClickName);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
        #endregion
    }

    /// <summary>
    /// Multi-ListItem
    /// </summary>
    public class MultiListBoxItem
    {

        #region Fields
        private ListItemCollection items;
        private object _dataSource = null;
        private string _style = string.Empty;
        private MultiListBoxStyle _styleSheet = new MultiListBoxStyle();
        #endregion


        #region Propertity
        /// <summary>
        /// 获取列表控件项的集合
        /// </summary>
        [Browsable(false)]
        public ListItemCollection Items
        {
            get
            {
                if (items == null)
                {
                    items = new ListItemCollection();
                }
                return items;
            }
        }

        /// <summary>
        /// 获取或设置列表控件的数据源
        /// </summary> 
        [Browsable(false)]
        public object DataSource
        {
            get
            {
                ValidateDataSource();
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Css样式名称")]
        public string Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Css样式定义,包括高度，宽度。如果定义了<Style>样式名称，该属性可以忽略")]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public MultiListBoxStyle StyleSheet
        {
            get { return _styleSheet; }
            set { _styleSheet = value; }
        }
        #endregion

        /// <summary>
        /// 验证数据源
        /// </summary>
        protected void ValidateDataSource()
        {
            if (_dataSource != null && !(_dataSource is ICollection) && !(_dataSource is IEnumerable) && !(_dataSource is IListSource))
            {
                throw new InvalidOperationException("DataBoundControl_InvalidDataSourceType");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Reflector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object InvokeMethod(ListItemCollection items, string methodName, object[] parameters)
        {
            Type type = items.GetType();
            FieldInfo fieldInfo = type.GetField("saveAll", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Default);
            fieldInfo.SetValue(items, (object)true);//
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (methodInfo != null)
            {
                return methodInfo.Invoke(items, parameters);
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MultiListBoxStyle
    {
        #region Fields
        private string _width;
        private string _height;

        #endregion

        #region Propertity
        /// <summary>
        /// 宽
        /// </summary>
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 高
        /// </summary>
        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class WebCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// 使用指定的<paramref name="category"/>名称
        /// </summary>
        /// <param name="category"></param>
        internal WebCategoryAttribute(string category)
            : base(category)
        {
        }

        public override object TypeId
        {
            get
            {
                return typeof(CategoryAttribute);
            }
        }

    }
}
