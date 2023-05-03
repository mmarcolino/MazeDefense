using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collisions : MonoBehaviour
{
   
    float time;
    bool counting_timer;
    float speed_memory;
    private void Start()
    {


        time = 0;
        counting_timer = false;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        trap_kill_enemy(collision);
        end_kill_enemy(collision);
       
    }
   
    private void trap_kill_enemy(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {

            if (collision.GetComponent<Trap_Type>().type.Equals(gameObject.GetComponent<Enemy_Type>().type))
            {
                Destroy(gameObject);
            }
                    
        }
    }
    private void end_kill_enemy(Collider2D collision)
    {
        if(collision.tag == "End")
        {
            Destroy(gameObject);
        }
    }
}
