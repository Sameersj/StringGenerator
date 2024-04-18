using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StringGenerator.Data;
using StringGenerator.Models;

namespace StringGenerator.Controllers
{
    public class StringPropertiesController : Controller
    {
        private StringGeneratorContext db = new StringGeneratorContext();

        // GET: StringProperties
        public async Task<ActionResult> Index()
        {
            return View(await db.StringProperties.ToListAsync());
        }

        // GET: StringProperties/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringProperties stringProperties = await db.StringProperties.FindAsync(id);
            if (stringProperties == null)
            {
                return HttpNotFound();
            }
            return View(stringProperties);
        }

        // GET: StringProperties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StringProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StringCount,StringLength,IsNumeric,IsUpperCase,IsLowerCase,IsUniqueString")] StringProperties stringProperties)
        {
            // Generate random string based on user input
            var ListOfRandomStrings = Models.StringProperties.GenerateRandomString(stringProperties.StringCount, stringProperties.StringLength, stringProperties.IsNumeric, stringProperties.IsUpperCase, stringProperties.IsLowerCase, stringProperties.IsUniqueString);
            // Convert List to comma separated string
            stringProperties.RandomString = string.Join(",", ListOfRandomStrings);
            stringProperties.RandomString = stringProperties.RandomString.Replace(",", ", ");
            if (ModelState.IsValid)
            {
                // First delete all existing records and then add new record
                db.StringProperties.RemoveRange(db.StringProperties);

                db.StringProperties.Add(stringProperties);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stringProperties);
        }

        // GET: StringProperties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringProperties stringProperties = await db.StringProperties.FindAsync(id);
            if (stringProperties == null)
            {
                return HttpNotFound();
            }

            return View(stringProperties);
        }

        // POST: StringProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StringCount,StringLength,IsNumeric,IsUpperCase,IsLowerCase,IsUniqueString")] StringProperties stringProperties)
        {
            // Generate random string based on user input
            var ListOfRandomStrings = Models.StringProperties.GenerateRandomString(stringProperties.StringCount, stringProperties.StringLength, stringProperties.IsNumeric, stringProperties.IsUpperCase, stringProperties.IsLowerCase, stringProperties.IsUniqueString);
            // Convert List to comma separated string
            stringProperties.RandomString = string.Join(",", ListOfRandomStrings);
            stringProperties.RandomString = stringProperties.RandomString.Replace(",", ", ");
            if (ModelState.IsValid)
            {
                db.Entry(stringProperties).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stringProperties);
        }

        // GET: StringProperties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringProperties stringProperties = await db.StringProperties.FindAsync(id);
            if (stringProperties == null)
            {
                return HttpNotFound();
            }
            return View(stringProperties);
        }

        // POST: StringProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StringProperties stringProperties = await db.StringProperties.FindAsync(id);
            db.StringProperties.Remove(stringProperties);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
