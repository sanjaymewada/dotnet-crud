using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Demo.Models


{
    public class Employee
    {
        public int Id { get; set; }   

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Department { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required, EmailAddress]
        public string Email  { get; set; }

        [Required]
        public int? CountryId { get; set; }
        [Required]
        public int? StateId { get; set; }
        [Required]
        public int? CityId { get; set; }

        public string DocumentPath { get; set; }  


        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }



    }
}
