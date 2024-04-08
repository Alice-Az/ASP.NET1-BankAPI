using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO.Customer
{
    public class CustomerRequest
    {
        [Required]
        [StringLength(6)]
        public string Gender { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Givenname { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Zipcode { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Emailaddress { get; set; } = null!;

        [Required]
        [StringLength(256)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(3)]
        public string Password { get; set; } = null!;
    }
}
