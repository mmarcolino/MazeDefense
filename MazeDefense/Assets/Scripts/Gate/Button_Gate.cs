using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Gate : MonoBehaviour
{
    public bool is_open;
    private float speed_memory;
    private Collider2D collider_memory;
    public Sprite gate_open;
    public Sprite gate_closed;
    public SpriteRenderer gate_visual;

    private void Start()
    {
        if(is_open)
        {
            gate_visual.sprite = gate_open;
        }
        else
        {
            gate_visual.sprite = gate_closed;
        }
    }
    public void change_state()
    {
        if (is_open)
        {
            Debug.Log("Fechando");
            is_open = false;
            gate_visual.sprite = gate_closed;
        }
        else
        {
            Debug.Log("Abrindo");
            is_open = true;
            gate_visual.sprite = gate_open;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Enemy")
        {
            Debug.Log("ISOPEN ANTES " + is_open);
            if (is_open == false)
            {
                Debug.Log("TESTAO " + is_open);
                collision.GetComponent<Enemy_Movement>().speed = 0;
                StartCoroutine(wait_until_open(collision));
            }
          
        }
    }
 IEnumerator wait_until_open(Collider2D collision)
    {
        
        yield return new WaitUntil(() => is_open == true);
        collision.GetComponent<Enemy_Movement>().speed = collision.GetComponent<Enemy_Type>().get_speed();
    }
}
