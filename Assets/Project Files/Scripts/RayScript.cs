using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer rend;
    EdgeCollider2D col;

    public List<Vector2> linePoints = new List<Vector2>();

    void Start()
    {
        rend = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        linePoints[0] = rend.GetPosition(0);
        linePoints[1] = rend.GetPosition(1);
        col.SetPoints(linePoints);
    }
}
