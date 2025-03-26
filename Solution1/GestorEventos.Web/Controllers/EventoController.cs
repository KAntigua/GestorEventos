using GestorEventos.Application.DTOs;
using GestorEventos.Domain.Entities;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class EventoController : Controller
    {
        private readonly HttpClient _httpClient;

        public EventoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Evento");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<IEnumerable<EventoViewModel>>(content);
                return View("Index", eventos);
            }

            return View(new List<EventoViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(EventoDTO eventos)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(eventos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Evento", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el evento.");
                }
            }

            return View(eventos);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<EventoViewModel>(content);

                return View(eventos);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventoDTO eventos)
        {


            var json = JsonConvert.SerializeObject(eventos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Evento/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el evento.");
            }

            return View(eventos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<EventoViewModel>(content);

                return View(eventos);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Evento/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el evento.";
                return RedirectToAction("Index");
            }
        }
    }
}
