using Microsoft.Data.SqlClient;//all ado.net related predefined classes are present in Microsoft.Data.SqlClient namespace,so we need to import that namespace.
using SynchronousCommunicationProcessInDotNet.Models;
using System.Data;

namespace SynchronousCommunicationProcessInDotNet.Repositories
{
    public class SynchronousEmployeeRepository
    {
        //In repository class we are going to write all the database related  Communicationcode we will write here.
        //all ado.net related predefined classes are present in Microsoft.Data.SqlClient namespace,
        //so we need to import that namespace.
        /*
        1.In Synchronous Communication use Synchronous Methods Only(It menas Without using async, await, Task Keywords in method Preparation Time)
        2.In Asynchronus Communication use Asynchronous Methods Only(it means using Async, await, Task Keywords in Method Preaprtion Time)
        */
        string connectionString = "data source=DESKTOP-S8CP3CH;integrated security=yes;Encrypt=True;TrustServerCertificate=True;initial catalog=hotelmanagement";
        public bool AddEmployee(Employee empdetail)//Her we are passing Employee class object as parameter because we need to insert all the details of employee in database,so we are passing whole object.
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_AddEmployee_New", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                con.Open();//we must open the connection manualay
                cmd.ExecuteNonQuery();
                con.Close();//we must close the connection.
            }
            return true;
        }

        public bool DeleteEmployeeByEmpid(int empid)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("Usp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public List<Employee> GetAllEmployee()
        {
            List<Employee> lstemp = new List<Employee>();
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Usp_GetEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();//will return results of select statement
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.empid = Convert.ToInt32(reader["empid"]);
                        emp.empname = Convert.ToString(reader["empname"]);
                        emp.empsalary = Convert.ToInt32(reader["empsalary"]);
                        lstemp.Add(emp);
                    }
                    con.Close();
                }
                return lstemp;
            }
        }

        public Employee GetEmployeeByEmpid(int empid)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetEmployeeId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.empid = Convert.ToInt32(dr["empid"]);
                    emp.empname = Convert.ToString(dr["empname"]);
                    emp.empsalary = Convert.ToInt32(dr["empsalary"]);
                }
            }
            return emp;
        }

        public bool UpdateEmployee(Employee empdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empdetail.empid);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

    }
}
