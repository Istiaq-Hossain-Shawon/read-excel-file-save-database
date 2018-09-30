using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadEmployee.DAL;

namespace UploadEmployee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            button1.Font = new Font("Arial", 10, FontStyle.Bold,GraphicsUnit.Point);
            button2.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
            button3.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt); //read excel file  
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = dtExcel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            } 
        }
        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); //to close the window(Form1)  
        }
        public string GetCode()
        {
            //using (var db = new AssetManagementDBEntities())
            //{
            //    string prefix = "EMP";
            //    string a = "000001";
            //    string invNo = prefix + a;
            //    var inv = (from p in db.Employees
            //               where p.Code.Contains(prefix)
            //               orderby p.Id descending
            //               select p.Code).FirstOrDefault();
            //    if (inv != null)
            //    {
            //        a = inv.Substring(prefix.Length);
            //        invNo = prefix + (Convert.ToDecimal(a) + 1000001).ToString().Substring(1);
            //    }
            //    return invNo;
            //}
            string prefix = "EMP";
            string a = "000001";
            string invNo = prefix + a;
            string sql = "select code from Employee where Code like '%EMP%' order by Code desc";
            DataTable dt = DataProvider.GetData(sql);
            string code = dt.Rows[0]["code"].ToString();

            if (dt.Rows.Count>0)
            {
                a = code.Substring(prefix.Length);
                invNo = prefix + (Convert.ToDecimal(a) + 000001).ToString("000000");
            }

            return invNo;
        }
        public bool InsertIntoDepertment(string Name)
        {
            string Code = GetDeptCode();
            string sql = @"INSERT INTO [dbo].[Department]
           ([Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate])
     VALUES   ('" + Name + "','" + Code + "','Admin','" + DateTime.Now + "','Admin','" + DateTime.Now + "')";
            return DataProvider.ExecuteNonQuery(sql);
        }
        public string GetDeptCodeByName(string Name)
        {
            string sql = "select Code from Department where Name='" + Name + "' or Code='" + Name + "'  ";
            DataTable dt = DataProvider.GetData(sql);
            if (dt.Rows.Count > 0)
            {
                string code = dt.Rows[0]["Code"].ToString();
                return code;
            }
            else
            {
                if (InsertIntoDepertment(Name))
                {
                    return GetDeptCodeByName(Name);
                } 
            }
           
            return "";
        }
        public string GetDeptCode()
        {
            string prefix = "D";
            string a = "0001";
            string invNo = prefix + a;
            string sql = "select code from Department where Code like '%D%' order by Code desc";
            DataTable dt = DataProvider.GetData(sql);
            string code = dt.Rows[0]["code"].ToString();

            if (dt.Rows.Count > 0)
            {
                a = code.Substring(prefix.Length);
                invNo = prefix + (Convert.ToDecimal(a) + 0001).ToString("0000");
            }

            return invNo;
        }
        public bool InsertIntoDesignation(string Name)
        {
            string Code = GetDesignationCode();
            string sql = @"INSERT INTO [dbo].[Designation]
           ([Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate])
     VALUES   ('" + Name + "','" + Code + "','Admin','" + DateTime.Now + "','Admin','" + DateTime.Now + "')";
            return DataProvider.ExecuteNonQuery(sql);
        }
        public string GetDesignationCodeByName(string Name)
        {
            string sql = "select Code from Designation where Name='" + Name + "' or   Code='" + Name + "'";
            DataTable dt = DataProvider.GetData(sql);
            if (dt.Rows.Count > 0)
            {
                string code = dt.Rows[0]["Code"].ToString();
                return code;
            }
            else
            {
                if (InsertIntoDesignation(Name))
                {
                    return GetDesignationCodeByName(Name);
                }
            }

            return "";
        }
        public string GetDesignationCode()
        {
            string prefix = "DG";
            string a = "0001";
            string invNo = prefix + a;
            string sql = "select code from Designation where Code like '%DG%' order by Code desc";
            DataTable dt = DataProvider.GetData(sql);
            string code = dt.Rows[0]["code"].ToString();

            if (dt.Rows.Count > 0)
            {
                a = code.Substring(prefix.Length);
                invNo = prefix + (Convert.ToDecimal(a) + 0001).ToString("0000");
            }

            return invNo;
        }





        public bool InsertIntoLocation(string Name)
        {
            string Code = GetLocationCode();
            string sql = @"INSERT INTO [dbo].[LocationSetup]
           ([Name]
           ,[Code]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate],[ContactPerson],[Mobile])
     VALUES   ('" + Name + "','" + Code + "','Admin','" + DateTime.Now + "','Admin','" + DateTime.Now + "','NAN','NAN')";
            return DataProvider.ExecuteNonQuery(sql);
        }
        public string GetLocationCodeByName(string Name)
        {
            string sql = "select Code from LocationSetup where Name='" + Name + "' or Code='" + Name + "'";
            DataTable dt = DataProvider.GetData(sql);
            if (dt.Rows.Count > 0)
            {
                string code = dt.Rows[0]["Code"].ToString();
                return code;
            }
            else
            {
                if (InsertIntoLocation(Name))
                {
                    return GetLocationCodeByName(Name);
                }
            }

            return "";
        }
        public string GetLocationCode()
        {
            string prefix = "L";
            string a = "0001";
            string invNo = prefix + a;
            string sql = "select code from LocationSetup where Code like '%L%' order by Code desc";
            DataTable dt = DataProvider.GetData(sql);
            string code = dt.Rows[0]["code"].ToString();

            if (dt.Rows.Count > 0)
            {
                a = code.Substring(prefix.Length);
                invNo = prefix + (Convert.ToDecimal(a) + 0001).ToString("0000");
            }
            return invNo;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool outputResult = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index == 0)
                {
                    continue;
                }
                StringBuilder builder = new StringBuilder();

                string name = row.Cells[0].Value==null ? "": row.Cells[0].Value.ToString();
                if (name == "")
                {
                    continue;
                }

                string Code = GetCode();

                //string CodeTempName = row.Cells[1].Value==null ? "": row.Cells[1].Value.ToString();
                //string Code = GetDeptCodeByName(CodeTempName);

                string E_ID = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();


                //string Dept = row.Cells[3].Value == null ? "" : row.Cells[3].Value.ToString();
                string DeptTempName = row.Cells[3].Value == null ? "" : row.Cells[3].Value.ToString().Trim();
                string Dept = GetDeptCodeByName(DeptTempName);




                //string Designation = row.Cells[4].Value == null ? "" : row.Cells[4].Value.ToString();

                string DesignationTempName = row.Cells[4].Value == null ? "" : row.Cells[4].Value.ToString().Trim();
                string Designation = GetDesignationCodeByName(DesignationTempName);



                string ContactNo = row.Cells[5].Value == null ? "" : row.Cells[5].Value.ToString();
                string SecondContactNo = row.Cells[6].Value == null ? "" : row.Cells[6].Value.ToString();
                string Email = row.Cells[7].Value == null ? "" : row.Cells[7].Value.ToString();
                string Address = row.Cells[8].Value == null ? "" : row.Cells[8].Value.ToString();
                string DateOFJoining = (row.Cells[9].Value == null || row.Cells[9].Value.ToString() == "NULL") ? "1/1/1900" : row.Cells[9].Value.ToString();

                string CreatedBy = row.Cells[10].Value == null ? "" : row.Cells[10].Value.ToString();

                string CreatedDate = (row.Cells[11].Value == null || row.Cells[11].Value.ToString() == "NULL") ? "1/1/1900" : row.Cells[11].Value.ToString();

                string UpdatedBy = row.Cells[12].Value == null ? "" : row.Cells[12].Value.ToString();
                string UpdatedDate = (row.Cells[13].Value == null || row.Cells[13].Value.ToString() == "NULL") ? "1/1/1900" : row.Cells[13].Value.ToString();
                
                //string Location = row.Cells[13].Value == null ? "" : row.Cells[13].Value.ToString();

                string LocationTempName = row.Cells[14].Value == null ? "" : row.Cells[14].Value.ToString().Trim();
                string Location = GetLocationCodeByName(LocationTempName);

               
                builder.Append(@"INSERT INTO [dbo].[Employee] 
           ([Name]
           ,[Code]
           ,[E_ID]
           ,[Dept]
           ,[Designation]
           ,[ContactNo]
           ,[SecondContactNo]
           ,[Email]
           ,[Address]
           ,[DateOfJoning]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[Location])
     VALUES
           ('" + name + "','" + Code + "','" + E_ID + "','" + Dept + "','" + Designation + "','" + ContactNo + "','" + SecondContactNo + "','" + Email + "','" + Address + "', '" + DateOFJoining + "','" + CreatedBy + "','" + DateTime.Now + "' ,'"
              + UpdatedBy + "','" + DateTime.Now + "','" + Location + "') ").Append(" ");
                bool result = DataProvider.ExecuteNonQuery(builder.ToString());
                if (result)
                {
                    outputResult = true;
                }
            }
            if (outputResult)
            {
                MessageBox.Show("Inserted Successfully.....", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor.Current = Cursors.Default;

            }
            else
            {
                MessageBox.Show("Fail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Cursor.Current = Cursors.Default;
                
            }
        }
    }
}
