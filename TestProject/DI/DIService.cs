using DataAccessLayer.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repository.Student;
using Repository.Repository.Teacher;

namespace TestProject.DI
{
    public class DIService
    {
        public static void InitialiseDependencies(IServiceCollection service)
        {
            service.AddTransient<IDataAccess, DataAccess>();
            service.AddTransient<IStudentRepository, StudentRepository>();
            service.AddTransient<ITeacherRepository, TeacherRepository>();
        }
    }
}
