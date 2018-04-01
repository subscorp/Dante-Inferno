using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private string m_thumbnailsFolder;
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size


        public ImageServiceModal(string outputFolder, int thumbnailSize)
        {
            m_OutputFolder = outputFolder;
            m_thumbnailsFolder = outputFolder + "\\" + "Thumbnails";
            m_thumbnailSize = thumbnailSize;
        }

        public string AddFile(string path, out bool result)
        {
            if (File.Exists(path))
            {
                try
                {
                    //getting creation time in year and month
                    var x = File.GetCreationTime(path);
                    int year = x.Year;
                    int month = x.Month;

                    //getting  the imagePath and the thumbnailPath
                    var imageYearPath = m_OutputFolder + "\\" + year;
                    var imageMonthPath = imageYearPath + "\\" + month;
                    var imagePath = imageMonthPath + "\\" + Path.GetFileName(path);
                    var thumbnailYearPath = m_thumbnailsFolder + "\\" + year;
                    var thumbnailMonthPath = thumbnailYearPath + "\\" + month;
                    var thumbnailPath = thumbnailMonthPath + "\\" + Path.GetFileName(path);

                    //check if outputFolder exists, if not- create it
                    if (!Directory.Exists(m_OutputFolder))
                    {
                        Directory.CreateDirectory(m_OutputFolder);
                    }

                    //check if thumbnailFolder exists, if not- create it
                    if(!Directory.Exists(m_thumbnailsFolder))
                    {
                        Directory.CreateDirectory(m_thumbnailsFolder);
                    }

                    //check if year folder exists, if not- create it both for the image and for the thumbnail
                    if (!Directory.Exists(imageYearPath))
                    {
                        Directory.CreateDirectory(imageYearPath);
                        Directory.CreateDirectory(thumbnailYearPath);
                    }

                    //check if month folder exists, if not- create it both for the image and for the thumbnail
                    if (!Directory.Exists(imageMonthPath))
                    {
                        Directory.CreateDirectory(imageMonthPath);
                        Directory.CreateDirectory(thumbnailMonthPath);
                    }

                    //if the image isn't already in the path, copy it to there
                    if (!File.Exists(imagePath))
                    {
                        File.Move(path, imagePath);
                    }

                    //creating thumbnail and saving it in the correct folder
                    Image image = Image.FromFile(imagePath);
                    Image thumb = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                    thumb.Save(Path.ChangeExtension(thumbnailPath, "thumb"));

                    result = true;
                    return null;
                }
                catch (Exception ex)
                {
                    result = false;
                    return ex.Message;
                }
            }
            result = false;
            return null;
        }

        #endregion
    }
}
