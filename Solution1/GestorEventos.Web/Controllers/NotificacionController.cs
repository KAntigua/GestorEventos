using GestorEventos.Application.DTOs;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class NotificacionController : Controller
    {
        private readonly HttpClient _httpClient;

        public NotificacionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Notifiacion");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var notificaciones = JsonConvert.DeserializeObject<IEnumerable<NotificacionViewModel>>(content);
                return View("Index", notificaciones);
            }

            return View(new List<NotificacionViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(NotificacionDTO notificaciones)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(notificaciones);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Notifiacion", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la notificacion.");
                }
            }

            return View(notificaciones);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Notifiacion/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var notificaciones = JsonConvert.DeserializeObject<NotificacionViewModel>(content);

                return View(notificaciones);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NotificacionViewModel notificaciones)
        {


            var json = JsonConvert.SerializeObject(notificaciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Notifiacion/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la notificacion.");
            }

            return View(notificaciones);
        }





        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Notifiacion/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var notificaciones = JsonConvert.DeserializeObject<NotificacionViewModel>(content);

                return View(notificaciones);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Notifiacion/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar la notificacion.";
                return RedirectToAction("Index");
            }
        }
    }
}
