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

    [SerializeField] NodeContent content;
    public NodeContent Content { get { return content; } }
}

public enum NodeContent
{
    None,
    Succubus,
    Boss,
    Exit,
    Shop,
}
