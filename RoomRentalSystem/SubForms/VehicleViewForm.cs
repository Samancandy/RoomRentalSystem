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
    public partial class VehicleViewForm : UserControl
    {
        private List<Vehicle> vehicle = new List<Vehicle>();
        public VehicleViewForm()
        {
            InitializeComponent();
        }

        private void btnVehicleNew_Click(object sender, EventArgs e)
        {
            OpenForm("Add new");
        }

        private void btnVehicleUpdate_Click(object sender, EventArgs e)
        {
            OpenForm("Update");
        }
        void OpenForm(string txt)
        {
            MenuForm.vehicleInput.ChangeBtn(txt);
            Vehicle vehicle = null;
            if (dgvVehicle.Rows.Count != 0 && dgvVehicle.Rows != null)
                vehicle = (Vehicle)dgvVehicle.CurrentRow.DataBoundItem;
            MenuForm.View.Visible = false; 
            MenuForm.vehicleInput.SetBox(vehicle);
            MenuForm.vehicleInput.Visible = true;
            MenuForm.View = MenuForm.vehicleInput;
        }

        internal void RefreshData()
        {
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from tbVehicle";
                IDataReader srd = cmd.ExecuteReader();
                vehicle.Clear();
                while (srd.Read())
                {
                    vehicle.Add(new Vehicle(srd.GetString(srd.GetOrdinal("Ve_ID")),
                                          srd.GetString(srd.GetOrdinal("Ve_Type")),
                                          srd.GetString(srd.GetOrdinal("Model")),
                                          srd.GetString(srd.GetOrdinal("Color")),
                                          srd.GetString(srd.GetOrdinal("Power")),
                                          srd.GetString(srd.GetOrdinal("Plate")))   
                               );
                }
                Form1.con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = vehicle;
            dgvVehicle.DataSource = bs;
            Form1.FitColumn(dgvVehicle);
        }
    }
}
