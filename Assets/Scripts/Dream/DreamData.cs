using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDream", menuName = "Data/Dream", order = 3)]
public class DreamData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    
    [SerializeField] int succubiCount;
    public int SuccubiCount { get { return succubiCount; } }

    [SerializeField] CharacterData boss;
    public CharacterData Boss { get { return boss; } }

    [SerializeField] List<CharacterData> succubi;
    public List<CharacterData> Succubi { get { return succubi; } }
    
    [SerializeField] ShopData shop;
    public ShopData Shop { get { return shop; } }

    [SerializeField] List<DreamMapData> maps;
    public List<DreamMapData> Maps { get { return maps; } }

    [SerializeField] List<DreamData> nexts;
    public List<DreamData> Nexts { get { return nexts; } }
}

[System.Serializable]
public class DreamNodeData
{
    [SerializeField] Coordinate coo;
    public Coordinate Coo { get { return coo; } set { coo = value; } }

    [SerializeField] Vector3 position;
    public Vector3 Position { get { return position; } set { position = value; } }

    [SerializeField] NodeContent content;
    public NodeContent Content { get { return content; } set { content = value; } }

    [SerializeField] List<int> nodeLinks = new List<int>();
    public List<int> NodeLinks { get { return nodeLinks; } set { nodeLinks = value; } }

    public DreamNodeData() { }
    public DreamNodeData(NodeContent content, Vector3 position) {
        this.content = content;
        this.position = position;
    }
}

public enum NodeContent
{
    None,
    Succubus,
    Boss,
    Start,
    Exit,
    Shop,
    Meditation,
}
