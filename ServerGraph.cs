
public class WebPage
{
public string Name {get; set;}
public string Server {get; set;}
public List<WebPage> E {get; set;}

public WebPage(string name, string host){

name = "test";
Console.WriteLine(name);
}
public int FindLink(string name){
    return 1;
}


}
public class WebGraph{
private List<WebPage> P;

// 2 marks
// Create an empty WebGraph
public WebGraph( ){
    
}
// 2 marks
// Return the index of the webpage with the given name; otherwise return -1
private int FindPage(string name){
    return 10;
}
// 4 marks
// Add a webpage with the given name and store it on the host server
// Return true if successful; otherwise return false
public bool AddPage(string name, string host, ServerGraph S){
    return false;
}
// 8 marks
// Remove the webpage with the given name, including the hyperlinks
// from and to the webpage
// Return true if successful; otherwise return false
public bool RemovePage (string name, ServerGraph S){
    return false;
}
// 3 marks
// Add a hyperlink from one webpage to another
// Return true if successful; otherwise return false
public bool AddLink (string from, string to){

    return false;
}
// 3 marks
// Remove a hyperlink from one webpage to another
// Return true if successful; otherwise return false
public bool RemoveLink (string from, string to){
    
    return false;
}
// 6 marks
// Return the average length of the shortest paths from the webpage with
// given name to each of its hyperlinks
// Hint: Use the method ShortestPath in the class ServerGraph
public float AvgShortestPaths(string name, ServerGraph S){
    return 1.2;
}
// 3 marks
// Print the name and hyperlinks of each webpage
public void PrintGraph( ){

}

}




public class ServerGraph
{
    // 3 marks
    private class WebServer
    {
        public string Name;
        public List<WebPage> P;
        public WebServer(string name)
        {
            Name = name;
            P = new List<WebPage>();
        }
    }
    private WebServer[] V;
    private bool[,] E;
    private int NumServers;

    // 2 marks
    // Create an empty server graph
    public ServerGraph()
    {
        V = new WebServer[2];
        E = new bool[2, 2];
        int NumServers = 0;

    }
    // 2 marks
    // Return the index of the server with the given name; otherwise return -1
    private int FindServer(string name)
    {
        int i;
        for (i = 0; i < NumServers; i++)
        {
            if (V[i].Equals(name))

                return i;
        }

        return -1;

    }
    // 3 marks
    // Double the capacity of the server graph with the respect to web servers
    private void DoubleCapacity()
    {


    }
    // 3 marks
    // Add a server with the given name and connect it to the other server
    // Return true if successful; otherwise return false
    public bool AddServer(string name, string other)
    {
        int i, j;

        if ((i = FindServer(name)) > -1 && (j = FindServer(other)) > -1)
        {
            if (E[i, j] == false)
            {
                // Update the connection status in the adjacency matrix
                E[i, j] = true;


                // Return true to indicate success
                return true;
            }
        }

        // Return false if conditions are not met
        Console.WriteLine("no server ");
        return false;
    }
    // 3 marks
    // Add a webpage to the server with the given name
    // Return true if successful; other return false
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
    // 4 marks
    // Remove the server with the given name by assigning its connections
    // and webpages to the other server
    // Return true if successful; otherwise return false
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
                    if (E[i, s] == true)
                    {
                        E[j, s] = true;

                        if (E[s, i] == true)
                        {
                            E[s, j] = true;
                        }
                    }
                    NumServers--;
                    return true;

                }
            }
        }
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

        if(indexOfFrom != -1 && indexOfTo != -1){
            
            E[indexOfFrom,indexOfTo] = true;

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

    }
    // Return the shortest path from one server to another
    // Hint: Use a variation of the breadth-first search
    public int ShortestPath(string from, string to)
    {

    }
    // 4 marks
    // Print the name and connections of each server as well as
    // the names of the webpages it hosts
    public void PrintGraph(){

            for (int i = 0; i < V.Count; i++){
                Console.WriteLine(V[i].Name);
            for (j = 0; j < V[i].E.Count; j++){
                Console.WriteLine("(" + V[i].Name + "," + V[i].E[j] + ","  + V[i].P + ")");
            
        }

    }
 }

class Program
{
    static void Main()
    {
        // Step 1: Instantiate a server graph and a web graph
        ServerGraph serverGraph = new ServerGraph();


        // Step 2: Add three servers
        serverGraph.AddServer("Server1", "Server1");

    }
}


}


