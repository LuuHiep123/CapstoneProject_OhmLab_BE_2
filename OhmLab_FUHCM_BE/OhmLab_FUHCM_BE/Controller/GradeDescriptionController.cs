using BusinessLayer.RequestModel.Grade;
using BusinessLayer.RequestModel.GradeDesciption;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OhmLab_FUHCM_BE.Controller
{
    [Route("api/GradeDescription/")]
    [ApiController]
    public class GradeDescriptionController : ControllerBase
    {
        private readonly IGradeDescriptionService _gradeDescriptioinService;
        private readonly ILogger<GradeController> _logger;

        public GradeDescriptionController(IGradeDescriptionService gradeDescriptioinService, ILogger<GradeController> logger)
        {
            _gradeDescriptioinService = gradeDescriptioinService;
            _logger = logger;
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

        [HttpPost("SearchGradeDescription")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment")]
        public async Task<IActionResult> GetAllGradeDescription(GetAllGradeDescriptionRequestModel model)
        {
            try
            {
                var result = await _gradeDescriptioinService.GetAllGradeDescription(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Grade/{gradeId}")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment")]
        public async Task<IActionResult> GetAllGradeDesciptionByGradeId(int gradeId)
        {
            try
            {
                var result = await _gradeDescriptioinService.GetGradeDescriptionByGradeId(gradeId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("MyGrade")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment,Student")]
        public async Task<IActionResult> GetAllGradeDescriptionByStudentId()
        {
            try
            {
                var userId = this.GetCurrentUserId();
                var result = await _gradeDescriptioinService.GetGradeDescriptionByUserid(userId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer,Student")]
        [HttpGet("{gradeDescriptionId}")]
        public async Task<IActionResult> GetGradeById(int gradeDescriptionId)
        {
            try
            {
                var result = await _gradeDescriptioinService.GetGradeDescriptionById(gradeDescriptionId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGradeDescription([FromBody] UpdateGradeDescriptionRequestModel model)
        {
            try
            {
                var result = await _gradeDescriptioinService.UpdateGradeDescription(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
