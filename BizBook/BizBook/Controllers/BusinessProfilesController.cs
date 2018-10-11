using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BizBook.Data;
using BizBook.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BizBook.Controllers
{
    public class BusinessProfilesController : Controller
    {
        private readonly IHostingEnvironment he;
        private readonly ApplicationDbContext _context;

        public BusinessProfilesController(ApplicationDbContext context,IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }

        // GET: BusinessProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessProfile.ToListAsync());
        }

        // GET: BusinessProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessProfile = await _context.BusinessProfile
                .FirstOrDefaultAsync(m => m.BusinessID == id);
            if (businessProfile == null)
            {
                return NotFound();
            }

            return View(businessProfile);
        }

        // GET: BusinessProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessID,BusinessName,BusinessType,StreetAddress,CityStateZip,BusinessBio,Promotions,Link")] BusinessProfile businessProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessProfile);
        }

        // GET: BusinessProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessProfile = await _context.BusinessProfile.FindAsync(id);
            if (businessProfile == null)
            {
                return NotFound();
            }
            return View(businessProfile);
        }

        // POST: BusinessProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessID,BusinessName,BusinessType,StreetAddress,CityStateZip,BusinessBio,Promotions,Link")] BusinessProfile businessProfile)
        {
            if (id != businessProfile.BusinessID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessProfileExists(businessProfile.BusinessID))
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
            return View(businessProfile);
        }

        // GET: BusinessProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessProfile = await _context.BusinessProfile
                .FirstOrDefaultAsync(m => m.BusinessID == id);
            if (businessProfile == null)
            {
                return NotFound();
            }

            return View(businessProfile);
        }

        // POST: BusinessProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessProfile = await _context.BusinessProfile.FindAsync(id);
            _context.BusinessProfile.Remove(businessProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessProfileExists(int id)
        {
            return _context.BusinessProfile.Any(e => e.BusinessID == id);
        }
        public async Task<IActionResult> Map(int? id)
        {
            {
                if (id == null)
                {
                    //not sure how to revise this for Core.  This code should alert user in thr case there is no user logged in.
                    //return HttpStatusCode.BadRequest;
                }
                BusinessProfile businessProfile = _context.BusinessProfile.Find(id);
                if (businessProfile == null)
                {
                    return NotFound();
                }
                ViewBag.ApplicationUserId = new SelectList(_context.Users, "Id", "UserRole", businessProfile.ApplicationUser);
                ViewBag.CustomerAddress = businessProfile.StreetAddress;
                ViewBag.CustomerZip = businessProfile.CityStateZip;
                return View(businessProfile);
            }
        }
        public IActionResult UploadImage(string fullName, IFormFile pic, int? id)
        {

            {
                if (id == null)
                {
                    //not sure how to revise this for Core.  This code should alert user in thr case there is no user logged in.
                    //return HttpStatusCode.BadRequest;
                }
                //BusinessProfile businessProfile = _context.BusinessProfile.Find(id);
                //if (businessProfile == null)
                //{
                //    return NotFound();
                //}
                ViewData["fname"] = fullName;
                if (pic != null)
                {
                    var fileName = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));
                    ViewData["FileLocation"] = "/" + Path.GetFileName(pic.FileName);
                }
                return View();
            }
        }
    }
}
