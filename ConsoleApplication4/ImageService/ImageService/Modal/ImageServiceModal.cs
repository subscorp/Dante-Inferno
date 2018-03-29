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
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        public ImageServiceModal(string outputFolder)
        {
            m_OutputFolder = outputFolder;
        }

        public string AddFile(string path, out bool result)
        {
            if (File.Exists(path))
            {

                try
                {
                    if (!Directory.Exists(m_OutputFolder))
                    {
                        Console.WriteLine("m_outputFolder didn't exist");
                        Directory.CreateDirectory(m_OutputFolder);
                    }

                    var x = File.GetCreationTime(path);

                    int year = x.Year;
                    int month = x.Month;
                    var yearPath = m_OutputFolder + "\\" + year;
                    var monthPath = yearPath + "\\" + month;
                    var filePath = monthPath + "\\" + Path.GetFileName(path);
                    if (!Directory.Exists(yearPath))
                    {
                        Console.WriteLine("yearPath directory didn't exist");
                        Directory.CreateDirectory(yearPath);
                    }

                    if (!Directory.Exists(monthPath))
                    {
                        Console.WriteLine("monthPath directory didn't exist");
                        Directory.CreateDirectory(monthPath);
                    }

                    if (!File.Exists(filePath))
                    {
                        File.Copy(path, filePath);
                    }

                    result = true;
                    return null;
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine("cought an exception");
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
            }

            result = false;
            return null;

            //(?) create thumbnail
        }

        #endregion

    }
}
