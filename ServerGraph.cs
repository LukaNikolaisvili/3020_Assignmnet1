/*
ASSIGNMENT#1 - COIS-3020

TEAM MEMBERS:

Luka Nikolaisvili - 0674677
Farzad Imran - 0729901
Freddrick Nkwonta - 0703772

-----------------

*/

using System.Linq.Expressions;

using System;
using System.Collections.Generic;

public class ServerGraph : WebGraph
{
    // 3 marks
    private class WebServer
    {
        public string Name;
        public List<WebPage> P;
        public List<bool> E;

        public WebServer(string name)
        {
            Name = name;
            P = new List<WebPage>();
            E = new List<bool>();
        }

    }
    private WebServer[] V;
    private bool[,] E;
    private int NumServers;


    public ServerGraph()
    {

        V = new WebServer[1];
        E = new bool[1, 1];
        NumServers = 0;


    }
    // 2 marks
    // Return the index of the server with the given name; otherwise return -1
    private int FindServer(string name)
    {
        for (int i = 0; i < NumServers; i++)
        {
            if (string.Equals(V[i].Name.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase))
                return i;

        }
        return -1;
    }
    // 3 marks
    // Double the capacity of the server graph with the respect to web servers
    private void DoubleCapacity()
    {

        int newVLength = V.Length * 2;
        Array.Resize(ref V, newVLength);


        int rowCounter = E.GetLength(0);
        int columnCounter = E.GetLength(1);


        bool[,] doubledMatrix = new bool[rowCounter * 2, columnCounter * 2];


        Array.Copy(E, 0, doubledMatrix, 0, rowCounter * columnCounter);

        E = doubledMatrix;
    }
    // 3 marks
    // Add a server with the given name and connect it to the other server
    // Return true if successful; otherwise return false
    public bool AddServer(string name, string other)
    {
        if (NumServers == 0)
        {
            Console.WriteLine("No servers Exist");
            Console.WriteLine("Creating new server called 'Root' ");

            V[NumServers] = new WebServer("Root");
            E[0, 0] = false;
            NumServers = NumServers + 1;

            Console.WriteLine("No servers Added ");
            return false;

        }

        int i, j;


        if (NumServers >= 1)
        {

            if ((j = FindServer(other)) != -1)
            {
                if ((i = FindServer(name)) == -1)
                {

                    if (NumServers == V.Length)
                    {
                        DoubleCapacity();
                    }

                    V[NumServers] = new WebServer(name);
                    E[NumServers, j] = true;
                    E[j, NumServers] = true;

                    NumServers++;


                    Console.WriteLine("Added server: " + name);

                    return true;

                }
            }
            else
            {
                Console.WriteLine("Other server doesnt exists.");
                return false;

            }

            return false;


        }

        return false;



    }
    // 3 marks
    // Add a webpage to the server with the given name
    // Return true if successful; otherwise return false
    public bool AddWebPage(WebPage w, string name)
    {
        int i = FindServer(name);

        if (i > -1)
        {
            V[i].P.Add(w);
            return true;
        }
        return false;

    }
    // Remove the server with the given name by assigning its connections
    // and webpages to the other server
    // Return true if successful; otherwise return false
    public bool RemoveServer(string name, string other)
    {
        int i = FindServer(name);
        int j = FindServer(other);

        if (i == -1 || j == -1)
        {
            Console.WriteLine("Both servers or one of the servers does not exist.");
            return false;
        }
        else
        {


            foreach (WebPage page in V[i].P)
            {

                V[j].P.Add(page);
                Console.WriteLine(page + "is being move to server" + other);
            }



            for (int s = i; s < NumServers - 1; s++)
            {
                V[s] = V[s + 1];
                for (int t = 0; t < NumServers; t++)
                {
                    E[s, t] = E[s + 1, t];
                    E[t, s] = E[t, s + 1];
                }
            }


            Console.WriteLine(name + " server has be Removed");

            NumServers--;

            return true;

        }


    }

    // Remove the webpage from the server with the given name
    // Return true if successful; otherwise return false
    public bool RemoveWebPage(string webpage, string name)
    {

        return false;
    }
    // 3 marks
    // Add a connection from one server to another
    // Return true if successful; otherwise return false
    // Note that each server is connected to at least one other server
    public bool AddConnection(string from, string to)
    {
        int indexOfFrom = FindServer(from);
        int indexOfTo = FindServer(to);

        if (indexOfFrom != -1 && indexOfTo != -1)
        {
            V[indexOfFrom].E[indexOfTo] = true;
            return true;
        }
        return false;

    }
    // 10 marks
    // Return all servers that would disconnect the server graph into
    // two or more disjoint graphs if ever one of them would go down
    // Hint: Use a variation of the depth-first search
    public string[] CriticalServers()
    {
        int time = 0;
        bool[] visited = new bool[NumServers];
        int[] disc = new int[NumServers];
        int[] low = new int[NumServers];
        int[] parent = new int[NumServers];
        bool[] artipoints = new bool[NumServers];
        List<string> result = new List<string>(); // Use a list to store the resulting critical servers

        for (int i = 0; i < NumServers; i++)
        {
            if (!visited[i])
            {
                CriticalServers(i, ref time, visited, disc, low, parent, artipoints, result);
            }
        }

        return result.ToArray(); // Convert the list to an array and return
    }

