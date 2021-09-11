using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDream", menuName = "Data/Dream", order = 3)]
public class DreamData : ScriptableObject
{
    [SerializeField] int dreamSize;
    public int DreamSize { get { return dreamSize; } }

    [SerializeField] int succubi;
    public int Succubi { get { return succubi; } }

    [SerializeField] string boss;
    public string Boss { get { return boss; } }

    [SerializeField] List<DreamNodeData> nodes;
    public List<DreamNodeData> Nodes { get { return nodes; } }

    [SerializeField] Coordinate start = new Coordinate(0,0);
    public Coordinate Start { get { return start; } }
}

[System.Serializable]
public class DreamNodeData
{
    [SerializeField] Coordinate coo;
    public Coordinate Coo { get { return coo; } set { coo = value; } }

    [SerializeField] NodeContent content;
    public NodeContent Content { get { return content; } }

    [SerializeField] string value;
    public string Value { get { return value; } }
}

public enum NodeContent
{
    None,
    Succubus,
    Exit,
    Boss,
}
