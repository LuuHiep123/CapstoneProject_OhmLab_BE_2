using BusinessLayer.RequestModel.Room;
using BusinessLayer.RequestModel.Semester;
using BusinessLayer.RequestModel.SemesterSubject;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Semester;
using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OhmLab_FUHCM_BE.Controller
{
    [Route("api/SemesterSubject/")]
    [ApiController]
    public class SemesterSubjectController : ControllerBase
    {
        private readonly ISemesterSubjectService _semesterSubjectService;
        public SemesterSubjectController(ISemesterSubjectService semesterSubjectService)
        {
            _semesterSubjectService = semesterSubjectService;
        }

        [Authorize(Roles = "Admin,HeadOfDepartment")]
        [HttpPost("SemesterSubject")]
        public async Task<IActionResult> CreateSemesterSubject(CreateSemesterSubjectRequestModel model)
        {
            try
            {
                var result = await _semesterSubjectService.CreateSemesterSubjectAsync(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllSemesterSubject(GetAllSemesterSubjectRequestModel model)
        {
            try
            {
                var result = await _semesterSubjectService.GetAllAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("getbyId/{id}")]
        public async Task<IActionResult> GetSemesterSubjectById(int id)
        {
            try
            {
                var result = await _semesterSubjectService.GetByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HeadOfDepartment")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSemesterSubject(int id, UpdateSemesterSubjectRequestModel model)
        {
            try
            {
                var result = await _semesterSubjectService.UpdateAsync(id, model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("semester/{SemesterId}")]
        public async Task<IActionResult> GetSemesterSubjectBySemesterId(int SemesterId)
        {
            try
            {
                var result = await _semesterSubjectService.GetBySemesterIdAsync(SemesterId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("subject/{SubjectId}")]
        public async Task<IActionResult> GetSemesterSubjectBySubjectId(int SubjectId)
        {
            try
            {
                var result = await _semesterSubjectService.GetBySubjectIdAsync(SubjectId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("SubjectIdAndSemesterId")]
        public async Task<IActionResult> GetSemesterSubjectBySubjectIdAndSemesterId(int SubjectId, int SemesterId)
        {
            try
            {
                var result = await _semesterSubjectService.GetBySemesterIdAndSubjectIdAsync(SubjectId, SemesterId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
