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
        serverGraph.AddServer("Server1", "Server2");
        serverGraph.AddServer("Server2", "Server1");
        serverGraph.AddServer("Server3", "Server2");

        // 2. Add webpages
        WebPage page2 = new WebPage("Page2", "Server2");
        WebPage page3 = new WebPage("Page3", "Server3");

        webGraph.AddPage("Page1", "Server1", serverGraph);
        webGraph.AddPage("Page2", "Server2", serverGraph);
        webGraph.AddPage("Page3", "Server3", serverGraph);

        // 3. Add connections between servers
        serverGraph.AddConnection("Server1", "Server2");
        serverGraph.AddConnection("Server2", "Server3");

        //Take a look at this method...
        serverGraph.PrintGraph();

         string[] result1 = serverGraph.CriticalServers();
        for (int i = 0; i < result1.Length; i++)
    {
        Console.Write(result1[i] + " ");
    }
    }

}

