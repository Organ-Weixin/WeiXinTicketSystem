using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WeiXinTicketSystem.Models.Banner
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="Activity"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this BannerEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                Title = module.Title,

                Created = module.Created.ToFormatDateString(),
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString(),
                Status = module.Status.GetDescription(),



                //Image = GetThumbnail(Image.FromFile(HttpRuntime.AppDomainAppPath.ToString() + module.Image),504,374) 
                //Image = Image.FromFile(HttpRuntime.AppDomainAppPath.ToString() + module.Image)
                
                Image = GetImage(module.Image),
                FileName = GetFileName(module.Image)

            };
        }

        /// <summary>
        /// 为图片生成缩略图  
        /// </summary>
        /// <param name="phyPath">原图片的路径</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <returns></returns>
        public static System.Drawing.Image GetThumbnail(System.Drawing.Image image, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            //从Bitmap创建一个System.Drawing.Graphics
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            //设置 
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //下面这个也设成高质量
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //下面这个设成High
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //把原始图像绘制成上面所设置宽高的缩小图
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, width, height);

            gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            return bmp;
        }


        private static string GetFileName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            string filename=path.Substring(path.LastIndexOf("\\")+1,path.Length-1- path.LastIndexOf("\\"));
            string filename1 = filename.Split('.')[0];
            return filename1;
        }


        private static string GetImage(string image)
        {
            if (string.IsNullOrEmpty(image))
                return "";
            return image.Replace(@"\", @"\\");
        }
        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this BannerEntity module, CreateOrUpdateBannerViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Title = model.Title;
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                module.StartDate = DateTime.Parse(model.StartDate);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                module.EndDate = DateTime.Parse(model.EndDate);
            }

            module.Status = (YesOrNoEnum)model.Status;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateBannerViewModel model, BannerEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Title = module.Title;
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();
            model.Status = (int)module.Status;

        }
    }
}