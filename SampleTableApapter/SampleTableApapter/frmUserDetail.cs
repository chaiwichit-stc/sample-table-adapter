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
    public partial class frmUserDetail : Form
    {

        SharedCommon.ActionType _actionType;
        SampleTableAdapterDataSet.usersRow _selectedUser;

        public frmUserDetail()
        {
            InitializeComponent();
        }

        public void SetEnableEditMode()
        {
            txtUserId.Enabled = false;
        }

        public void SetEnableDeleteMode()
        {
            txtUserId.Enabled = false;
            txtPassword.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            chkIsActive.Enabled = false;
        }

        public void DisplayMode()
        {
            lblAction.Text = $"{_actionType.ToString()}";
            btnSaveNormal.Text = $"{_actionType.ToString()} Normal";
            btnSaveStored.Text = $"{_actionType.ToString()} Stored";
        }

        public void DisplayInfo()
        {
            txtUserId.Text = _selectedUser.user_id;
            txtPassword.Text = _selectedUser.password;
            txtFirstName.Text = _selectedUser.first_name;
            txtLastName.Text = _selectedUser.last_name;
            chkIsActive.Checked = _selectedUser.is_active;
        }



        public frmUserDetail(SharedCommon.ActionType actionType, SampleTableAdapterDataSet.usersRow selectedUser)
        {
            InitializeComponent();
            this._actionType = actionType;
            this._selectedUser = selectedUser;
        }

        private void frmUserDetail_Load(object sender, EventArgs e)
        {

            DisplayMode();

            switch (_actionType)
            {
                case SharedCommon.ActionType.Add:
                    break;
                case SharedCommon.ActionType.Edit:
                    SetEnableEditMode();
                    DisplayInfo();
                    break;
                case SharedCommon.ActionType.Delete:
                    SetEnableDeleteMode();
                    DisplayInfo();
                    break;
                default:
                    break;
            }
        }

        private void btnSaveNormal_Click(object sender, EventArgs e)
        {
            switch (_actionType)
            {
                case SharedCommon.ActionType.Add:

                    int? userIdCount = usersTableAdapter1.CountByUserId(txtUserId.Text);

                    if (userIdCount > 0)
                    {
                        MessageBox.Show($"'{txtUserId.Text}' already be used.");
                        return;
                    }

                    usersTableAdapter1.Insert(txtUserId.Text
                                                ,txtPassword.Text
                                                ,txtFirstName.Text
                                                ,txtLastName.Text 
                                                ,chkIsActive.Checked);

                    break;
                case SharedCommon.ActionType.Edit:
                    usersTableAdapter1.Update(_selectedUser.user_id
                                                ,txtPassword.Text
                                                ,txtFirstName.Text
                                                ,txtLastName.Text
                                                ,chkIsActive.Checked);
                    break;
                case SharedCommon.ActionType.Delete:
                    usersTableAdapter1.Delete(_selectedUser.user_id);
                    break;
                default:
                    break;
            }

            this.Close();
        }

        private void btnSaveStored_Click(object sender, EventArgs e)
        {
            switch (_actionType)
            {
                case SharedCommon.ActionType.Add:
                    int? userIdCount = (int?)usersTableAdapter1.SpCountByUserId(txtUserId.Text);

                    if (userIdCount > 0)
                    {
                        MessageBox.Show($"'{txtUserId.Text}' already be used.");
                        return;
                    }

                    usersTableAdapter1.SpInsert(txtUserId.Text
                                                , txtPassword.Text
                                                , txtFirstName.Text
                                                , txtLastName.Text
                                                , chkIsActive.Checked);
                    break;
                case SharedCommon.ActionType.Edit:
                    usersTableAdapter1.SpUpdate(_selectedUser.user_id
                                                , txtPassword.Text
                                                , txtFirstName.Text
                                                , txtLastName.Text
                                                , chkIsActive.Checked);
                    break;
                case SharedCommon.ActionType.Delete:
                    usersTableAdapter1.SpDelete(_selectedUser.user_id);
                    break;
                default:
                    break;
            }

            this.Close();
        }

    }
}
