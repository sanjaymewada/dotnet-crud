using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Demo.Models
{
    public class State
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
