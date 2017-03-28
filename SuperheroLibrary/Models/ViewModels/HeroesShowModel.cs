using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models.ViewModels
{
    public class HeroesShowModel: BaseViewModel
    {
        public ICollection<HeroShowModel> HeroesList { get; set; }
    }
}