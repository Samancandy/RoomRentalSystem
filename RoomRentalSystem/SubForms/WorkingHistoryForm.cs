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
    public partial class WorkingHistoryForm : UserControl
    {
        public WorkingHistoryForm()
        {
            InitializeComponent();
            cboWH.SelectedIndexChanged += cboWH_SelectedIndexChanged;
        }

        private void cboWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWH.SelectedIndex == 0) RefreshData("tbWorking");
            else if (cboWH.SelectedIndex == 1) RefreshData("tbWorkingHistory");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select WorkingID, twg.PersonID, per.Name, Position, Salary from tbWorking twg inner join tbPerson per on twg.PersonID = per.PersonID where per.Name like '%" + txtSearch.Text + "%'";
            DataTable dtb = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(Form1.ExecuteQuery(sql));
            adp.Fill(dtb);
            dgvWorkingHistory.DataSource = dtb;
            Form1.FitColumn(dgvWorkingHistory);
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            Working working = null;
            if (dgvWorkingHistory.Rows.Count != 0 && dgvWorkingHistory.Rows != null)
                working = (Working)dgvWorkingHistory.CurrentRow.DataBoundItem;
            MenuForm.workingUpdate.FillWorkerID();
            MenuForm.workingUpdate.FillBox(working); 
            MenuForm.View.Visible = false;
            MenuForm.workingUpdate.Visible = true;
            MenuForm.View = MenuForm.workingUpdate;
        }
        private List<Working> working = new List<Working>();
        private List<WorkingHistory> workingHistory = new List<WorkingHistory>();
        internal void RefreshData(string table)
        {
            BindingSource bs = new BindingSource();
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from " + table;
                IDataReader srd = cmd.ExecuteReader(); 
                if (table.Equals("tbWorking"))
                {
                    working.Clear();
                    while (srd.Read())
                    {
                        {
                            working.Add(new Working(srd.GetString(srd.GetOrdinal("WorkingID")),
                                                  srd.GetString(srd.GetOrdinal("PersonID")),
                                                  srd.GetString(srd.GetOrdinal("Position")),
                                                  srd.GetInt32(srd.GetOrdinal("Salary")))
                                       );
                        }
                    }
                    bs.DataSource = working;
                }
                else if (table.Equals("tbWorkingHistory"))
                {
                    workingHistory.Clear();
                    while (srd.Read())
                    {
                        workingHistory.Add(new WorkingHistory(srd.GetString(srd.GetOrdinal("WHID")),
                                              srd.GetString(srd.GetOrdinal("PersonID")),
                                              srd.GetString(srd.GetOrdinal("WorkingID")),
                                              srd.GetDateTime(srd.GetOrdinal("StartDate")),
                                              srd.GetDateTime(srd.GetOrdinal("EndDate")))
                                   );
                    }
                    bs.DataSource = workingHistory;
                }
                srd.Close();
                Form1.con.Close();
            }
            catch (Exception e)
            {
                Form1.con.Close();
                MessageBox.Show(e.ToString());
            }
            dgvWorkingHistory.DataSource = bs;
            Form1.FitColumn(dgvWorkingHistory);
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            WorkerLeave();
        }
        private string WHID = "WH00001";
        private DateTime EndDate = DateTime.Now;
        private void GetIDnEndDate(ref string whid, ref DateTime enddate)
        {
            try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string sql = "select top 1 WHID from tbWorkingHistory order by WHID desc";
                cmd.CommandText = sql;
                IDataReader rd = cmd.ExecuteReader();
                  try
                    {
                        if (rd.Read())
                        {
                            //WHID = (int.Parse(rd.GetString(rd.GetOrdinal("WHID")).Substring(2, 7)) + 1).ToString();
                            //MessageBox.Show((int.Parse(rd.GetString(rd.GetOrdinal("WHID")).Substring(2, 7)) + 1).ToString());
                            WHID = "WH00002";
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error here" + e.ToString());
                    }
                Form1.con.Close();
            }
            catch (Exception e)
            {
                Form1.con.Close();
                MessageBox.Show("Error in getid enddate\n" + e.ToString());
            }
        }
        private void WorkerLeave()
        {   
            try
            {   
                Form1.con.Open();
                string WKID = "";
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                GetIDnEndDate(ref WHID, ref EndDate);
                cmd.CommandText = "select * from tbWorking where WorkingID = '" + dgvWorkingHistory.CurrentRow.Cells[0].Value.ToString() + "'";
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                { 
                    WKID = reader.GetString(reader.GetOrdinal("WorkingID"));
                    string sql = string.Format("insert into tbWorkingHistory values('{0}','{1}','{2}','{3}','{4}')",
                        WHID,
                        reader.GetString(reader.GetOrdinal("PersonID")),
                        WKID,  
                        reader.GetDateTime(reader.GetOrdinal("StartDate")),
                        EndDate);
                    reader.Close();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = "delete from tbWorking where WorkingID = '"+WKID+"'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                Form1.con.Close();
            }
            catch (Exception e)
            {
                Form1.con.Close();
                MessageBox.Show("Hello"+e.ToString()); 
            }
            this.RefreshData("tbWorking");
        }
        private void btnCustomerNew_Click(object sender, EventArgs e)
        {
            MenuForm.View.Visible = false;
            MenuForm.working.Visible = true;
            MenuForm.View = MenuForm.working;
        }
    }
}
