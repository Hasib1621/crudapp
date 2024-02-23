using crudapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crudapp.Service
{
    public class DepartmentDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<Department> GetDepts()
        {
            cmd = new SqlCommand("dept_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<Department> list = new List<Department>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Department
                {
                    Id = (int)dr["Id"],
                    DeptName = (string)dr["DeptName"],
                    DeptCode = dr["DeptCode"].ToString(),
                });
            }

            return list;
        }

        public bool InsertDept(Department dept)
        {
            try
            {
                cmd = new SqlCommand("dept_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dName", dept.DeptName);
                cmd.Parameters.AddWithValue("@dCode", dept.DeptCode);
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

        public bool UpdateDept(Department dept)
        {
            try
            {
                cmd = new SqlCommand("dept_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dName", dept.DeptName);
                cmd.Parameters.AddWithValue("@dCode", dept.DeptCode);
                cmd.Parameters.AddWithValue("@id", dept.Id);
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

        public int DeleteDept(int id)
        {
            try
            {
                cmd = new SqlCommand("dept_delete", con);
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