using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rotate_Path : MonoBehaviour
{
    public Transform this_waypoint;
    public int number_of_connections;
    public List<Transform> adjacent_waypoints_sentido_horario;
    [HideInInspector]
    public Boolean rotated;
    [HideInInspector]
    public List<Transform> adjacencyList;


    // Start is called before the first frame update
    void Start()
    {
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ChangeTileOnClick>().click) 
        {
            adjacencyList = new List<Transform>();
            gameObject.GetComponent<ChangeTileOnClick>().click = false;
            rotated = true; //sinaliza que houve rotação
            for (int i = 0; i < number_of_connections - 1; i++)
            {
                Transform wp = adjacent_waypoints_sentido_horario.First();
                adjacent_waypoints_sentido_horario.RemoveAt(0);
                adjacent_waypoints_sentido_horario.Add(wp);
            }
            for (int i = 0;i < number_of_connections - 1; i++) 
            {
                adjacencyList.Insert(i, adjacent_waypoints_sentido_horario[i]);
            }
            this_waypoint.GetComponent<Waypoint>().next_waypoints = adjacencyList.ToArray();
        }
    }
}
