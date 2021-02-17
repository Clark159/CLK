using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Data.SqlClient
{
    public static partial class SqlCommandExtensions
    {
        // Methods
        public static SqlDataReader ExecuteReader(this SqlCommand command, int index, int count, string orderbyText)
        {
            #region Contracts

            if (command == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(orderbyText) == true) throw new ArgumentNullException();

            #endregion

            // CommandParameters
            command.Parameters.Add(new SqlParameter("@__StartRowNumber", index + 1));
            command.Parameters.Add(new SqlParameter("@__EndRowNumber", index + count));

            // LimitText
            var limitText = GetLimitText(command.CommandText, orderbyText);

            // ExecuteReader
            var commandText = command.CommandText;
            try
            {
                // Set
                command.CommandText = limitText;

                // Execute
                return command.ExecuteReader();
            }
            finally
            {
                // Reset
                command.CommandText = commandText;
            }
        }

        public static int ExecuteQueryCount(this SqlCommand command, string fieldName = "*")
        {
            #region Contracts

            if (command == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(fieldName) == true) throw new ArgumentNullException();

            #endregion

            // QueryCountText
            var queryCountText = GetQueryCountText(command.CommandText, fieldName);

            // ExecuteQueryCount
            var commandText = command.CommandText;
            try
            {
                // Set
                command.CommandText = queryCountText;

                // Execute
                return Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                // Reset
                command.CommandText = commandText;
            }
        }
    }

    public static partial class SqlCommandExtensions
    {
        // Methods
        public static SqlDataReader ExecuteReader(this SqlCommandScope command, int index, int count, string orderbyText)
        {
            #region Contracts

            if (command == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(orderbyText) == true) throw new ArgumentNullException();

            #endregion

            // CommandParameters
            command.Parameters.Add(new SqlParameter("@__StartRowNumber", index + 1));
            command.Parameters.Add(new SqlParameter("@__EndRowNumber", index + count));

            // LimitText
            var limitText = GetLimitText(command.CommandText, orderbyText);

            // ExecuteReader
            var commandText = command.CommandText;
            try
            {
                // Set
                command.CommandText = limitText;

                // Execute
                return command.ExecuteReader();
            }
            finally
            {
                // Reset
                command.CommandText = commandText;
            }
        }

        public static int ExecuteQueryCount(this SqlCommandScope command, string fieldName = "*")
        {
            #region Contracts

            if (command == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(fieldName) == true) throw new ArgumentNullException();

            #endregion

            // QueryCountText
            var queryCountText = GetQueryCountText(command.CommandText, fieldName);

            // ExecuteQueryCount
            var commandText = command.CommandText;
            try
            {
                // Set
                command.CommandText = queryCountText;

                // Execute
                return Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                // Reset
                command.CommandText = commandText;
            }
        }
    }

    public static partial class SqlCommandExtensions
    {
        // Methods
        private static string GetLimitText(string commandText, string orderbyText)
        {
            #region Contracts

            if (string.IsNullOrEmpty(commandText) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(orderbyText) == true) throw new ArgumentNullException();

            #endregion

            // Remove "SELECT"
            commandText = commandText.Trim().Remove(0, 6);

            // LimitText
            var limitText = @"SELECT * 
                              FROM   (
                                       SELECT ROW_NUMBER() OVER(ORDER BY {0}) __RowNumber, {1}
                                     ) __RowNumberTable
                              WHERE  __RowNumberTable.__RowNumber BETWEEN @__StartRowNumber AND @__EndRowNumber";

            limitText = string.Format(limitText, orderbyText, commandText);

            // Return
            return limitText;
        }

        private static string GetQueryCountText(string commandText, string fieldName = "*")
        {
            #region Contracts

            if (string.IsNullOrEmpty(commandText) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(fieldName) == true) throw new ArgumentNullException();

            #endregion

            // QueryCountText
            var queryCountText = @"SELECT COUNT({0})
                                   FROM   (
                                            {1}
                                          ) __QueryCountTable";

            queryCountText = string.Format(queryCountText, fieldName, commandText);

            // Return
            return queryCountText;
        }
    }
}