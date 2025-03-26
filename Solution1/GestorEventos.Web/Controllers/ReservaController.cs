using GestorEventos.Application.DTOs;
using GestorEventos.Domain.Entities;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class ReservaController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReservaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Reserva");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<IEnumerable<ReservaViewModel>>(content);
                return View("Index", reservas);
            }

            return View(new List<ReservaViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ReservaDTO reservas)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(reservas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Reserva", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la reserva.");
                }
            }

            return View(reservas);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Reserva/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<ReservaViewModel>(content);

                return View(reservas);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservaViewModel reservas)
        {


            var json = JsonConvert.SerializeObject(reservas);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Reserva/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la reserva.");
            }

            return View(reservas);
        }



        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Reserva/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<ReservaViewModel>(content);

                return View(reservas);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Reserva/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar la reserva.";
                return RedirectToAction("Index");
            }
        }
    }
}
