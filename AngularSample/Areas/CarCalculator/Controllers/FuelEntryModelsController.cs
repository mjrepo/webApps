using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSample.Areas.CarCalculator.Models;

namespace AngularSample.Areas.CarCalculator.Controllers
{
    public class FuelEntryModelsController : ApiController
    {
        private FuelEntryDbContext db = new FuelEntryDbContext();

        // GET: api/FuelEntryModels
        [ActionName("DefaultAction")]
        public IEnumerable<FuelEntryModel> GetFuelEntries()
        {
            var defaultCar = db.Cars.Include(i=>i.FuelEntries).SingleOrDefault(i => i.IsSelected);
            return defaultCar == null ? Enumerable.Empty<FuelEntryModel>() : defaultCar.FuelEntries;
        }

        // GET: api/FuelEntryModels/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(FuelEntryModel))]
        public IHttpActionResult GetFuelEntryModel(int id)
        {
            FuelEntryModel fuelEntryModel = db.FuelEntries.Find(id);
            if (fuelEntryModel == null)
            {
                return NotFound();
            }

            return Ok(fuelEntryModel);
        }

        // PUT: api/FuelEntryModels/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFuelEntryModel(int id, FuelEntryModel fuelEntryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fuelEntryModel.Id)
            {
                return BadRequest();
            }

            db.Entry(fuelEntryModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelEntryModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FuelEntryModels
        [ActionName("DefaultAction")]
        [ResponseType(typeof(FuelEntryModel))]
        public IHttpActionResult PostFuelEntryModel(FuelEntryModel fuelEntryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var defaultCar = db.Cars.Single(i => i.IsSelected);
            fuelEntryModel.CarId = defaultCar.Id;
            db.FuelEntries.Add(fuelEntryModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fuelEntryModel.Id }, fuelEntryModel);
        }

        // DELETE: api/FuelEntryModels/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(FuelEntryModel))]
        public IHttpActionResult DeleteFuelEntryModel(int id)
        {
            FuelEntryModel fuelEntryModel = db.FuelEntries.Find(id);
            if (fuelEntryModel == null)
            {
                return NotFound();
            }

            db.FuelEntries.Remove(fuelEntryModel);
            db.SaveChanges();

            return Ok(fuelEntryModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuelEntryModelExists(int id)
        {
            return db.FuelEntries.Count(e => e.Id == id) > 0;
        }
    }
}