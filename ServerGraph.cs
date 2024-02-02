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
        V = new WebServer[2];
        NumServers = 0;
    }

    private int FindServer(string name)
    {
        int i;
        for (i = 0; i < NumServers; i++)
        {
            if (V[i].Name == name)
                return i;
        }
        return -1;
    }

    private void DoubleCapacity()
    {
        int newCapacity = V.Length * 2;
        while (V.Length < newCapacity)
        {
            V.Append(new WebServer(""));
        }
    }

    public bool AddServer(string name, string other)
    {
        int i, j;

        if ((i = FindServer(name)) > -1 && (j = FindServer(other)) > -1)
        {
            if (!V[i].E[j])
            {
                V[i].E[j] = true;
                return true;
            }
        }
        Console.WriteLine("no server ");
        return false;
    }

    public bool AddWebPage(WebPage w, string name)
    {
        int i;

        if ((i = FindServer(name)) > -1)
        {
            V[i].P.Add(w);
            return true;
        }
        return false;
    }

    public bool RemoveServer(string name, string other)
    {
        int i;
        int j = FindServer(other);

        if ((i = FindServer(name)) > -1)
        {
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
        // Mark all the vertices as not visited
		bool[] visited = new bool[V];
		int[] disc = new int[V];
		int[] low = new int[V];
		int[] parent = new int[V];
		bool[] ap = new bool[V]; // To store articulation points
		string[] result = new string[V];
		int j = 0;

		// Initialize parent and visited, and
		// ap(articulation point) arrays
		for (int i = 0; i < V; i++)
		{
			parent[i] = NIL;
			visited[i] = false;
			ap[i] = false;
		}

		// Call the recursive helper function to find articulation
		// points in DFS tree rooted with vertex 'i'
		for (int i = 0; i < V; i++)
			if (visited[i] == false)
				CSRecurse(i, visited, disc, low, parent, ap);

		// Now ap[] contains articulation points, print them
		for (int i = 0; i < V; i++)
		{
			if (ap[i] == true) 
			{
				Console.Write(i + " ");
				result[j] = i.ToString();
				j++;

			}

		}
		return result;
    }
    
    // A recursive function that find articulation points using DFS
	// u --> The vertex to be visited next
	// visited[] --> keeps track of visited vertices
	// disc[] --> Stores discovery times of visited vertices
	// parent[] --> Stores parent vertices in DFS tree
	// ap[] --> Store articulation points
	void CSRecurse(int u, bool[] visited, int[] disc,
				int[] low, int[] parent, bool[] ap)
	{

		// Count of children in DFS Tree
		int children = 0;

		// Mark the current node as visited
		visited[u] = true;

		// Initialize discovery time and low value
		disc[u] = low[u] = ++time;

		// Go through all vertices adjacent to this
		foreach (int i in adj[u])
		{
			int v = i; // v is current adjacent of u

			// If v is not visited yet, then make it a child of u
			// in DFS tree and recur for it
			if (!visited[v])
			{
				children++;
				parent[v] = u;
				CSRecurse(v, visited, disc, low, parent, ap);

				// Check if the subtree rooted with v has
				// a connection to one of the ancestors of u
				low[u] = Math.Min(low[u], low[v]);

				// u is an articulation point in following cases

				// (1) u is root of DFS tree and has two or more children.
				if (parent[u] == NIL && children > 1)
					ap[u] = true;

				// (2) If u is not root and low value of one of its child
				// is more than discovery value of u.
				if (parent[u] != NIL && low[v] >= disc[u])
					ap[u] = true;
			}

			// Update low value of u for parent function calls.
			else if (v != parent[u])
				low[u] = Math.Min(low[u], disc[v]);
		}
	}

    public int ShortestPath(string from, string to)
    {
        return 0;
    }

    public void PrintGraph()
    {
        for (int i = 0; i < NumServers; i++)
        {
            Console.WriteLine(V[i].Name);
            for (int j = 0; j < NumServers; j++)
            {
                Console.WriteLine("(" + V[i].Name + "," + V[i].E[j] + "," + V[i].P + ")");
            }
        }
    }



}

class Program
{
    static void Main()
    {
        // Step 1: Instantiate a server graph and a web graph
        ServerGraph serverGraph = new ServerGraph();
        WebGraph webGraph = new WebGraph();
        WebPage page1 = new WebPage("name1", "localhost");
        // Step 2: Add three servers
        serverGraph.AddServer("Server1", "Server1");

        Console.WriteLine(serverGraph.AddConnection("server1", "server2"));
        serverGraph.PrintGraph();
        Console.WriteLine(serverGraph.RemoveServer("server1", "server2"));
        Console.WriteLine(serverGraph.AddWebPage(page1,"name"));


        //Take a look at this method...
        serverGraph.PrintGraph();
    }

}

