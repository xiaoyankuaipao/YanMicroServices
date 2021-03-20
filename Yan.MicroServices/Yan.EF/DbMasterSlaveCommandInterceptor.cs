using System;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Yan.EF
{
    /// <summary>
    /// 
    /// </summary>
    public class DbMasterSlaveCommandInterceptor: DbCommandInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _masterConnectionString;

        /// <summary>
        /// 
        /// </summary>
        private readonly string _slaveConnectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterConnectionString"></param>
        /// <param name="slaveConnectionString"></param>
        public DbMasterSlaveCommandInterceptor(string masterConnectionString, string slaveConnectionString)
        {
            _masterConnectionString = masterConnectionString;
            _slaveConnectionString = slaveConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSaveConnectionString()
        {
            var readArr = _slaveConnectionString.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var resultConn = string.Empty;
            if (readArr.Any())
            {
                resultConn = readArr[Convert.ToInt32(Math.Floor((double) new Random().Next(0, readArr.Length)))];
            }

            return resultConn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        private void UpdateToSlave(DbCommand command)
        {
            if (!string.IsNullOrEmpty(GetSaveConnectionString()))
            {
                if (command.CommandText.ToLower().StartsWith("insert", StringComparison.InvariantCultureIgnoreCase) ==
                    false)
                {
                    bool isDistributedTran = Transaction.Current != null &&
                                             Transaction.Current.TransactionInformation.Status !=
                                             TransactionStatus.Committed;

                    bool isDbTran = command.Transaction != null;

                    if (!isDbTran && !isDistributedTran)
                    {
                        command.Connection.Close();
                        command.Connection.ConnectionString = GetSaveConnectionString();
                        command.Connection.Open();
                    }
                }
            }
        }

    }
}
