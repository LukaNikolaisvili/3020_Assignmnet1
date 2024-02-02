using System.Linq.Expressions;

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
        while (V.Length < newCapacity)
        {
            V.Append(new WebServer(""));
        }
    }

    public void Capacity2()
    {
        DoubleCapacity();
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

        //FInd server method has some issue needs to be taken a look
        int indexOfFrom = FindServer(from);
        int indexOfTo = FindServer(to);

        // Debugging statements to check the indices      
        Console.WriteLine("Index of from: " + indexOfFrom);
        Console.WriteLine("Index of to: " + indexOfTo);

        if (indexOfFrom != -1 && indexOfTo != -1)
        {
            V[indexOfFrom].E[indexOfTo] = true;
            return true;
        }
        return false;
    }



    public string[] CriticalServers()
    {
        string[] arr = { "hello world" };

        return arr;
    }

    public int ShortestPath(string from, string to)
    {
        return 0;
    }

    public new void PrintGraph()
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

        while(true){
            ServerGraph serverGraph = new ServerGraph();
            WebGraph webGraph = new WebGraph();
            
           
             Console.WriteLine("\n1: Add Server ");
             Console.WriteLine("2: Add Page ");
             Console.WriteLine("3: Add connection ");
             Console.WriteLine("exit: type [exit] to close the application ");

             for(int i =0; i<36; i++){
                Console.Write("-");
             }

            Console.WriteLine("\ntype the opertaion you would like to perform ");
            string input = Console.ReadLine();

             
             

 switch(input){
        case "1":
            Console.WriteLine("Enter 1st server name: ");
            string server1 = Console.ReadLine();
            Console.WriteLine("Enter 2nd server name: ");
            string server2 = Console.ReadLine();
            Console.WriteLine("\nResult:");
            serverGraph.AddServer(server1, server2);

            break;
        case "2":
            Console.WriteLine("Enter your website name: ");
            string websiteName = Console.ReadLine();
            Console.WriteLine("Enter the server you want to host your webiste at: ");
            string hostingServer = Console.ReadLine();
            Console.WriteLine("\nResult: ");
            WebPage page = new WebPage(websiteName, hostingServer);

            break;

        case "3":
            Console.WriteLine("Enter 1st server name to connect from: ");
            string serverFrom = Console.ReadLine();
            Console.WriteLine("Enter 2nd server name to connect to: ");
            string serverTo = Console.ReadLine();
            Console.WriteLine("\nResult:");
            serverGraph.AddServer(serverFrom, serverTo);

            break;
        
        case "exit":
             
             Environment.Exit(0);
            break;

  default:
    Console.WriteLine("'Oops you were close bud, try again!'");
    // code block
    break;
}

        }
        // Step 1: Instantiate a server graph and a web graph
        

        // Step 2: Add three servers
        
        // serverGraph.AddServer("Server2", "Server1");
        // serverGraph.AddServer("Server3", "Server2");

        // // 2. Add webpages
        // WebPage page1 = new WebPage("facebook", "server2");
        // WebPage page2 = new WebPage("Page2", "Server3");
        // WebPage page3 = new WebPage("Page3", "Server4");
        // WebPage page4 = new WebPage("Page4", "Server5");



        // Console.WriteLine(serverGraph.AddConnection("Server1", "Server2"));
        // serverGraph.AddConnection("Server1", "Server2");





        // webGraph.PrintGraph();

    }

}

