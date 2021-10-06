using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLink : SimpleUILine
{
    DreamNode node1;
    public DreamNode Node1
    {
        get { return node1; }
        set
        {
            node1 = value;
            if (points.Count < 1)
                points.Add(value.transform);
            else
                points[0] = value.transform;

            SetAllDirty();
        }
    }

    DreamNode node2;
    public DreamNode Node2
    {
        get { return node2; }
        set
        {
            node2 = value;

            if (points.Count < 1)
                points.Add(value.transform);

            if(points.Count < 2)
                points.Add(value.transform);
            else
                points[0] = value.transform;

            points[1] = value.transform;
            SetAllDirty();
        }
    }

    public bool Contains(DreamNode n1, DreamNode n2)
    {
        return (node1 == n1 && node2 == n2) || (node1 == n2 && node2 == n1);
    }

    public void Delete()
    {
        node1.RemoveNeighbour(node2);
        //node2.RemoveNeighbour(node1);
        //node1.CellLinks.Remove(this);
        //node2.CellLinks.Remove(this);

        Destroy(gameObject);
    }
}
