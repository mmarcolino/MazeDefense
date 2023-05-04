using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button_of_Gate : MonoBehaviour
{

    private void OnMouseDown()
    {
        Transform pai = transform.parent;
        for (int i = 1;i < pai.childCount; i++)
        {
        transform.parent.GetChild(i).GetComponent<Button_Gate>().change_state();
        }
    }
}
