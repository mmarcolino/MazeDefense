using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Colorblind_Menu : MonoBehaviour
{
   public GameObject obj;
    public void open()
    {
        
        if(!obj.activeSelf)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
        
    }
}
