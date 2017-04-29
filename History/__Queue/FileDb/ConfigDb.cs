using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

using FileDbNs;

namespace ConfigDbNs
{
    // Note: these values rely on the fact that the AutoInc value for ID starts at 0
    public enum ConfigDbKey { LocalMachine = 0, CurrentUser = 1 }

    public enum ConfigDbDataType { String, StringArray }

    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Class for reading/writing ConfigDb files which are FileDb database files.
    /// </summary>
    /// 
    public class ConfigDb : IDisposable
    {
        const String ErrKeyNotFound = "Key not found";
        const String ErrValueNotFound = "Value not found";
        const String ErrCantConvert = "Cannot convert '{0}' to {1}";
        const String ErrCantHaveValues = "CurrentUser and LocalMachine keys cannot have values";
        const String ErrKeysCannotBeEmpty = "Key names cannot be null or empty";

        const String StrID = "ID";
        const String StrParentID = "ParentID";
        const String StrName = "Name";
        const String StrType = "Type";
        const String StrDataType = "DataTypeEnum";
        const String StrValue = "Value";
        const String StrValues = "Values";

        FileDb _db;

        public FileDb Db
        {
            get { return _db; }
            set { _db = value; }
        }

        bool _disposed;

        enum KeyValueType { Key = 1, Value = 2 }

