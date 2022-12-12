using System.Collections.Generic;

namespace TestProject.Models.Auth
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>
        {
            new UserModel { Username = "admin", Password = "admin" }
        };
    }
}
