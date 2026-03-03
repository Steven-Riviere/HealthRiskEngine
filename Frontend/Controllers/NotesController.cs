using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http.Headers;

namespace FrontEnd.Controllers
{
    public class NotesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _authApiUrl;

        public NotesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _authApiUrl = _configuration["AuthApiBase:Url"] ?? "http://localhost:5183";
        }

        private bool SetAuthorizationHeader()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        // GET: Récupérer les notes d’un patient
        public async Task<List<NoteViewModel>> GetNotesForPatient(int patientId)
        {
            if (!SetAuthorizationHeader())
                return new List<NoteViewModel>();

            try
            {
                var notes = await _httpClient.GetFromJsonAsync<List<NoteViewModel>>($"{_authApiUrl}/notes/patient/{patientId}"); return notes ?? new List<NoteViewModel>();
            }

            catch
            {
                // Si le microservice note est down, on ne bloque pas la page patient
                return new List<NoteViewModel>();
            }
        }

        // GET: Formulaire création note
        public async Task<IActionResult> Create(int patientId)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            var patient = await _httpClient.GetFromJsonAsync<PatientViewModel>($"{_authApiUrl}/patients/{patientId}");

            if (patient == null)
                return NotFound();

            var model = new NoteViewModel
            {
                PatientId = patientId,
                DateAppointment = DateTime.Now
            };

            ViewBag.FirstName = patient.FirstName;
            ViewBag.LastName = patient.LastName;

            return View(model);
        }

        // POST: Création note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel note)
        {
            if (!SetAuthorizationHeader())
                return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {

                var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/notes", note);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "Patients", new { id = note.PatientId });
                }

                ModelState.AddModelError("", "Erreur lors de l'ajout de la note");
            }

            var patient = await _httpClient.GetFromJsonAsync<PatientViewModel>($"{_authApiUrl}/patients/{note.PatientId}"); if (patient != null) { ViewBag.FirstName = patient.FirstName; ViewBag.LastName = patient.LastName; }
            return View(note);
        }

    }
}