        #region IDisposable

        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        //
        public void Dispose()
        {
            Dispose( true );

            // This object will be cleaned up by the Dispose method.
            // Therefore you should call GC.SupressFinalize to take this object off the finalization queue
            // and prevent finalization code for this object from executing a second time.

            GC.SuppressFinalize( this );
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        //
        protected virtual void Dispose( bool disposing )
        {
            // Check to see if Dispose has already been called.
            
            if( !this._disposed )
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                
                if( disposing )
                {
                    // Dispose managed resources.
                    Close();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.

                // e.g. CloseHandle( handle );

                // Note disposing has been done.
                _disposed = true;
            }
        }

        #endregion IDisposable

        public static ConfigDb OpenOrCreate( String fileName )
        {
            var configDb = new ConfigDb();
            if( !File.Exists( fileName ) )
                configDb.create( fileName );
            else
                configDb.Open( fileName );

            return configDb;
        }

        public static ConfigDb Create( String fileName )
        {
            var configDb = new ConfigDb();
            configDb.create( fileName );
            return configDb;
        }

        public void Open( String fileName )
        {
            _db = new FileDb();
            _db.Open( fileName, false );
        }

        public void Close()
        {
            if( _db != null )
                _db.Close();
        }

        public bool IsOpen
        {
            get
            {
                bool bRet = false;
                if( _db != null )
                    bRet = _db.IsOpen;
                return bRet;
            }
        }

        void checkDbOpen()
        {
            if( !_db.IsOpen )
		        throw new Exception( "No open file" );
        }

        void create( String fileName )
        {
            // Database schema:
            // ID  ParentID  Name  Type  Value  Values

            Field field;
            List<Field> fieldLst = new List<Field>( 10 );

            field = new Field( StrID, DataTypeEnum.Int );
            field.AutoIncStart = 0;
            field.IsPrimaryKey = true;
            fieldLst.Add( field );

            field = new Field( StrParentID, DataTypeEnum.Int );
            fieldLst.Add( field );

            field = new Field( StrName, DataTypeEnum.String );
            fieldLst.Add( field );

            field = new Field( StrType, DataTypeEnum.Int );
            fieldLst.Add( field );

            field = new Field( StrDataType, DataTypeEnum.Int );
            fieldLst.Add( field );

            field = new Field( StrValue, DataTypeEnum.String );
            fieldLst.Add( field );

            field = new Field( StrValues, DataTypeEnum.String );
            field.IsArray = true;
            fieldLst.Add( field );

            _db = new FileDb();
            _db.Create( fileName, fieldLst.ToArray() );

            FieldValues fieldValues = new FieldValues(3);
            fieldValues.Add( StrParentID, (Int32) (-1) );
            fieldValues.Add( StrName, "LocalMachine" );
            fieldValues.Add( StrType, (Int32) KeyValueType.Key );
            fieldValues.Add( StrDataType, (Int32) ConfigDbDataType.String );
            _db.AddRecord( fieldValues );

            fieldValues = new FieldValues(3);
            fieldValues.Add( StrParentID, (Int32) (-1) );
            fieldValues.Add( StrName, "CurrentUser" );
            fieldValues.Add( StrType, (Int32) KeyValueType.Key );
            fieldValues.Add( StrDataType, (Int32) ConfigDbDataType.String );
            _db.AddRecord( fieldValues );
        }

        String getRoot( ConfigDbKey regKeyRoot, String keyPath ) // String pszRoot )
        {
	        String key, userName;

	        if( regKeyRoot == ConfigDbKey.CurrentUser )
	        {
                userName = Environment.UserName;
		        if( userName.Length == 0 )
			        userName = "User"; // give it a default username which will be for all users of this machine

			    if( keyPath.Length > 0 )
				    key = String.Format( "{0}/{1}", userName, keyPath );
			    else
				    key = String.Format( "{0}", userName );
		    }
		    else
			    key = keyPath;

            return key;
        }

        // helper method for when a string filter is used in a query
        Table getMatchingRecords( string filter, string[] fieldList, bool includeIndex, string[] orderByList )
        {
            FilterExpressionGroup srchExpGrp = null;

            if( !string.IsNullOrEmpty( filter ) )
            {
                srchExpGrp = FilterExpressionGroup.Parse( filter );
            }
            Table table = _db.SelectRecords( srchExpGrp, fieldList, orderByList, includeIndex );
            return table;
        }

        public ConfigDbKey OpenKey( ConfigDbKey rootKey, String keyName, bool createIfNeedBe )
        {
            checkDbOpen();

            keyName = keyName.Trim();

            if( rootKey != ConfigDbKey.CurrentUser &&  (keyName == null || keyName.Length == 0) )
                throw new Exception( "Invalid keyName parameter" );

            if( rootKey == ConfigDbKey.CurrentUser )
	            keyName = getRoot( rootKey, keyName );

            Int32 id = (Int32) rootKey;
            String[] keys = keyName.Split( @"\/".ToCharArray() );

            foreach( String sKey in keys )
            {
                String filter = String.Format( "ParentID = {0} AND ~Name = '{1}'", id, sKey );
                Table table = getMatchingRecords( filter, new String[] { StrID }, false, null );

                if( table.Count > 0 )
                {
                    id = (Int32) table[0][0];
                }
                else
                {
                    if( createIfNeedBe )
                    {
                        // create it
                        FieldValues fieldValues = new FieldValues( 3 );
                        fieldValues.Add( StrParentID, id );
                        fieldValues.Add( StrName, sKey );
                        fieldValues.Add( StrType, (Int32) KeyValueType.Key );
                        fieldValues.Add( StrDataType, (Int32) ConfigDbDataType.String );
                        int index = _db.AddRecord( fieldValues );

                        // get the new ID
                        Record record = _db.GetRecordByIndex( index, new String[] { StrID } );
                        id = (int) record[0];
                    }
                    else
                    {
                        throw new Exception( ErrKeyNotFound );
                    }
                }
            }

            return (ConfigDbKey) id;
        }

        public void SetValue( ConfigDbKey key, String valueName, ConfigDbDataType dataType, object value )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( ErrCantHaveValues );

            Table table;

            if( valueName != null )
                valueName = valueName.Trim();

    		// cannot set CURRENT_USER key directly - must append the Username
            if( key == ConfigDbKey.CurrentUser )
                key = OpenKey( key, String.Empty, true );
            else
            {
                // make sure the key is there
                FilterExpression srchExp = new FilterExpression( StrID, (Int32) key, EqualityEnum.Equal, MatchTypeEnum.UseCase );
                table = _db.SelectRecords( srchExp, new String[] { StrID } );

                if( table.Count == 0 )
                {
                    throw new Exception( ErrKeyNotFound );
                }
            }

            int id = -1;
            FieldValues fieldValues = new FieldValues( 5 );
            
            // these fields are always set regardless of adding or updating
            fieldValues.Add( StrDataType, (Int32) dataType );
            if( dataType == ConfigDbDataType.StringArray )
                fieldValues.Add( StrValues, value );
            else
                fieldValues.Add( StrValue, value );

            if( string.IsNullOrEmpty( valueName ) )
            {
                // we use the Key's value instead of creating a new 
                id = (Int32) key;
            }
            else
            {
                String filter = String.Format( "ParentID = {0} AND Type = {1} AND ~Name = '{2}'", (int) key, (int) KeyValueType.Value, valueName );
                table = getMatchingRecords( filter, new String[] { StrID }, false, null );

                if( table.Count > 0 )
                {
                    // found an existing value - update it
                    id = (int) table[0][0];
                }
            }

            if( id > -1 )
            {
                // update
                _db.UpdateRecordByKey( id, fieldValues );
            }
            else // add new value
            {
                fieldValues.Add( StrParentID, (Int32) key );
                fieldValues.Add( StrType, (Int32) KeyValueType.Value );
                fieldValues.Add( StrName, valueName );
                int index = _db.AddRecord( fieldValues );
            }
        }

