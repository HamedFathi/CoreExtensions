using System;
using System.Data;

namespace CoreExtensions.DataSet
{
    public static class DataSetExtensions
    {
        /// <summary>
        /// Gets the value of the column with the column name specified from the DataRow in the type specified. If the value is DBNull.Value, null / Nothing
        /// will be returned, if TValue is a nullable value type or a reference type, the default value for TValue will be returned otherwise. 
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>the value of the column specified, or the default value for the type specified if not found.</returns>
        /// <remarks>Use this method instead of Field(Of TValue) if you don't want to receive cast exceptions</remarks>
        public static TValue GetValue<TValue>(this DataRow row, string columnName)
        {
            TValue toReturn = default(TValue);
            if (!((row == null) || string.IsNullOrEmpty(columnName) || (row.Table == null) || !row.Table.Columns.Contains(columnName)))
            {
                object columnValue = row[columnName];
                if (columnValue != DBNull.Value)
                {
                    Type destinationType = typeof(TValue);
                    if (typeof(TValue).IsNullableValueType())
                    {
                        destinationType = destinationType.GetGenericArguments()[0];
                    }
                    if (columnValue is TValue)
                    {
                        toReturn = (TValue)columnValue;
                    }
                    else
                    {
                        toReturn = (TValue)Convert.ChangeType(columnValue, destinationType);
                    }
                }
            }
            return toReturn;
        }
    }
}
