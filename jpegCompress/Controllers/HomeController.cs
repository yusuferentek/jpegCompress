using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;


namespace jpegCompress.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {


           
            if (file != null && file.ContentLength > 0)
            {
                file.SaveAs(Server.MapPath("~/Uploads/" + file.FileName));
                string yol = "C:/Users/Yusuf Eren/source/repos/jpegCompress/jpegCompress/Uploads/" + file.FileName;
                //FileStream fs = new FileStream(yol,FileMode.Open,FileAccess.Read);
                //BinaryReader br = new BinaryReader(fs);
                //byte[] resim = br.ReadBytes((int)fs.Length);
                //br.Close();
                //fs.Close();
                //MySqlConnection connection = new MySqlConnection("Server=localhost; Database=jpegcompress; Uid=root; Pwd=asdf1234;");
                //MySqlCommand cmd = new MySqlCommand("insert into tbl_uploads (img) values (@p4) ",connection);
                //cmd.Parameters.Add("@p4",MySqlDbType.Blob,resim.Length).Value=resim;
                //try
                //{
                //    connection.Open();
                //    cmd.ExecuteNonQuery();

                //}catch(Exception ex)
                //{
                //    Console.WriteLine(ex.Message.ToString());
                //}
                //finally
                //{
                //    connection.Close();
                //}

                
                
                CompressImage(@yol, "C:/Users/Yusuf Eren/source/repos/jpegCompress/jpegCompress/img", 20);
               
            }
            return View();
        }

        public static void CompressImage(string sourcepath, string destpath, int quality)
        {
            var FileName = Path.GetFileName(sourcepath);
            destpath = destpath + "\\" + FileName;
            using (Bitmap bmpl = new Bitmap(sourcepath))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmpl.Save(destpath, jpgEncoder, myEncoderParameters);
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }
}       
