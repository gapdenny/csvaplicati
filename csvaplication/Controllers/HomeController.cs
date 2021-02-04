using AutoMapper;
using csvaplication.Models;
using CsvHelper;
using CsvHelper.Configuration;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace csvaplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvRepository _csvRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        private readonly ICsvService _csvService;

        public HomeController(ILogger<HomeController> logger, ICsvRepository csvRepository, IWebHostEnvironment appEnvironment, IMapper mapper, ICsvService csvService)
        {
            _logger = logger;
            _csvRepository = csvRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
            _csvService = csvService;
        }

        [Route("Home/UploadAsyn")]
        public IActionResult UploadAsyn()
        {
            return View();
        }

        [HttpPost]
        [Route("Home/UploadAsync")]
        public async Task<IActionResult> UploadAsync(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // шлях до папки Files
                string path = _appEnvironment.WebRootPath + "/Files/" + Guid.NewGuid() + "." + uploadedFile.FileName;
                

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);

                   
                }
                FileService fileService = new FileService();
                IEnumerable<string> strings = fileService.ParseFile(path);
                List<ViewCsv> entities = new List<ViewCsv>();
                foreach (var e in strings)
                {

                    IEnumerable<string> elements = fileService.SplitString(e);
                    ViewCsv dto = fileService.GetCsv(elements);

                    entities.Add(dto);
                }
                await _csvService.Insert(entities);
            }

            return RedirectToAction("Index");
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
        // GET: Csvs
        public async Task<IActionResult> Index()
        {
            return  View(await _csvService.GetAll());
        }

        // GET: Csvs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var csv = await _csvService.GetById(id);
            if (csv == null)
            {
                return NotFound();
            }

            return View(csv);
        }

        // GET: Csvs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Csvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,DateOfBirth,Phone,Salary,IsMaried")] ViewCsv csv)
        {
            if (ModelState.IsValid)
            {
                await _csvService.Insert(csv);
                
                return RedirectToAction(nameof(Index));
            }
            return View(csv);
        }

        // GET: Csvs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var csv = await _csvService.GetById(id);
            if (csv == null)
            {
                return NotFound();
            }
            return View(csv);
        }

        // POST: Csvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DateOfBirth,Phone,Salary,IsMaried")] ViewCsv csv)
        {
            if (id != csv.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _csvService.Update(csv);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CsvExists(csv.Id))
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
            return View(csv);
        }

        // GET: Csvs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var csv = await _csvService.GetById(id);
            if (csv == null)
            {
                return NotFound();
            }

            return View(csv);
        }

        // POST: Csvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var csv = await _csvService.GetById(id);
            await _csvService.Delete(csv.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool CsvExists(int id)
        {
            return _csvRepository.IsAny(id);
        }
    }
}
