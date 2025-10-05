using System.ComponentModel.DataAnnotations;

namespace CRUD_Demo.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }
    }
}
