using InsurenceAPI.Data;
using InsurenceAPI.DTOs;
using InsurenceAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace InsurenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumController : ControllerBase
    {
        private DataContext _context { get; }

        public PremiumController(DataContext context)
        {
            this._context = context;
        }



        [HttpPost]
        public async Task<IActionResult> CalculatePremium([FromBody] PremiumInputDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Calculate the premiums
            var ratingfactor = await GetOccupationRatingFactor(data.Occupation);
            if (ratingfactor.Factor < 0)
            {
                return BadRequest(new ApiException(400, ratingfactor.Description, string.Empty));

            }

            decimal deathPremium = (data.SumInsured * ratingfactor.Factor * data.Age) / 1000 * 12;
            decimal tpdPremium = (data.SumInsured * ratingfactor.Factor * data.Age) / 1234;


            return Ok(new PremiumResultDto() { DeathPremium = deathPremium, TpdPremium = tpdPremium });

        }

        private async Task<RatingFactorData> GetOccupationRatingFactor(string occupation)
        {
            var ocuupationData = this._context.Occupations.Where(o => o.Name.ToLower().Trim() == occupation.ToLower().Trim());
            if (ocuupationData == null || ocuupationData.Count() == 0)
            {
                return new RatingFactorData() { Factor = -1, Description = "Mentioned occupation not found in database" };
                
            }
            if (ocuupationData.Count() > 1)
            {
                return new RatingFactorData() { Factor = -1, Description = "Mentioned occupation have multiple configuration in database" };
                
            }
            return  new RatingFactorData() { Factor = ocuupationData.First().Rating.Factor };
        }

    }

    public class RatingFactorData
    {
        public decimal Factor { get; set; }
        public string Description { get; set; }
    }

   
}
