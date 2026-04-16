using ClusterSalesManagementSystem.DTOs;
using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using ClusterSalesManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ClusterSalesManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService service;
        private readonly IRepository<User> repo;
        private readonly PasswordService passwordService;

        public UserController(UserService service, IRepository<User> repo, PasswordService passwordService)
        {
            this.service = service;
            this.repo = repo;
            this.passwordService = passwordService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.UserName
            };

            await service.Register(user, model.Password);

            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get user from DB
            var users = await repo.GetAll();
            var user = users.FirstOrDefault(x => x.UserName == model.Username);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username";
                return View(model);
            }

            // Verify Password
            bool isValid = passwordService.Verify(user.PasswordHash, model.Password);

            if (!isValid)
            {
                ViewBag.Error = "Invalid Password";
                return View(model);
            }

            var token = GenerateToken(user);

            Response.Cookies.Append("JWToken", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddHours(1)
            });

            HttpContext.Session.SetString("JWToken", token);

            // Redirect to Dashboard
            return RedirectToAction("SalesList", "Sales");
        }
        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureJwtKey_123456789012345"));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWToken");
            return RedirectToAction("Login");
        }
    }
}
