using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AkbasTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AkbasTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                // Şifreyi MD5 ile şifreleme
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Password));
                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }
                string hashedPassword = strBuilder.ToString();

                // Kullanıcı doğrulama
                var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == hashedPassword);
                if (user != null)
                {
                    // Giriş başarılı
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Giriş başarısız
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View();
        }
    }
}
