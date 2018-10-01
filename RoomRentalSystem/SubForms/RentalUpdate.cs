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
