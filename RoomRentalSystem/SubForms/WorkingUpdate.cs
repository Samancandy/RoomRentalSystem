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

namespace RoomRentalSystem.SubForms
{
    public partial class WorkingUpdate : UserControl
    {
        public WorkingUpdate()
        {
            InitializeComponent();
        }

        internal void FillBox(Classes.Working working)
        {
            if (working == null) return;
            txtWorkingID.Text = working.WorkingID; cboWorkerID.SelectedItem = working.WorkerID;
            cboPosition.SelectedItem = working.Position; txtSalary.Text = working.Salary.ToString();
        }
        private void ClearBox()
        {
            txtWorkingID.Clear(); cboWorkerID.SelectedIndex = -1;
            cboPosition.SelectedIndex = -1; txtSalary.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MenuForm.View.Visible = false;
            MenuForm.workingHistory.Visible = true;
            MenuForm.View = MenuForm.workingHistory; 
            MenuForm.workingHistory.RefreshData("tbWorking");
            ClearBox();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            ClearBox();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql = string.Format("update tbWorking set PersonID = '{0}', Position = '{1}', Salary = '{2}' where WorkingID = '{3}'",
               cboWorkerID.SelectedItem.ToString(), cboPosition.SelectedItem.ToString(), txtSalary.Text, txtWorkingID.Text);
            ExQuery(sql);
        }
        private void ExQuery(string sql)
        {
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Form1.con.Close();
                MessageBox.Show("Updated successfully!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Failed to update!!!");
                Form1.con.Close();
            }
        }
        public void FillWorkerID()
        {
            WorkingForm.GetWorkerID(cboWorkerID);
        }
    }
}
