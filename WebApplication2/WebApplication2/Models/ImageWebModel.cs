using System;
using System.IO;
using System.Web.Hosting;
using Communication;

public class ImageWebModel
{
    private GUIClient client;
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

    public ImageWebModel()
	{
        client = GUIClient.Instance;
        client.Connect();

        using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile("/App_Data/info.txt")))
        {
            Student_Details = sr.ReadToEnd();
        }

        Status = client.Connected() ? "Running" : "Stopped";

        PhotosNumber = client.GetNumberOfPhotos();

	}
}
