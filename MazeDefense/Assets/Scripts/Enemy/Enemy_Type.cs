using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type : MonoBehaviour
{
    public string type;
    private float speed;
   
    public void change_type(string new_type)
    {
        if (new_type.Equals("White"))
        {

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            type = new_type;
            speed = 1f;
            GetComponent<Enemy_Movement>().speed = speed;
        }

        if (new_type.Equals("Red"))
        {

           SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
           spriteRenderer.color = Color.red;
           type = new_type;
            speed = 1f;
           GetComponent<Enemy_Movement>().speed = speed;
        }
        
    }
    public float get_speed()
    {
        return speed;
    }
}
