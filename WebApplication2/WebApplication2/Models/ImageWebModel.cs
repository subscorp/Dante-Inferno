using System;
using System.IO;
using System.Threading;
using System.Web.Hosting;
using Communication;
using WebApplication2.Models;

/// <summary>
/// Class for model, which manages the main window in the ASP.net app
/// </summary>
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

    //checks whether the service is running, and number of photos
    public void UpdateParameters()
    {
        Status = client.Connected() ? "Running" : "Stopped";
        
        if (client.Connected())
            PhotosNumber = client.GetNumberOfPhotos();
    }

    // Constructor
    public ImageWebModel() : base()
	{

        //reading the details of students from file
        using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile("/App_Data/info.txt")))
        {
            Student_Details = sr.ReadToEnd();
        }

        //updating status and photo number parameters
        UpdateParameters();
	}
}
