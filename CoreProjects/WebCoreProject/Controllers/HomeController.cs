using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entity;
using NLog;
using WebCoreProject.Filter;
using WebCoreProject.Models;

namespace WebCoreProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IRepository<Shop> repositoryShop;
        private readonly IRepository<User> repositoryUser;

        public HomeController(IRepository<Shop> repositoryShop, IRepository<User> repositoryUser)
        {
            this.repositoryShop = repositoryShop;
            this.repositoryUser = repositoryUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult GetUser()
        {
            var res= repositoryUser.All();
            return View(res);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }



        [HttpPost]
        public bool AddUser(string UserName,string Password,string Email="",string Mobile="")
        {
            User u = new User();
            u.UserName = UserName;
            u.Password = GetMD5String(Password);
            u.Email = Email;
            u.Mobile = Mobile;
            repositoryUser.Add(u);
            return true;
        }

        [HttpGet]
        public ActionResult Shop()
        {
            LogEventInfo log = new LogEventInfo();
            log.Message = "商品页";
            log.Level = NLog.LogLevel.Info;
            log.Properties["LogType"] = "Debug";
            logger.Log(log);
            var res = repositoryShop.All();
            return View(res);
        }

        [HttpGet]
        public ActionResult AddShop()
        {
            return View();
        }

        [HttpPost]
        public bool AddShop(string ShopName, double ShopPrice, bool State)
        {
            Shop shop = new Shop();
            shop.ShopName = ShopName;
            shop.ShopPrice = ShopPrice;
            shop.States = State;
            repositoryShop.Add(shop);
            return true;
        }
        [TestActionFilter]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            logger.Info("登陆页");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LoginAsync(string userName,string password)
        {

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,userName),
                new Claim("password",password)
            };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                userPrincipal, 
                new AuthenticationProperties { 
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20), 
                    IsPersistent = false , 
                    AllowRefresh=false 
                });
            return Redirect("/Home/Shop");
        }

        [HttpPost]
        public async Task<IActionResult> Logout() 
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
            return Redirect("/Home/Login");
        }


        public string GetMD5String(string passwd)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(passwd);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
