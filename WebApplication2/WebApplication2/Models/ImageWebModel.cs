using System;
using System.IO;
using System.Threading;
using System.Web.Hosting;
using Communication;
using WebApplication2.Models;

public class ImageWebModel : WebModel
{
    private string status;
    private int photos_number;
    private string student_details;

    public string Status {
        get { return status; }
        set { status = value; }
    }

    public int PhotosNumber
    {
        get { return photos_number; }
        set { photos_number = value; }
    }

    public string Student_Details {
        get { return student_details; }
        set { student_details = value; }
    }

    public void UpdateParameters()
    {
        Status = client.Connected() ? "Running" : "Stopped";

        if (client.Connected())
            PhotosNumber = client.GetNumberOfPhotos();
    }

    public ImageWebModel() : base()
	{
        using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile("/App_Data/info.txt")))
        {
            Student_Details = sr.ReadToEnd();
        }


        UpdateParameters();

	}
}
