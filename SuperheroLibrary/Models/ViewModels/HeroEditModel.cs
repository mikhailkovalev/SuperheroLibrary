using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models.ViewModels
{
    public class HeroEditModel : BaseHeroModel
    {
        public HttpPostedFileBase UploadImage { get; set; }
        public byte[] Image { get; set; }
    }
}