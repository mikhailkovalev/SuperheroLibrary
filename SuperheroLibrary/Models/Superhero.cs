﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models
{
    public class Superhero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Superability> Abilities { get; set; }

        public Superhero()
        {
            Abilities = new List<Superability>();
        }
    }
}