using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Direction : MonoBehaviour
{
    public int path_direction;
    private void OnMouseDown()
    {


        if (path_direction == 0)
        {
            path_direction = 1;
            
        }
        else
        {
            path_direction = 0;
            
        }


    }
}

