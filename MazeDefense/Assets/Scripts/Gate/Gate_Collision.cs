using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Gate_Collision : MonoBehaviour
{
    private float speed_memory;
    private bool gate_open;
    private float delay_time;
   
    private float time;

    private void Start()
    {
        delay_time = gameObject.GetComponent<ChangeTileOnClick>().delay_time;
        gate_open = true;
        time = delay_time;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Enemy" && !gate_open)
        {
            StartCoroutine(gate_stop_enemy(collision));
        }
        

    }
    private void Update()
    {
        
        if (!gate_open)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0) {
            gameObject.GetComponent<ChangeTileOnClick>().change_sprite();
            gate_open = true;
            time = delay_time;
        }
        
    }
    public void OnMouseDown()
    {
        if(gate_open)
        {
            gate_open = false;
        }
    }
    
    private IEnumerator gate_stop_enemy(Collider2D collision)
    {
             speed_memory = collision.GetComponent<Enemy_Movement>().speed;
             collision.GetComponent<Enemy_Movement>().speed = 0;
             yield return new WaitForSeconds(time);
             collision.GetComponent<Enemy_Movement>().speed = speed_memory;
    }
}
