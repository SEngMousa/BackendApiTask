using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverListDTO>> GetDriverById(int id)
        {
            var driver = await _driverService.GetDriverById(id);
            return driver != null ? Ok(driver) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<DriverListDTO>> GetAllDrivers()
        {
            var drivers = await _driverService.GetAllDrivers();
            return Ok(drivers);
        }

        [HttpGet("alphabetically")]
        public async Task<ActionResult<DriverListDTO>> GetAllDriversAlphabetically()
        {
            var drivers = await _driverService.GetAllDriversAlphabetically();
            return Ok(drivers);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverDTO driverDTO)
        {
            await _driverService.AddDriver(driverDTO);
            return NoContent();
        }

        [HttpGet("{id}/alphabetized_name")]
        public async Task<ActionResult<DriverAlphabetizedDTO>> GetAlphabetizedName(int id)
        {
            var driver = await _driverService.GetAlphabetizedName(id);
            return Ok(driver);
        }

        [HttpPut ]
        public async Task<IActionResult> UpdateDriver( [FromBody] UpdateDriverDTO driverDTO)
        {
          

            await _driverService.UpdateDriver(driverDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            
            await _driverService.DeleteDriver(id);
            return NoContent();
        }

        [HttpPost("InsertRandomNames/{count}")]
        public async Task<IActionResult> InsertRandomNames(int count)
        {
            await _driverService.InsertRandomNames(count);
            return NoContent();
        }
    }
}
