using System;
using ServiceManagement.Models;

namespace ServiceManagement.Session
{
    [Serializable]
    public class LoggedUserInfo
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool UserIsActive { get; set; }
        public int? idStudent { get; set; }
        public int? idParent { get; set; }
        public int? idStaff { get; set; }

        public static LoggedUserInfo FromDbTiamUser(User appUser )
        {
            LoggedUserInfo result = new LoggedUserInfo();

            Common.CopyProperties(appUser, result);

            return result;
        }
    }
}