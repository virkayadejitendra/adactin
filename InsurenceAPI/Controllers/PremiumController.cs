using InsurenceAPI.Data;
using InsurenceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

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
            decimal deathPremium = (data.SumInsured * ratingfactor * data.Age) / 1000 * 12;
            decimal tpdPremium = (data.SumInsured * ratingfactor * data.Age) / 1234;


            return Ok(new PremiumResultDto() { DeathPremium = deathPremium, TpdPremium = tpdPremium });
        }

        private async Task<decimal> GetOccupationRatingFactor(string occupation)
        {
            var ocuupationData = this._context.Occupations.Where(o => o.Name.ToLower().Trim() == occupation.ToLower().Trim());
            if (ocuupationData == null || ocuupationData.Count() == 0)
            {
                throw new Exception("Mentioned occupation not found in database");
            }
            if (ocuupationData.Count() > 1)
            {
                throw new Exception("Mentioned occupation have multiple configuration in database");
            }
            return ocuupationData.First().Rating.Factor;
        }

    }

   
}
