using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models.ViewModels
{
    public class AbilityEditModel: BaseAbilityModel
    {
        public byte[] Image { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }
    }
}