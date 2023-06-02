using System;
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
    public List<GameObject> rotate;
    public List<GameObject> gates;
    [HideInInspector]
    public Boolean flag;
    public List<Enemy_Spawner> enemySpawners;
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
        foreach (GameObject r in rotate)
        {
            Rotate_Path rp = r.GetComponent<Rotate_Path>();
            if (rp.rotated == true)
            {
                rp.rotated = false;
                List<Transform> list = new List<Transform>();
                Node vertex = graph.FindNode(rp.this_waypoint);
                List<Edge> edges = new List<Edge>();
                foreach (Edge edge in vertex.edgeList)
                {
                    if(!rp.adjacencyList.Contains(edge.endNode.getWaypoint()))
                    {
                        edges.Add(new Edge(vertex, edge.endNode));
                    }
                    else 
                        list.Add(edge.endNode.getWaypoint());
                }
                foreach(Edge edge in edges)
                {
                    graph.RemoveEdge(edge.startNode.getWaypoint(), edge.endNode.getWaypoint());
                }
                foreach (Transform teste in rp.adjacencyList)
                {
                    if(!list.Contains(teste.transform))
                    {
                        graph.AddEdge(vertex.getWaypoint(), teste.transform);
                    }
                }

                foreach (Enemy_Spawner spawner in enemySpawners)
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Transform enemy = spawner.transform.GetChild(i);
                        IEnumerable<Transform> pathCollection = getPath(enemy.GetComponent<Enemy_Movement>().target, enemy.GetComponent<Enemy_Movement>().final_waypoint);
                        List<Transform> path = new List<Transform>();
                        if (pathCollection == null) Destroy(enemy.gameObject);
                        else path.AddRange(pathCollection);
                        enemy.GetComponent<Enemy_Movement>().path = path;
                        enemy.GetComponent<Enemy_Movement>().counter = 0;
                    }
                }
            }
        }
        foreach(GameObject gate in gates)
        {
            Gate gate_script = gate.GetComponent<Gate>();
            if (gate_script.changed && gate_script.open) 
            {
                gate_script.changed = false;
                graph.AddEdge(gate_script.first_waypoint, gate_script.next_waypoint);
                foreach (Enemy_Spawner spawner in enemySpawners)
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Transform enemy = spawner.transform.GetChild(i);
                        IEnumerable<Transform> pathCollection = getPath(enemy.GetComponent<Enemy_Movement>().target, enemy.GetComponent<Enemy_Movement>().final_waypoint);
                        List<Transform> path = new List<Transform>();
                        if (pathCollection == null) Destroy(enemy.gameObject);
                        else path.AddRange(pathCollection);
                        enemy.GetComponent<Enemy_Movement>().path = path;
                        enemy.GetComponent<Enemy_Movement>().counter = 0;
                    }
                }
            }
            else if (gate_script.changed && !gate_script.open)
            {
                gate_script.changed = false;
                Edge e = graph.FindEdge(gate_script.first_waypoint.transform, gate_script.next_waypoint.transform);
                graph.RemoveEdge(e.startNode.getWaypoint(), e.endNode.getWaypoint());

                foreach (Enemy_Spawner spawner in enemySpawners)
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Transform enemy = spawner.transform.GetChild(i);
                        IEnumerable<Transform> pathCollection = getPath(enemy.GetComponent<Enemy_Movement>().target, enemy.GetComponent<Enemy_Movement>().final_waypoint);
                        List<Transform> path = new List<Transform>();
                        if (pathCollection == null) Destroy(enemy.gameObject);
                        else path.AddRange(pathCollection);
                        enemy.GetComponent<Enemy_Movement>().path = path;
                        enemy.GetComponent<Enemy_Movement>().counter = 0;
                    }
                }
            }
        }
    }

    public List<Transform> getPath(Transform first, Transform final)
    {
        List<Transform>  path = graph.AStar(first, final);
        return path;
    }
}
