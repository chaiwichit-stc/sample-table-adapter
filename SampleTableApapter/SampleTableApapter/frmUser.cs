using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleTableApapter
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGetNormal_Click(object sender, EventArgs e)
        {
            usersTableAdapter1.Fill(sampleTableAdapterDataSet1.users);
        }

        private void btnGetStored_Click(object sender, EventArgs e)
        {
            usersTableAdapter1.SpFill(sampleTableAdapterDataSet1.users);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUserDetail frm = new frmUserDetail();
            frm.ShowDialog();

            usersTableAdapter1.Fill(sampleTableAdapterDataSet1.users);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return; 
            }

            string selectedUserId = dataGridView1.SelectedRows[0].Cells["useridDataGridViewTextBoxColumn"].Value.ToString();

            SampleTableAdapterDataSet.usersRow selectedUser = sampleTableAdapterDataSet1.users.Where(x => x.user_id == selectedUserId).SingleOrDefault();

            frmUserDetail frm = new frmUserDetail(SharedCommon.ActionType.Edit, selectedUser);
            frm.ShowDialog();

            usersTableAdapter1.Fill(sampleTableAdapterDataSet1.users);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            sampleTableAdapterDataSet1.users.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            string selectedUserId = dataGridView1.SelectedRows[0].Cells["useridDataGridViewTextBoxColumn"].Value.ToString();

            SampleTableAdapterDataSet.usersRow selectedUser = sampleTableAdapterDataSet1.users.Where(x => x.user_id == selectedUserId).SingleOrDefault();

            frmUserDetail frm = new frmUserDetail(SharedCommon.ActionType.Delete, selectedUser);
            frm.ShowDialog();

            usersTableAdapter1.Fill(sampleTableAdapterDataSet1.users);
        }
    }
}