        object getValue( ConfigDbKey key, String valueName, out ConfigDbDataType dataType )
        {
            dataType = ConfigDbDataType.String;

            if( valueName != null )
                valueName = valueName.Trim();

            // cannot set CURRENT_USER key directly - must append the Username
            if( key == ConfigDbKey.CurrentUser )
                key = OpenKey( key, String.Empty, true );

            // make sure the key is there and get the default values in case we need them
            FilterExpression srchExp = new FilterExpression( StrID, (Int32) key, EqualityEnum.Equal, MatchTypeEnum.UseCase );
            Table table = _db.SelectRecords( srchExp, new String[] { StrDataType, StrValue, StrValues } );

            if( table.Count == 0 )
            {
                throw new Exception( ErrKeyNotFound );
            }

            object value = null;

            // if valueName is null or empty, use the Key's value, which we already have from above

            if( !string.IsNullOrEmpty( valueName ) )
            {
                String filter = String.Format( "ParentID = {0} AND Type = {1} AND ~Name = '{2}'", (int) key, (int) KeyValueType.Value, valueName );
                table = getMatchingRecords( filter, new String[] { StrDataType, StrValue, StrValues }, false, null );

                if( table.Count == 0 )
                {
                    // Note: its easier to program against if we don't throw an error if there is no Value
                    // throw new Exception( ErrValueNotFound );
                    return null;
                }

                Debug.Assert( table.Count == 1 );
            }

            dataType = (ConfigDbDataType) table[0][StrDataType];

            if( dataType == ConfigDbDataType.String )
                value = table[0][StrValue];
            else
                value = table[0][StrValues];
            
            return value;
        }

        public String GetValue( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( ErrCantHaveValues );

            ConfigDbDataType dataType;
            object value = getValue( key, valueName, out dataType );

            if( dataType == ConfigDbDataType.StringArray )
                throw new Exception( "The value is a String array and cannot be converted - use GetStringArray instead." );

            return value as String;
        }

        public String[] GetArrayValue( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            ConfigDbDataType dataType;
            object value = getValue( key, valueName, out dataType );

            if( dataType != ConfigDbDataType.StringArray )
                throw new Exception( "The value is not a String array - use GetValue instead." );

            return value as String[];
        }

