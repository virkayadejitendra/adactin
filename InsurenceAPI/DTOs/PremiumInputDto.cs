using System.ComponentModel.DataAnnotations;

namespace InsurenceAPI.DTOs
{
    public class PremiumInputDto

    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid decimal number.")]
        public decimal SumInsured { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        [Range(1, 70)]
        public int Age { get; set; }
    }

}
