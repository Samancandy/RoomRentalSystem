using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomRentalSystem.SubForms
{
    public partial class RentalUpdate : UserControl
    {
        public RentalUpdate()
        {
            InitializeComponent();
            btnBack.Click += Back;
        }

        private void Back(object sender, EventArgs e)
        {
            MenuForm.View.Visible = false;
            MenuForm.rentalHistory.Visible = true;
            MenuForm.View = MenuForm.rentalHistory;
            ClearBox();
        }

        internal void FillBox(Rental rental)
        {
            txtID.Text = rental.ID; txtCustomerID.Text = rental.CustomerID;
            txtRoomNo.Text = rental.RoomNo; dtpDate.Value = rental.RentDate;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql = string.Format("update tbRental set PersonID = '{0}', RoomNo = '{1}', RentalDate = '{2}' where RentalID = '{3}')",
               txtCustomerID.Text, txtRoomNo.Text, dtpDate.Text, txtID.Text);
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
            txtID.Clear(); txtCustomerID.Clear(); txtRoomNo.Clear();
        }
    }
}
