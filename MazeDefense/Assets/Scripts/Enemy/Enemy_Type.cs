using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type : MonoBehaviour
{
    public string type;
    private float speed;
    public Sprite Warrior;
    public Sprite Archer;
   
    public void change_type(string new_type)
    {
        if (new_type.Equals("Warrior"))
        {

            type = new_type;
            speed = 1f;
            GetComponent<Enemy_Movement>().speed = speed;
            Sprite novaSprite = Warrior;
            GetComponentInChildren<SpriteRenderer>().sprite = novaSprite;
        }

        if (new_type.Equals("Archer"))
        {
            Debug.Log("Caiu aq");
            Sprite novaSprite = Archer;
            GetComponentInChildren<SpriteRenderer>().sprite = novaSprite;
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
