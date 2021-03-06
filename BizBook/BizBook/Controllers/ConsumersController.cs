﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BizBook.Data;
using BizBook.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BizBook.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ConsumersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Consumers
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPost.ToListAsync());
        }
        public async Task<IActionResult> BusinessIndex()
        {
            return View(await _context.BusinessProfile.ToListAsync());
        }

        // GET: Consumers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Consumer
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
           
        }
        public IActionResult BusinessDetails(int? id)
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            ViewBag.Title = "Views Dot Net | A pusher - .Net Tutorial";

            var visitors = 0;

            if (System.IO.File.Exists("visitors.txt"))
            {
                string noOfVisitors = System.IO.File.ReadAllText("visitors.txt");
                visitors = Int32.Parse(noOfVisitors);
            }

            ++visitors;

            var visit_text = (visitors == 1) ? " view" : " views";
            System.IO.File.WriteAllText("visitors.txt", visitors.ToString());

            ViewData["visitors"] = visitors;
            ViewData["visitors_txt"] = visit_text;
   
            var businessProfile = _context.BusinessProfile.Where(b => b.BusinessID == id).FirstOrDefault();                
            if (id == null)
            {
                return NotFound();
            }

            return View(businessProfile);
        }
        // GET: Consumers/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Consumers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsumerID,ConsumerName,StreetAddress,CityStateZip,ApplicationUserId,IsSubscribed")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                consumer.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(consumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", consumer.ApplicationUserId);
            return View(consumer);
        }

        // GET: Consumers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Consumer
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Consumers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsumerID,ConsumerName,StreetAddress,CityStateZip,ApplicationUserId")] Consumer consumer)
        {
            if (id != consumer.ConsumerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumerExists(consumer.ConsumerID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", consumer.ApplicationUserId);
            return View(consumer);
        }

        // GET: Consumers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ConsumerID == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        // POST: Consumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumer = await _context.Consumer.FindAsync(id);
            _context.Consumer.Remove(consumer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        //public async Task<IActionResult> AddToMyFeed()
        //{
        //    var 
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyFeed()
        {
            return View(await _context.BlogPost.ToListAsync());
        }

        private bool ConsumerExists(int id)
        {
            return _context.Consumer.Any(e => e.ConsumerID == id);
        }
        public IActionResult Map(int? id)
        {
            {
                var businessProfile = _context.BusinessProfile.Where(b => b.BusinessID == id).FirstOrDefault();
                if (id == null)
                {
                    return NotFound();
                }
                ViewBag.CustomerAddress = businessProfile.StreetAddress;
                ViewBag.CustomerZip = businessProfile.CityStateZip;
                return View(businessProfile);
            }
        }

        public async Task<IActionResult> SavedBusinesses()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Consumer
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            var consumerId = user.ConsumerID;
            var savedBusinessId = await _context.SavedBusiness.Include(m => m.businessProfile).Where(c => c.ConsumerId == consumerId).ToListAsync();
            return View(savedBusinessId);
        }

        //public async Task<IActionResult> SavedBusinesses()
        //{
        //    var userId = _userManager.GetUserId(HttpContext.User);
        //    var user = await _context.Consumer
        //        .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
        //    var consumerId = user.ConsumerID;
        //    //var savedBusinesses = 

        //    NewsfeedJunctionTable newsfeedJunctionTable = new NewsfeedJunctionTable();

        //    newsfeedJunctionTable.savedBusiness = _context.SavedBusiness.Where(c => c.ConsumerId == consumerId);
        //    newsfeedJunctionTable.blogPost = _context.BlogPost.Where(b => b.)
        //    return View();
        //}

        public async Task<IActionResult> AddSavedBusiness(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Consumer
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            var consumerId = user.ConsumerID;
            var savedBusiness = new SavedBusiness();
            savedBusiness.BusinessId = id;
            savedBusiness.ConsumerId = consumerId;
            _context.Add(savedBusiness);
            await _context.SaveChangesAsync();
            return RedirectToAction("BusinessIndex");

            return View("BusinessIndex");


            if (user == null)
            {
                return NotFound();
            }

            return View();
        }
    }
}
