using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login.BLL;
using Login.Model;



namespace Login
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        //判断帐号和密码不能为空
        private bool CheckEmpty(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                msgDiv1.MsgDivShow("帐号不能为空", 1);
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                msgDiv1.MsgDivShow("密码不能为空", 1);
                return false;
            }
            return true;
        }

        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
           //首先获取账号和密码
           //再判断账号和密码不能为空
            string username = txtName.Text.Trim();
            string password = txtPassword.Text;
            string msg = "";
            if (CheckEmpty(username, password))
            {
                UserInfoBLL bll = new UserInfoBLL();
                if (bll.GetPasswordByUserName(username, password, out msg))
                {
                    //登录成功
                    msgDiv1.MsgDivShow(msg, 1, Bind);
                }
                else
                {
                    msgDiv1.MsgDivShow(msg, 1);
                }
            }

        }

        //关闭窗体
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Bind()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
