/*
ASSIGNMENT#1 - COIS-3020

TEAM MEMBERS:

Luka Nikolaisvili - 0674677
Farzad Imran - 0729901
Freddrick Nkwonta - 0703772

-----------------

*/


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
        Console.WriteLine(name + " is hosted on " + host);
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

        for (int i = 0; i < P.Count; i++)
        {
            if (P[i].Name == name)
                return 1;
        }
        return -1;
    }


    public bool AddPage(string name, string host, ServerGraph S)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(host) || S == null)
        {
            return false;
        }
        else
        {
            ServerGraph sg = new ServerGraph();

            sg.AddPage(name, host, S);


            WebPage createPage = new WebPage(name, host);

            S.P.Add(createPage);

            return true;

        }
    }

    public bool RemovePage(string name, ServerGraph S)
    {

        int id = FindPage(name);
        if (id != -1)
        {
            S.P.RemoveAt(id);
            return true;
        }

        return false;
    }

    public bool AddLink(string from, string to)
    {

        int indexFrom = FindPage(from);
        int indexTo = FindPage(to);

        if (indexFrom != -1 && indexTo != -1)
        {
            P[indexFrom].E.Add(P[indexTo]);

            return true;
        }

        return false;

    }

    public bool RemoveLink(string from, string to)
    {

        int indexFrom = FindPage(from);
        int indexTo = FindPage(to);

        if (indexFrom != -1 && indexTo != 1)
        {


            if (P[indexFrom].E.Contains(P[indexTo]))
            {

                P[indexFrom].E.Remove(P[indexTo]);

                return true;
            }
        }

        return false;

    }

    // 6 marks
    // Return the average length of the shortest paths from the webpage with
    // given name to each of its hyperlinks
    // Hint: Use the method ShortestPath in the class ServerGraph

    // public float AvgShortestPaths(string name, ServerGraph S)
    // {
    //     int i = FindPage(name);

    //     if (i == -1)
    //     {
    //         console.WriteLine("webpage does not exist.");
    //         return -1;
    //     }



    // }

    public void PrintGraph()
    {


    }
}


