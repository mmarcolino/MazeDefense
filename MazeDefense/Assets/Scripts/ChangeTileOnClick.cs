using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTileOnClick : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector3Int tilePosition;
    [SerializeField] private TileBase newTileSprite;
    
    private TileBase originalTileSprite;

    private void Awake()
    {
        originalTileSprite = tileMap.GetTile(tilePosition);
    }

    private void OnMouseDown()
    {
        if (tileMap.GetTile(tilePosition) == originalTileSprite)
        {
            tileMap.SetTile(tilePosition, newTileSprite);
        }
        else
        {
            tileMap.SetTile(tilePosition, originalTileSprite);
        }
    }
}
