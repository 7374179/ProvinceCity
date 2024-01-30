using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvinceCity.Data;
using ProvinceCity.Models;

namespace ProvinceCity.Controllers
{
    public class ProvincesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvincesController(ApplicationDbContext context)
        {
            _context = context; 
        }

        // GET: Provinces
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Provinces.ToListAsync());
            var provinces = await _context.Provinces
                                .Include(p => p.Cities) // Cities 컬렉션을 포함시켜 로드
                                .ToListAsync();
            return View(provinces);
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var province = await _context.Provinces
            //     .FirstOrDefaultAsync(m => m.ProvinceCode == id);
        var province = await _context.Provinces
            .Include(p => p.Cities)
            .FirstOrDefaultAsync(p => p.ProvinceCode == id);


            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // GET: Provinces/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CityName = new SelectList(_context.Cities
                    .GroupBy(c => c.CityName)
                    .Select(g => g.First()), "CityName", "CityName");

            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("ProvinceCode,ProvinceName,CitiesString")] Province province)
{
    if (ModelState.IsValid)
    {
        // CitiesString을 City 객체의 리스트로 변환하는 메소드 호출
        province.Cities = ConvertCitiesStringToList(province.CitiesString, province.ProvinceCode);
        
        _context.Add(province);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    return View(province);
}


        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var province = await _context.Provinces.FindAsync(id);
            var province = await _context.Provinces
                .Include(p => p.Cities) // 관련된 Cities를 포함하여 로드합니다.
                .FirstOrDefaultAsync(p => p.ProvinceCode == id);

            if (province == null)
            {
                return NotFound();
            }
            
            // ViewBag.CityName = new SelectList(_context.Cities
            //                     .GroupBy(c => c.CityName)
            //                     .Select(g => g.First()), "CityName", "CityName");

            province.CitiesString = province.Cities != null ? 
                String.Join(", ", province.Cities.Select(c => c.CityName)) : 
                string.Empty;

            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, [Bind("ProvinceCode,ProvinceName,CitiesString")] Province provinceViewModel)
{
    if (id != provinceViewModel.ProvinceCode)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // 데이터베이스에서 기존 Province 엔티티를 찾습니다.
            var provinceToUpdate = await _context.Provinces
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(p => p.ProvinceCode == id);

            if (provinceToUpdate == null)
            {
                return NotFound();
            }

            // ViewModel의 변경 사항을 기존 엔티티에 반영합니다.
            provinceToUpdate.ProvinceName = provinceViewModel.ProvinceName;
            provinceToUpdate.CitiesString = provinceViewModel.CitiesString;

            // CitiesString에서 City 객체의 리스트로 변환하고 업데이트합니다.
            provinceToUpdate.Cities = ConvertCitiesStringToList(provinceViewModel.CitiesString, provinceToUpdate.ProvinceCode);

            // Entity Framework Core를 사용하여 엔티티를 업데이트합니다.
            _context.Update(provinceToUpdate);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProvinceExists(provinceViewModel.ProvinceCode))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(provinceViewModel);
}


private List<City> ConvertCitiesStringToList(string citiesString, string provinceCode)
{
    if (string.IsNullOrEmpty(citiesString))
        return new List<City>();

    var cityNames = citiesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    var cities = cityNames.Select(name => new City { CityName = name.Trim(), ProvinceCode = provinceCode }).ToList();
    return cities;
}


        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var province = await _context.Provinces
            //     .FirstOrDefaultAsync(m => m.ProvinceCode == id);
            var province = await _context.Provinces
                .Include(p => p.Cities) // 관련된 Cities를 포함하여 로드합니다.
                .FirstOrDefaultAsync(p => p.ProvinceCode == id);


            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province != null)
            {
                try
                {
                    _context.Provinces.Remove(province);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log the error message to your logging system
                    Console.WriteLine(ex.Message);
                    return NotFound();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinceExists(string id)
        {
            return _context.Provinces.Any(e => e.ProvinceCode == id);
        }
    }
}
