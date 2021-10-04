using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePanel : MonoBehaviour
{
    [SerializeField] DreamNode dreamNodePrefab;

    public Dictionary<NodeContent, List<DreamNode>> Nodes { get; set; } = new Dictionary<NodeContent, List<DreamNode>>();
    //List<DreamNode> nodes = new List<DreamNode>();

    public void AddNodes(List<DreamNodeData> nDatas, GridManager manager)
    {
        Clear();

        foreach(DreamNodeData nData in nDatas)
        {
            DreamNode node = Instantiate(dreamNodePrefab, transform);
            node.Init(nData.Position, manager);
            //node.SetType(nData.Content);
            //node.transform.localPosition = nData.Position;

            if (!Nodes.ContainsKey(nData.Content))
                Nodes.Add(nData.Content, new List<DreamNode>());
            Nodes[nData.Content].Add(node);
        }
    }

    void Clear()
    {
        foreach(List<DreamNode> list in Nodes.Values)
            foreach (DreamNode node in list)
                Destroy(node.gameObject);
        Nodes.Clear();
    }
}
