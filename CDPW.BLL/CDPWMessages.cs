using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDPW.BLL
{
    public class CDPWMessages
    {
        public static  String ACCT_NOT_ACTIVE = "The account is not activated. Please see your email account for the activation message.";
        //public static String NO_USER = "Sorry, there is no user  with the email address/username provided. In order to access the application, please Sign-Up by using &#60;I don't have an account&#62;.";
        public static String NO_USER = "Sorry, there is no user  with the email address/username provided. In order to access the application, please Sign-Up by using <b>I don't have an account</b>.";
        public static String WRONG_PWD = "Sorry, the password you provided is not correct. If you forgot your password, try to recover it <a href='cdprecoverpasswd.aspx'>here</a>.";
        public static  String SAME_USERNAME = "Sorry, there is another user registered with this same username.";
        public static  String SAME_EMAIL = "Sorry, there is another user registered with this email address.";
        public static String LOGIN_RECOVER = "Try to login <a href='cdprecoverpasswd.aspx'>here</a> or, if you forgot your password, try to recover here (link).";
        public static  String SIGN_UP_S = "The sign up process completed with success. A confirmation message has been sent to the email address provided";
        public static String ACCOUNT_ACTIVATED = "Congratulations! You have activated your account.";
        public static String ACCOUNT_ALREADY_ACTIVATED = "Account is already activated.";
        
        public static String EMAIL_SUBJECT_RESET_PWD = "CDP - Reset your password request";
        public static String EMAIL_SUBJECT_SIGNUP_CONFIRM = "CDP - Sign-up confirm";
        public static String EMAIL_SUBJECT_NEW_PASSWORD = "CDP - New password to access your account";
        
    }
}
