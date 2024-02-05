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
using System.Formats.Tar;

public class WebPage
{
    public string Name { get; set; }
    public string Server { get; set; }
    public List<WebPage> E { get; set; }

    public WebPage(string name, string host)
    {
        Name = name;
        Server = host;
        
        Console.WriteLine("host or server with this name can not be found! ");
   
        Console.WriteLine(name + " is hosted on " + host);

        E = new List<WebPage>();

    }

    public int FindLink(string name)
    {
        WebGraph wg = new WebGraph();

        for (int i = 0; i < E.Count; i++)
        {
            var item = E[i];
            if (name.Equals(item.Name))
            {

                wg.pageName = item.Name;

                return 1;
            }
        }

        //There is no such link
        return -1;
    }
}

public class WebGraph
{

    public string pageName = "";
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

    // Adds a webpage with the given name, attached to the host server, 
    // and passed with a servergaph object to work with
    public bool AddPage(string name, string host, ServerGraph S)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(host) || S == null)
        {
            return false;
        }
        else
        {
            // Instantiate a new WebPage object
            WebPage createPage = new WebPage(name, host);

            // Add the webpage to the server in the ServerGraph
            bool addedSuccessfully = S.AddWebPage(createPage, host);

            // If the page was added successfully to the server in the ServerGraph, add it to the WebGraph
            if (addedSuccessfully)
            {
                P.Add(createPage);
                return true;
            }
            else
            {
                Console.WriteLine("Server does not exist.");
                return false;
            }
        }
    }

    // Removes a page with the given name, with a passed servergraph object to interact with.
  public void RemovePage(string name,ServerGraph S)
{
    int index = S.FindPage(name);
    if (index != -1)
    {
        S.P.RemoveAt(index);
    }
}

    // Adds a link that connects one webpage to another
    // It's like a server connection but for webpages! :D
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

    // Removes a link from the starting page that connects to the ending page if found
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
public float AvgShortestPaths(string name, ServerGraph S)
{
    int locatePage = FindPage(name);

    if (locatePage == -1)
    {
        Console.WriteLine("Page with this name does not exist");
        return -1;
    }

    int shortestPath = 0;
    foreach (var links in P[locatePage].E)
    {
        var shortestLinkPath = S.ShortestPath(P[locatePage].Server, links.Server);
        if (shortestLinkPath != -1)
        {
            shortestPath += shortestLinkPath;
            Console.WriteLine($"Shortest path {P[locatePage].Server} to {links.Server} is: {shortestLinkPath}");
        }
    }

    if (P[locatePage].E.Count == 0)
    {
        Console.WriteLine("No hyperlinks found for this page.");
        return -1;
    }

    float avg = (float)shortestPath / P[locatePage].E.Count;

    return avg;
}




  
    // Prints the webgraph!
    public void PrintGraph()
    {
        WebGraph wg = new WebGraph();
        ServerGraph sg = new ServerGraph();
        foreach (WebPage page in P)
        {
            if (page.FindLink(page.Name) > -1 )
            {
                
                Console.WriteLine("website with name: \n" + ServerGraph.firstPage + " is linked to " + ServerGraph.secondPage);
            }
            else
            {
                Console.WriteLine("website with name: \n" + page.Name + " does not have any links");
            }

        }
    }

}