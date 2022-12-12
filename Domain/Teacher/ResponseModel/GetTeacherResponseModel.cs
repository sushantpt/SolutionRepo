using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Teacher.ResponseModel
{
    public class GetTeacherResponseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
