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
    public partial class VehicleInputForm : UserControl
    {
        public Vehicle vehicle = null;
        public VehicleInputForm()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            if (vehicle != null)
            {
                txtVehicleId.Text = vehicle.ID;
                txtVehicleModel.Text = vehicle.Model;
                txtColor.Text = vehicle.Color;
                //cboVehicleType.SelectedItem = vehicle.Type;
                //txtVehiclePlate.Text = vehicle.Plate;
            }
            else btnVechicleUpdate.Text = "Add new";
        }

        public void ChangeBtn(string txt)
        {
            btnVechicleUpdate.Text = txt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MenuForm.View.Visible = false;
            MenuForm.vehicleView.Visible = true;
            MenuForm.View = MenuForm.vehicleView;
            this.ClearBox();
            MenuForm.vehicleView.RefreshData();
        }

        private void btnVechicleCancle_Click(object sender, EventArgs e)
        {
            ClearBox();
        }

        private void ClearBox()
        {
            txtVehicleId.Clear(); cboVehicleType.SelectedIndex = -1; txtVehicleModel.Clear();
            txtColor.Clear(); txtPower.Clear(); txtVehiclePlate.Clear();
        }
        private void btnVechicleUpdate_Click(object sender, EventArgs e)
        {
            AddingField();
        }
        private void AddingField()
        {
            string sql = "";
            if (this.vehicle == null)
            {
                sql = string.Format("insert into tbVehicle values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                txtVehicleId.Text, cboVehicleType.SelectedItem.ToString(), txtVehicleModel.Text, txtColor.Text, txtPower.Text, txtVehiclePlate.Text);
            }
            else
            {
                 sql = string.Format("update tbVehicle set Ve_Type = '{0}', Model = '{1}', Color = '{2}', Power = '{3}', Plate = '{4}' where Ve_ID = '{5}'",
                cboVehicleType.SelectedItem.ToString(), txtVehicleModel.Text, txtColor.Text, txtPower.Text, txtVehiclePlate.Text, txtVehicleId.Text);
            }
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                Form1.con.Close();
                MessageBox.Show("Successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Form1.con.Close();
                MessageBox.Show("Failed.");
            }
        }
        public void SetBox(Vehicle vehicle)
        {
            if (vehicle == null) return; 
            txtVehicleId.Text = vehicle.ID; cboVehicleType.SelectedItem = vehicle.VehicleType;
            txtVehicleModel.Text = vehicle.Model; txtColor.Text = vehicle.Color;
            txtPower.Text = vehicle.Power; txtVehiclePlate.Text = vehicle.Plate;
        }
    }
}
