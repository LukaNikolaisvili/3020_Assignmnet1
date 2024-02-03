using System.Linq.Expressions;

using System;
using System.Collections.Generic;

public class ServerGraph : WebGraph
{
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
    private int NumServers;

    public ServerGraph()
    {
        // V = new WebServer[2];
        // NumServers = 0;


        V = new WebServer[1]; // Increase the capacity to accommodate "hi"
        NumServers = 1; // Start with 1 server ("hi" server)

        // Add the "hi" server
        V[0] = new WebServer("hi");
        V[0].E.Add(false); // "hi" server has no connections initially
    }

    private int FindServer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return -1;

        for (int i = 0; i < NumServers; i++)
        {
            if (string.Equals(V[i].Name.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase))
                return i;
        }
        return -1;
    }

    private void DoubleCapacity()
    {
        int newCapacity = V.Length * 2;
        Array.Resize(ref V, newCapacity);
    }

    public void Capacity2()
    {
        DoubleCapacity();
    }

    public bool AddServer(string name, string other)
    {
        int i, j;

        // Check if the other server exists
        if ((j = FindServer(other)) != -1)
        {
            // Check if the server to be added doesn't already exist
            if ((i = FindServer(name)) == -1)
            {
                // Check if the array needs resizing
                if (NumServers == V.Length)
                {
                    DoubleCapacity();
                }

                // Add the new server and connect it to the existing server
                V[NumServers] = new WebServer(name);
                V[NumServers].E.Add(false);  // Add a new connection (false means no connection)
                V[NumServers].E[j] = true;
              
                NumServers++;

                // Print the name of the added server
                Console.WriteLine("Added server: " + name);

                return true;
            }
            else
            {
                Console.WriteLine("Server already exists.");
            }
        }
        else
        {
            Console.WriteLine("The other server does not exist.");
        }

        return false;
    }




    public bool AddWebPage(WebPage w, string name)
    {
        int i = FindServer(name);

        if (i > -1)
        {
            V[i].P.Add(w);
            NumServers++;
            return true;
        }
        return false;
    }

    public bool RemoveServer(string name, string other)
    {
        int i = FindServer(name);
        int j = FindServer(other);

        if (i == -1 || j == -1)
            return false;

        for (int s = 0; s < NumServers; s++)
        {
            if (s != i)
            {
                if (V[i].E[s])
                {
                    V[j].E[s] = true;
                    if (V[s].E[i])
                    {
                        V[s].E[j] = true;
                    }
                }
                NumServers--;
                return true;
            }
        }
        return false;
    }

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

	public string[] CriticalServers()
	{
		int i;
		int j = 0;
		int time = 0;
		bool[] visited = new bool[NumServers];
		int[] disc = new int[NumServers];
		int[] low = new int[NumServers];
		int[] parent = new int[NumServers];
		bool[] artipoints = new bool[NumServers]; // To store articulation points
		string[] result = new string[NumServers]; // To store the resulting string

		for (i = 0; i < NumServers; i++)     // Set all vertices as unvisited
        {
			visited[i] = false;
			artipoints[i] = false;
			parent[i] = -1;
		}


		for (i = 0; i < NumServers; i++)
			if (!visited[i])                  // (Re)start with vertex i
			{
				CriticalServers(i, time, visited, disc, low, parent, artipoints);
				Console.WriteLine();
			}

		// Now artipoints[] contains articulation points, print them
		for (int i = 0; i < NumServers; i++)
		{
			if (artipoints[i] == true)
			{
				Console.Write(i + " ");
				result[j] = i.ToString();
				j++;

			}

		}
		return result;
	}

	private void CriticalServers(int i, int time, bool[] visited, int[] disc, int[] low, int[] parent, bool[] artipoints)
	{
		int j;
		// Count of children in DFS Tree
		int children = 0;
		// Sets the discovery and lowest discovery time to the passed time variable.
		disc[i] = low[i] = ++time;

		visited[i] = true;    // Output vertex when marked as visited
		Console.WriteLine(i);

		for (j = 0; j < NumServers; j++)    // Visit next unvisited adjacent vertex
			if (!visited[j] && E[i, j] > -1)
			{
				children++;
				parent[j] = i;
				CriticalServers(j, time, visited, disc, low, parent, artipoints);

				// Check if the subtree rooted with j has
				// a connection to one of the ancestors of i
				low[i] = Math.Min(low[i], low[j]);

				// i is a critical server (articulation point) if any of the following statements hold true:
				// Case 1:  i is the root of the tree and has 2 or more children
				if (parent[i] == -1 && children > 1)
					artipoints[i] = true;

				// Case 2: i isn't a root of the tree, and one of its children have a lowest discovery time 
				// greater than i's discovery time
				if (parent[i] != -1 && low[j] >= disc[i])
					artipoints[i] = true;
			}
			// Update low value of i for parent function calls.
			else if (j != parent[i])
				low[i] = Math.Min(low[i], disc[j]);
	}

    public int ShortestPath(string from, string to)
    {
        int fromIndex = FindServer(from);
        int toIndex = FindServer(to);

        if (fromIndex == -1 || toIndex == -1)
        {
            Console.WriteLine("Server not found.");
            return -1;
        }

        Queue<WebServer> queue = new Queue<WebServer>();
        bool[] visited = new bool[NumServers];
        int[] distances = new int[NumServers];

        queue.Enqueue(V[fromIndex]);
        visited[fromIndex] = true;
        distances[fromIndex] = 0;

        while (queue.Count > 0)
        {
            WebServer currentServer = queue.Dequeue();

            if (currentServer == V[toIndex])
                return distances[FindServer(currentServer.Name)];

            for (int i = 0; i < NumServers; i++)
            {
                if (currentServer.E[i] && !visited[i])
                {
                    queue.Enqueue(V[i]);
                    visited[i] = true;
                    distances[i] = distances[FindServer(currentServer.Name)] + 1;
                }
            }
        }

        Console.WriteLine("No path found.");
        return -1;
    }

    public new void PrintGraph()
    {
        for (int i = 0; i < NumServers; i++)
        {
            Console.WriteLine(V[i].Name);
            for (int j = 0; j < V[i].E.Count; j++)
            {

                Console.WriteLine("(" + V[i].Name + "," + V[i].E[j] + "," + V[j].P.ToArray().ToString() + ")");
            }
        }
    }

    static void Main()
    {
        // ServerGraph gp = new ServerGraph();
        // gp.AddServer("server1", "hi");

        // WebPage pages = new WebPage("facebook", "server1");
        // //   gp.AddPage("facebook","server1",gp);

        // Console.WriteLine(gp.V.Length); //before the method 
        // gp.DoubleCapacity();
        // Console.WriteLine(gp.V.Length); //after 
        // gp.PrintGraph();
        // gp.DoubleCapacity();
        // gp.DoubleCapacity();
        // gp.DoubleCapacity();
        // Console.WriteLine(gp.V.Length);
        
        /*		
        string[] result = gp.CriticalServers();
		for (int i = 0; i < result.Length; i++)
		{
			Console.Write(result[i] + " ");
		}
		Console.WriteLine(); */


        ServerGraph serverGraph = new ServerGraph();
        WebGraph webGraph = new WebGraph();

        while (true)
        {


            Console.WriteLine("\n1: Add Server ");
            Console.WriteLine("2: Add Page ");
            Console.WriteLine("3: Find shortest Path");
            Console.WriteLine("4: double the capacity ");
            Console.WriteLine("5: Print the graph ");
            Console.WriteLine("exit: type [exit] to close the application ");

            for (int i = 0; i < 36; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine("\ntype the opertaion you would like to perform ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("Enter 1st server name: ");
                string server1 = Console.ReadLine();
                Console.WriteLine("Enter 2nd server name: ");
                string server2 = Console.ReadLine();
                Console.WriteLine("\nResult:");
                serverGraph.AddServer(server1, server2);
                
            }

            else if (input == "2")
            {
                Console.WriteLine("Enter your website name: ");
                string websiteName = Console.ReadLine();
                Console.WriteLine("Enter the server you want to host your webiste at: ");
                string hostingServer = Console.ReadLine();
                Console.WriteLine("\nResult: ");
                WebPage page = new WebPage(websiteName, hostingServer);
            }
            else if (input == "3")
            {   
                Console.WriteLine("Enter starting server: ");
                string startingServer = Console.ReadLine();
                Console.WriteLine("Enter the server you want to get to: ");
                string serverToGetTo = Console.ReadLine();
                Console.WriteLine("\nResult: ");
                Console.WriteLine(serverGraph.ShortestPath(startingServer,serverToGetTo));
                
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

