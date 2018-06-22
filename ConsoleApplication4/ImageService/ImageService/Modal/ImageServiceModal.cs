using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Class for model, which manages files in a folder.
    /// </summary>
    /// <seealso cref="ImageService.Modal.IImageServiceModal" />
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private string m_thumbnailsFolder;
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        DirectoryInfo di;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServiceModal"/> class.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <param name="thumbnailSize">Size of the thumbnail.</param>
        public ImageServiceModal(string outputFolder, int thumbnailSize)
        {
            m_OutputFolder = outputFolder;
            m_thumbnailsFolder = outputFolder + "\\" + "Thumbnails";
            m_thumbnailSize = thumbnailSize;
        }


        //we initialize this once so that if the function is repeatedly called
        //it doesn't stress the garbage man
        private static Regex r = new Regex(":");
        /// <summary>
        /// The Function Adds A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        public string AddFile(string path, out bool result)
        {
            Image thumb = null;
            const int PropertyTagExifDTOrig = 0x9003;
            if (File.Exists(path))
            {
                try
                {
                    DateTime date = DateTime.MinValue ;
                    
                    //this is added for stability. Otherwise file issues can happen.
                    Thread.Sleep(100);

                    //getting creation time in year and month
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (Image image = Image.FromStream(fs))
                    {
                        var hasDate = image.PropertyIdList.Contains(PropertyTagExifDTOrig);
                        if (hasDate)
                        {
                            PropertyItem propItem = image.GetPropertyItem(PropertyTagExifDTOrig);
                            var vals = string.Join(", ", propItem.Value);
                            string strDateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                            if (!string.IsNullOrWhiteSpace(strDateTaken))
                            {
                                hasDate = true;
                                date = DateTime.Parse(strDateTaken);
                            }
                        }
                        if (!hasDate)
                        {
                            date = File.GetCreationTime(path);
                        }
                        thumb = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                        image.Dispose();
                    }
                    var x = date;
                    int year = x.Year;
                    int month = x.Month;

                    //getting  the imagePath and the thumbnailPath
                    var imageYearPath = m_OutputFolder + "\\Photos" + "\\" + year;
                    var imageMonthPath = imageYearPath + "\\" + month;
                    var imagePath = imageMonthPath + "\\" + Path.GetFileName(path);
                    var thumbnailYearPath = m_thumbnailsFolder + "\\" + year;
                    var thumbnailMonthPath = thumbnailYearPath + "\\" + month;
                    var thumbnailPath = thumbnailMonthPath + "\\" + Path.GetFileName(path);

                    //check if outputFolder exists, if not- create it
                    if (!Directory.Exists(m_OutputFolder))
                    {
                        di = Directory.CreateDirectory(m_OutputFolder);
                        di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        Directory.CreateDirectory(m_OutputFolder + "\\Photos");
                    }

                    //check if thumbnailFolder exists, if not- create it
                    if (!Directory.Exists(m_thumbnailsFolder))
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

                    else
                    {
                        File.Delete(imagePath);
                        File.Move(path, imagePath);
                    }

                    //creating thumbnail and saving it in the correct folder
                    thumb.Save(thumbnailPath);
                    result = true;
                    return null;
                }
                catch (Exception ex)
                {
                    result = false;
                    return ex.Message;
                }
                finally
                {
                    //disposing the thumbnail from memory
                    if (thumb != null)
                    {
                        thumb.Dispose();
                    }
                    
                }
            }
            result = false;
            return null;
        }

        #endregion
    }
}
