using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotplan
{
    class Login
    {
        public async void LoginUser()
        {
            LoginModel m = new LoginModel();
            Users u = new Users();
            var user = await LoginAuth.GetUser(u.Email, u.Password);
            if (user != null)
            {
                if(m.Email == user.Email && m.Password == user.Password)
                {
                    m.flag = true;
                }
            }
            else
            {
                m.flag = false;
            }
        }
    }
}