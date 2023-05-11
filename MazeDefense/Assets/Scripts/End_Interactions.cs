using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Interactions : MonoBehaviour
{
    private int life;
    private void Start()
    {
        life = 10;
    }
    
    
    public void reduce_life()
    {
        life--;
        Debug.Log("Vida Atual" + life);
        if (life == 0)
        {
            end_scene();
        }
    }
    private void end_scene()
    {

    }

}
