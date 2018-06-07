using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeWebserviceREST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public bool AddEmploee(Employee eml)
        {
            try
            {
                data.Employees.InsertOnSubmit(eml);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DeleteEmploee(int idE)
        {
            try
            {
                Employee employeeToDelete =
                     (from employee in data.Employees where employee.empID == idE select employee).Single();
                data.Employees.DeleteOnSubmit(employeeToDelete);
                data.SubmitChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetProductList()
        {
            try
            {

                return (from employee in data.Employees select employee).ToList();
            }
            catch
            {

                return null;
            }
        }

        public bool UpdateEmploee(Employee eml)
        {
            Employee employeeToModify =
                 (from employee in data.Employees where employee.empID == eml.empID select employee).Single();

            employeeToModify.Age = eml.Age;
            employeeToModify.address = eml.address;
            employeeToModify.firstName = eml.firstName;
            employeeToModify.lastName = eml.lastName;
            data.SubmitChanges();
            return true;
        }
    }
}
