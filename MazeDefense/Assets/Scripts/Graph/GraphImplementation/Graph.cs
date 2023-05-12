using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    //Lista de arestas
    List<Edge> edges = new List<Edge>();
    //Lista de v�rtices (� preciso uma classe pr�pria devido ao custo do A*)
    List<Node> nodes = new List<Node>();
    //Lista do melhor caminho
    List<Transform> path = new List<Transform>();

    //Adiciona v�rtice na lista de v�rtices, � chamada pelo GraphManager ao construir o grafo
    public void AddVertex(Transform wp)
    {
        Node vertex = new Node(wp);
        nodes.Add(vertex);
    }

    //Adiciona aresta 
    public void AddEdge(Transform from, Transform to)
    {
        Node v = FindNode(from); //recupera o waypoint (transform) do v�rtice v
        Node w = FindNode(to);  //recupera o waypoint (transform) do v�rtice w

        //Se os waypoints forem encontrados, criamos uma aresta entre eles
        if (v.getWaypoint() != null && w.getWaypoint() != null)
        {
            Edge e = new Edge(v, w);
            //adicionamos na lista de arestas
            edges.Add(e);
            //adicionamos na lista de adjac�ncia do v�rtice
            v.edgeList.Add(e);
        }
        //TODO implementar c�digo para imprimir gizmos para visualizar a aresta no projeto
    }

    public void RemoveEdge(Transform from, Transform to) 
    {
        Node v = FindNode(from); //recupera o waypoint (transform) do v�rtice v
        Node w = FindNode(to);  //recupera o waypoint (transform) do v�rtice w
        Edge edge = null;

        foreach (Edge e in edges)
        {
            if (e.startNode.getWaypoint() == v.getWaypoint() && e.endNode.getWaypoint() == w.getWaypoint())
            {
                edge = e;
                edges.Remove(e);
                v.edgeList.Remove(e);
                break;
            }
        }
    }

    //Fun��o de encontrar o waypoint (transform)
    public Node FindNode(Transform wp)
    {
        foreach (Node n in nodes)
        {
            if (n.getWaypoint() == wp)
                return n;
        }
        return null;
    }

    //Fun��o para calcular a dist�ncia entre os waypoints (comprimento da aresta)
    float distance(Node v, Node w)
    {
        return (Vector3.SqrMagnitude(v.getWaypoint().transform.position - w.getWaypoint().transform.position));
    }

    //Transform[]
    public List<Transform> AStar(Transform start, Transform end)
    {
        Node first = FindNode(start); //v�rtice de onde est� come�ando
        Node final = FindNode(end); //v�rtice onde queremos ir
        if (start == null ||  end == null) //verifica se n�o est�o vazios
        {
            return null; //retorna null
        }

        List<Node> open = new List<Node>(); //incializamos uma lista de v�rtices visitados
        List<Transform> openAsVertex = new List<Transform>(); //gambiarra 
        List<Node> closed = new List<Node>(); //incializamos uma lista de v�rtices explorados
        float gScore = 0;
        float hScore = 0;
        float fScore = 0;
        float lastFscore = 0;
        bool isBetter;

        //Os custos do v�rtice de inicio
        first.g = 0; //custo g = dist�ncia do primeiro v�rtice at� o atual
        first.h = distance(first, final); //custo h = dist�ncia do v�rtice atual at� o final
        first.f = first.h; // Custo final � a soma dos dois

        open.Add(first); //coloca o primeiro v�rtice na lista de v�rtices a serem analisados
        while (open.Count > 0) //enquanto houver v�rtices abertos
        {
            int i = lowestFCost(open); //pega o �ndice menor custo f da lista de v�rtices em aberto
            Node thisNode = open[i]; //escolhe o v�rtice do �ndice
            if (thisNode.getWaypoint() == final.getWaypoint()) //Verficia se chegamos no final, se sim vamos salvar o caminho
            {
                ReconcstructPath(first, final);
                return path; //retorna o caminho
            }
            open.RemoveAt(i); //removemos o v�rtice da lista de em aberto, uma vez que j� o exploramos
            Node neighbour;
            foreach (Edge e in thisNode.edgeList) //para cada aresta na sua lista de adjacencia
            {
                neighbour = e.endNode; //recuperamos o vizinho 

                if (closed.IndexOf(neighbour) > 1)
                    continue; //se j� tivermos visitado o vizinho, o ignoramos

                gScore = thisNode.g + distance(thisNode, neighbour); //incrementamos a dist�ncia do inicio at� o atual com base do v�rtice passado
                hScore = distance(neighbour, final);
                fScore = gScore + hScore;

                if (openAsVertex.IndexOf(neighbour.getWaypoint()) == -1) //se o vizinho n�o tiver sido visitado
                {
                    open.Add(neighbour); //adiciona o vizinho na lista de visitados
                    openAsVertex.Add(neighbour.getWaypoint());
                    isBetter = true; //achamos um caminho melhor
                }
                else if (fScore < lastFscore) 
                {
                    isBetter = true;  //achamos um caminho melhor
                    open.Add(neighbour); //adiciona o vizinho na lista de visitados
                    openAsVertex.Add(neighbour.getWaypoint());
                }
                else
                    isBetter = false;  //n�o achamos um caminho melhor

                if (isBetter) //se for uma melhor solu��o 
                {
                    neighbour.cameFrom = thisNode; //atualiza a origem
                    neighbour.g = gScore; //atualiza o custo g
                    neighbour.h = distance(thisNode, final); //atualiza o custo h 
                    neighbour.f = neighbour.g + neighbour.h; //atualiza o custo final
                    lastFscore = fScore;
                }

            }
            closed.Add(thisNode); //adicionamos o v�rtice para a lista de explorados
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
        //inicializamos as vari�veis
        float lowestf = 0;
        int lowestfNodePosition = 0;

        lowestf = list[0].f; //atribuimos o custo final do primeiro v�rtice ao menor valor

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
