using InsurenceAPI.Data;
using InsurenceAPI.DTOs;
using InsurenceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private DataContext _context { get; }

        public OccupationController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOccupations()
        {
            var result =  await this._context.Occupations.Select(occ=>occ.Name).ToListAsync();

            return Ok(result);
        }

    }
}
