using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.DTO.Account
{
    public class NewAccountRequest
    {
        [Required]
        [RegularExpression("^(AfterTransaction|Weekly|Monthly)$")]
        public string Frequency { get; set; } = null!;

        [Required]
        [Range(1,2)]
        public int? AccountTypesId { get; set; }
    }
}
