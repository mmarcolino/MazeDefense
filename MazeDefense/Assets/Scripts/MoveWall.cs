using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MoveWall : MonoBehaviour
{

    [SerializeField]
    private Tilemap tilemap;

    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         // Get the current cell position of the game object
    //         Vector3Int currentCell = tilemap.WorldToCell(transform.position);

    //         // Calculate the position of the target cell one tile to the left
    //         Vector3Int targetCell = currentCell + new Vector3Int(-1, 0, 0);

    //         // Get the world position of the target cell
    //         Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

    //         // Move the game object to the target position
    //         transform.position = targetPosition;
    //     }
    //     if (Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         // Get the current cell position of the game object
    //         Vector3Int currentCell = tilemap.WorldToCell(transform.position);

    //         // Calculate the position of the target cell one tile to the left
    //         Vector3Int targetCell = currentCell + new Vector3Int(1, 0, 0);

    //         // Get the world position of the target cell
    //         Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

    //         // Move the game object to the target position
    //         transform.position = targetPosition;
    //     }
    // }

    void OnMouseDown()
    {
        if (!isMoving)
        {
            // Get the current cell position of the game object
            Vector3Int currentCell = tilemap.WorldToCell(transform.position);

            // Calculate the position of the target cell one tile to the left
            Vector3Int targetCell = currentCell + new Vector3Int(1, 0, 0);

            // Get the world position of the target cell
            Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

            // Move the game object to the target position
            transform.position = targetPosition;

            // Set the flag to indicate that the game object is moving
            isMoving = true;

            // Call the ReturnToOriginalPosition method after 3 seconds
            Invoke("ReturnToOriginalPosition", 3f);
        }
    }

    private void ReturnToOriginalPosition()
    {
        // Move the game object back to the original position
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);

            // Calculate the position of the target cell one tile to the left
        Vector3Int targetCell = currentCell + new Vector3Int(-1, 0, 0);

            // Get the world position of the target cell
        Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

            // Move the game object to the target position
        transform.position = targetPosition;

        
        isMoving = false;
    }
}

