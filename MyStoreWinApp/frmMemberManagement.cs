using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        
        bool IsAdmin = frmLogin.IsAdmin;
        IMemberRepository memberRepository = new MemberRepository();
        MemberObject memberLogin = frmLogin.Member;
        //Create a data source
        BindingSource source;
        public frmMemberManagement()
        {
            InitializeComponent();
        }
        

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            if (IsAdmin)
            {
                btnDelete.Enabled = true;
                LoadMemberListByNameDescending();
            }
            else
            {
                btnAdd.Enabled = false;
                dgvData.Enabled = false;
                txtSearch.Enabled = false;
                cbCity.Enabled = false;
                cbCountry.Enabled = false;
                nID.Enabled = false;
                btnLoad.Enabled = false;

                LoadMemberByID(memberLogin.MemberID);

            }
        }
        private void LoadReset()
        {
            cbCity.Items.Clear();
            cbCountry.Items.Clear();
            var citys = memberRepository.GetListCity();
            var countrys = memberRepository.GetListCountry();
            cbCity.Items.Add("All");
            cbCountry.Items.Add("All");
            foreach (string c in citys)
            {
                //if (c != null)
                    cbCity.Items.Add(c);
                
                
            }
            foreach (string c in countrys)
            {
                //if (c != null)
                    cbCountry.Items.Add(c);
                
                
            }

            cbCity.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
        }
        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get member");
            }
            return member;
        }
        public void LoadMemberListByNameDescending()
        {
            LoadReset();
            var members = (List<MemberObject>)memberRepository.GetListMemberDescendingByName();
            AddToDataGridView(members);
        }
        public void FilterMemberByCity(string city)
        {
            txtSearch.Text = "";
            nID.Value = 0;           
            cbCountry.SelectedIndex = 0;
            List<MemberObject> members;
            if (city.Equals("All"))
            {
                members = (List<MemberObject>)memberRepository.GetMembers();
            }
            else
            {
                members = (List<MemberObject>)memberRepository.GetMemberByCity(city);
            }
            AddToDataGridView(members);
        }
        public void FilterMemberByCountry(string country)
        {
            txtSearch.Text = "";
            nID.Value = 0;
            cbCity.SelectedIndex = 0;
            List<MemberObject> members;
            if (country.Equals("All"))
            {
                members = (List<MemberObject>)memberRepository.GetMembers();
            }
            else
            {
                members = (List<MemberObject>)memberRepository.GetMemberByCountry(country);
            }
            
            AddToDataGridView(members);
        }
        public void SearchMembersByName(string name)
        {
            nID.Value = 0;
            cbCity.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
            List<MemberObject> members = (List<MemberObject>)memberRepository.GetMemberByName(name);
            AddToDataGridView(members);
        }
        public void SearchMemberByID(int id)
        {
             
            txtSearch.Text = "";
            cbCity.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
                     
            MemberObject member = memberRepository.GetMemberByID(id);
            AddToDataGridView(member);
        }
        private void AddToDataGridView(List<MemberObject> members)
        {
            try
            {
                //The BindingSource component is designed to simplyfy
                //the process of binding controls to an underlying data source
                source = new BindingSource();
                source.DataSource = members;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvData.DataSource = null;
                dgvData.DataSource = source;
                if (!IsAdmin)
                {
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                        btnUpdate.Enabled = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void AddToDataGridView(MemberObject members)
        {
            try
            {
                //The BindingSource component is designed to simplyfy
                //the process of binding controls to an underlying data source
                source = new BindingSource();
                source.DataSource = members;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvData.DataSource = null;
                dgvData.DataSource = source;
                if (!IsAdmin)
                {
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (members == null)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                        btnUpdate.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no member had an id like that!", "Load member list",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            
            LoadMemberListByNameDescending();

        }

        private void btnClose_Click(object sender, EventArgs e)
            => Close();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            frmMemberDetails.Show();
            LoadMemberListByNameDescending();
            frmMemberDetails.Show();
            //if (frmMemberDetails.DialogResult == DialogResult.OK)
            //{
            //    LoadMemberListByNameDescending();
            //}
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Are you really want to delete data?",
                                "Member Management - Delete",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            
            try
            {
                var member = GetMemberObject();
                if (d == DialogResult.OK)
                {
                    memberRepository.RemoveMember(member.MemberID);
                }
                
                LoadMemberListByNameDescending();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsAdmin)
            {
                frmMemberDetails frmMemberDetails = new frmMemberDetails
                {
                    Text = "Update member",
                    InsertOrUpdate = true,
                    MemberInfo = GetMemberObject(),
                    MemberRepository = memberRepository
                };
                if (frmMemberDetails.ShowDialog() == DialogResult.OK)
                {
                    LoadMemberListByNameDescending();
                    source.Position = source.Count - 1;
                }
            }
            else
            {
                frmMemberDetails frmMemberDetails = new frmMemberDetails
                {
                    Text = "Update your account member",
                    InsertOrUpdate = true,
                    MemberInfo = frmLogin.Member,
                    MemberRepository = memberRepository
                };
                if (frmMemberDetails.ShowDialog() == DialogResult.OK)
                {
                    LoadMemberByID(memberLogin.MemberID);
                };
                
            }
        }

        public void LoadMemberByID(int id)
        {
            MemberObject member = memberRepository.GetMemberByID(id);
            AddToDataGridView(member);
        }

        

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = cbCity.Text;
            
            FilterMemberByCity(city);
            
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string country = cbCountry.Text;
            
            FilterMemberByCountry(country);
            
        }

        private void nID_ValueChanged(object sender, EventArgs e)
        {
            int id = (int) nID.Value;
            if (id != 0)
                SearchMemberByID(id);
                     
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            if (!search.Equals(""))
                SearchMembersByName(search);
        }

        private void frmMemberManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Are you really want to exit?",
                                "Member Management - Exit",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            if (d == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        
    }
}
