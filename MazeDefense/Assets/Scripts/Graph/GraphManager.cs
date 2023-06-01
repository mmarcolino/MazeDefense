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
            if(r.GetComponent<ChangeTileOnClick>().removeLeft == true)
            {
                Transform wp = r.GetComponent<ChangeTileOnClick>().this_waypoint;
                Transform left = r.GetComponent<ChangeTileOnClick>().left_waypoint;
                Transform right = r.GetComponent<ChangeTileOnClick>().right_waypoint;
                graph.RemoveEdge(wp, left);
                graph.AddEdge(wp, right);
                flag = true;
                foreach (Enemy_Spawner spawner in enemySpawners)
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Transform enemy = spawner.transform.GetChild(i);
                        List<Transform> path = new List<Transform>(getPath(enemy.GetComponent<Enemy_Movement>().target, enemy.GetComponent<Enemy_Movement>().final_waypoint));
                        enemy.GetComponent<Enemy_Movement>().path = path;
                        enemy.GetComponent<Enemy_Movement>().counter = 0;
                    }
                }
                r.GetComponent<ChangeTileOnClick>().removeLeft = false;
            }
            else if(r.GetComponent<ChangeTileOnClick>().removeRight == true)
            {
                Transform wp = r.GetComponent<ChangeTileOnClick>().this_waypoint;
                Transform right = r.GetComponent<ChangeTileOnClick>().right_waypoint;
                Transform left = r.GetComponent<ChangeTileOnClick>().left_waypoint;
                graph.RemoveEdge(wp, right);
                graph.AddEdge(wp, left);
                flag = true;
                foreach (Enemy_Spawner spawner in enemySpawners)
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Transform enemy = spawner.transform.GetChild(i);
                        List<Transform> path = new List<Transform>(getPath(enemy.GetComponent<Enemy_Movement>().target, enemy.GetComponent<Enemy_Movement>().final_waypoint));
                        enemy.GetComponent<Enemy_Movement>().path = path;
                        enemy.GetComponent<Enemy_Movement>().counter = 0;
                    }
                }
                r.GetComponent<ChangeTileOnClick>().removeRight = false;
            }
        }
    }

    public List<Transform> getPath(Transform first, Transform final)
    {
        List<Transform>  path = graph.AStar(first, final);
        return path;
    }
}
