using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalLibrary
{
    public class EmpOrm
    {
        public int Eid { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public int DeptID { get; set; }
    }

    public class CDal             // utility class
    {

        SqlConnection conn;
        SqlCommand cmd;

        private readonly string connstr;

        public CDal()
        {
            conn = new SqlConnection(connstr);
            connstr = @"data source=.\sqlexpress;initial catalog=Employee;Integrated Security=true";
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public IEnumerable<EmpOrm> GetEmployees()
        {
            List<EmpOrm> list = new List<EmpOrm>();
            cmd.CommandText = "select * from employee";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new EmpOrm { Eid = reader.GetInt32(0), EmpName = reader.GetString(1), DeptID = reader.GetInt32(2) });
            }
            reader.Close();
            conn.Close();
            return list;
        }

        public EmpOrm GetEmpById(int id)
        {
            List<EmpOrm> list = new List<EmpOrm>();
            cmd.CommandText = $"select * from employee where Eid={id}";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                list.Add(new EmpOrm { Eid = reader.GetInt32(0), EmpName = reader.GetString(1), DeptID = reader.GetInt32(2) });
            }
            reader.Close();
            conn.Close();
            return list.Count == 0 ? null : list[0];
        }

        public bool AddEmp(EmpOrm emp)
        {
            cmd.CommandText = $"insert into  employee values({emp.Eid},'{emp.EmpName}',{emp.DeptID})";
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected > 0;
        }

        public bool ModifyEmp(EmpOrm emp)
        {
            cmd.CommandText = $"update employee set EName='{emp.EmpName}',Dept={emp.DeptID} where Eid={emp.Eid}";
            System.Console.WriteLine(cmd.CommandText);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected > 0;
        }

        public bool DeleteEmp(int id)
        {
            cmd.CommandText = $"delete employee where Eid={id}";
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected > 0;
        }
    }
}
