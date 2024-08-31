using CarsCRUD.Data;
using CarsCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsCRUD.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsContext _context;

        public CarsController(CarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Car> cars = await _context.Cars.ToListAsync();
            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FirstOrDefaultAsync(item => item.Id == id);
            
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var car= await _context.Cars.FirstOrDefaultAsync(item=>item.Id == id);
            
            if(car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();

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
