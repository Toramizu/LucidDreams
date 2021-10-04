using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Hacked from : https://www.youtube.com/watch?v=--LB7URk60A
public class SimpleUILine : Graphic
{
    [SerializeField] protected List<Transform> points;

    [SerializeField] float thickness = 10f;


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        List<Vector2> points = new List<Vector2>();
        foreach (Transform point in this.points)
            points.Add(point.localPosition);

        if (points.Count < 2)
            return;

        float angle = 0;

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];

            if (i < points.Count - 1)
                angle = GetAngle(points[i], points[i + 1]) + 45f;

            DrawVerticesForPoint(point, vh, angle);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 3;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
    }
}
