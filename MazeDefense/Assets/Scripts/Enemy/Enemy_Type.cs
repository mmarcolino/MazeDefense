using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type : MonoBehaviour
{
    public string type;
   
    public void change_type(string new_type)
    {
        
        if (new_type.Equals("Red"))
        {

           SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
           spriteRenderer.color = Color.red;
           type = new_type;
           GetComponent<Enemy_Movement>().speed = 4f;
        }
        
    }
}
