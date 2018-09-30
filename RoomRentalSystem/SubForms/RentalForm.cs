using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoomRentalSystem.SubForms;

namespace RoomRentalSystem
{
    public partial class RentalForm : UserControl
    {
        public RentalForm()
        {
            InitializeComponent();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            ClearBox();
        }
        private void ClearBox()
        {
            txtID.Clear(); txtCustomerID.Clear(); txtRoomNo.Clear();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql = string.Format("insert into tbRental values('{0}','{1}','{2}','{3}')",
               txtID.Text, txtCustomerID.Text, txtRoomNo.Text, dtpDate.Text);
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
    }
}
