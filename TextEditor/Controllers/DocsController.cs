using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextEditor.Data;
using TextEditor.Data.Services;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    [Authorize]
    public class DocsController : Controller
    {
        private readonly IDocsService _service;
        public DocsController(IDocsService service)
        {
            _service = service;
        }

        // GET: Docs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _service.GetAll();
            applicationDbContext = applicationDbContext.Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(applicationDbContext);
        }

       

        // GET: Docs/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Docs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,UserId")] Doc doc)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(doc);
                return RedirectToAction(nameof(Index));
            }
            
            return View(doc);
        }

        // GET: Docs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _service.GetById(id);
            if (doc == null)
            {
                return NotFound();
            }
            if (doc.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Docs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,UserId")] Doc doc)
        {
            if (id != doc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {              
                await _service.Update(doc);               
                return RedirectToAction(nameof(Index));
            }
            
            return View(doc);
        }

        // GET: Docs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var doc = await _service.GetById(id);
            if (doc == null)
            {
                return NotFound();
            }
            if (doc.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var doc = await _service.GetById(id);
            if (doc != null)
            {
                await _service.Delete(doc);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
