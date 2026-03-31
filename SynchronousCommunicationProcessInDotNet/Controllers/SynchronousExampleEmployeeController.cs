using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynchronousCommunicationProcessInDotNet.Dtos;
using SynchronousCommunicationProcessInDotNet.Services;

namespace SynchronousCommunicationProcessInDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronousExampleEmployeeController : ControllerBase
    {
        /*
=>In .NET, 2 main types of communication patterns are commonly used: Synchronous and Asynchronous communication.
 Synchronous and Asynchronous communication mainly describe how your code calls APIs, services,
 or methods and how it waits for results.
1.synchronous communication:
=========================
1. In synchronous communication, the User waits for the called method to complete 
before proceeding to the next line of code.It is a blocking operation,
meaning that the thread executing the code will be blocked until the operation is complete.

 (or)
In synchronous execution, User should   waits until the previous task completes.
==================================================================================
Key Points
===============
In synchronous communication we are not used any "async" and "await" and "Task"  keywords.
=>If you try to call any datbase opertion method it will block one thread to be complete the process.
=>upto the time of completion of the process, the thread will be busy and it will not be able to do any other work.
=>user should wait until the process is completed, then only it will be able to do any other work.
=>Synchronous communication Executes line by line and Simple to understand.
=>Synchronous communication is Slow for long operations Like (API/DB calls)
=>ASynchronous communication is Fast for long operations Like (API/DB calls),because it does not block the thread while waiting for the operation to complete.
1.In Synchronous Communication use Synchronous Methods Only(It menas Without using async,await,Task Keywords in method Preparation Time)
2.In Asynchronus Communication use Asynchronous Methods Only(it means using Async,await,Task Keywords in Method Preaprtion Time)
=======================================================================================
*/
        SynchronousEmployeeServices employeeServices = new SynchronousEmployeeServices();
        //Based on Route name it will call the respective api method.
        //Route is playing a very Keyrole Here.
        //if you want to call any controller api methods routing is very important.
        [HttpGet]
        [Route("GetAllEmployee")]//Child route declared here.
        //we can write shortcut this way also [HttpGet("GetAllEmployee")] instead of writing [HttpGet] and [Route("GetAllEmployee")] separately.
        public IActionResult GetAllEmployee()
        {//IActionResult is the return Type of api method,it will return the status code along with response message/data to the client.
            //Api Method mainly used to handle the http request and return the http response to the client,so we are using IActionResult as return type of api method.
            //In api method we are using status code to return the response to the client,so we are using StatusCode method to return the status code along with response message/data to the client.

            try
            {
                //Here try block is used to handle the exceptions that may occur during the execution of the code. If any exception occurs, it will be caught in the catch block and we can return a status code with a message to the client.
                var empdata = employeeServices.GetAllEmployee();
                if (empdata.Count == 0)
                {//Inside status code method we are passing the status code and the message to the client. StatusCodes.Status400BadRequest is a predefined status code in ASP.NET Core that represents a bad request. It indicates that the server cannot process the request due to client error, such as invalid input or missing parameters.
                 //different statuscodes we can pass inside statuscode method.
                 //return StatusCode(StatusCodes.Status404NotFound, "bad request");
                 //(or) both are same
                    return NotFound("Data Not exist in the table");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public IActionResult GetEmployeeById([FromRoute] int empid)
        {
            try
            {
                var empdata = employeeServices.GetEmployeeByEmpid(empid);
                if (empdata.empid == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data Not exist in the table");
                    //(Or) Both are same
                    //return NotFound("Data Not exist in the table");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }



        [HttpPost]//insert purpose use this Addemployee
        [Route("AddEmployee")]//routename describe here.
        //Model binder is used to bind the data from the request body to the model class. In this case, we are using [FromBody] attribute to bind the data from the request body to the EmployeeDto class. EmployeeDto is a Data Transfer Object (DTO) that is used to transfer data between layers of the application. It contains properties that represent the employee data that we want to insert into the database.
        public IActionResult Post([FromBody] EmployeeDto empdto)
        {
            try
            {
                //!ModelState.IsValid is used to check whether the model state is valid or not. If the model state is not valid, it will return a status code with a message to the client. If the model state is valid, it will proceed to add the employee data to the database.
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {//oldway:var result=obj.AddEmployee(empdto);
                    var employeeData = employeeServices.AddEmployee(empdto);
                    return StatusCode(StatusCodes.Status201Created, "employee added sucessfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpPut]//update the data purpose we are used.
        [Route("UpdateEmployee")]
        public IActionResult Put([FromBody] EmployeeDto empdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = employeeServices.UpdateEmployee(empdto);
                    return StatusCode(StatusCodes.Status200OK, "employee updated sucessfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpDelete]
        //[Route("DeleteEmployeeByEmpid")]
        public IActionResult delete(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var res = employeeServices.DeleteEmployeeByEmpid(empid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "not found");
                }
                else
                {//if the value is deleted successfully ,we are returnuing the status code 200 with the message content deleted successfully to the client.
                    return StatusCode(StatusCodes.Status200OK, "content deleted successfully");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

    }
}
