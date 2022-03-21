using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessObject;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        public static string Email = "";
        public static string Password = "";
        public static bool IsAdmin = false;
        public static MemberObject Member = null;
        IMemberRepository memberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
            /*txtEmail.Text = "admin@fstore.com";
            txtPassword.Text = "admin@@";
            txtEmail.Text = "anh1@gmail.com";
            txtPassword.Text = "123456";*/
            
            
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
        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            emailAddress = emailAddress.Trim().ToLower();
            if (emailAddress.Length == 0)
            {
                errorMessage = "Email address is required.";
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

            errorMessage = "Email address must be valid email address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorEmail.SetError(txtEmail, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidPassword(txtPassword.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtPassword.Select(0, txtPassword.Text.Length);

                // Set the ErrorProvider error with the text to display.
                this.errorPassword.SetError(txtPassword, errorMsg);
            }
        }
        public bool ValidPassword(string password, out string errorMessage)
        {
            // Confirm that the password must be between 2 and 10 character.
            password = password.Trim();
            if (password.Length < 2 || password.Length > 10)
            {
                errorMessage = "Password must be between 2 and 10 character.";
                return false;
            }
            errorMessage = "";
            return true;
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorPassword.SetError(txtPassword, "");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Email = txtEmail.Text;
            Password = txtPassword.Text;
            MemberObject admin = memberRepository.LoadJson();
            if (Email.Equals(admin.Email) && Password.Equals(admin.Password))
            {
                IsAdmin = true;
                frmMemberManagement frmMemberManagement = new frmMemberManagement();
                frmMemberManagement.ShowDialog();
                this.Close();
            }
            MemberObject member = memberRepository.LoginMember(txtEmail.Text, txtPassword.Text);
            if (member == null)
            {
                MessageBox.Show("User not found!", "Login Page - Not Found"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Member = member;
                frmMemberManagement frmMemberManagement = new frmMemberManagement();
                frmMemberManagement.ShowDialog();
                this.Close();
            }
        }
    }
}
