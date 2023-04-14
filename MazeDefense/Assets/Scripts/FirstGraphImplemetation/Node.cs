using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Inicializando vari�veis
    public int gCost, hCost; //custos
    public bool obstacle; //marca se � ou n�o um obst�culo
    public Vector3 worldPosition; //Coordenadas
    public int GridX, GridY;
    public Node parent;

    public Node(bool _obstacle, Vector3 _worldPos, int _gridX, int _gridY)
    {
        this.obstacle = _obstacle;
        this.worldPosition = _worldPos;
        this.GridX = _gridX;
        this.GridY = _gridY;
    }

    public int FCost { get { return gCost + hCost; } }
    public void SetObstacle (bool isOb) { obstacle = isOb; }
}
