using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Transform corporation;
    [SerializeField] private LineRenderer path;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private float area;
    private List<MapVertex> pathVertexes = new();
    [SerializeField] private GameObject truckPrefab;
    [SerializeField] private List<Color> pathColors = new();
    [SerializeField] private Transform truckBtnParent;
    private int choiceTruckIndex;
    private MilkTag pathMilkTag;

/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            if (pathMilkTag != 0)
                return;

            SetPathByTag(collision.GetComponent<House>().GetTag());

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {

        }
    }*/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetTruckIndex(int i) => choiceTruckIndex = i;

    private void DrawRoad()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float corporationSpace = Vector3.Distance(mousePosition, corporation.position);
            if (corporationSpace-10 >= area
                || TruckManager.instance.CheckTruckDrive(choiceTruckIndex))
            {
                return;
            }
                

            pathVertexes.Add(mapManager.map.vertexes[0]);
        }

        if (Input.GetMouseButton(0))
        {
            if (pathVertexes.Count == 0)
                return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            RenderPathByMouse(mousePosition, pathVertexes.Count + 1);
            //ChangePathColliderByMouse(mousePosition, pathVertexes.Count + 1);

            foreach(MapVertex v in mapManager.map.vertexes)
            {
                if (CheckDuplicateVertex(v))
                    continue;

                float distance = Vector3.Distance(mousePosition, v.vertexPosition);
                if (distance<=area)
                {
                    
                    pathVertexes.Add(v);
                    RenderPath();
                    ChangePathCollider();
                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            if (TruckManager.instance.CheckTruckDrive(choiceTruckIndex))
                return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            float distance = Vector3.Distance(mousePosition, corporation.position);


            if (distance >= area
                || pathVertexes.Count<=1)
            {

                path.positionCount = 0;
                pathVertexes.Clear();
                return;
            }

            //GameObject truck = Instantiate(truckPrefab, corporation.position, Quaternion.identity);
            pathVertexes.Add(mapManager.map.vertexes[0]);
            ChangePathCollider();
            TruckManager.instance.DriveTruck(choiceTruckIndex, pathVertexes, pathColors[(int)pathMilkTag]);

           

            path.positionCount = 0;
            pathVertexes.Clear();
            
        }

    }

    private bool CheckDuplicateVertex(MapVertex v)
    {
        foreach(MapVertex mv in pathVertexes)
        {
            if (ReferenceEquals(mv,v))
            {
                return true;
            }
        }
        return false;
    }

    private void RenderPathByMouse(Vector3 mousePos, int pathCount)
    {
        path.positionCount = pathCount;
        path.SetPosition(pathCount-1, mousePos);

    }

    private void ChangePathColliderByMouse(Vector3 mousePos, int pathCount)
    {
        Vector2[] points = new Vector2[2];
        points[0] = pathVertexes[pathVertexes.Count - 1].vertexPosition;
        points[1] = mousePos;
        col.points = points;
    }

    private void RenderPath()
    {
        path.positionCount = pathVertexes.Count;
        for (int i = 0; i < pathVertexes.Count; i++)
        {
            path.SetPosition(i, pathVertexes[i].vertexPosition);
        }
    }
    private void ChangePathCollider()
    {
        Vector2[] points = new Vector2[pathVertexes.Count];
        for (int i = 0; i < pathVertexes.Count; i++)
        {
            points[i] = pathVertexes[i].vertexPosition;
        }

        col.points = points;
    }

    public void SetPathByTag(MilkTag tag)
    {
        Debug.Log(tag);
        pathMilkTag = tag;
        path.startColor = pathColors[(int)tag];
        path.endColor = pathColors[(int)tag];
        truckBtnParent.GetChild(choiceTruckIndex).GetComponent<ChooseTruckButton>()
            .SetButtonColor(pathColors[(int)tag]);
    }

   
    private void Update()
    {
        DrawRoad();
    }
}
