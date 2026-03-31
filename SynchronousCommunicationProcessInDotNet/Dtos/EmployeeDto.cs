namespace SynchronousCommunicationProcessInDotNet.Dtos
{
    //In dotnetcore /mvc we called this as Model class.
    //In dotnetcore /webapi we called this as Dto class.
    //Dto stands for Data Transfer Object, it is used to transfer data between different layers of the application. It is a simple class that contains properties to hold the data that we want to transfer. Dto classes are used to decouple the internal representation of the data from the external representation, which can help to improve the maintainability and scalability of the application.

    public class EmployeeDto
    {
        public int empid { get; set; }
        public int empsalary { get; set; }
        public string empname { get; set; }

    }
}
