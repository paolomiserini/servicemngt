using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Security.Cryptography;
using ServiceManagement.DAL;

namespace ServiceManagement
{
    public static class Common
    {
        #region Application Constants
        // User types
        public const string DIRECTOR = "DIR";
        public const string ADMINISTRATOR = "ADM";
        public const string TEACHER = "TEC";
        public const string STUDENT = "STD";
        public const string PARENT = "PAR";

        // Log type
        public const string INFORMATION = "INFORMATION";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";

        #endregion

        public class StudentsDebts
        {
            public int Id { get; set; }
            public string StudentName { get; set; }
            public string StudentSurname { get; set; }
            public string SudentPhone { get; set; }
            public int TotalDebt { get; set; }
        }

        public class StringFromResource
        {
            public static string Translation(string resourceKey)
            {
                ResourceManager rm = new ResourceManager("ServiceManagement.LocalResource.Resource", Assembly.GetExecutingAssembly());

                // Retrieve the value of the string resource named "welcome".
                // The resource manager will retrieve the value of the  
                // localized resource using the caller's current culture setting.
                String str = rm.GetString(resourceKey);

                return str;
            }
        }

        public class SalaryStatus
        {
            public static List<string> ListOfSalaryStatus
            {
                get
                {
                    return new List<string> { "PAYED", "CALCULATED", "READY TO PAY" };
                }
            }
        }

        #region SerializationDeserializationFunctions

        public static string SerializeObject<T>(T item)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);

            return StringToBase64(json);
        }

        public static T DeserializeObject<T>(string base64value)
        {
            string json = Base64ToString(base64value);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }


        public static string StringToBase64(string value)
        {
            byte[] data = System.Text.Encoding.UTF32.GetBytes(value);

            return Convert.ToBase64String(data);
        }

        public static string Base64ToString(string value)
        {
            byte[] data = Convert.FromBase64String(value);

            return System.Text.Encoding.UTF32.GetString(data);
        }

        #endregion SerializationDeserializationFunctions

        public static void CopyProperties(object Source, object Destination)
        {
            var destProps = Destination.GetType().GetProperties().Where(s => s.CanWrite).ToList();

            foreach (var destProp in destProps)
            {
                var sourceProp = Source.GetType().GetProperty(destProp.Name);

                if (sourceProp != null && sourceProp.CanRead)
                {
                    destProp.SetValue(Destination, sourceProp.GetValue(Source));
                }
            }
        }

        public class Settings
        {
            public static int SessionMinutesTimeout
            {
                get
                {
                    int result = 10;

                    int.TryParse(ConfigurationManager.AppSettings["SessionMinutesTimeout"] ?? result.ToString(), out result);

                    return result;
                }
            }
        }


        public class EcryptDecrypt
        {
            public static string Encrypt(string str)
            {
                string EncrptKey = "2018;[checkBEST)Lucum0n31963@@@";
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }

            public static string Decrypt(string str)
            {
                str = str.Replace(" ", "+");
                string DecryptKey = "2018;[checkBEST)Lucum0n31963@@@";
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byte[] inputByteArray = new byte[str.Length];

                byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
        }

        public class UserGenerator
        {
            public static UserLoginOption LoginOption(string name, string surname, PasswordOptions PwdOption)
            {
                var _loginoption = new UserLoginOption();
                var _username = name + "." + surname;
                try
                {
                    ServiceManagementContext db = new ServiceManagementContext();

                    // check is username exists
                    Models.User _userexist;
                    _userexist = db.Users.Where(u => u.Username == _username).FirstOrDefault();

                    if (_userexist == null)
                    {
                        _loginoption.Username = _username;
                    }
                    else // creo la username aggiungendo un numero alla fine
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            _username = _username + i.ToString();
                            _userexist = db.Users.Where(u => u.Username == _username).FirstOrDefault();
                            if (_userexist == null)
                            {
                                _loginoption.Username = _username;
                                break;
                            }
                        }
                    }

                    // genero una random password
                    _loginoption.ClearPassword = GenerateRandomPassword();

                    // cripto la password creata
                    _loginoption.Password = Common.EcryptDecrypt.Encrypt(_loginoption.ClearPassword);

                    _loginoption.IsOk = true;

                }
                catch (Exception)
                {
                    _loginoption.IsOk = false;
                }

                // Return all login info

                return _loginoption;
            }

            public static int CalculateAge(DateTime BirthDate)
            {
                int YearsPassed = DateTime.Now.Year - BirthDate.Year;
                // Are we before the birth date this year? If so subtract one year from the mix
                if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
                {
                    YearsPassed--;
                }
                return YearsPassed;
            }
        }

        public class UserLoginOption
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ClearPassword { get; set; }
            public bool IsOk { get; set; }
        }

        public class PasswordOptions
        {
            public int RequiredLength { get; set; }
            public int RequiredUniqueChars { get; set; }
            public bool RequireDigit { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireNonAlphanumeric { get; set; }
            public bool RequireUppercase { get; set; }
        }

        // Genera una random password
        // you can pass a list of parameter as option or leave null for default parameter
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            string[] randomChars = new[] { "ABCDEFGHJKLMNOPQRSTUVWXYZ", "abcdefghijkmnopqrstuvwxyz", "0123456789", "!@$?_-" };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
