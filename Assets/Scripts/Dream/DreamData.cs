using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDream", menuName = "Data/Dream", order = 3)]
public class DreamData : ScriptableObject, XmlAsset
{
    [XmlIgnore]
    [SerializeField] string id;
    [XmlAttribute("ID")]
    public string ID { get { return id; } set { id = value; } }

    [XmlIgnore]
    [SerializeField] int succubiCount;
    [XmlIgnore]
    public int SuccubiCount { get { return succubiCount; } }

    [XmlIgnore]
    [SerializeField] SuccubusData boss;
    [XmlIgnore]
    public SuccubusData Boss { get { return boss; } }

    [XmlIgnore]
    [SerializeField] List<string> succubi;
    [XmlIgnore]
    public List<SuccubusData> Succubi {
        get {
            List<SuccubusData> s = new List<SuccubusData>();
            foreach (string suc in succubi)
                s.Add(AssetDB.Instance.Succubi[suc]);
            return s;
        }
    }

    [XmlIgnore]
    [SerializeField] ShopData shop;
    [XmlIgnore]
    public ShopData Shop { get { return shop; } }

    [XmlIgnore]
    [SerializeField] List<string> maps;
    [XmlIgnore]
    public List<DreamMapData> Maps {
        get {
            List<DreamMapData> data = new List<DreamMapData>();
            foreach (string map in maps)
                data.Add(AssetDB.Instance.DreamMaps[map]);

            return data;
        }
    }


    [XmlIgnore]
    [SerializeField] List<DreamData> nexts;
    [XmlIgnore]
    public List<DreamData> Nexts { get { return nexts; } }

    [XmlIgnore]
    [SerializeField] List<DialogueData> meditations;
    [XmlIgnore]
    public List<DialogueData> Meditations { get { return meditations; } }
}
