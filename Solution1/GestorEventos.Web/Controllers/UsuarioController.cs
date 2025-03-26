using GestorEventos.Application.DTOs;
using GestorEventos.Domain.Entities;
using GestorEventos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestorEventos.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7246/api");
        }
       

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Usuario");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var salas = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(content);
                return View("Index", salas);
            }

            return View(new List<UsuarioViewModel>());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(UsuarioDTO usuarios)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuarios);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Usuario", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario.");
                }
            }

            return View(usuarios);
        }

        public async Task<IActionResult> Edit (int id)
        {
            var response = await _httpClient.GetAsync($"/api/Usuario/{id}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                return View(usuarios);

            }
            else 
            {
                return RedirectToAction("Details");
                  
            
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, UsuarioViewModel usuarios)
        {


            var json = JsonConvert.SerializeObject(usuarios);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Usuario/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", new { id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el usuario.");
            }

            return View(usuarios);
        }
           


        

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Usuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Details");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Usuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el usuario.";
                return RedirectToAction("Index");
            }
        }
    }
}

