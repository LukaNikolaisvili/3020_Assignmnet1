using System;
using System.Collections.Generic;

public class WebPage
{
    public string Name { get; set; }
    public string Server { get; set; }
    public List<WebPage> E { get; set; }

    public WebPage(string name, string host)
    {
        Name = name;
        Server = host;
        Console.WriteLine(name + " and the host is " + host);
    }

    public int FindLink(string name)
    {
        return 1;
    }
}

public class WebGraph
{
    private List<WebPage> P;

    public WebGraph()
    {
        P = new List<WebPage>();
    }

    // 2 marks
    // Return the index of the webpage with the given name; otherwise return -1
    private int FindPage(string name)
    {

        for (int i = 0; i < P.count; i++)
        {
            if (P[i].Name == name)
                return 1;
        }
        return -1;
    }


    public bool AddPage(string name, string host, ServerGraph S)
    {
        return false;
    }

    public bool RemovePage(string name, ServerGraph S)
    {
        return false;
    }

    public bool AddLink(string from, string to)
    {
        return false;
    }

    public bool RemoveLink(string from, string to)
    {
        return false;
    }

    public float AvgShortestPaths(string name, ServerGraph S)
    {
        return 2;
    }

    public void PrintGraph()
    {

    }
}