using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Services
{
    public class ImageService
    {
        public static byte[] GetImageData(HttpPostedFileBase uploadImage)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }
            return imageData;
        }
    }
}