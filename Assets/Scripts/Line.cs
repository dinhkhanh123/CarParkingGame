using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] float minPointDistance;

    [HideInInspector] public List<Vector3> point = new();
    [HideInInspector] public int pointCount = 0;

    [HideInInspector] public float lenght=0f;


    private float pointFixedYAxis;

    private Vector3 prevPoint;

    private void Start()
    {
        gameObject.SetActive(false);
        pointFixedYAxis = lineRenderer.GetPosition(0).y;
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
        lineRenderer.positionCount = 0;
        pointCount = 0;
        point.Clear();
        lenght = 0f;
    }

    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = pointFixedYAxis;

        if(pointCount >=1 && Vector3.Distance(newPoint,GetLastPoint()) < minPointDistance)
        return;

        //else:

        if(pointCount == 0)
            prevPoint = newPoint;


        point.Add(newPoint);
        pointCount++;

        lenght += Vector3.Distance(prevPoint,newPoint); 
        prevPoint = newPoint;

        //line renderer update
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount-1,newPoint);
    }

    private Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    public void SetColor(Color color)
    {
        lineRenderer.sharedMaterials[0].color = color;
    }
}
