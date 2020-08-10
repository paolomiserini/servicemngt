using System;
using System.Web;

namespace ServiceManagement.Session
{
    [Serializable]
    public class TypedSession
    {
        public static TypedSession Current
        {
            get
            {
                if (HttpContext.Current.Session["CurrentSession"] == null)
                    HttpContext.Current.Session["CurrentSession"] = new TypedSession();

                return HttpContext.Current.Session["CurrentSession"] as TypedSession;
            }
        }

        public LoggedUserInfo LoggedUser { get; set; }

        public DateTime LastAction { get; private set; }

        public string SaveNewObject<T>(T value)
        {
            string key = Guid.NewGuid().ToString();

            SaveObject(value, key);

            return key;
        }

        public void SaveObject<T>(T value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }

        public T GetObject<T>(string key)
        {
            var result = HttpContext.Current.Session[key];

            return (T)result;
        }

        public void Logout()
        {
            TypedSession c = Current;

            c.LoggedUser = null;

            HttpContext.Current.Session["CurrentSession"] = c;
        }

        public void UpdateLastAction()
        {
            TypedSession c = Current;

            c.LastAction = DateTime.Now;

            HttpContext.Current.Session["CurrentSession"] = c;
        }
    }
}