using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    public bool isPlacable;
    [SerializeField] GameObject towerPrefab;
    GridManager gridManager;
    Vector2Int coordinates;
    Pathfinder pathFinder;
    /*public event Action blockpath;*/
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
        coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    //will have to change here
    private void OnMouseDown()
    {
      /*  blockpath?.Invoke();*/
        //trigger the event here pathfinder will block path
        //Debug.Log("the iswalakeble "+gridManager.getGrid[coordinates].isWalkable);
        if (gridManager.getGrid[coordinates].isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            
            GameObject.Instantiate(towerPrefab,transform.position,Quaternion.identity);
            isPlacable = false;
        }
    }
}
