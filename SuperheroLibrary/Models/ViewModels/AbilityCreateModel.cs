using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuperheroLibrary.Models.ViewModels
{
    public class AbilityCreateModel: BaseAbilityModel
    {
        public HttpPostedFileBase UploadImage { get; set; }
    }
}