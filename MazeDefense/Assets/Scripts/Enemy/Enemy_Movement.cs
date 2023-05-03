using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Movement : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    [SerializeField] Transform starting_waypoint;
    [SerializeField] public int direction;
  
    

    void Start()
    {
        starting_waypoint = GameObject.Find("Waypoints").transform.GetChild(0);
        direction = 0;
        target = starting_waypoint;
        

    }

    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
       
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Waypoint")
        {

            
            target = collision.GetComponent<Waypoint>().next_waypoints[direction].transform;
            direction = 0;
        }
        if (collision.tag == "Rotation_Path")
        {
            
            direction = collision.GetComponent<Change_Direction>().path_direction;

        }
    }
}
