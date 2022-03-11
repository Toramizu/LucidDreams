using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MapMaker : MonoBehaviour, GridManager
{
    [SerializeField] NodeMaker nodePrefab;
    [SerializeField] NodeLink linePrefab;

    [SerializeField] TMP_InputField mapName;

    NodeMaker selected;
    NodeContent? contentType;

    List<NodeMaker> nodes = new List<NodeMaker>();
    List<NodeLink> links = new List<NodeLink>();

    public void NewNode()
    {
        NodeMaker newNode = Instantiate(nodePrefab, transform, false);
        nodes.Add(newNode);
        newNode.Init(new Coordinate(), this);
        newNode.gameObject.SetActive(true);

        if(contentType != null && contentType != NodeContent.None)
            newNode.SetType(contentType.Value);
    }

    public void Clicked(DreamNode node)
    {
        if (contentType == NodeContent.None)
            Link(node);
        else if (contentType != null)
        {
            if (contentType != node.Type)
                node.SetType(contentType.Value);
            else
                node.SetType(NodeContent.None);
        }

        //Debug.Log(node.Coordinate);
    }

    void Link(DreamNode node)
    {
        if (selected)
            selected.SetColor();

        if (selected == node || node == null)
        {
            selected = null;
        }
        else
        {
            if (selected)
            {
                NodeLink alreadyPresent = null;
                //bool needsLinking = true;
                foreach (NodeLink link in links)
                    if (link.Contains(selected, node))
                        alreadyPresent = link;
                        //needsLinking = false;

                if (alreadyPresent)
                {
                    links.Remove(alreadyPresent);
                    ((NodeMaker)alreadyPresent.Node1).CellLinks.Remove(alreadyPresent);
                    ((NodeMaker)alreadyPresent.Node2).CellLinks.Remove(alreadyPresent);
                    Destroy(alreadyPresent.gameObject);
                }
                else
                {
                    NodeLink link = Instantiate(linePrefab, transform, false);
                    links.Add(link);
                    link.Node1 = selected;
                    link.Node2 = node;

                    selected.CellLinks.Add(link);
                    ((NodeMaker)node).CellLinks.Add(link);
                }
            }

            selected = (NodeMaker)node;
            selected.ColorSelect();
        }
    }

    public void Delete(NodeMaker node)
    {
        for(int i = node.CellLinks.Count -1; i >= 0; i--)
        {
            NodeLink link = node.CellLinks[i];
            if (node == link.Node1)
                ((NodeMaker)link.Node2).CellLinks.Remove(link);
            else if (node == link.Node2)
                ((NodeMaker)link.Node1).CellLinks.Remove(link);

            links.Remove(link);
            link.Delete();

            //node.CellLinks[i].Delete();
            //links.Remove(node.CellLinks[i]);
        }

        nodes.Remove(node);
        Destroy(node.gameObject);
        //Remove lines
    }

    void Clear()
    {
        foreach (NodeMaker node in nodes)
            Destroy(node.gameObject);
        nodes.Clear();

        foreach (NodeLink link in links)
            Destroy(link.gameObject);
        links.Clear();
    }

    #region ContentSwitch
    void ResetSelection()
    {
        selected.SetColor();
        selected = null;
    }

    public void SetLink(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.None;
        else
            contentType = null;
    }

    public void SetSuccubus(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Succubus;
        else
            contentType = null;
    }

    public void SetBoss(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Boss;
        else
            contentType = null;
    }

    public void SetShop(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Shop;
        else
            contentType = null;
    }

    public void SetMedit(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Meditation;
        else
            contentType = null;
    }

    public void SetStart(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Start;
        else
            contentType = null;
    }

    public void SetExit(bool toggle)
    {
        if (selected) ResetSelection();

        if (toggle)
            contentType = NodeContent.Exit;
        else
            contentType = null;
    }
    #endregion

    #region Save & Load

    public void Save()
    {
        Debug.Log("Saving " + mapName.text);

        DreamMapData data = new DreamMapData();
        data.ID = mapName.text;
        data.Nodes = new List<DreamNodeData>();

        Dictionary<NodeLink, int> linksID = new Dictionary<NodeLink, int>();
        for (int i = 0; i < links.Count; i++)
            linksID.Add(links[i], i);

        foreach(NodeMaker node in nodes)
        {
            DreamNodeData nData = new DreamNodeData(node.Type, node.transform.localPosition);
            data.Nodes.Add(nData);
            foreach (NodeLink link in node.CellLinks)
                nData.NodeLinks.Add(linksID[link]);
        }

        AssetDB.Instance.DreamMaps.Add(data);
        AssetDB.Instance.DreamMaps.SaveToXml();
    }

    public void Load()
    {
        string map = mapName.text;

        if (AssetDB.Instance.DreamMaps.ContainsID(map))
        {
            Debug.Log("Loading " + map + "...");
            Clear();
            DreamMapData data = AssetDB.Instance.DreamMaps[map];
            Dictionary<int, NodeLink> links = new Dictionary<int, NodeLink>();

            foreach (DreamNodeData node in data.Nodes)
            {
                NodeMaker newNode = Instantiate(nodePrefab, transform, false);
                nodes.Add(newNode);
                newNode.Init(node.Position, this);
                newNode.SetType(node.Content);
                newNode.gameObject.SetActive(true);

                foreach (int link in node.NodeLinks) {
                    if (links.ContainsKey(link))
                    {
                        links[link].Node2 = newNode;
                        newNode.CellLinks.Add(links[link]);
                    }
                    else
                    {
                        NodeLink newLink = Instantiate(linePrefab, transform, false);
                        newLink.Node1 = newNode;
                        newNode.CellLinks.Add(newLink);
                        links.Add(link, newLink);
                        this.links.Add(newLink);
                    }
                }
            }

            int i = 0;
            foreach (NodeLink link in this.links)
                link.transform.SetSiblingIndex(i++);
            foreach (NodeMaker node in nodes)
                node.transform.SetSiblingIndex(i++);
        }
        else
            Debug.Log("Map " + map + " not found...");
    }
    #endregion
}
