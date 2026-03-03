using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _authApiUrl;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _authApiUrl = _configuration["AuthApiBase:Url"] ?? "http://localhost:5183";
        }

        //Get
        public IActionResult Login()
        {
            return View();
        }

        //Post Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            Console.WriteLine($"Tentative de login avec email: {email}, Password: {password}");

            var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/auth/login", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Identifiants incorrects");
                return View();
            }

            // Récupère le token JWT
            var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (content == null || !content.TryGetValue("token", out var token))
            {
                ModelState.AddModelError("", "Erreur lors de la récupération du token");
                return View();
            }
            // Stocke dans un cookie
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Patients");
        }

        //Get
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }
    }
}