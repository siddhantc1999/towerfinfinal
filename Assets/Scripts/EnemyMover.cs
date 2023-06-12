using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Node> Path=new List<Node>();
    public List<Vector2Int> pathVector;
    GameObject pathLane;
    Pathfinder pathfinder;
    GridManager gridManager;
    public Vector3 endPosition;
    public Vector3 startPosition;
    public Vector3 presentPosition;
    public Vector3 whileendPosition;
    public float maintimer;
   /// //////

    // Start is called before the first frame update
    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        FindObjectOfType<Pathfinder>().regeneratepath += RegeneratePath;
       
    }
    void OnEnable()
    {
        //Debug.Log("here on enable");
        ReturnToStart(Vector2Int.zero);
        RegeneratePath();
    }
    //private void ReturnToStart(Vector2Int startcoordinates)
    //{
    //    transform.position = gridManager.GetPositionFromCoordinates(pathfinder.getstartcoordinates);
    //}
        //create aniotgher methodf for findpath and startcoroutine move
        public void RegeneratePath()
    {
        //probelm lies here in regenarte path getting called even if iuts not actove
        if (gameObject.activeInHierarchy==true)
        {
       /*     Debug.Log("in active heirerchy sibling indiex "+transform.GetSiblingIndex());*/
            StopAllCoroutines();
            FindPath();
         /*   ReturnToStart();*/
            StartCoroutine(Move());
        }
        else
        {
         
            FindPath();
        }
        //StopAllCoroutines();
        //FindPath();
        //ReturnToStart();
        //StartCoroutine(Move());
    }
  
    private void ReturnToStart()
    {
       
        //problem lies here
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.getstartcoordinates);
    }
    private void ReturnToStart(Vector2Int startcoordinates)
    {
        
        transform.position = gridManager.GetPositionFromCoordinates(startcoordinates);
    }

    private void FindPath()
    {


        Path.Clear();
        pathVector.Clear();
        //thgis has to be called again
        //Debug.Log("in find new path");
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        Path = pathfinder.GetNewPath(coordinates);
       
        foreach(Node path in Path)
        {
            pathVector.Add(path.coordinates);
        }
        //Debug.Log("the coordinates "+Path[1].coordinates);
    }

    IEnumerator Move()
    {
        int i;
        //foreach to for
        // for(int i=0;i<Path.count;i++)
        //path with node
        //foreach (Node waypointnode in Path)
        //i should start from 1
       

        for (i = 1; i < Path.Count; i++)
        {

            //if (i == 1)
            //{
            //    Debug.Log("the i " + i);
            //}
            //if (transform.GetSiblingIndex() == 0 && gameObject.active == true)
            //{
            //    Debug.Log("here2");
            //}
            //problem with start position
             startPosition = gameObject.transform.position;
            presentPosition = gameObject.transform.position;
            endPosition = gridManager.GetPositionFromCoordinates(Path[i].coordinates);
            float timer = 0;
            transform.LookAt(endPosition);
            while(timer<1f)
            {
                
                maintimer = timer;

                transform.position = Vector3.Lerp(startPosition, endPosition,timer);
               
                timer += Time.deltaTime;
                //Debug.Log("the value of i "+i);
                //Debug.Log("the endposition "+ endPosition);
                //whileendPosition = endPosition;


                yield return new WaitForEndOfFrame();
            }
        }
        
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("here in log");
    //}
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("here in log");
    }
}
