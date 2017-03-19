using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperheroLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<Superhero> Heroes { get; set; }
        public IEnumerable<Superability> Abilities { get; set; }

        public User()
        {
            Heroes = new List<Superhero>();
            Abilities = new List<Superability>();
        }
    }
}