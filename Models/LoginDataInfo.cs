namespace ServiceManagement.Models
{
    public class LoginDataInfo
    {
        public LoginDataInfo()
        {
            Username = string.Empty;
            Password = string.Empty;
            LoginFailed = false;
            UserNotActive = false;
            WrongPassword = false;
        }

        private string _username;
        public string Username
        {
            get { return _username ?? string.Empty; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password ?? string.Empty; }
            set { _password = value; }
        }


        // Response Properties

        public bool LoginFailed { get; set; }
        public bool UserNotActive { get; set; }
        public bool WrongPassword { get; set; }

    }
}