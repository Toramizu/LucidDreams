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
            node.SetType(contentType.Value);

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
                bool needsLinking = true;
                foreach (NodeLink link in links)
                    if (link.Contains(selected, node))
                        needsLinking = false;

                if (needsLinking)
                {
                    NodeLink link = Instantiate(linePrefab, transform, false);
                    link.Node1 = selected;
                    link.Node2 = node;

                    selected.CellLinks.Add(link);
                    node.CellLinks.Add(link);
                }
            }

            selected = (NodeMaker)node;
            selected.ColorSelect();
        }
    }

    public void Delete(NodeMaker node)
    {
        for(int i = node.CellLinks.Count -1; i >= 0; i--)
            node.CellLinks[i].Delete();

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

        DreamMapData data = (DreamMapData)ScriptableObject.CreateInstance("DreamMapData");
        //DreamMapData data = new DreamMapData();
        data.ID = mapName.text;
        data.Nodes = new List<DreamNodeData>();

        foreach(NodeMaker node in nodes)
        {
            DreamNodeData nData = new DreamNodeData(node.Type, node.transform.localPosition);
            data.Nodes.Add(nData);
        }

        AssetDatabase.CreateAsset(data, "Assets/Data/Dream/Maps/" + data.ID + ".asset");
        AssetDatabase.SaveAssets();
    }

    public void Load()
    {
        string map = mapName.text;

        if (GameManager.Instance.Assets.Maps.ContainsKey(map))
        {
            Debug.Log("Loading " + map + "...");
            Clear();
            DreamMapData data = GameManager.Instance.Assets.Maps[map];

            foreach(DreamNodeData node in data.Nodes)
            {
                NodeMaker newNode = Instantiate(nodePrefab, transform, false);
                nodes.Add(newNode);
                newNode.Init(node.Position, this);
                newNode.SetType(node.Content);
                newNode.gameObject.SetActive(true);
            }
        }
        else
            Debug.Log("Map " + map + " not found...");
    }
    #endregion
}
