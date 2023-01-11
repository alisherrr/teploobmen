using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using TeploobmenLibrary;
using TeploobmenWeb.Data;
using TeploobmenWeb.Models;

namespace TeploobmenWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext _context;

        private int _userId;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int.TryParse(User.FindFirst("Id")?.Value, out _userId);
        }

        [HttpPost]
        public IActionResult Result(TeploobmenInput input)
        {
            // Сохранение варианта исходных данных
            if (!string.IsNullOrEmpty(input.Name))
            {
                var existVariant = _context.Variants.FirstOrDefault(x => x.Name == input.Name);

                if (existVariant != null)
                {
                    existVariant.InitTemperatureMaterial = input.InitTemperatureMaterial;
                    existVariant.InitTemperatureGas = input.InitTemperatureGas;
                    existVariant.SpeedGas = input.SpeedGas;
                    existVariant.MiddleHeatСapacity = input.MiddleHeatСapacity;
                    existVariant.ConsumptionMaterial = input.ConsumptionMaterial;
                    existVariant.HeatСapacityMaterial = input.HeatСapacityMaterial;
                    existVariant.VolumetricHeatTransferCoefficient = input.VolumetricHeatTransferCoefficient;
                    existVariant.ApparatusDiameter = input.ApparatusDiameter; 

                    _context.Variants.Update(existVariant);
                    _context.SaveChanges();
                }
                else
                {
                    var variant = new Variant
                    {
                        Name = input.Name,
                        InitTemperatureMaterial = input.InitTemperatureMaterial,
                        InitTemperatureGas = input.InitTemperatureGas,
                        SpeedGas = input.SpeedGas,
                        MiddleHeatСapacity = input.MiddleHeatСapacity,
                        ConsumptionMaterial = input.ConsumptionMaterial,
                        HeatСapacityMaterial = input.HeatСapacityMaterial,
                        VolumetricHeatTransferCoefficient = input.VolumetricHeatTransferCoefficient,
                        ApparatusDiameter = input.ApparatusDiameter, 
                        UserId = _userId,
                        CreatedAt = DateTime.Now
                    };

                    _context.Variants.Add(variant);
                    _context.SaveChanges();
                }               
            }

            // Выполнение расчета
            var lib = new TeploobmenSolver(input);
            var result = lib.Solve();

            return View(result);
        }

        [HttpGet]
        public IActionResult Index(int? variantId)
        {
            var viewModel = new HomeViewModel();

            if (variantId != null)
            {
                viewModel.Variant = _context.Variants
                    .Where(x => x.UserId == _userId || x.UserId == 0)
                    .FirstOrDefault(x => x.Id == variantId);
            }

            viewModel.Variants = _context.Variants
                .Where(x => x.UserId == _userId || x.UserId == 0)
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Remove(int? variantId)
        {
            var variant = _context.Variants
                .Where(x => x.UserId == _userId || x.UserId == 0)
                .FirstOrDefault(x => x.Id == variantId);

            if (variant != null)
            {
                _context.Variants.Remove(variant);
                _context.SaveChanges();

                TempData["message"] = $"Вариант {variant.Name} удален.";
            }
            else
            {
                TempData["message"] = $"Вариант не найден.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}