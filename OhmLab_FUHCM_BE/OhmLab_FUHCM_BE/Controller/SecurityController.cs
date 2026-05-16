using BusinessLayer.RequestModel.RegistrationSchedule;
using BusinessLayer.RequestModel.Security;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OhmLab_FUHCM_BE.Controller
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IsecurityService _securityService;
        public SecurityController(IsecurityService securityService)
        {
            _securityService = securityService;
        }


        [Authorize(Roles = "Admin,HeadOfDepartment,Security")]
        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn(CheckInSecurityRequestModel model)
        {
            try
            {
                var result = await _securityService.CheckIn(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
