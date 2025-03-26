using GestorEventos.Application.DTOs;
using GestorEventos.Domain.Entities;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class HorarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public HorarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Horario");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var horarios = JsonConvert.DeserializeObject<IEnumerable<HorarioViewModel>>(content);
                return View("Index", horarios);
            }

            return View(new List<HorarioViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(HorarioDTO horarios)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(horarios);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Horario", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el horario.");
                }
            }

            return View(horarios);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Horario/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var horarios = JsonConvert.DeserializeObject<HorarioViewModel>(content);

                return View(horarios);

            }
            else
            {
                return RedirectToAction("Details");


            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HorarioViewModel horarios)
        {


            var json = JsonConvert.SerializeObject(horarios);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Horario/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el horario.");
            }

            return View(horarios);
        }





        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Horario/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var horarios = JsonConvert.DeserializeObject<HorarioViewModel>(content);

                return View(horarios);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Horario/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el horario.";
                return RedirectToAction("Index");
            }
        }
    }
}
