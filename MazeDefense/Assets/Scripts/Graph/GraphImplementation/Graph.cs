using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    //Lista de arestas
    public List<Edge> edges = new List<Edge>();
    //Lista de vértices (é preciso uma classe própria devido ao custo do A*)
    public List<Node> nodes = new List<Node> ();

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
            Edge e = new Edge (v, w);
            //adicionamos na lista de arestas
            edges.Add(e);
            //adicionamos na lista de adjacência do vértice
            v.edgeList.Add(e);
        }
        //TODO implementar código para imprimir gizmos para visualizar a aresta no projeto
    }

    //Função de encontrar o waypoint (transform)
    Node FindNode(Transform wp)
    {
        foreach (Node n in nodes)
        {
            if (n.getWaypoint() == wp)
                return n;
        }
        return null;
    }

    //Função para calcular a distância entre os waypoints (comprimento da aresta)
    float distance (Node v, Node w) 
    {
        return (Vector3.SqrMagnitude(v.getWaypoint().transform.position - w.getWaypoint().transform.position));
    }

    //Transform[]
    public void AStar ()
    {
        
    }
}
