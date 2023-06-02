using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    //Lista de arestas
    List<Edge> edges = new List<Edge>();
    //Lista de vértices (é preciso uma classe própria devido ao custo do A*)
    List<Node> nodes = new List<Node>();
    //Lista do melhor caminho
    List<Transform> path = new List<Transform>();

    //Adiciona vértice na lista de vértices, é chamada pelo GraphManager ao construir o grafo
    public void AddVertex(Transform wp)
    {
        Node vertex = new Node(wp);
        nodes.Add(vertex);
    }

    //Adiciona aresta 
    public void AddEdge(Transform from, Transform to)
    {
        Node v = FindNode(from); //recupera o waypoint (transform) do vértice v
        Node w = FindNode(to);  //recupera o waypoint (transform) do vértice w

        //Se os waypoints forem encontrados, criamos uma aresta entre eles
        if (v.getWaypoint() != null && w.getWaypoint() != null)
        {
            Edge e = new Edge(v, w);
            //adicionamos na lista de arestas
            edges.Add(e);
            //adicionamos na lista de adjacência do vértice
            v.edgeList.Add(e);
        }
        //TODO implementar código para imprimir gizmos para visualizar a aresta no projeto
    }

    public void RemoveEdge(Transform from, Transform to) 
    {
        Node v = FindNode(from); //recupera o waypoint (transform) do vértice v
        Node w = FindNode(to);  //recupera o waypoint (transform) do vértice w
        Edge edge = null;

        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].startNode.getWaypoint() == v.getWaypoint() && edges[i].endNode.getWaypoint() == w.getWaypoint())
            {
                edge = edges[i];
                edges.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < v.edgeList.Count; i++)
        {
            if (v.edgeList[i].endNode.getWaypoint() == w.getWaypoint())
            {
                v.edgeList.RemoveAt(i);
                break;
            }
        }
    }

    //Função de encontrar o waypoint (transform)
    public Node FindNode(Transform wp)
    {
        foreach (Node n in nodes)
        {
            if (n.getWaypoint() == wp)
                return n;
        }
        return null;
    }

    public Edge FindEdge(Transform from, Transform to)
    {
        foreach (Edge e in edges)
        {
            if (e.startNode.getWaypoint() == from && e.endNode.getWaypoint() == to)
                return e;
        }
        return null;
    }

    //Função para calcular a distância entre os waypoints (comprimento da aresta)

    //Transform[]
    public List<Transform> AStar(Transform start, Transform end)
    {
        Node first = FindNode(start); //vértice de onde está começando
        Node final = FindNode(end); //vértice onde queremos ir
        if (start == null ||  end == null) //verifica se não estão vazios
        {
            return null; //retorna null
        }

        List<Node> open = new List<Node>(); //incializamos uma lista de vértices visitados
        List<Transform> openAsTransform = new List<Transform>(); //gambiarra 
        List<Node> closed = new List<Node>(); //incializamos uma lista de vértices explorados
        List<Transform> closedAsTransform = new List<Transform>(); // gambiarra para closed
        float gScore = 0;
        float hScore = 0;
        float fScore = 0;

        //Os custos do vértice de inicio
        first.g = 0; //custo g = distância do primeiro vértice até o atual
        first.h = Vector3.Distance(first.getWaypoint().position, final.getWaypoint().position); //custo h = distância do vértice atual até o final
        first.f = first.h; // Custo final é a soma dos dois

        open.Add(first); //coloca o primeiro vértice na lista de vértices a serem analisados
        while (open.Count > 0) //enquanto houver vértices abertos
        {
            int i = lowestFCost(open); //pega o índice menor custo f da lista de vértices em aberto
            Node thisNode = open[i]; //escolhe o vértice do índice
            if (thisNode.getWaypoint() == final.getWaypoint()) //Verficia se chegamos no final, se sim vamos salvar o caminho
            {
                ReconcstructPath(first, final);
                return path; //retorna o caminho
            }
            open.RemoveAt(i); //removemos o vértice da lista de em aberto, uma vez que já o exploramos
            Node neighbour;
            foreach (Edge e in thisNode.edgeList) //para cada aresta na sua lista de adjacencia
            {
                neighbour = e.endNode; //recuperamos o vizinho 

                if (closedAsTransform.Contains(neighbour.getWaypoint()))
                    continue; //se já tivermos visitado o vizinho, o ignoramos

                gScore = thisNode.g + Vector3.Distance(thisNode.getWaypoint().position, neighbour.getWaypoint().position); //incrementamos a distância do inicio até o atual com base do vértice passado
                hScore = Vector3.Distance(neighbour.getWaypoint().position, final.getWaypoint().position); // calculamos a heuristica (disância até o final)
                fScore = gScore + hScore; //a soma dos dois
                if(fScore < neighbour.f || ! openAsTransform.Contains(neighbour.getWaypoint())) //se f for menor que a do vizinho ou se o vizinho não estiver na lista de descorbertos
                {
                    //Setandos valores para o vizinho
                    neighbour.g = gScore; 
                    neighbour.h = hScore;
                    neighbour.f = fScore;
                    //Setando o vértice de origem do vizinho
                    neighbour.cameFrom = thisNode;
                    if (! openAsTransform.Contains(neighbour.getWaypoint()))
                    {
                        open.Add(neighbour); //adiciona o vizinho na lista de visitados
                        openAsTransform.Add(neighbour.getWaypoint());
                    }       
                }
            }
            closed.Add(thisNode); //adicionamos o vértice para a lista de explorados
            closedAsTransform.Add(thisNode.getWaypoint());
        }
        return null; //null
    }

    public void ReconcstructPath (Node start, Node end)
    {
        path.Clear(); //limpamos o caminho caso tenha sido procurado uma vez
        path.Add(end.getWaypoint()); //adicionamos o ultimo (vamos fazer o caminho reverso pelos pais)

        var p = end.cameFrom;
        while (p.getWaypoint() != start.getWaypoint() && p.getWaypoint() != null) 
        {
            path.Insert(0, p.getWaypoint()); //vamos adicionando na primeira posicao o vertice mais atual do caminho do final ate o inicio
            p = p.cameFrom;
        }
        path.Insert(0, start.getWaypoint());
    }

    int lowestFCost(List<Node> list)
    {
        //inicializamos as variáveis
        float lowestf = 0;
        int lowestfNodePosition = 0;

        lowestf = list[0].f; //atribuimos o custo final do primeiro vértice ao menor valor

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].f < lowestf)
            {
                lowestf = list[i].f;
                lowestfNodePosition = i;
            }
            //incializamos com 0 e somamos no final de cada iteracao
        }

        return lowestfNodePosition;
    }
}
