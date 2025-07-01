using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UdemyCarBook.Dto.CarFeatureDtos;
using UdemyCarBook.Dto.CategoryDtos;
using UdemyCarBook.Dto.FeatureDtos;

namespace UdemyCarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCarFeatureDetail")]
    public class AdminCarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7019/api/CarFeatures?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(List<ResultCarFeatureByCarIdDto> resultCarFeatureByCarIdDto)
        {

            foreach(var item in  resultCarFeatureByCarIdDto)
            {
                if (item.Available)
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("https://localhost:7019/api/CarFeatures/CarFeatureChangeAvailableToTrue?id=" + item.CarFeatureID);
                    
                }
                else
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("https://localhost:7019/api/CarFeatures/CarFeatureChangeAvailableToFalse?id=" + item.CarFeatureID);
                }
            }
            return RedirectToAction("Index", "AdminCar");
        }

        [HttpGet]
        [Route("CreateFeatureByCarId/{id}")]
        public async Task<IActionResult> CreateFeatureByCarId(int carId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7019/api/Features");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                ViewBag.CarId = carId; // View'da kullanmak için
                return View(values);
            }

            return View(new List<ResultFeatureDto>());
        }



        [HttpPost]
        [Route("CreateFeatureByCarId")]
        public async Task<IActionResult> CreateFeatureByCarId(List<int> FeatureIDs, int CarID)
        {
            if (FeatureIDs == null || !FeatureIDs.Any())
            {
                ModelState.AddModelError("", "Lütfen en az bir özellik seçiniz.");

                // Özellikleri tekrar yükle
                var clientReload = _httpClientFactory.CreateClient();
                var resReload = await clientReload.GetAsync("https://localhost:7019/api/Features");
                var features = new List<ResultFeatureDto>();

                if (resReload.IsSuccessStatusCode)
                {
                    var json = await resReload.Content.ReadAsStringAsync();
                    features = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(json);
                }

                ViewBag.CarId = CarID;
                return View(features);
            }

            var client = _httpClientFactory.CreateClient();

            foreach (var featureId in FeatureIDs)
            {
                var dto = new CreateCarFeatureDetailDto
                {
                    FeatureID = featureId,
                    CarID = CarID
                };

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ✅ DOĞRU ENDPOINT (CQRS kullanıyorsan bunu API'de tanımlamalısın)
                var response = await client.PostAsync("https://localhost:7019/api/CarFeatures", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Bazı özellikler eklenemedi.";
                }
            }

            TempData["Success"] = "Özellikler başarıyla araca eklendi.";
            return RedirectToAction("Index", "AdminCar");
        }





    }
}
