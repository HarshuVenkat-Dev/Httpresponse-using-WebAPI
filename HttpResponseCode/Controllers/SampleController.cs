using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpResponseCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            using (var context = new TestEmployeesContext())
            {
                var result = (from row in context.Employee select row).ToList();
                /*select sd;*/
                return (IEnumerable<Employee>)result;
            }
        }

        [HttpGet("{id}")]
        public Employee GetByid(int id)
        {
            using (var context = new TestEmployeesContext())
            {
                var stuData = context.Employee.Find(id);

                return stuData;
            }
        }

        // POST api/values
        [HttpPost]

        public string Post([FromBody] Employee value)
        {
            using (var context = new TestEmployeesContext())
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        context.Employee.Add(value);
                        context.SaveChanges();
                        return "Employee Detail Added";
                    }
                    else
                    {
                        return "Employee Detail not Added";
                    }
                }
                catch (Exception ex)
                {
                    return "Error";
                }
            }
        }

        [HttpPut("{Id}")]
        // PUT api/values/5
        public string Put([FromBody] Employee value, int Id)
        {
            if (!ModelState.IsValid)
                return "Not a valid model";

            using (var context = new TestEmployeesContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        /*db.Entry(value).State = EntityState.Modified;
                        db.SaveChanges();*/
                        var validstudents = from c in context.Employee
                                            where c.EmpId == Id
                                            select c;
                        foreach (Employee stud in validstudents)
                        {
                            stud.EmpId = value.EmpId;
                            stud.EmpName = value.EmpName;
                            stud.EmpCity = value.EmpCity;
                        }
                        context.SaveChanges();
                    }
                    return "Employee Updated";
                }
                catch
                {
                    return "Employee Updated Error";
                }
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]

        public string Delete(int id)
        {
            using (var context = new TestEmployeesContext())
            {
                try
                {
                    var studentData = context.Employee.Find(id);
                    context.Employee.Remove(studentData);
                    context.SaveChanges();
                    return "Employee Detail Delete";
                }
                catch
                {
                    return "Invalid Model";
                }
            }
        }
    }
}
