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

    private int FindPage(string name)
    {
        return 10;
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