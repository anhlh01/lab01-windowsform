using System;
using System.ComponentModel;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessObject;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {
        bool isAdmin = frmLogin.IsAdmin;
        MemberObject memberLogin = frmLogin.Member;

        public frmMemberDetails()
        {
            InitializeComponent();
        }
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public MemberObject MemberInfo { get; set; }

        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MemberInfo = MemberRepository.GetMemberByID(memberLogin.MemberID);
            }
            
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate)
            {
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtMemberName.Text = MemberInfo.MemberName;
                txtEmail.Text = MemberInfo.Email;
                txtPassword.Text = MemberInfo.Password;
                txtCity.Text = MemberInfo.City;
                txtCountry.Text = MemberInfo.Country;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                var member = new MemberObject
                { 
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                if (!InsertOrUpdate)
                {
                    MemberRepository.AddMember(member);
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                }
                
                this.Close();
                //if (isAdmin)
                //{
                //    MessageBox.Show("vui ve nha");
                //    LoadMemberListByNameDescending();
                //}
                //else
                //{
                //    LoadMemberByID(memberLogin.MemberID);
                //}

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message,
                                InsertOrUpdate == false ? "Add a new member"
                                : "Update a member",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);               
            }
            //finally
            //{
            //    txtMemberID.Focus();
            //    txtMemberID.SelectAll();
            //}
        }
        public void ShowErrorMessage(string e)
        {
            MessageBox.Show(e, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
        private void btnCancel_Click(object sender, EventArgs e)
            => Close();
        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "email address is required.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "email address must be valid email address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }
        public bool ValidString(string str, int min, int max , out string errorMessage)
        {
            // Confirm that string must be between nim and max characters
            if (str.Length < min || str.Length > max)
            {
                errorMessage = $"{str} must be between {min} and {max}!";
                return false;
            }

            errorMessage = "";
            return true;
        }
        public bool ValidString(String str, out string errorMessage)
        {
            if (str.Length == 0)
            {
                errorMessage = $"{str} can not be empty!";
                return false;
            }

            errorMessage = "";
            return true;
        }
        private void txtMemberName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidString(txtMemberName.Text, 2 , 50 , out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtMemberName.Select(0, txtMemberName.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorName.SetError(txtMemberName, errorMsg);
            }
        }

        private void txtMemberName_Validated(object sender, EventArgs e)
        {
            errorName.SetError(txtMemberName, "");
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            string errorMsg;
            if (!ValidEmailAddress(txtEmail.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtEmail.Select(0, txtEmail.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorEmail.SetError(txtEmail, errorMsg);
            }
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            errorEmail.SetError(txtEmail, "");
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            errorPassword.SetError(txtPassword, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidString(txtPassword.Text, 2, 10, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtPassword.Select(0, txtPassword.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorPassword.SetError(txtPassword, errorMsg);
            }
        }

        private void txtCity_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidString(txtCity.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtCity.Select(0, txtCity.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorCity.SetError(txtCity, errorMsg);
            }
        }

        private void txtCity_Validated(object sender, EventArgs e)
        {
            errorCity.SetError(txtCity, "");
        }

        private void txtCountry_Validated(object sender, EventArgs e)
        {
            errorCountry.SetError(txtCountry, "");
        }

        private void txtCountry_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidString(txtCountry.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtCountry.Select(0, txtCountry.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorCountry.SetError(txtCountry, errorMsg);
            }
        }

        
    }
}
