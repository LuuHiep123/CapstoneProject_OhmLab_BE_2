using BusinessLayer.RequestModel.Assignment;
using BusinessLayer.RequestModel.EquipmentType;
using BusinessLayer.RequestModel.Grade;
using BusinessLayer.RequestModel.Kit;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Grade;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace OhmLab_FUHCM_BE.Controller
{
    [ApiController]
    [Route("api/Grade/")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly ILogger<GradeController> _logger;

        public GradeController(IGradeService gradeService, ILogger<GradeController> logger)
        {
            _gradeService = gradeService;
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

        // Giảng viên chấm điểm cho team
        [HttpPost("GradeForTeam")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment")]
        public async Task<IActionResult> GradeForTeam(CreateGradeRequestModel model)
        {
            try
            {
                // Lấy lecturerId từ token
                var lecturedId = GetCurrentUserId();

                var result = await _gradeService.CreateGrade(model, lecturedId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // lấy tất cả điểm của team
        [HttpPost("SearchGrade")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment")]
        public async Task<IActionResult> GetAllGrae(GetAllGradeRequestModel model)
        {
            try
            {
                var result = await _gradeService.GetAllGrade(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("SearchGradeByRegisterScheduleIdAndTeamId")]
        [Authorize(Roles = "Lecturer,Admin,HeadOfDepartment")]
        public async Task<IActionResult> GetAllGraeforRegistrationScheduleAndTeamId(GetGradeOfTeamByRegistrationScheduleIdAndTeamId model)
        {
            try
            {
                var result = await _gradeService.GetGradeOfTeamByRegistrationScheduleIdAndTeamId(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GradeTeamLab: {Message}", ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer")]
        [HttpGet("Registration/{registrationScheduleId}")]
        public async Task<IActionResult> GetGradeByRegistrationScheduleId(int registrationScheduleId)
        {
            try
            {
                var result = await _gradeService.GetGradeOfTeamByRegistrationScheduleId(registrationScheduleId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer")]
        [HttpGet("Team/{teamId}")]
        public async Task<IActionResult> GetGradeByTeamId(int teamId)
        {
            try
            {
                var result = await _gradeService.GetGradeOfTeamByTeamId(teamId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer")]
        [HttpGet("{gradeId}")]
        public async Task<IActionResult> GetGradeById(int gradeId)
        {
            try
            {
                var result = await _gradeService.GetGradeOfTeamById(gradeId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment,Lecturer")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeRequestModel model)
        {
            try
            {
                var result = await _gradeService.UpdateGrade(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
