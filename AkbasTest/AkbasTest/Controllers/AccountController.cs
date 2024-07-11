using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AkbasTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkbasTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountContext _context;

        public AccountController(AccountContext context)
        {
            _context = context;
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
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Password));
                    StringBuilder strBuilder = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        strBuilder.Append(b.ToString("x2"));
                    }
                    string hashedPassword = strBuilder.ToString();

                    // Kullanıcı doğrulama
                    var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == hashedPassword);
                    if (user != null)
                    {
                        // Giriş başarılı
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Giriş başarısız
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View();
        }
    }
}
