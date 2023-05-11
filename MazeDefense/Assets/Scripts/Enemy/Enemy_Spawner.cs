using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public Transform Enemy;
    public string[] type_enemy;
    public int[] n_enemys_per_wave;
    public float time_between_waves;
    public bool randomize_time_between_enemys;
    public int time_between_enemys;
    public int min_time_between_enemys;
    public int max_time_between_enemys;
    public Transform nearest_waypoint;
    public Transform final_waypoint;


    void Start()
    {
        StartCoroutine(spawner());
    }
    private IEnumerator spawner()
    {
        for (int i = 0;i< n_enemys_per_wave.Length; i++)
        {
            
            yield return StartCoroutine(wave_spawner(i));
            yield return new WaitForSeconds(time_between_waves);
            
        }
    }
    private IEnumerator wave_spawner(int pos)
    {
        Debug.Log("PROXIMA WAVE");
        for(int i = 0;i < n_enemys_per_wave[pos]; i++)
        {
            Transform new_enemy = Instantiate(Enemy, transform.position, transform.rotation);     
            new_enemy.GetComponent<Enemy_Type>().change_type(type_enemy[Random.Range(0, type_enemy.Length)]);
            new_enemy.GetComponent<Enemy_Movement>().starting_waypoint = nearest_waypoint;
            new_enemy.GetComponent<Enemy_Movement>().final_waypoint = final_waypoint;
            if (randomize_time_between_enemys)
            {
                yield return new WaitForSeconds(Random.Range(min_time_between_enemys, max_time_between_enemys));

            }else
            {
                yield return new WaitForSeconds(time_between_enemys);
            }
            
        }
    }
   
}
