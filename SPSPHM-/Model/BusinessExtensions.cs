using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSPHM.Model
{
    public static class BusinessExtensions
    {
        #region Methods (14) 

        // Public Methods (14) 

        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Encloses the string around % characters if the string doesnt already contain this character
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ApplyWildCard(this string value)
        {
            string result = value;
            if (!string.IsNullOrEmpty(value) && value.IndexOf('%') == -1)
            {
                result = string.Format("%{0}%", value);
            }
            return result;
        }

        public static bool ColumnExists(this System.Data.IDataReader reader, string columnName)
        {
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ensures the value is valid for SQL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EnsureValidForSql(this string value)
        {
            return value != null ? value.Replace("'", "''") : null;
        }

        public static T Find<T>(this Collection<T> collection, Predicate<T> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                    return item;
            }
            return default(T);
        }

        public static bool GetBoolean(this System.Data.IDataReader reader, string columnName)
        {
            if (reader[columnName] is DBNull) return false;
            if ((int)reader.GetValue(columnName) == 1) return true;
            else return false;
        }

        public static bool? GetNullableBoolean(this System.Data.IDataReader reader, string columnName)
        {
            if (reader.IsDBNull(columnName))
                return null;
            else
                return Convert.ToBoolean(reader.GetValue(columnName));
        }

        public static decimal? GetNullableDecimal(this System.Data.IDataReader reader, string columnName)
        {
            if (reader.IsDBNull(columnName))
                return null;
            else
                return Convert.ToDecimal(reader.GetValue(columnName));
        }

        public static int? GetNullableInt(this System.Data.IDataReader reader, string columnName)
        {
            if (reader.IsDBNull(columnName))
                return null;
            else
                return Convert.ToInt32(reader.GetValue(columnName));
        }

        /// <summary>
        /// Returns a blank string if no colum, exist. If the column does exist, the column value is converted to a string
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static string GetString(this System.Data.IDataReader reader, string columnName)
        {
            if (reader.IsDBNull(columnName))
                return string.Empty;
            else
                return Convert.ToString(reader.GetValue(columnName));
        }

        /// <summary>
        /// Return DataReader Value Converting DBNull to Null
        /// </summary>
        /// <param name="reader">Datareader to get data from.</param>
        /// <param name="columnName">Name of data column.</param>
        /// <returns></returns>
        public static object GetValue(this System.Data.IDataReader reader, string columnName)
        {
            if (reader[columnName] is DBNull)
                return null;
            else if (reader[columnName].GetType() == typeof(DateTime))
                return ((DateTime)reader[columnName]);
            else
                return reader[columnName];
        }
        public static object GetValue(this System.Data.IDataRecord reader, string columnName)
        {
            if (reader[columnName] is DBNull)
                return null;
            else if (reader[columnName].GetType() == typeof(DateTime))
                return ((DateTime)reader[columnName]);
            else
                return reader[columnName];
        }

        public static T GetValueFromEnum<T>(this System.Data.IDataReader reader, string columnName)
        {
            return (T)Enum.Parse(typeof(T), Convert.ToString(reader[columnName]), true);
        }

        /// <summary>
        /// Return True if specified named column is DBNull in the DataReader.
        /// </summary>
        /// <param name="reader">Datareader to get data from.</param>
        /// <param name="columnName">Name of data column.</param>
        /// <returns></returns>
        public static bool IsDBNull(this System.Data.IDataReader reader, string columnName)
        {
            return (reader[columnName] is DBNull);
        }

        /// <summary>
        /// Get the DbType for the current object
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns>If typeof PrincipalDateTime is used DbType.Date is returned</returns>
        public static DbType ToDbType(this Type sourceType)
        {

            DbType dbt;
            try
            {
                if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    //to handle Principaldatetime
                    if (sourceType.Equals(typeof(DateTime?)))
                    {
                        dbt = DbType.DateTime;
                    }
                    else
                    {
                        dbt = (DbType)Enum.Parse(typeof(DbType), Nullable.GetUnderlyingType(sourceType).Name);
                    }
                }
                else
                {
                    //to handle Principaldatetime
                    if (sourceType.Equals(typeof(DateTime)))
                    {
                        dbt = DbType.DateTime;
                    }
                    else
                    {
                        dbt = (DbType)Enum.Parse(typeof(DbType), sourceType.Name);
                    }
                }
            }
            catch
            {
                dbt = DbType.Object;
            }
            return dbt;

        }
        #endregion Methods 
    }

}
