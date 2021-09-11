using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamGrid : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] DreamNode dreamNodePrefab;
    [SerializeField] GridLayoutGroup layout;

    Dictionary<Coordinate, DreamNode> nodes;

    public DreamNode this[Coordinate coo]
    {
        get {
            if (nodes.ContainsKey(coo))
                return nodes[coo];
            else
                return null;
            }
    }

    public void Init(DreamManager manager)
    {
        nodes = new Dictionary<Coordinate, DreamNode>();
        for(int i = -y/2; i <= y / 2; i++)
            for(int j = -x / 2; j <= x/2; j++)
            {
                DreamNode n = Instantiate(dreamNodePrefab, transform, false);
                Coordinate c = new Coordinate(j, i);
                n.Init(c, manager);
                nodes.Add(c, n);

                //n.Toggle(Random.Range(0, 2) == 1);
            }
    }

    public void Clear()
    {
        foreach (DreamNode node in nodes.Values)
            node.Toggle(false);
    }

    public void SetNode(DreamNodeData data)
    {
        if (nodes.ContainsKey(data.Coo))
            nodes[data.Coo].Init(data);
    }
}
