using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour, GridManager
{
    [SerializeField] NodeMaker nodePrefab;

    List<NodeMaker> nodes = new List<NodeMaker>();

    public void NewNode()
    {
        NodeMaker newNode = Instantiate(nodePrefab, transform, false);
        nodes.Add(newNode);
        newNode.Init(new Coordinate(), this);
        newNode.gameObject.SetActive(true);
    }

    public void Clicked(DreamNode node)
    {
        Debug.Log(node.Coordinate);
    }

    public void Delete(NodeMaker node)
    {
        nodes.Remove(node);
        Destroy(node.gameObject);
        //Remove lines
    }
}
