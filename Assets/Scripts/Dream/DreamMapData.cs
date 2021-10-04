using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDreamMap", menuName = "Data/DreamMap", order = 4)]
public class DreamMapData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } set { id = value; } }

    [SerializeField] List<DreamNodeData> nodes;
    public List<DreamNodeData> Nodes { get { return nodes; } set { nodes = value; } }
}
