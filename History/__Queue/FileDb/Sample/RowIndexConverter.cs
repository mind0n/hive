using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FileDbNs;

namespace SampleApp
{
    //=====================================================================
    /// <summary>
    /// Used by the binding framework to allow editing in the DataGrid.
    /// </summary>
    /// 
    public class RowIndexConverter : System.Windows.Data.IValueConverter
    {
        private System.Windows.Data.IValueConverter _valueConverter;

        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            // obtain the 'bound' property via the Row string indexer
            string index = parameter as string;
            object propertyValue = null;
            Record record = value as Record;
            if( record != null )
                propertyValue = record[index];
            else
            {
                FieldSetter propertyValueChange = value as FieldSetter;
                if( propertyValueChange != null )
                    propertyValue = propertyValueChange.Value;
            }

            // convert if required
            if( _valueConverter != null )
            {
                propertyValue = _valueConverter.Convert( propertyValue, targetType, parameter, culture );
            }

            return propertyValue;
        }

        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            object valueToConvert = value;

            // convert if required
            if( _valueConverter != null )
            {
                valueToConvert = _valueConverter.ConvertBack( valueToConvert, targetType, parameter, culture );
            }

            // inform the bound Row instance of the property value change
            return new FieldSetter( parameter as string, valueToConvert );
        }
    }
}
