using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDPW.BLL
{
    public class CDPWMessages
    {
        public static  String MSG_ACCT_NOT_ACTIVE = "The account is not activated. Please see your email account for the activation message.";
        //public static String NO_USER = "Sorry, there is no user  with the email address/username provided. In order to access the application, please Sign-Up by using &#60;I don't have an account&#62;.";
        public static String MSG_LOGIN_RECOVER = "Try to login <a href='cdprecoverpasswd.aspx'>here</a> or, if you forgot your password, try to recover here (link).";
        public static  String MSG_SIGN_UP_S = "The sign up process completed with success. A confirmation message has been sent to the email address provided";
        public static String MSG_ACCOUNT_ACTIVATED = "Congratulations! You have activated your account.";
        public static String MSG_ACCOUNT_ALREADY_ACTIVATED = "Account is already activated.";

        public static String ERR_MSG_LOGOUT_LOGIN_EMAIL = "Please <a href='cdplogout.aspx'> Log Out</a>  and Log in again.<br/>If the problem persists please <A HREF='mailto:office@columnasoft.com?subject={0}'>email us</A> with your Name, Address, Email and Plan";

        public static String ERR_NO_USER = "Sorry, there is no user  with the email address/username provided. In order to access the application, please Sign-Up by using <b>I don't have an account</b>.";
        public static String ERR_WRONG_PWD = "Sorry, the password you provided is not correct. If you forgot your password, try to recover it <a href='cdprecoverpasswd.aspx'>here</a>.";
        public static String ERR_SAME_USERNAME = "Sorry, there is another user registered with the same username.";
        public static String ERR_SAME_EMAIL = "Sorry, there is another user registered with this email address. If you forgot your password, try to recover it <a href='cdprecoverpasswd.aspx'>here</a>.";
        public static String ERR_MSG_ERR = "There is a problem processing your request.";
        
        public static String EMAIL_SUBJECT_RESET_PWD = "CDP - Reset your password request";
        public static String EMAIL_SUBJECT_SIGNUP_CONFIRM = "CDP - Sign-up confirm";
        public static String EMAIL_SUBJECT_NEW_PASSWORD = "CDP - New password to access your account";
    }
}
