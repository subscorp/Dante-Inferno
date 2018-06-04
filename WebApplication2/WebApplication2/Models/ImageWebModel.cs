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

    public string Student_Details
    {
        get { return student_details; }
    }

    public ImageWebModel()
	{
        client = GUIClient.Instance;

        using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile("/App_Data/info.txt")))
        {
            student_details = sr.ReadToEnd();
        }
	}
}
