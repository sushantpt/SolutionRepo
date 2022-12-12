using Domain.Entity.Student;
using Domain.Student.RequestModel;
using Domain.Student.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Student
{
    public interface IStudentRepository
    {
        // using ADO .Net and stored procedure
        Task<GetStudentDetailResponseModel> GetStudentDetailById(GetStudentDetailRequestModel requestModel);
        Task<List<GetStudentDetailResponseModel>> GetStudentsDetail(GetStudentDetailRequestModel requestModel);
    }
}
