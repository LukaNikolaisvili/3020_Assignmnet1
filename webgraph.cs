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
    // Getters and Setters
    public string Name { get; set; }
    public string Server { get; set; }
    public List<WebPage> E { get; set; }


    // Constructors
    public WebPage(string name, string host)
    {
        Name = name;
        Server = host;
        E = new List<WebPage>();

    }

    // 2 marks
    // Return the index of the webpage with the given name; otherwise return -1
    // Method to find the current index location of a link
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
        // Declare a list for webpages
        P = new List<WebPage>();
    }

    // 2 marks
    // Return the index of the webpage with the given name; otherwise return -1
    private int FindPage(string name)
    {
        for (int i = 0; i < P.Count; i++)
        {
            if (P[i].Name == name)
                return i; // Return the index if found
        }
        return -1; // Return -1 if not found
    }

    // 4 marks
    // Add a webpage with the given name and store it on the host server
    // Return true if successful; otherwise return false

    public bool AddPage(string name, string host, ServerGraph S)

    {
        //check if name parmeter pass are empty or null 
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
                Console.WriteLine(name + " is hosted on " + host);
                return true;
            }
            else // if server does not exists.
            {
                Console.WriteLine("Server does not exist.");
                return false;
            }
        }
    }

    // 8 marks
    // Remove the webpage with the given name, including the hyperlinks
    // from and to the webpage
    // Return true if successful; otherwise return false
    public bool RemovePage(string name, ServerGraph S)
    {
        int pageIndex = FindPage(name);
        if (pageIndex == -1)
            return false; // Webpage not found

        //Webpage object pageToremove will be assigned to P[pageIndex]
        WebPage pageToRemove = P[pageIndex];
        //and with the method remove at we will remove with index
        P.RemoveAt(pageIndex);


        foreach (var page in P)
        {
            //starting from the end and going to the beginning(reversed) basically 
            for (int i = page.E.Count - 1; i >= 0; i--)
            {
                //if page at index I name is the same as the name that we want to remove 
                if (page.E[i].Name == name)
                {
                    //then we will go ahead and remove that index
                    page.E.RemoveAt(i);
                }
            }
        }

        //for printing out keeping the boolean variable declared and assigned to be returned out from the method
        bool removedFromServer = S.RemoveWebPage(name, pageToRemove.Server);

        //returns boolean true or false, if removed true, if not false
        return removedFromServer;
    }

    // 3 marks
    // Adds a link that connects one webpage to another
    // It's like a server connection but for webpages! :D
    public bool AddLink(string from, string to)
    {
        int indexFrom = FindPage(from);
        int indexTo = FindPage(to);

        // Check if both pages exist
        if (indexFrom == -1 || indexTo == -1)
        {
            return false; // since its or, and we want to link pages, it will return false, because true and false is true...
        }

        // Check for an existing link to avoid duplication
        var existingLink = P[indexFrom].E.Find(page => page.Name == to);
        if (existingLink != null)
        {
            return false; // returns false because Link already there.
        }

        P[indexFrom].E.Add(P[indexTo]);

        return true;
    }

    // 3 marks
    // Remove a hyperlink from one webpage to another
    // Return true if successful; otherwise return false
    // Removes a link from the starting page that connects to the ending page if found
    public bool RemoveLink(string from, string to)
    {
        //indexes of from to TO
        int indexFrom = FindPage(from);
        int indexTo = FindPage(to);

        //checking if both are not equal to one 
        if (indexFrom != -1 && indexTo != -1)
        {
            //if indexFrom contains IndexTo
            if (P[indexFrom].E.Contains(P[indexTo]))
            {
                //we will remove link between them
                P[indexFrom].E.Remove(P[indexTo]);

                //return true
                return true;
            }
        }

        //return false if there is no link or page does not exist at all
        return false;
    }


    // 6 marks
    // Return the average length of the shortest paths from the webpage with
    // given name to each of its hyperlinks
    // Hint: Use the method ShortestPath in the class ServerGraph
    public float AvgShortestPaths(string name, ServerGraph S)
    {
        // Initial check to see if the page exists
        // Can't search for something that isn't there!
        int locatePage = FindPage(name);

        if (locatePage == -1)
        {
            Console.WriteLine("Page with this name does not exist");
            return -1;
        }

        int shortestPath = 0;       // Initial counter for the Shortest path
        foreach (var links in P[locatePage].E)
        {
            // Function call to find the shortest path
            var shortestLinkPath = S.ShortestPath(P[locatePage].Server, links.Server);
            // If a shortest path was found (-1 if not)
            if (shortestLinkPath != -1)
            {
                shortestPath += shortestLinkPath;
                Console.WriteLine($"The most short path {P[locatePage].Server} to the {links.Server} -- {shortestLinkPath}");
            }
        }

        // Checks if the number of edges on the page equals 0,
        // If so that means no hyperlinks
        if (P[locatePage].E.Count == 0)
        {
            Console.WriteLine("No hyperlinks found for this page.");
            return -1;
        }

        //finds the shortest average.

        float avg = (float)shortestPath / P[locatePage].E.Count;

        return avg;
    }





    // 3 marks
    // Print the name and hyperlinks of each webpage
    public void PrintGraph()
    {
        foreach (WebPage page in P) //look at all the pages 
        {
            Console.WriteLine("Webpage: " + page.Name);
            if (page.E.Count > 0)
            {
                Console.WriteLine("Hyperlinks:");
                foreach (WebPage hyperlink in page.E) //look at all the connections 
                {
                    Console.WriteLine("- " + hyperlink.Name);
                }
            }
            else
            {
                Console.WriteLine("No hyperlinks.");
            }
            Console.WriteLine();
        }
    }
}