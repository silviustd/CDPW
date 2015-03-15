using System;
using System.Web;
using System.Web.SessionState;

namespace CDPW.components
{
    public class Sessiune
    {
        // Create Session object to store user's data
        private void Create(HttpSessionState Sesiune, string Name, object objUser)
        {
            if (Check(Sesiune) == false)
            {
                HttpContext.Current.Session.Add(Name, objUser);
            }
            else
            {
                ClearSession(Sesiune, Name);
                HttpContext.Current.Session.Add(Name, objUser);
            }
        }

        // Checking if the session exists
        public bool Check(HttpSessionState Sesiune)
        {
            bool Exists = false;

            if (!HttpSessionState.Equals(Sesiune, null))
            {
                Exists = true;
            }

            return Exists;
        }


        // Clear the session info
        public void ClearSession(HttpSessionState Sesiune, string Name)
        {
            if (!HttpSessionState.Equals(Sesiune, null))
            {
                HttpContext.Current.Session.Remove(Name);
            }
        }


        //public static object Get(HttpSessionState Sesiune, string Name, object objUser)
        //{
        //    object oUser = new object()
            
        //}
    }
}