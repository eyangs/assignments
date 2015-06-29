using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.DAL;
using Login.Model;



namespace Login.BLL
{
    public class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();
        //判断用户是否登录成功
        public bool GetPasswordByUserName(string userName, string password, out string msg)
        {
            bool flag = false;
            //根据用户帐号查询该用户的信息
            UserInfo user = dal.GetPasswordByUserName(userName);
            if (user != null)
            {
                //帐号存在
                //判断密码
                if (user.Password == password )
                {
                    msg = "登录成功";
                    flag = true;
                }
                else
                {
                    //密码错误
                    msg = "用户名或密码错误";
                }
            }
            else
            {
                msg = "用户名不存在";
            }
            return flag;
        }
    }
}
