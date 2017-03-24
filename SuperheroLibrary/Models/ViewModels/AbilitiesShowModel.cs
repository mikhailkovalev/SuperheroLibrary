using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models.ViewModels
{
    public class AbilitiesShowModel: BaseViewModel
    {
        public ICollection<AbilityShowModel> AbilitiesList { get; set; }
    }
}