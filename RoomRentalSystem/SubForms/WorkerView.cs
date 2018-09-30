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
    public partial class WorkerView : UserControl
    {
        public WorkerView()
        {
            InitializeComponent();
            //int x = dgvCustomer.ClientSize.Width;
            //for (int i = 0; i < 5; i++)
            //    dgvCustomer.Rows[i].Height = x / 5;
        }

        private void btnCustomerNew_Click(object sender, EventArgs e)
        {
            OpenWorkerF("Add new", null);
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            Person person = null;
            if(dgvCustomer.Rows.Count != 0 && dgvCustomer.Rows != null)
                person = (Person) dgvCustomer.CurrentRow.DataBoundItem;
            OpenWorkerF("Update", person);
        }
        void OpenWorkerF(string txt, Person w)
        {
            MenuForm.worker.SetWorker(txt, w);
            MenuForm.View.Visible = false;
            MenuForm.worker.Visible = true;
            MenuForm.View = MenuForm.worker;
        }
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from tbPerson where PersonID = '{0}'", dgvCustomer.CurrentRow.Cells[0].Value.ToString());
            Form1.ExecuteQuery(sql);
            this.RefreshData();
        }
        private List<Person> person = new List<Person>();
        internal void RefreshData(){
        try
            {
                Form1.con.Open();
                SqlCommand cmd = Form1.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from tbPerson where PersonID like 'wk%'";
                IDataReader srd = cmd.ExecuteReader();
                person.Clear();
                while (srd.Read())
                { 
                    person.Add(new Person(srd.GetString(srd.GetOrdinal("PersonID")),
                                          srd.GetString(srd.GetOrdinal("Name")),
                                          srd.GetString(srd.GetOrdinal("Gender")),
                                          srd.GetDateTime(srd.GetOrdinal("DOB")),
                                          srd.GetString(srd.GetOrdinal("Phone")),
                                          srd.GetString(srd.GetOrdinal("Addr")))
                               );
                }
                Form1.con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = person;
            dgvCustomer.DataSource = bs;
            Form1.FitColumn(dgvCustomer);
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string sql = "select * from tbPerson where PersonID like 'wk%' and Name like '%" + txtCustomerSearch.Text + "%'";
            DataTable dtb = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(Form1.ExecuteQuery(sql));
            adp.Fill(dtb);
            dgvCustomer.DataSource = dtb;
            Form1.FitColumn(dgvCustomer);
        }
    }
}
