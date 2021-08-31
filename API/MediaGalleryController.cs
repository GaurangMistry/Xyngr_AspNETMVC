using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class MediaGalleryController : ApiController
    {
        // GET: api/MediaGallery
        public IEnumerable<MediaGalleryViewModel> Get()
        {
            return new DALMediaGallery().GetAllMediaGallery(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public IEnumerable<MediaGalleryViewModel> GetAllMediaGalleryByBranchID(Int64 id)
        {
            return new DALMediaGallery().GetAllMediaGalleryByBranchID(id);
        }

        // GET: api/MediaGallery/5
        public MediaGalleryViewModel Get(int id)
        {
            return new DALMediaGallery().GetMediaGalleryByID(id);
        }

        // POST: api/MediaGallery
        public void Post(MediaGalleryViewModel MediaGallery)
        {
            new DALMediaGallery().SaveMediaGallery(MediaGallery, "INSERT");
        }

        // PUT: api/MediaGallery/5
        public void Put(MediaGalleryViewModel MediaGallery)
        {
            new DALMediaGallery().SaveMediaGallery(MediaGallery, "UPDATE");
        }

        // DELETE: api/MediaGallery/5
        public void Delete(int id)
        {
            new DALMediaGallery().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
