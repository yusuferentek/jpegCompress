using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                string yol = "C:/Users/Yusuf Eren/source/repos/jpegCompress/jpegCompress/Uploads/"+file.FileName;
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
