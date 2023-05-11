using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphManager : MonoBehaviour
{
    public GameObject waypoints;
    public GameObject specialWaypoint;
    [HideInInspector] Graph graph;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gph = new GameObject();
        gph.AddComponent<Graph>(); //gambiarra para inicializar um objeto
        graph = gph.GetComponent<Graph>();
        if (waypoints.transform.childCount != 0) //verifica se há waypoints
        {
            for (int i = 0; i < waypoints.transform.childCount; i++) //adiciona os vértices
            {
                Transform vertex = waypoints.transform.GetChild(i);
                graph.AddVertex(vertex);
            }
            for (int i = 0; i < waypoints.transform.childCount; i++) //adiciona as arestas
            {
                Transform vertex = waypoints.transform.GetChild(i);
                Transform[] adjacencyList = vertex.GetComponent<Waypoint>().next_waypoints;
                foreach (Transform nextVertex in adjacencyList)
                {
                    if (nextVertex.Equals(specialWaypoint.transform))
                    {
                        graph.AddVertex(nextVertex);
                    }
                    graph.AddEdge(vertex, nextVertex);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Transform> getPath(Transform first, Transform final)
    {
        List<Transform>  path = graph.AStar(first, final);
        return path;
    }
}
