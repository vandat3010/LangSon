using System.Net;

namespace Namek.Core
{
    public class Encryption
    {
        public static string EncryptPassword(string password)
        {
            // hash password
            return SecurityUtility.EncryptBase64(SecurityUtility.EncryptMd5(password), Constant.KeyPassWord);
        }

        public static bool CheckPassword(string password, string hashPassword)
        {
            return hashPassword.Equals(EncryptPassword(password));
        }

        public static string EncryptCaptcha(string textToBeEncrypted)
        {
            // hash captcha
            return SecurityUtility.EncryptBase64(SecurityUtility.EncryptMd5(textToBeEncrypted),
                Constant.CaptchaPassWord);
        }

        public static bool CheckCaptcha(string captcha, string hashCaptcha)
        {
            return hashCaptcha.Equals(EncryptCaptcha(captcha));
        }

        public static string Encrypt(string textToBeEncrypted, string key)
        {
            return SecurityUtility.EncryptBase64(textToBeEncrypted, key);
        }

        public static string Decrypt(string stringEncrypted, string key)
        {
            return SecurityUtility.DecryptBase64(stringEncrypted, key);
        }
    }
}