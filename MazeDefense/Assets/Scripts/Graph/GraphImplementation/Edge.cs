using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    //V�rtice V
    public Node startNode;
    //V�rtice w
    public Node endNode;

    public Edge(Node v, Node w)
    {
        startNode = v;
        endNode = w;
    }
}
