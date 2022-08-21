using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLoginSystemSample.Models
{
    /// <summary>
    /// 註冊資料
    /// </summary>
    public class Register
	{
		/// <summary>
		/// 登入帳密
		/// </summary>
		public LoginIn LoginIn { get; set; }

		/// <summary>
		/// 使用者名稱
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 使用者信箱
		/// </summary>
		public string UserEmail { get; set; }
	}
}