using crudapp.Models;
using crudapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crudapp.Service
{
    public class UserDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        /*public List<UsersDepartmentViewModel> GetUsersWithDeptName()
        {
            cmd = new SqlCommand("sp_select_with_dept_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<UsersDepartmentViewModel> list = new List<UsersDepartmentViewModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new UsersDepartmentViewModel
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Email = dr["Email"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    DeptId = (int)dr["DeptId"],
                    DeptName = (string)dr["DeptName"],
                    Departments = new List<Department> { new Department { Id = (int)dr["DeptId"], DeptName = (string)dr["DeptName"] } }
                });
            }

            return list;
        }*/

        public List<UsersDepartmentViewModel> GetUsersWithDeptName()
        {
            cmd = new SqlCommand("SELECT u.*, d.DeptName FROM Users u JOIN Department d ON u.DeptId = d.Id", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<UsersDepartmentViewModel> list = new List<UsersDepartmentViewModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new UsersDepartmentViewModel
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Email = dr["Email"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    DeptId = (int)dr["DeptId"],
                    DeptName = (string)dr["DeptName"],
                    Departments = new List<Department> { new Department { Id = (int)dr["DeptId"], DeptName = (string)dr["DeptName"] } }
                });
            }

            return list;
        }

        public List<UserModel> GetUsers()
        {
            cmd = new SqlCommand("sp_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<UserModel> list = new List<UserModel>();
            foreach(DataRow dr in dt.Rows)
            {
                list.Add(new UserModel
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Email = dr["Email"].ToString(),
                    Age = Convert.ToInt32(dr["Age"])
                }) ;
            }

            return list;
        }

        public bool InsertUser(UserModel user)
        {
            try
            {
                cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@deptId", user.DeptId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUser(UserModel user)
        {
            try
            {
                cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@deptId", user.DeptId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteUser(int id)
        {
            try
            {
                cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}