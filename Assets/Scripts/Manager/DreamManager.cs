using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamManager : MonoBehaviour
{
    [SerializeField] DreamGrid grid;

    DreamNode currentNode;

    private void Start()
    {
        grid.Init(this);
    }

    public void StartDream(DreamData data)
    {
        grid.Clear();

        foreach (DreamNodeData nData in data.Nodes)
            grid.SetNode(nData);

    }

    public void Clicked(DreamNode node)
    {

    }
}
