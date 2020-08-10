using ServiceManagement.DAL;
using System;

namespace ServiceManagement
{
    public class Logger
    {
 
        public static void Log(string tableName, int idRecord, string user, string message, string logType)
        {
            if (message.Length > 4000)
                message = message.Substring(0, 4000);

            using (var ctx = new ServiceManagementContext())
            {
                ctx.Logs.Add(new Models.Log()
                {
                    LogTableName = tableName,
                    LogIdTable = idRecord,
                    LogUsrUpd = user,
                    LogDtUpd = DateTime.Now,
                    LogMessage = message,
                    LogType = logType
                });

                ctx.SaveChanges();
            }
        }
 
    }
}
