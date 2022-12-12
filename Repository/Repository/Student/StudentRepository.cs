using DataAccessLayer.DataAccess;
using Domain.Entity.Student;
using Domain.Student.RequestModel;
using Domain.Student.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Student
{
    public class StudentRepository : IStudentRepository
    {
        IDataAccess _dao;
        public StudentRepository(IDataAccess dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Get call by particular id
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<GetStudentDetailResponseModel> GetStudentDetailById(GetStudentDetailRequestModel requestModel)
        {
            var response = new GetStudentDetailResponseModel();
            try
            {
                var sql = $@"EXEC spStudent @Flag = {_dao.FilterString(requestModel.Flag)}, @Id = {_dao.FilterString(requestModel.Id)}";
                var res = _dao.ExecuteDataSet(sql);
                if (res != null)
                {
                    var dt1 = res.Tables[0];
                    if (dt1 != null)
                    {
                        if (dt1.Rows != null)
                        {
                            response = new GetStudentDetailResponseModel();
                            response.Id = dt1.Rows[0]["Id"].ToString();
                            response.FirstName = dt1.Rows[0]["FirstName"].ToString();
                            response.LastName = dt1.Rows[0]["LastName"].ToString();
                            response.Email = dt1.Rows[0]["Email"].ToString();
                            response.PhoneNumber = dt1.Rows[0]["PhoneNumber"].ToString();
                            response.Address = dt1.Rows[0]["Address"].ToString();
                        }
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                return response = null;
            }
        }


        /// <summary>
        /// Method to get all students detail
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<GetStudentDetailResponseModel>> GetStudentsDetail(GetStudentDetailRequestModel requestModel)
        {
            var studentList = new List<GetStudentDetailResponseModel>();
            try
            {
                var sql = $@"EXEC spStudent @Flag = {_dao.FilterString(requestModel.Flag)}";
                var res = _dao.ExecuteDataSet(sql);

                if (res != null)
                {
                    var dt1 = res.Tables[0];
                    if (dt1 != null)
                    {
                        if (dt1.Rows != null)
                        {
                            foreach (DataRow item in dt1.Rows)
                            {
                                var response = new GetStudentDetailResponseModel();
                                response.Id = item["Id"].ToString();
                                response.FirstName = item["FirstName"].ToString();
                                response.LastName = item["LastName"].ToString();
                                response.Email = item["Email"].ToString();
                                response.PhoneNumber = item["PhoneNumber"].ToString();
                                response.Address = item["Address"].ToString();
                                studentList.Add(response);
                            }
                        }
                    }
                }
                return studentList;
            }
            catch (Exception ex)
            {
                return studentList = null;
            }
        }

    }
}
