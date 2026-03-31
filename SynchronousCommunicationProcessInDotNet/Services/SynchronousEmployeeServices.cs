using SynchronousCommunicationProcessInDotNet.Dtos;
using SynchronousCommunicationProcessInDotNet.Models;
using SynchronousCommunicationProcessInDotNet.Repositories;

namespace SynchronousCommunicationProcessInDotNet.Services
{
    public class SynchronousEmployeeServices
    {
        SynchronousEmployeeRepository _empRepositoryobj = new SynchronousEmployeeRepository();
        //ASYNC AND AWAIT and TASK KEYWORDS ARE USED TO IMPLEMENT ASYNCHONOUS COMMUNICATION IN C#. ASYNCHRONOUS COMMUNICATION HELPS TO IMPROVE THE PERFORMANCE OF THE APPLICATION BY ALLOWING OTHER TASKS TO EXECUTE WHILE WAITING FOR A LONG-RUNNING OPERATION TO COMPLETE. THE ASYNC KEYWORD IS USED TO DECLARE A METHOD AS ASYNCHRONOUS, AND THE AWAIT KEYWORD IS USED TO WAIT FOR THE COMPLETION OF AN ASYNCHRONOUS OPERATION WITHOUT BLOCKING THE MAIN THREAD.
        public List<EmployeeDto> GetAllEmployee()
        {
            List<EmployeeDto> lstempdto = new List<EmployeeDto>();
            //tO CALL THE ASYN METHODS IN SOME OTHER CLASS WE WILL USE AWAIT KEYWORD AND THE CALLING METHOD SHOULD BE ASYNCHRONOUS IN NATURE
            var emp = _empRepositoryobj.GetAllEmployee();
            foreach (Employee empobj in emp)
            {
                EmployeeDto empdto = new EmployeeDto();
                empdto.empid = empobj.empid;
                empdto.empname = empobj.empname;
                empdto.empsalary = empobj.empsalary;
                lstempdto.Add(empdto);
            }
            return lstempdto;
        }
        public EmployeeDto GetEmployeeByEmpid(int empid)
        {
            var empobj = _empRepositoryobj.GetEmployeeByEmpid(empid);
            EmployeeDto empdto = new EmployeeDto();
            empdto.empid = empobj.empid;
            empdto.empname = empobj.empname;
            empdto.empsalary = empobj.empsalary;
            return empdto;
        }
        public bool AddEmployee(EmployeeDto empdetail)
        {
            Employee empobj = new Employee();//assiging the data to employee object here.
            empobj.empid = empdetail.empid;
            empobj.empname = empdetail.empname;
            empobj.empsalary = empdetail.empsalary;
            _empRepositoryobj.AddEmployee(empobj);
            return true;
        }
        public bool UpdateEmployee(EmployeeDto empdetail)
        {
            Employee empobj = new Employee();
            empobj.empid = empdetail.empid;
            empobj.empname = empdetail.empname;
            empobj.empsalary = empdetail.empsalary;
            _empRepositoryobj.UpdateEmployee(empobj);
            return true;
        }
        public bool DeleteEmployeeByEmpid(int empid)
        {
            _empRepositoryobj.DeleteEmployeeByEmpid(empid);
            return true;
        }

    }
}
