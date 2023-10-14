using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private Transform corporation;
    [SerializeField] private LineRenderer path;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private float area;
    private List<MapVertex> pathVertexes = new();
    [SerializeField] private GameObject truckPrefab;
    [SerializeField] private List<Color> pathColors = new();
    [SerializeField] private Transform truckBtnParent;
    private List<House> houses = new();
    private List<House> housesTMP = new(); 
    private int choiceTruckIndex;
    private MilkTag pathMilkTag;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            
            House h = collision.GetComponent<House>();
            if (h.GetTag()!=pathMilkTag)
                return;

            if (houses.Count+housesTMP.Count == TruckManager.instance.GetMilkCount())
                return;

            housesTMP.Add(h);
            h.OnSelectedEffect();

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            House h = collision.GetComponent<House>();
            
            int index = CheckDuplicateHouse(h);
            if (index == -1)
                return;

            Debug.Log("houseTMP");
            housesTMP.RemoveAt(index);
            h.OffSelectedEffect();
        }
    }

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
                

            pathVertexes.Add(MapManager.instance.map.vertexes[0]);
        }

        if (Input.GetMouseButton(0))
        {
            if (pathVertexes.Count == 0)
                return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            RenderPathByMouse(mousePosition, pathVertexes.Count + 1);
            ChangePathColliderByMouse(mousePosition, pathVertexes.Count + 1);

            foreach(MapVertex v in pathVertexes[pathVertexes.Count-1].neighbors)
            {
                if (CheckDuplicateVertex(v))
                    continue;

                float distance = Vector3.Distance(mousePosition, v.vertexPosition);
                if (distance<=area)
                {
                    houses.AddRange(housesTMP);
                    housesTMP.Clear();
                    pathVertexes.Add(v);
                    RenderPath();

                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            houses.AddRange(housesTMP);
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
                foreach(House h in houses)
                {
                    h.OffSelectedEffect();
                }
                houses.Clear();
                housesTMP.Clear();
                return;
            }

            pathVertexes.Add(MapManager.instance.map.vertexes[0]);
            Color pathColor = pathColors[(int)pathMilkTag];
            pathColor.a = 0.7f;
            TruckManager.instance.DriveTruck(choiceTruckIndex, pathVertexes, pathColor,houses);

            MapManager.instance.map.AddPath(pathVertexes);

            path.positionCount = 0;
            pathVertexes.Clear();
            housesTMP.Clear();
            houses.Clear();
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

    private int CheckDuplicateHouse(House h)
    {

        for (int i = 0; i < housesTMP.Count; i++)
        {
            if (ReferenceEquals(housesTMP[i], h))
            {
                return i;

            }
        }

        return -1;
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
        if (TruckManager.instance.CheckTruckDrive(choiceTruckIndex))
            return;

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
