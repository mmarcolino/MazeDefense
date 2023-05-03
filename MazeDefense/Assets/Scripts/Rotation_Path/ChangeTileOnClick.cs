using System;
using System.Collections;
using UnityEngine;

public class ChangeTileOnClick : MonoBehaviour
{
    [SerializeField] Sprite original_sprite;
    [SerializeField] Sprite new_sprite;
    public float delay_time;
    public GameObject visual;
    private bool can_change;
    private void Start()
    {
        SpriteRenderer spriteRenderer = visual.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = original_sprite;
        can_change = true;
       
    }
    private void OnMouseDown()
    {

        if (can_change) {
            StartCoroutine(timer_change_sprite());
        }
        

    }
    
    private IEnumerator timer_change_sprite()
    {
        change_sprite();
        can_change = false;
        yield return new WaitForSeconds(delay_time);
        can_change = true;
    }
    public void change_sprite()
    {
        if (visual.GetComponent<SpriteRenderer>().sprite == original_sprite)
        {

            visual.GetComponent<SpriteRenderer>().sprite = new_sprite;
        }
        else
        {

            visual.GetComponent<SpriteRenderer>().sprite = original_sprite;
        }
    }
}
