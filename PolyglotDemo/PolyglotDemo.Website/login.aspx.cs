using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PolyglotDemo.Data;
using PolyglotDemo.Model;
using System.Security.Cryptography;

namespace PolyglotDemo.Website
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PolyglotDemo.Data.Test.DatabaseInitialize.DatabaseInitializeFactory("Mongo");
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            MongoDataContext mongoContext = new MongoDataContext();
            if (username.Text.Length > 0)
            {
                RootDirectory user = mongoContext.GetFileStructure(username.Text).FirstOrDefault();

                if (user != null)
                {
                    if (validatePassword(user.pw))
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Response.Write("Please remember that the Guest password is \"Guest\"" + user.un);
                    }
                }
                else
                {
                    Response.Write("The username or password you entered is invalid<br>");
                }
            }
            else
            {
                Response.Write("Please enter some sort of value");
            }
        }

        protected Boolean validatePassword(string dbPassword)
        {
            byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes(password.Text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] inputHash = hashstring.ComputeHash(inputBytes);
            string inputHashString = System.Text.Encoding.UTF8.GetString(inputHash);
            
            if (dbPassword.Equals(inputHashString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}