using crudapp.Models;
using crudapp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudapp.Controllers
{
    public class DepartmentController : Controller
    {
        readonly DepartmentDAL deptDAL = new DepartmentDAL();
        readonly UserDAL userDAL = new UserDAL();

        public ActionResult Index()
        {
            var data = deptDAL.GetDepts();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department dept)
        {
            if (deptDAL.InsertDept(dept))
            {
                TempData["InsertDeptMsg"] = "<script>alert('Department saved successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["InsertDeptErrorMsg"] = "<script>alert('Department not saved')</script>";
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = deptDAL.GetDepts().Find(a => a.Id == id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Department dept)
        {
            if (deptDAL.UpdateDept(dept))
            {
                TempData["UpdateDeptMsg"] = "<script>alert('Department updated successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["UpdateDeptErrorMsg"] = "<script>alert('Department not updated')</script>";
            }
            return View();
        }

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            var data = userDAL.GetUsersWithDeptName().Where(i=> i.DeptId == id).ToList();
            if (data.Count > 0)
            {
                TempData["DeleteDeptMsg"] = "<script>alert('Department Cannot be deleted, because this department already in use')</script>";
                return RedirectToAction("Index");
            }
            if (deptDAL.DeleteDept(id) > 0)
            {
                TempData["DeleteDeptMsg"] = "<script>alert('User deleted successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["DeleteDeptErrorMsg"] = "<script>alert('User not deleted')</script>";
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var data = deptDAL.GetDepts().Find(a => a.Id == id);
            return View(data);
        }
    }
}