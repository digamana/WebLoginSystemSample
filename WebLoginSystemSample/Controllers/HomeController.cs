using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebLoginSystemSample.Models;

namespace WebLoginSystemSample.Controllers
{
    public class HomeController : Controller
    {
		/*
         備註Member資料表的欄位內容請看SQL/CreatMember.sql
         */

		/// <summary>
		/// 登入畫面
		/// </summary>
		/// <returns></returns>
		public ActionResult Register()
        {       
            return View();
        }
        /// <summary>
        /// 登入時的首頁
        /// </summary>
        /// <returns></returns>
		public ActionResult Login()
		{
			return View();
		}

		/// <summary>
		/// 執行註冊
		/// </summary>
		/// <param name="RegModel"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Register(Register RegModel)
		{  
			// 資料庫連線
			string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Model1"].ConnectionString;
			LoginMessage MessageModel = new LoginMessage();
			string NewPwd = SHA256_Encrypt.StringEncrypt(RegModel.LoginIn.UserID, RegModel.LoginIn.UserPwd);
			if (string.IsNullOrEmpty(RegModel.LoginIn.UserID) || string.IsNullOrEmpty(RegModel.LoginIn.UserPwd) || string.IsNullOrEmpty(RegModel.UserName) || string.IsNullOrEmpty(RegModel.UserEmail))
			{
				MessageModel.ErrMsg = "請輸入資料";
				return Json(MessageModel);
			}
			using (SqlConnection conns = new SqlConnection(connStr))
			{
				///在資料庫中搜尋 ID有無重複項目
				string strSql = $"select * from Member where UserID = '{RegModel.LoginIn.UserID}'";
				var accounts = conns.Query<LoginIn>(strSql);
				if (accounts.Count() > 0)
				{
					MessageModel.ErrMsg = "此登入帳號已存在";
					return Json(MessageModel);
				}
				string InsertSQL = $@"INSERT INTO Member (UserID,UserPwd,UserName,UserEmail) VALUES ('{RegModel.LoginIn.UserID}', '{NewPwd}', '{RegModel.UserName}', '{RegModel.UserEmail}')";
				conns.Execute(InsertSQL);
				MessageModel.ResultMsg = "註冊完成";
			}
			return Json(MessageModel);
		}


		/// <summary>
		/// 執行登入
		/// </summary>
		/// <param name="loginIn"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Login(LoginIn loginIn)
		{
			// 資料庫連線
			string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Model1"].ConnectionString;
			LoginMessage MessageModel = new LoginMessage();

			// 檢查輸入資料
			if (string.IsNullOrEmpty(loginIn.UserID) || string.IsNullOrEmpty(loginIn.UserPwd))
			{
				MessageModel.ErrMsg = "請輸入資料";
				return Json(MessageModel);
			}
			var CheckPwd = SHA256_Encrypt.StringEncrypt(loginIn.UserID, loginIn.UserPwd);
			using (SqlConnection conns = new SqlConnection(connStr))
			{
				
				string strSql = $"select * from Member where UserID = '{loginIn.UserID}' and UserPwd = '{CheckPwd}'";
				var accounts = conns.Query<LoginIn>(strSql);
				if (accounts.Count() > 0)
				{   // 有查詢到資料，表示帳號密碼正確

					// 將登入帳號記錄在 Session 內
					Session["UserID"] = loginIn.UserID;
					MessageModel.ResultMsg = "登入成功";
				}
				else
				{
					// 查無資料，帳號或密碼錯誤
					MessageModel.ErrMsg = "帳號或密碼錯誤";
				}
			}
			// 輸出json
			return Json(MessageModel);
		}
	}
}