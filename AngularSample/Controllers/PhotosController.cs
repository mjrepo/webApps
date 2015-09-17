using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSample.Models;

namespace AngularSample.Controllers
{
    public class PhotosController : ApiController
    {
        private PhotoModelDbContext db = new PhotoModelDbContext();

        // GET: api/Photos
        public IQueryable<PhotoModel> GetPhotos()
        {
            return db.Photos;
        }

        // GET: api/Photos/5
        [ResponseType(typeof(PhotoModel))]
        public IHttpActionResult GetPhotoModel(int id)
        {
            PhotoModel photoModel = db.Photos.Find(id);
            if (photoModel == null)
            {
                return NotFound();
            }

            return Ok(photoModel);
        }

        // PUT: api/Photos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhotoModel(int id, PhotoModel photoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photoModel.PhotoId)
            {
                return BadRequest();
            }

            db.Entry(photoModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoModelExists(id))
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

        // POST: api/Photos
        [ResponseType(typeof(PhotoModel))]
        public IHttpActionResult PostPhotoModel(PhotoModel photoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photos.Add(photoModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photoModel.PhotoId }, photoModel);
        }

        // DELETE: api/Photos/5
        [ResponseType(typeof(PhotoModel))]
        public IHttpActionResult DeletePhotoModel(int id)
        {
            PhotoModel photoModel = db.Photos.Find(id);
            if (photoModel == null)
            {
                return NotFound();
            }

            db.Photos.Remove(photoModel);
            db.SaveChanges();

            return Ok(photoModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoModelExists(int id)
        {
            return db.Photos.Count(e => e.PhotoId == id) > 0;
        }
    }
}