        public Int32 GetValueAsInt( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            Int32 value; // = Int32.Parse( sVal );
            if( !Int32.TryParse( sVal, out value ) )
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof(Int32).ToString() ) );
            return value;
        }

        public UInt32 GetValueAsUInt( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            UInt32 value; // = UInt32.Parse( sVal );
            if( !UInt32.TryParse( sVal, out value ) )
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof( UInt32 ).ToString() ) );
            return value;
        }

        public Double GetValueAsSingle( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            Single value; // = Double.Parse( sVal );
            if( !Single.TryParse( sVal, out value ) )
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof( Single ).ToString() ) );
            return value;
        }

        public Double GetValueAsDouble( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            Double value; // = Double.Parse( sVal );
            if( !Double.TryParse( sVal, out value ) )
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof( Double ).ToString() ) );
            return value;
        }

        public Boolean GetValueAsBoolean( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            Boolean value; // = Boolean.Parse( sVal );
            if( !Boolean.TryParse( sVal, out value ) )
            {
                Byte bVal;
                if( Byte.TryParse( sVal, out bVal ) )
                {
                    if( bVal == 0 )
                    {
                        value = false;
                        goto Done;
                    }
                    else if( bVal == 1 )
                    {
                        value = true;
                        goto Done;
                    }                    
                }
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof( Boolean ).ToString() ) );
            }
        Done:
            return value;
        }

        public DateTime GetValueAsDateTime( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            String sVal = GetValue( key, valueName );
            DateTime value; // = DateTime.Parse( sVal );
            if( !DateTime.TryParse( sVal, out value ) )
                throw new Exception( string.Format( ErrCantConvert, sVal, typeof( DateTime ).ToString() ) );
            return value;
        }

        public void DeleteValue( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( "CurrentUser and LocalMachine keys cannot have Values" );

            if( valueName != null )
                valueName = valueName.Trim();

            // make sure the key is there

            FilterExpression srchExp = new FilterExpression( StrID, (Int32) key, EqualityEnum.Equal, MatchTypeEnum.UseCase );
            Table table = _db.SelectRecords( srchExp, new String[] { StrDataType, StrValue, StrValues } );
            if( table.Count == 0 )
            {
                throw new Exception( ErrKeyNotFound );
            }

            KeyValueType valueType = string.IsNullOrEmpty( valueName ) ? KeyValueType.Key : KeyValueType.Value;
            String filter;

            if( string.IsNullOrEmpty( valueName ) )
            {
                // update the Key record
                SetValue( key, valueName, ConfigDbDataType.String, null );
            }
            else
            {
                // delete the Value record
                filter = String.Format( "ParentID = {0} AND Type = {1} AND ~Name = '{2}'", (Int32) key, (Int32) KeyValueType.Value, valueName );
                FilterExpressionGroup srchExpGrp = FilterExpressionGroup.Parse( filter );
                _db.DeleteRecords( srchExpGrp );
            }
        }

        public void DeleteKey( ConfigDbKey key )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( "Cannot delete CurrentUser or LocalMachine keys" );

            deleteSubKeys( key );
            deleteKey( key );
        }

        void deleteKey( ConfigDbKey key )
        {
            FilterExpressionGroup srchExpGroup = FilterExpressionGroup.Parse( 
                string.Format( "ParentID = {0} OR ID = {0}", (Int32) key ) );
            _db.DeleteRecords( srchExpGroup );
        }

        void deleteSubKeys( ConfigDbKey key )
        {
            FilterExpression srchExp = new FilterExpression( StrParentID, (Int32) key, EqualityEnum.Equal, MatchTypeEnum.UseCase );
            Table table = _db.SelectRecords( srchExp, new String[] { StrID } );

            if( table.Count > 0 )
            {
                foreach( Record record in table )
                {
                    int id = (int) record[0];
                    deleteSubKeys( (ConfigDbKey) id );
                    deleteKey( (ConfigDbKey) id );
                }
            }
        }

        /// <summary>
        /// Returns all of the Key names under the specified Key.
        /// </summary>
        /// <param name="parentKey">The parent key to enumerate</param>
        /// <returns>List of ConfigDbKey/KeyName pairs</returns>
        /// 
        public List<KeyValuePair<ConfigDbKey, String>> EnumKeys( ConfigDbKey parentKey )
        {
            checkDbOpen();

            if( parentKey == ConfigDbKey.CurrentUser )
                parentKey = OpenKey( parentKey, String.Empty, true );

            List<KeyValuePair<ConfigDbKey, String>> keys = null;
            String filter = String.Format( "ParentID = {0} AND Type = {1}", (int) parentKey, (int) KeyValueType.Key );
            Table table = getMatchingRecords( filter, new String[] { StrID, StrName }, false, new String[] { "~" + StrName } );

            if( table.Count > 0 )
            {
                keys = new List<KeyValuePair<ConfigDbKey, String>>( table.Count );
                foreach( Record record in table )
                {
                    ConfigDbKey key = (ConfigDbKey) record[0];
                    string keyName = (string) record[1];
                    var kvp = new KeyValuePair<ConfigDbKey, String>( key, keyName );
                    keys.Add( kvp );
                }
            }
            else
                keys = new List<KeyValuePair<ConfigDbKey, String>>( 0 );                

            return keys;
        }

        /// <summary>
        /// Returns all of the Value names and, optionally, the Values under the specified Key.
        /// The Value can be either String or String[]
        /// </summary>
        /// <param name="parentKey">The parent key to enumerate</param>
        /// <param name="getValues">Specifies whether to also return the value</param>
        /// <returns>List of String/Value pairs</returns>
        /// 
        public List<KeyValuePair<String, object>> EnumValues( ConfigDbKey parentKey, bool getValues )
        {
            checkDbOpen();

            if( parentKey == ConfigDbKey.CurrentUser || parentKey == ConfigDbKey.LocalMachine )
                throw new Exception( ErrCantHaveValues );

            List<KeyValuePair<String, object>> values = null;

            // get the default value of the parent Key
            ConfigDbDataType dataType;
            object defValue = getValue( parentKey, null, out dataType );

            String filter = String.Format( "ParentID = {0} AND Type = {1}", (int) parentKey, (int) KeyValueType.Value );
            Table table = getMatchingRecords( filter, new String[] { StrName, StrDataType, StrValue, StrValues }, false, new String[] { "~" + StrName } );

            int numRecs = table.Count + 1; // (hasDefaultValue ? 1 : 0);
            values = new List<KeyValuePair<String, object>>( table.Count );

            // add the Default value
            var kvp = new KeyValuePair<String, object>( String.Empty, defValue );
            values.Add( kvp );

            // add all the others
            if( table.Count > 0 )
            {
                foreach( Record record in table )
                {
                    string valueName = (string) record[StrName];
                    dataType = (ConfigDbDataType) record[StrDataType];
                    string ndx = dataType == ConfigDbDataType.String ? StrValue : StrValues;
                    object value = record[ndx];
                    kvp = new KeyValuePair<String, object>( valueName, value );
                    values.Add( kvp );
                }
            }

            return values;
        }

        public List<KeyValuePair<ConfigDbKey, String>> EnumUserKeys()
        {
            checkDbOpen();

            List<KeyValuePair<ConfigDbKey, String>> keys = null;
            String filter = String.Format( "ParentID = {0} AND Type = {1}", (int) ConfigDbKey.CurrentUser, (int) KeyValueType.Key );
            Table table = getMatchingRecords( filter, new String[] { StrID, StrName }, false, new String[] { "~" + StrName } );

            if( table.Count > 0 )
            {
                keys = new List<KeyValuePair<ConfigDbKey, String>>( table.Count );
                foreach( Record record in table )
                {
                    ConfigDbKey key = (ConfigDbKey) record[0];
                    string keyName = (string) record[1];
                    var kvp = new KeyValuePair<ConfigDbKey, String>( key, keyName );
                    keys.Add( kvp );
                }
            }
            else
                keys = new List<KeyValuePair<ConfigDbKey, String>>( 0 );

            return keys;
        }

        public void RenameKey( ConfigDbKey key, string newKeyName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( "Cannot rename CurrentUser and LocalMachine root keys" );

            if( newKeyName != null )
                newKeyName = newKeyName.Trim();

            if( string.IsNullOrEmpty( newKeyName ) )
                throw new Exception( ErrKeysCannotBeEmpty );

            //String filter = String.Format( "ID = {0} AND Type = {1}", (int) key, (int) KeyValueType.Key );
            String filter = String.Format( "ID = {0}", (int) key );

            FieldValues fieldValues = new FieldValues( 1 );
            fieldValues.Add( StrName, newKeyName );
            int numUpdated = _db.UpdateRecords( FilterExpressionGroup.Parse( filter ), fieldValues );
        }

        public void RenameKey( ConfigDbKey key, string keyName, string newKeyName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser )
                key = OpenKey( key, String.Empty, true );
            
            if( keyName != null )
                keyName = keyName.Trim();

            if( newKeyName != null )
                newKeyName = newKeyName.Trim();

            if( string.IsNullOrEmpty( keyName ) || string.IsNullOrEmpty( newKeyName ) )
                throw new Exception( ErrKeysCannotBeEmpty );

            String filter = String.Format( "ID = {0}", (int) key );

            FieldValues fieldValues = new FieldValues( 1 );
            fieldValues.Add( StrName, newKeyName );
            int numUpdated = _db.UpdateRecords( FilterExpressionGroup.Parse( filter ), fieldValues );
        }

        public void RenameValue( ConfigDbKey key, string valueName, string newValueName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( ErrCantHaveValues );

            if( valueName != null )
                valueName = valueName.Trim();

            if( newValueName != null )
                newValueName = newValueName.Trim();

            if( string.IsNullOrEmpty( valueName ) || string.IsNullOrEmpty( newValueName ) )
                throw new Exception( "Cannot rename default Value" );

            if( string.IsNullOrEmpty( newValueName ) )
                throw new Exception( "New Value name cannot be null or empty" );

            String filter = String.Format( "ParentID = {0} AND Type = {1} AND Name = '{2}'",
                                (int) key, (int) KeyValueType.Value, valueName );

            FieldValues fieldValues = new FieldValues( 1 );
            fieldValues.Add( StrName, newValueName );
            int numUpdated = _db.UpdateRecords( FilterExpressionGroup.Parse( filter ), fieldValues );
        }

        public bool HasValue( ConfigDbKey key, string valueName )
        {
            checkDbOpen();

            if( key == ConfigDbKey.CurrentUser || key == ConfigDbKey.LocalMachine )
                throw new Exception( ErrCantHaveValues );

            if( valueName != null )
                valueName = valueName.Trim();

            if( string.IsNullOrEmpty( valueName ) )
                throw new Exception( "Value cannot be null or empty" );

            String filter = String.Format( "ParentID = {0} AND Type = {1} AND Name = '{2}'",
                                (int) key, (int) KeyValueType.Value, valueName );

            Table table = getMatchingRecords( filter, new String[] { StrID }, false, null );

            return table.Count > 0;
        }

        public bool HasKey( ConfigDbKey parentKey, string keyName )
        {
            checkDbOpen();

            if( keyName != null )
                keyName = keyName.Trim();

            if( string.IsNullOrEmpty( keyName ) )
                throw new Exception( "Key name cannot be null or empty" );

            String filter = String.Format( "ParentID = {0} AND Type = {1} AND Name = '{2}'",
                                (int) parentKey, (int) KeyValueType.Key, keyName );

            Table table = getMatchingRecords( filter, new String[] { StrID }, false, null );

            return table.Count > 0;
        }

        public int GetValueCount( ConfigDbKey parentKey )
        {
		    checkDbOpen();

            String filter = String.Format( "ParentID = {0} AND Type = {1}",
                                (int) parentKey, (int) KeyValueType.Value );

            Table table = getMatchingRecords( filter, new String[] { StrID }, false, null );
            
            return table.Count;
        }

        public int GetSubKeyCount( ConfigDbKey key )
        {
            checkDbOpen();

            String filter = String.Format( "ParentID = {0} AND Type = {1}",
                                (int) key, (int) KeyValueType.Key );

            Table table = getMatchingRecords( filter, new String[] { StrID }, false, null );

            return table.Count;
        }

        public ConfigDbKey GetParentKey( ConfigDbKey key )
        {
            checkDbOpen();

            String filter = String.Format( "ID = {0}", (int) key );

            Table table = getMatchingRecords( filter, new String[] { StrParentID }, false, null );

            return (ConfigDbKey) table[0][0];
        }

        public ConfigDbDataType GetValueType( ConfigDbKey key, String valueName )
        {
            checkDbOpen();

            ConfigDbDataType dataType;
            getValue( key, valueName, out dataType );

            return dataType;
        }

        public String GetKeyName( ConfigDbKey key )
        {
            checkDbOpen();

            String filter = String.Format( "ID = {0}", (int) key );

            Table table = getMatchingRecords( filter, new String[] { StrName }, false, null );

            return (String) table[0][0];
        }

        public void Reparent( ConfigDbKey key, ConfigDbKey parentKey )
        {
            String filter = String.Format( "ID = {0}", (int) key );
            FieldValues fieldValues = new FieldValues( 1 );
            fieldValues.Add( StrParentID, (int) parentKey );
            int numUpdated = _db.UpdateRecords( FilterExpressionGroup.Parse( filter ), fieldValues );
        }

        public string EncryptString( string encryptKey, string value )
        {
            return _db.EncryptString( encryptKey, value );
        }

        public string DecryptString( string encryptKey, string value )
        {
            return _db.DecryptString( encryptKey, value );
        }
    }
}
