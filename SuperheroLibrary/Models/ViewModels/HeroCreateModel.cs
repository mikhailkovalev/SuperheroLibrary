using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models.ViewModels
{
    public class HeroCreateModel: BaseHeroModel
    {
        public HttpPostedFileBase UploadImage { get; set; }
        public ICollection<Superability> UserAblilities { get; set; }
        public int[] SelectedAbilities { get; set; }

        public HeroCreateModel()
        {
            UserAblilities = new List<Superability>();
        }
    }
}