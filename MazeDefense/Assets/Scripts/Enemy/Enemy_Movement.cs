using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public Transform target;
    [SerializeField] public Transform starting_waypoint;
    [SerializeField] public Transform final_waypoint;
    [SerializeField] public int direction;
    public List<Transform> path;
    public Transform currentWp;
    GraphManager gm;
    public int counter = 0;
    private bool movendoDireita;



    void Start()
    {
        currentWp = starting_waypoint;
        gm = GameObject.Find("GraphManager").GetComponent<GraphManager>();
        path = gm.getPath(starting_waypoint, final_waypoint);
        //starting_waypoint = GameObject.Find("Waypoints").transform.GetChild(0);
        direction = 0;
        target = path[0];
    }

    void Update()
    {
        
        Vector3 dir = target.position - transform.position;
        rotate(dir);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Waypoint")
        {
            currentWp = path[counter];
            counter++;
            target = path[counter];
            direction = 0;
        }

        //if (collision.tag == "Rotation_Path")
        //{  
        //    direction = collision.GetComponent<Change_Direction>().path_direction;
        //}
    }

    private void rotate(Vector3 dir)
    {
        
        

        if (dir.x < 0)
        {
            movendoDireita = false;
           
        }
        else if (dir.x > 0)
        {
            movendoDireita = true;
            
        }

       
        if (movendoDireita)
        {
           
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        else
        {
           
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
    }

}