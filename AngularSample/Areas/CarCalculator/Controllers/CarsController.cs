using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSample.Areas.CarCalculator.Models;

namespace AngularSample.Areas.CarCalculator.Controllers
{
    public class CarsController : ApiController
    {
        private FuelEntryDbContext db = new FuelEntryDbContext();

        // GET: api/Cars
        [ActionName("DefaultAction")]
        public IQueryable<Car> GetCars()
        {
            return db.Cars.Include(i=>i.FuelEntries);
        }

        // GET: api/Cars/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
           

            return Ok(car);
        }

        // PUT: api/Cars/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [ActionName("DefaultAction")]
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [ActionName("DefaultAction")]
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        [ActionName("SetDefault")]
        public IHttpActionResult SetDefault(int id)
        {
            foreach (var car in db.Cars)
            {
                db.Entry(car).State = EntityState.Modified;
                car.IsSelected = car.Id == id;
            }
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.Id == id) > 0;
        }
    }
}