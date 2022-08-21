using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebLoginSystemSample.Models
{
	/// <summary>
	/// 密碼加密儲存
	/// </summary>
    public static class SHA256_Encrypt
    {
		/// <summary>
		/// 進行密碼加密
		/// </summary>
		/// <param name="strAccunt">註冊時所輸入的帳號</param>
		/// <param name="Password">註冊時所輸入的密碼</param>
		/// <returns>回傳要儲存在資料庫中的「加密」後的密碼</returns>
		public static string StringEncrypt(string strAccunt,string Password)
        {
			string salt = strAccunt.Substring(0, 1).ToLower(); //使用帳號前一碼當作密碼金鑰
			SHA256 sha256 = SHA256.Create();
			byte[] bytes = Encoding.UTF8.GetBytes($"{salt}{Password}"); //將密碼金鑰及原密碼組合
			byte[] hash = sha256.ComputeHash(bytes);
			StringBuilder result = new StringBuilder();
			foreach (byte HashItem in hash)
			{
				result.Append(HashItem.ToString("X2"));
			}
			string NewPwd = result.ToString(); // 雜湊運算後密碼
			return NewPwd;
		}
	}
}