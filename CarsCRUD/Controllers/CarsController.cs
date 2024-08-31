using CarsCRUD.Data;
using CarsCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsCRUD.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsContext _context;
        private readonly ILogger<CarsController> _logger;

        public CarsController(CarsContext context,ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching all cars from the database");
            IEnumerable<Car> cars = await _context.Cars.ToListAsync();

            _logger.LogInformation("Successfully fetched {CarsCount} cars", cars.Count());
            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
				_logger.LogWarning("Details action invoked with null ID.");
				return NotFound();
            }

			_logger.LogInformation("Fetching details for car with ID: {CarId}", id);
			var car = await _context.Cars.FirstOrDefaultAsync(item => item.Id == id);
            
            if (car == null)
            {
				_logger.LogWarning("Car with ID {CarId} not found.", id);
				return NotFound();
            }

            _logger.LogInformation("Successfully fetched details for car with ID {CarId}", id);            
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int? id)
        {
            if (id==null)
            {
				_logger.LogWarning("Details action invoked with null ID.");
				return NotFound();
            }

			_logger.LogInformation("Fetching details for car with ID: {CarId}", id);
			var car= await _context.Cars.FirstOrDefaultAsync(item=>item.Id == id);
            
            if(car == null)
            {
				_logger.LogWarning("Car with ID {CarId} not found.", id);
				return NotFound();
            }

			_logger.LogInformation("Successfully found car with ID {CarId}", id);
			return View(car);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            _logger.LogInformation("Car with ID {CarId} is found", id);
            var car = await _context.Cars.FindAsync(id);

            if (car != null)
            {
                _logger.LogInformation("Car with ID {CarId} is removed from database", id);
                _context.Cars.Remove(car);
            }

            _logger.LogInformation("Datebase saved");
            await _context.SaveChangesAsync();

            _logger.LogInformation("Redirected to Cars/Index after car with ID {CarId} was removed", id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Year,Price,Description,IsAvailable")] Car car)
        {
            if (id!=car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Manufacturer, Model, Year, Price, Description, IsAvailable")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        private bool EntityExists(int id) 
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