    private void CriticalServers(int i, ref int time, bool[] visited, int[] disc, int[] low, int[] parent, bool[] artipoints, List<string> result)
    {
        int children = 0;
        disc[i] = low[i] = ++time;
        visited[i] = true;

        for (int j = 0; j < NumServers; j++)
        {
            if (E[i, j] && !visited[j] && V[j] != null) // Check if there's a connection and the server exists
            {
                children++;
                parent[j] = i;
                CriticalServers(j, ref time, visited, disc, low, parent, artipoints, result);

                low[i] = Math.Min(low[i], low[j]);

                if ((parent[i] == -1 && children > 1) || (parent[i] != -1 && low[j] >= disc[i]))
                {
                    artipoints[i] = true;
                }
            }
            else if (j != parent[i])
            {
                low[i] = Math.Min(low[i], disc[j]);
            }
        }

        if (artipoints[i])
        {
            result.Add(V[i].Name); // Add the critical server to the result list
        }
    }

    public void PrintCriticalServers(string[] criticalServers)
    {
        Console.WriteLine("Critical Servers:");
        foreach (string server in criticalServers)
        {
            Console.WriteLine(server);
        }
    }


    // 4 marks
    // Print the name and connections of each server as well as
    // the names of the webpages it hosts
    public new void PrintGraph()
    {
        for (int i = 0; i < NumServers; i++)
        {
            Console.WriteLine("Server: " + V[i].Name);
            Console.WriteLine("Connections:");

            for (int j = 0; j < NumServers; j++)
            {
                if (E[i, j])
                {
                    Console.WriteLine("- " + V[j].Name);
                }
            }

            Console.WriteLine("Webpages:");
            for (int s = 0; s < V[i].P.Count; s++)
            {
                Console.WriteLine("- " + V[i].P[s].Name);
            }

            Console.WriteLine();
        }
    }




    static void Main()
    {

        ServerGraph serverGraph = new ServerGraph();
        WebGraph webGraph = new WebGraph();
        serverGraph.AddServer("", "");
        
        while (true)
        {


            Console.WriteLine("\n1: Add Server ");
            Console.WriteLine("2: Add Page ");
            Console.WriteLine("3: Find shortest Path");
            Console.WriteLine("4: double the capacity ");
            Console.WriteLine("5: Print the graph ");
            Console.WriteLine("6: Locate the Critical servers ");
            Console.WriteLine("7: Remove the page ");
            Console.WriteLine("8: Remove the Server ");
            Console.WriteLine("9: Add Hyperlink between pages ");
            Console.WriteLine("exit: type [exit] to close the application ");

            for (int i = 0; i < 36; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine("\ntype the opertaion you would like to perform ");
            string input = Console.ReadLine();

            if (input == "1"){   
                if(serverGraph.NumServers > 0){
                Console.WriteLine("Enter 1st server name: ");
                string server1 = Console.ReadLine();
                Console.WriteLine("Enter 2nd server name: ");
                string server2 = Console.ReadLine();
                Console.WriteLine("\nResult:");
                serverGraph.AddServer(server1, server2);
            }
            
              

            }

            else if (input == "2")
            {
                Console.WriteLine("Enter your website name: ");
                string websiteName = Console.ReadLine();
                Console.WriteLine("Enter the server you want to host your webiste at: ");
                string hostingServer = Console.ReadLine();
                Console.WriteLine("\nResult: ");
                WebPage page = new WebPage(websiteName, hostingServer);
                serverGraph.AddWebPage(page, hostingServer);
            }
            else if (input == "3")
            {
                Console.WriteLine("Enter starting server: ");
                string startingServer = Console.ReadLine();
                Console.WriteLine("Enter the server you want to get to: ");
                string serverToGetTo = Console.ReadLine();
                Console.WriteLine("\nResult: ");
                // Console.WriteLine(serverGraph.AvgShortestPaths(startingServer,serverGraph));

            }
            else if (input == "4")
            {


                int oldCapacity = serverGraph.V.Length;
                serverGraph.DoubleCapacity();
                int newCapacity = serverGraph.V.Length;

                Console.WriteLine("server capacifty incresed from " + oldCapacity + " to " + newCapacity);
            }

            else if (input == "5")
            {


                Console.WriteLine("Printing graph...\n");
                serverGraph.PrintGraph();

            }

            else if (input == "6")
            {
                string[] criticalServers = serverGraph.CriticalServers();
                serverGraph.PrintCriticalServers(criticalServers);      
            }


            else if (input == "7")
            {
                Console.WriteLine("Enter the name of the page ");
                string pageName = Console.ReadLine();
                webGraph.RemovePage(pageName, serverGraph);
            }

            else if (input == "8")
            {
                Console.WriteLine("Enter the server name that you want to remove : ");
                string firstServerName = Console.ReadLine();
                Console.WriteLine("Enter the server you want to move removed server's connections to: ");
                string secondServerName = Console.ReadLine();
                serverGraph.RemoveServer(firstServerName, secondServerName);
            }

            else if (input == "9")
            {
                Console.WriteLine("Enter the 1st page name ");
                string firstPage = Console.ReadLine();
                Console.WriteLine("Enter the 2nd page name ");
                string secondPage = Console.ReadLine();
                webGraph.AddLink(firstPage, secondPage);


            }

            else if (input == "exit")
            {

                Environment.Exit(0);

            }


            else
            {
                Console.WriteLine("'Oops you were close bud, try again!'");

            }

        }
    }
}

