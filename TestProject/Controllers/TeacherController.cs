using Domain.Entity.Teacher;
using Domain.Teacher.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Teacher;
using System;
using System.Threading.Tasks;
using TestProject.ApplicationDbContext;
using TestProject.Models.Teacher;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeacherController : ControllerBase
    {

        private ITeacherRepository _repo;
        private readonly TestProjectDbContext _context;
        public TeacherController(ITeacherRepository repo, TestProjectDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        /// <summary>
        /// Get call to get particular teacher info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTeacherDetailById")]
        public async Task<ObjectResult> GetParticularTeacher(string id)
        {
            var request = new GetTeacherDetailRequestModel();
            request.Flag = "GetTeacherDetailById";
            request.Id = id.ToString();
            var res = await _repo.GetTeacherDetailById(request);
            var model = new TeacherModel();
            model.FirstName = res.FirstName;
            model.LastName = res.LastName;
            model.Email = res.Email;
            model.PhoneNumber = res.PhoneNumber;
            model.Address = res.Address;
            return Ok(model);
        }


        /// <summary>
        /// Get call to get all teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTeachersDetail")]
        public async Task<IActionResult> GetAllTeachersDetail()
        {
            var request = new GetTeacherDetailRequestModel();
            request.Flag = "GetTeachersDetail";
            var res = await _repo.GetTeachersDetail(request);
            return Ok(res);
        }

        /// <summary>
        /// Add a new teacher to our application (POST call)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherEntityModel teacherModel)
        {
            try
            {
                var teacherEntityModel = new TeacherEntityModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = teacherModel.FirstName,
                    LastName = teacherModel.LastName,
                    Email = teacherModel.Email,
                    PhoneNumber = teacherModel.PhoneNumber,
                    Address = teacherModel.Address
                };
                await _context.Teacher.AddAsync(teacherEntityModel);
                await _context.SaveChangesAsync();
                return Ok(teacherModel);
            }
            catch
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Put call to update existing teacher info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(Guid id, TeacherModel teacherModel)
        {
            try
            {
                if (id != null)
                {
                    var teacherEntityModel = await _context.Teacher.FindAsync(id);
                    if (teacherEntityModel != null)
                    {
                        teacherEntityModel.FirstName = teacherModel.FirstName;
                        teacherEntityModel.LastName = teacherModel.LastName;
                        teacherEntityModel.Email = teacherModel.Email;
                        teacherEntityModel.PhoneNumber = teacherModel.PhoneNumber;
                        teacherEntityModel.Address = teacherModel.Address;

                        await _context.SaveChangesAsync();
                        return Ok(teacherModel);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete call to remove particular teacher 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            try
            {
                var teacherEntityModel = await _context.Teacher.FindAsync(id);
                if (teacherEntityModel != null)
                {
                    _context.Teacher.Remove(teacherEntityModel);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
