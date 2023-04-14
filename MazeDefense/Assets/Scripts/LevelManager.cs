using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
[SerializeField]
private GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel(){
        
        float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for(int y = 0; y < 5; y++){
            for(int x = 0; x < 5; x++){
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(tileSize*x, tileSize*y, 0); 
            }
        }
    }
}
