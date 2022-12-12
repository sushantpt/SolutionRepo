using Domain.Entity.Student;
using Domain.Student.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Student;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestProject.ApplicationDbContext;
using TestProject.Models.Auth;
using TestProject.Models.Student;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _repo;
        private readonly TestProjectDbContext _context;
        public StudentController(IStudentRepository repo, TestProjectDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        /// <summary>
        ///  Get call to get particular student info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentDetailById")]
        public async Task<IActionResult> GetParticularStudent(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var request = new GetStudentDetailRequestModel();
                    request.Flag = "GetStudentDetailById";
                    request.Id = id.ToString();
                    var res = await _repo.GetStudentDetailById(request);
                    if (res != null)
                    {
                        var model = new StudentModel();
                        model.FirstName = res.FirstName;
                        model.LastName = res.LastName;
                        model.Email = res.Email;
                        model.PhoneNumber = res.PhoneNumber;
                        model.Address = res.Address;
                        return Ok(model);
                    }
                    else
                    {
                        var model = new StudentModel();
                        return Ok(model);
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
        /// Get call to get all students info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentsDetail")]
        public async Task<IActionResult> GetAllStudentsDetail()
        {
            try
            {
            var request = new GetStudentDetailRequestModel();
            request.Flag = "GetStudentsDetail";
            var res = await _repo.GetStudentsDetail(request);
            return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Post call to add new student
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentEntityModel studentModel)
        {
            try { 
                var studentEntityModel = new StudentEntityModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = studentModel.FirstName,
                    LastName = studentModel.LastName,
                    Email = studentModel.Email,
                    PhoneNumber = studentModel.PhoneNumber,
                    Address = studentModel.Address
                };
            await _context.Student.AddAsync(studentEntityModel);
            await _context.SaveChangesAsync();
            return Ok(studentModel);
            }
            catch
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Put call to update existing student info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Guid id, StudentModel studentModel)
        {
            try
            {
                if (id != null)
                {
                    var studentEntityModel = await _context.Student.FindAsync(id);
                    if (studentEntityModel != null)
                    {
                        studentEntityModel.FirstName = studentModel.FirstName;
                        studentEntityModel.LastName = studentModel.LastName;
                        studentEntityModel.Email = studentModel.Email;
                        studentEntityModel.PhoneNumber = studentModel.PhoneNumber;
                        studentEntityModel.Address = studentModel.Address;
                        
                        await _context.SaveChangesAsync();
                        return Ok(studentModel);
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
        /// Delete call to delete particular student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                var studentEntityModel = await _context.Student.FindAsync(id);
                if (studentEntityModel != null)
                {
                    _context.Student.Remove(studentEntityModel);
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
