using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class ImageGalleryController : ApiController
    {
        // GET: api/ImageGallery
        public IEnumerable<ImageGalleryViewModel> Get()
        {
            return new DALImageGallery().GetAllImageGallery(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public IEnumerable<ImageGalleryViewModel> GetAllImageGalleryByBranchID(Int64 id)
        {
            return new DALImageGallery().GetAllImageGalleryByBranchID(id);
        }

        // GET: api/ImageGallery/5
        public ImageGalleryViewModel Get(int id)
        {
            return new DALImageGallery().GetImageGalleryByID(id);
        }

        // POST: api/ImageGallery
        public void Post(ImageGalleryViewModel ImageGallery)
        {
            new DALImageGallery().SaveImageGallery(ImageGallery, "INSERT");
        }

        // PUT: api/ImageGallery/5
        public void Put(ImageGalleryViewModel ImageGallery)
        {
            new DALImageGallery().SaveImageGallery(ImageGallery, "UPDATE");
        }

        // DELETE: api/ImageGallery/5
        public void Delete(int id)
        {
            new DALImageGallery().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
