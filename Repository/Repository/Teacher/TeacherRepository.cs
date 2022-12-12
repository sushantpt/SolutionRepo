using DataAccessLayer.DataAccess;
using Domain.Teacher.RequestModel;
using Domain.Teacher.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Teacher
{
    public class TeacherRepository : ITeacherRepository
    {

        IDataAccess _dao;
        public TeacherRepository(IDataAccess dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Method to get teacher's detail by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GetTeacherResponseModel> GetTeacherDetailById(GetTeacherDetailRequestModel requestModel)
        {
            var response = new GetTeacherResponseModel();
            try
            {
                var sql = $@"EXEC spTeacher @Flag = {_dao.FilterString(requestModel.Flag)}, @Id = {_dao.FilterString(requestModel.Id)}";
                var res = _dao.ExecuteDataSet(sql);
                if (res != null)
                {
                    var dt1 = res.Tables[0];
                    if (dt1 != null)
                    {
                        if (dt1.Rows != null)
                        {
                            response = new GetTeacherResponseModel();
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
            catch (Exception ex)
            {
                return response = null;
            }
        }


        /// <summary>
        /// Method to get all teachers detail
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetTeacherResponseModel>> GetTeachersDetail(GetTeacherDetailRequestModel requestModel)
        {
            var teacherList = new List<GetTeacherResponseModel>();
            try
            {
                var sql = $@"EXEC spTeacher @Flag = {_dao.FilterString(requestModel.Flag)}";
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
                                var response = new GetTeacherResponseModel();
                                response.Id = item["Id"].ToString();
                                response.FirstName = item["FirstName"].ToString();
                                response.LastName = item["LastName"].ToString();
                                response.Email = item["Email"].ToString();
                                response.PhoneNumber = item["PhoneNumber"].ToString();
                                response.Address = item["Address"].ToString();
                                teacherList.Add(response);
                            }
                        }
                    }
                }
                return teacherList;
            }
            catch (Exception ex)
            {
                return teacherList = null;
            }
        }
    }
}
