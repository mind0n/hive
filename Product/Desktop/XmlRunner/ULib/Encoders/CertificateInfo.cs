#region File Header & Copyright Notice
/*
 * Copyright (C) 2007 MOTOROLA, INC. All Rights Reserved.
 * THIS SOURCE CODE IS CONFIDENTIAL AND PROPRIETARY AND MAY NOT BE USED
 * OR DISTRIBUTED WITHOUT THE WRITTEN PERMISSION OF MOTOROLA, INC.
 *
 */
#endregion

using System.Xml.Serialization;    

namespace ULib.Encoders{

    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.mot.com/system/service/common", IsNullable=false)]
    public partial class CertificateInfo {
        
        private string storeLocationField;
        
        private string storeNameField;
        
        private string findTypeField;
        
        private string findValueField;
        
        /// <remarks/>
        public string StoreLocation {
            get {
                return this.storeLocationField;
            }
            set {
                this.storeLocationField = value;
            }
        }
        
        /// <remarks/>
        public string StoreName {
            get {
                return this.storeNameField;
            }
            set {
                this.storeNameField = value;
            }
        }
        
        /// <remarks/>
        public string FindType {
            get {
                return this.findTypeField;
            }
            set {
                this.findTypeField = value;
            }
        }
        
        /// <remarks/>
        public string FindValue {
            get {
                return this.findValueField;
            }
            set {
                this.findValueField = value;
            }
        }
    }
}
