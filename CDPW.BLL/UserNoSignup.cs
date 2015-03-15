using System;

namespace CDPW.BLL
{
    public class UserNoSignup
    {
        private string _WAppUserName = string.Empty;
        private bool _NoSignUp = false;

        public string WAppUserName
        {
            get { return _WAppUserName; }
            set { _WAppUserName = value; }
        }

        public bool NoSignUp
        {
            get { return _NoSignUp; }
            set { _NoSignUp = value; }
        }
    }
}
