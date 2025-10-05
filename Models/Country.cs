using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Demo.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<State> States { get; set; }

    }
}
