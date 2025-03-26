using GestorEventos.Application.DTOs;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class SalaController : Controller
    {
        private readonly HttpClient _httpClient;

        public SalaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }

        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/SalaCotroller");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<IEnumerable<SalaViewModel>>(content);
                return View("Index", salas);
            }

            return View(new List<SalaViewModel>());

        }

        

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(SalaDTO salas)
        {
            if(ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(salas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/SalaCotroller", content);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Error al crear la sala.");
                }
            }

            return View(salas);
        }


        
        public async Task<IActionResult> Edit (int id)
        {
            var response = await _httpClient.GetAsync($"/api/SalaCotroller/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<SalaViewModel>(content);

                return View(salas);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SalaDTO salas)
        {
            if(ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(salas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/SalaCotroller/{id}", content);

                if (response.IsSuccessStatusCode) 
                { 
                
                return RedirectToAction("Index", new { id});
                
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la sala.");
                }
             
            }
            return View(salas);

        }

        public async Task<IActionResult> Details (int id)
        {
            var response = await _httpClient.GetAsync($"api/SalaCotroller/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<SalaViewModel> (content);

                return View(salas);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/SalaCotroller/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar la sala.";
                return RedirectToAction("Index");
            }
        }

    }
}
