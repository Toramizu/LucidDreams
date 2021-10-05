using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePanel : MonoBehaviour
{
    [SerializeField] DreamNode dreamNodePrefab;
    [SerializeField] NodeLink linkPrefab;

    [SerializeField] List<DreamNode> nodes = new List<DreamNode>();
    [SerializeField] List<NodeLink> links = new List<NodeLink>();

    public Dictionary<NodeContent, List<DreamNode>> AddNodes(List<DreamNodeData> nDatas, GridManager manager)
    {
        Clear();
        Dictionary<NodeContent, List<DreamNode>> nodesDict = new Dictionary<NodeContent, List<DreamNode>>();
        Dictionary<int, NodeLink> linksDict = new Dictionary<int, NodeLink>();

        foreach (DreamNodeData nData in nDatas)
        {
            DreamNode node = Instantiate(dreamNodePrefab, transform);
            node.Init(nData.Position, manager);
            //node.SetType(nData.Content);
            //node.transform.localPosition = nData.Position;

            if (!nodesDict.ContainsKey(nData.Content))
                nodesDict.Add(nData.Content, new List<DreamNode>());
            nodesDict[nData.Content].Add(node);
            nodes.Add(node);

            foreach (int link in nData.NodeLinks)
            {
                if (linksDict.ContainsKey(link))
                {
                    linksDict[link].Node2 = node;
                    node.CellLinks.Add(linksDict[link]);
                }
                else
                {
                    NodeLink newLink = Instantiate(linkPrefab, transform, false);
                    newLink.Node1 = node;
                    node.CellLinks.Add(newLink);
                    linksDict.Add(link, newLink);
                    links.Add(newLink);
                }
            }
        }

        int i = 0;
        foreach (NodeLink link in links)
            link.transform.SetSiblingIndex(i++);
        foreach (DreamNode node in nodes)
            node.transform.SetSiblingIndex(i++);

        return nodesDict;
    }

    void Clear()
    {
        foreach (DreamNode node in nodes)
            Destroy(node.gameObject);
        nodes.Clear();

        foreach (NodeLink link in links)
            Destroy(link.gameObject);
        links.Clear();
    }
}
