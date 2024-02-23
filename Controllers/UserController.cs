using crudapp.Models;
using crudapp.Service;
using crudapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;

namespace crudapp.Controllers
{
    public class UserController : Controller
    {
        readonly UserDAL userDAL = new UserDAL();
        readonly DepartmentDAL deptDAL = new DepartmentDAL();
        // GET: User

        public ActionResult List(string userName, string email, int? deptId)
        {
            var data = userDAL.GetUsersWithDeptName();

            if (!string.IsNullOrEmpty(userName))
            {
                data = data.Where(u => u.Name.ToLower().Contains(userName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(email))
            {
                data = data.Where(u => u.Email.ToLower().Contains(email.ToLower())).ToList();
            }

            if (deptId.HasValue && deptId > 0)
            {
                data = data.Where(u => u.DeptId == deptId).ToList();
            }

            ViewBag.Departments = new SelectList(deptDAL.GetDepts(), "Id", "DeptName");

            return View(data);
        }


        //public ActionResult List()
        //{
        //    var data = userDAL.GetUsersWithDeptName();
        //    return View(data);
        //}

        /*public ActionResult List()
        {
            var data = userDAL.GetUsers();
            return View(data);
        }*/

        public ActionResult Create()
        {
            var viewModel = new UsersDepartmentViewModel
            {
                Departments = deptDAL.GetDepts()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            if (userDAL.InsertUser(user))
            {
                TempData["InsertMsg"] = "<script>alert('User saved successfully')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["InsertErrorMsg"] = "<script>alert('User not saved')</script>";
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = userDAL.GetUsersWithDeptName().Find(a => a.Id == id);
            data.Departments = deptDAL.GetDepts();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            if (userDAL.UpdateUser(user))
            {
                TempData["UpdateMsg"] = "<script>alert('User updated successfully')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["UpdateErrorMsg"] = "<script>alert('User not updated')</script>";
            }
            return View();
        }

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            if (userDAL.DeleteUser(id) > 0)
            {
                TempData["DeleteMsg"] = "<script>alert('User deleted successfully')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["DeleteErrorMsg"] = "<script>alert('User not deleted')</script>";
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var data = userDAL.GetUsers().Find(a => a.Id == id);
            return View(data);
        }

        public ActionResult Report()
        {
            var data = userDAL.GetUsersWithDeptName();

            // Group the data by Department Name
            var groupedData = data.GroupBy(d => d.DeptName).ToList();

            return View(groupedData);
        }

        public ActionResult GenerateExcel()
    {
        var data = userDAL.GetUsersWithDeptName();
        var groupedData = data.GroupBy(d => d.DeptName).ToList();

        // Create a new Excel package
        using (var excelPackage = new ExcelPackage())
            {
            // Add a new worksheet to the Excel package
            var worksheet = excelPackage.Workbook.Worksheets.Add("DepartmentWiseReport");

            // Add column headers
            worksheet.Cells[1, 1].Value = "Department Name";
            worksheet.Cells[1, 2].Value = "User Name";
            worksheet.Cells[1, 3].Value = "User Email";
            worksheet.Cells[1, 4].Value = "User Age";
            //worksheet.Cells[1, 5].Value = "Total Users";

            // Populate data
            int row = 2;
            foreach (var group in groupedData)
            {
                var departmentName = group.Key;
                //var totalUsers = group.Count();
                foreach (var item in group)
                {
                    worksheet.Cells[row, 1].Value = departmentName;
                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.Email;
                    worksheet.Cells[row, 4].Value = item.Age;
                    /*if (row == 2)
                    {
                        worksheet.Cells[row, 5].Value = totalUsers;
                    }*/
                    row++;
                }
            }

            // Save the Excel package to a MemoryStream
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            stream.Position = 0;

            // Return the Excel file
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentWiseReport.xlsx");
        }
    }



}
}