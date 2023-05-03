using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    public float movespeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W)){

            inputVector.y = +1;
            Debug.Log("tempo     ");

        }
        if (Input.GetKey(KeyCode.S))
        {

            inputVector.y = -1;
            Debug.Log("tempo     ");
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;


        }
        if (Input.GetKey(KeyCode.D))
        {

            inputVector.x = +1;

        }
        inputVector = inputVector.normalized;

        Vector3 move = new Vector3(inputVector.x,inputVector.y);
        float playerSize = .7f;
        bool canMove = !Physics.Raycast(transform.position, move, playerSize);
        if (canMove)
        {
            transform.position += move * Time.deltaTime * movespeed;

        }

       

    }
}
