using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PolyglotDemo.Data;
using PolyglotDemo.Data.Test;
using PolyglotDemo.Model;

namespace PolyglotDemo.Website
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * grab the username and grab the file structure for that username
             * Bind the filestructure to the page
             * */
            if (!IsPostBack)
            {
                DatabaseInitialize.DatabaseInitializeFactory("Mongo");
                DatabaseInitialize.DatabaseInitializeFactory("Redis");
                MongoDataContext mongoContext = new MongoDataContext();

                RootDirectory directory = mongoContext.GetFileStructure("harageth").FirstOrDefault();

                Session["directory"] = directory;

                folders.DataSource = directory.folders;
                folders.DataBind();

                IEnumerable<string> dataBindFiles = directory.files;
                files.DataSource = dataBindFiles;
                files.DataBind();
            }
            else
            {
                
            }
        }

        protected void UploadFile_Click(object sender, EventArgs e)
        {
            if (uploadFileToDatabase.HasFile)
            {

                string contentType = uploadFileToDatabase.PostedFile.ContentType;

                string fileName = uploadFileToDatabase.PostedFile.FileName;
                Response.Write(fileName);
                byte[] byteArray = uploadFileToDatabase.FileBytes;

                //var wrapper = new ArchivedFilesWrapper();

                RedisDataContext redisContext = new RedisDataContext();
                RootDirectory directory = (RootDirectory) Session["directory"];
                redisContext.InsertFile(directory.un + virtualPath.Text+"/"+fileName, byteArray);
                
            }
        }

        protected void ChangeCWD_Click(object sender, EventArgs e)
        {
            LinkButton val = (LinkButton) sender;
            string newCWDName = val.Text;
            RootDirectory directory = (RootDirectory)Session["directory"];
            
            int index = directory.folders.FindIndex(x => x.folderName == newCWDName);
            Folder newCWD = directory.folders[index];

            folders.DataSource = newCWD.folders;
            folders.DataBind();

            IEnumerable<string> dataBindFiles = newCWD.files;

            files.DataSource = dataBindFiles;
            files.DataBind();

            virtualPath.Text = virtualPath.Text+newCWDName;
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            RedisDataContext redisContext = new RedisDataContext();
            LinkButton value = (LinkButton) sender;
            string fileName = "";
            string ext = "";
            int val = value.Text.LastIndexOf(".");
            if (val >= 0)
            {
                fileName = value.Text.Substring(0, val);
                
                ext = value.Text.Substring(val, value.Text.Length-val);
                
            }
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + value.Text);
            RootDirectory directory = (RootDirectory)Session["directory"];

            Byte[] buffer = redisContext.ReadFile(directory.un + virtualPath.Text + value.Text);

            string fileContents = System.Text.Encoding.Default.GetString(buffer);
            //Response.Write(fileContents);
            //Response.Write(virtualPath.Text + value.Text);
            Response.BinaryWrite(buffer);
            Response.End();
        }
    }
}