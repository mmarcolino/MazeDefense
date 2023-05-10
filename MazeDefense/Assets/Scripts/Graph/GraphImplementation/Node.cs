using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //custos
    public float g, h;
    //Lista de Adjacencia
    public List<Edge> edgeList = new List<Edge>();
    //Transform (wp)
    Transform waypoint;
    //construtor recebe um waypoint na forma de Transform
    public Node (Transform wp)
    {
        waypoint = wp;
    }

    public Transform getWaypoint() { return waypoint; }
}
