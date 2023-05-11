using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float life = 100f;
    public Image lifeBar;
    public void Start()
    {
        life = 100;
    }
    
    
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            reduce_life();
        }

        if (life<=0){
            SceneManager.LoadScene("Fase_de_teste");
        }
    }
    public void reduce_life()
    {
        life -= 10;
        lifeBar.fillAmount = life / 100f;
    }

}
