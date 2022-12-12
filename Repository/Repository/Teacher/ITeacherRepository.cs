using Domain.Teacher.RequestModel;
using Domain.Teacher.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Teacher
{
    public interface ITeacherRepository
    {
        // using ADO .Net and stored procedure
        Task<GetTeacherResponseModel> GetTeacherDetailById(GetTeacherDetailRequestModel requestModel);
        Task<List<GetTeacherResponseModel>> GetTeachersDetail(GetTeacherDetailRequestModel requestModel);
    }
}
