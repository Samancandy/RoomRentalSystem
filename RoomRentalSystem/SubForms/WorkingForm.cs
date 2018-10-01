using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RoomRentalSystem
{
    public partial class WorkingForm : UserControl
    {
        public WorkingForm()
        {
            InitializeComponent(); 
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql = string.Format("insert into tbWorking values('{0}','{1}','{2}','{3}')",
               txtWorkingID.Text, cboWorkerID.SelectedItem.ToString(), cboPosition.SelectedItem.ToString(), txtSalary.Text);
            if (Form1.ExecuteQuery(sql) == null)
            {
                MessageBox.Show("Adding transaction is failed.");
            }
            else
            {
                MessageBox.Show("Added successfully");
                ClearBox();
            }
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            ClearBox();
        }

        private void ClearBox()
        {
            txtWorkingID.Clear(); cboWorkerID.SelectedIndex = -1;
            cboPosition.SelectedIndex = -1; txtSalary.Clear();
        }
        public void FillWorkerID()
        {
            GetWorkerID(cboWorkerID);
        }
        public static void GetWorkerID(ComboBox cbo)
        {
            string sql = "select * from tbPerson where PersonID like 'WD%'";
            try{
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                IDataReader rdr = cmd.ExecuteReader();
                cbo.Items.Clear();
                while(rdr.Read())
                {
                    cbo.Items.Add(rdr.GetString(rdr.GetOrdinal("PersonID")));
                }
                Form1.con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
