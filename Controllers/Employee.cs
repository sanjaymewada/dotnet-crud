using CRUD_Demo.Data;
using CRUD_Demo.Models;
using CRUD_Demo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace CRUD_Demo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _context;

        public EmployeeController(IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAll();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                if (Document != null && Document.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Document.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Document.CopyToAsync(fileStream);
                    }

                    employee.DocumentPath = "/uploads/" + uniqueFileName;
                }

                await _employeeRepository.AddAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns();
            return View(employee);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewBag.States = new SelectList(_context.States.Where(s => s.CountryId == employee.CountryId), "Id", "Name", employee.StateId);
            ViewBag.Cities = new SelectList(_context.Cities.Where(c => c.StateId == employee.StateId), "Id", "Name", employee.CityId);

            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile Document)
        {
            if (id != employee.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingEmployee = await _employeeRepository.GetByIdAsync(id);
                if (existingEmployee == null) return NotFound();

                existingEmployee.Name = employee.Name;
                existingEmployee.Age = employee.Age;
                existingEmployee.Department = employee.Department;
                existingEmployee.Email = employee.Email;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.CountryId = employee.CountryId;
                existingEmployee.StateId = employee.StateId;
                existingEmployee.CityId = employee.CityId;

                if (Document != null && Document.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadsFolder))            
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Document.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Document.CopyToAsync(fileStream);
                    }

                    if (!string.IsNullOrEmpty(existingEmployee.DocumentPath))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingEmployee.DocumentPath.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    existingEmployee.DocumentPath = "/uploads/" + uniqueFileName;
                }

                await _employeeRepository.UpdateAsync(existingEmployee);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns();
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns()
        {
            var countries = await _context.Countries.ToListAsync();
            var states = await _context.States.ToListAsync();
            var cities = await _context.Cities.ToListAsync();

            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            ViewBag.States = new SelectList(states, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
        }

        [HttpPost]
        public async Task<JsonResult> GetStatesByCountry([FromForm] int countryId)
        {
            var states = await _context.States
                .Where(s => s.CountryId == countryId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();

            return Json(states);
        }

        [HttpPost]
        public async Task<JsonResult> GetCitiesByState([FromForm] int stateId)
        {
            var cities = await _context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            return Json(cities);
        }
    }
}
