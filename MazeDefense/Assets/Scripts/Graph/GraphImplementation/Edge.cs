using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    //Vértice V
    public Node startNode;
    //Vértice w
    public Node endNode;

    public Edge(Node v, Node w)
    {
        startNode = v;
        endNode = w;
    }
}
