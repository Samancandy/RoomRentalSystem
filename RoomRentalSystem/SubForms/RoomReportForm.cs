using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoomRentalSystem.Classes;
using System.Data.SqlClient;

namespace RoomRentalSystem
{
    public partial class RoomReportForm : UserControl
    {
        private List<Room> room = new List<Room>();
        public RoomReportForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (MenuForm.View.Equals(MenuForm.roomAdd)) return;
            MenuForm.View.Visible = false;
            MenuForm.roomAdd.Visible = true;
            MenuForm.View = MenuForm.roomAdd;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (MenuForm.View.Equals(MenuForm.roomUpdate)) return;
            Room room = null;
            if(dgvRoom.Rows.Count != 0 && dgvRoom.Rows != null)
                 room = (Room)dgvRoom.CurrentRow.DataBoundItem;
            MenuForm.View.Visible = false;
            MenuForm.roomUpdate.SetBox(room);
            MenuForm.roomUpdate.Visible = true;
            MenuForm.View = MenuForm.roomUpdate;
        }

        internal void RefreshData()
        {
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from tbRoom";
                IDataReader srd = cmd.ExecuteReader();
                room.Clear();
                while (srd.Read())
                {
                    room.Add(new Room(srd.GetString(srd.GetOrdinal("RoomNo")),
                                          srd.GetString(srd.GetOrdinal("RoomType")),
                                          srd.GetString(srd.GetOrdinal("Size")),
                                          srd.GetDouble(srd.GetOrdinal("Price")),
                                          srd.GetString(srd.GetOrdinal("Status")))
                               );
                }
                Form1.con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = room;
            dgvRoom.DataSource = bs;
            Form1.FitColumn(dgvRoom);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from tbRoom where PersonID = '{0}'", dgvRoom.CurrentRow.Cells[0].Value.ToString());
            Form1.ExecuteQuery(sql);
            this.RefreshData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select * from tbPerson where PersonID like 'cs%' and Name like '%" + txtSearch.Text + "%'";
            DataTable dtb = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(Form1.ExecuteQuery(sql));
            adp.Fill(dtb);
            dgvRoom.DataSource = dtb;
            Form1.FitColumn(dgvRoom);
        }
    }
}
