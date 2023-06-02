using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Gate : MonoBehaviour
{
    public Transform first_waypoint; //conexão padrão
    public Transform next_waypoint;
    [HideInInspector]
    public Boolean changed;
    [HideInInspector]
    public Boolean open;
    [HideInInspector]
    public List<Transform> adjacencyList;
    // Start is called before the first frame update
    void Start()
    {
        changed = false;
        open = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ChangeTileOnClick>().clickGate && gameObject.GetComponent<ChangeTileOnClick>().can_change == false)
        {
            gameObject.GetComponent<ChangeTileOnClick>().clickGate = false;
            changed = true;
            open = false;
            adjacencyList = first_waypoint.GetComponent<Waypoint>().next_waypoints.ToList();
            adjacencyList.Remove(next_waypoint);
            first_waypoint.GetComponent<Waypoint>().next_waypoints = adjacencyList.ToArray();
        }
        else if (gameObject.GetComponent<ChangeTileOnClick>().finished == true)
        {
            gameObject.GetComponent<ChangeTileOnClick>().finished = false;
            changed = true;
            open = true;
            adjacencyList = first_waypoint.GetComponent<Waypoint>().next_waypoints.ToList();
            adjacencyList.Add(next_waypoint);
            first_waypoint.GetComponent<Waypoint>().next_waypoints = adjacencyList.ToArray();
        }
    }
}
