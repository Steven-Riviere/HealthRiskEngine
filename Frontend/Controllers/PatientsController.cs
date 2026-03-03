using System.Net.Http.Headers;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class PatientsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _authApiUrl;

        public PatientsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _authApiUrl = _configuration["AuthApiBase:Url"] ?? "http://localhost:5183";
        }

        // Ajoute automatiquement le token dans les headers
        private bool SetAuthorizationHeader()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Pas de token dans le cookie !");
                return false;
            }

            Console.WriteLine("Token envoyé au Gateway: " + token);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return true;
        }

        // GET: /Patients
        public async Task<IActionResult> Index()
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            var patients = await _httpClient.GetFromJsonAsync<List<PatientViewModel>>($"{_authApiUrl}/patients");
            return View(patients);
        }

        // GET: /Patients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            var patient = await _httpClient.GetFromJsonAsync<PatientViewModel>($"{_authApiUrl}/patients/{id}");
            if (patient == null)
                return NotFound();

            try
            {
                patient.Notes = await _httpClient.GetFromJsonAsync<List<NoteViewModel>>($"{_authApiUrl}/notes/patient/{id}")
                                ?? new List<NoteViewModel>();

                // Appel API Diabetes
                var diabetesResponse = await _httpClient.PostAsJsonAsync(
                    $"{_authApiUrl}/diabetes/assess",
                    new DiabetesRiskViewModel { Patient = patient, Notes = patient.Notes }
                );

                if (diabetesResponse.IsSuccessStatusCode)
                    patient.DiabetesRisk = await diabetesResponse.Content.ReadFromJsonAsync<DiabetesRiskViewModel>();

                // Appel API Cancers
                var cancerResponse = await _httpClient.PostAsJsonAsync(
                    $"{_authApiUrl}/cancers/assess",
                    new CancersRiskViewModel { Patient = patient, Notes = patient.Notes }
                );

                if (cancerResponse.IsSuccessStatusCode)
                    patient.CancersRisk = await cancerResponse.Content.ReadFromJsonAsync<CancersRiskViewModel>();
            }
            catch
            {
                patient.DiabetesRisk = null;
                patient.CancersRisk = null;
            }

            return View(patient);
        }

        // GET: /Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patient)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/patients", patient);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Erreur lors de la création du patient");
            }

            return View(patient);
        }

        // GET: /Patients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            var patient = await _httpClient.GetFromJsonAsync<PatientViewModel>($"{_authApiUrl}/patients/{id}");
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: /Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patient)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            if (id != patient.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"{_authApiUrl}/patients/{id}", patient);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Erreur lors de la mise à jour du patient");
            }

            return View(patient);
        }
    }
}