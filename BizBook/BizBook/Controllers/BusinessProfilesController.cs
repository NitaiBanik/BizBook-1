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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RestSharp;
using RestSharp.Authenticators;


namespace BizBook.Controllers
{
    public class BusinessProfilesController : Controller
    {
        private readonly IHostingEnvironment he;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        
        public BusinessProfilesController(ApplicationDbContext context,IHostingEnvironment e, UserManager<IdentityUser> userManager)

        {
            _context = context;
            he = e;
            _userManager = userManager;
        }

        // GET: BusinessProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessProfile.ToListAsync());
        }

        // GET: BusinessProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.BusinessProfile
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
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
                businessProfile.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(businessProfile);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            SendSimpleMessage();
            return View("Details", businessProfile);
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

                if (pic == null)
                {
                    return View();

                }
              
                if (pic != null)
                {
                    var fileName = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));

                    var userid= User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var businessProfile =  _context.BusinessProfile
                        .FirstOrDefault(m => m.ApplicationUserId == userid);

                    businessProfile.Image1 = fileName;
                    _context.Update(businessProfile);
                    _context.SaveChangesAsync();
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));
                    ViewData["FileLocation"] = "/" + Path.GetFileName(pic.FileName);
                }
            
            return View(); 
        }

        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            Key.mailgunKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox704c2ec99b85406fa343c888c7f3507f.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@sandbox704c2ec99b85406fa343c888c7f3507f.mailgun.org>");
            request.AddParameter("to", "svolbrecht@yahoo.com");
            //request.AddParameter("to", "YOU@sandbox704c2ec99b85406fa343c888c7f3507f.mailgun.org");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
