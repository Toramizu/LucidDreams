using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDreamMap", menuName = "Data/DreamMap", order = 4)]
public class DreamMapData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    [SerializeField] public List<DreamNodeData> nodes;
    public List<DreamNodeData> Nodes { get { return nodes; } }

    [SerializeField] public Coordinate start = new Coordinate(0, 0);
    public Coordinate Start { get { return start; } }
}
