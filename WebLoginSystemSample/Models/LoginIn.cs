using System.ComponentModel.DataAnnotations.Schema;

namespace WebLoginSystemSample.Models
{
    /// <summary>
    /// 登入資料
    /// </summary>
    public class LoginIn
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string UserID { get; set; }
        
        /// <summary>
        /// 密碼
        /// </summary>
        public string UserPwd { get; set; }
    }
}