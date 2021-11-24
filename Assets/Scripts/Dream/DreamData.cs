using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DreamData : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlAttribute("SuccubiCount")]
    public int SuccubiCount { get; set; }

    [XmlIgnore]
    public SuccubusData Boss { get { return AssetDB.Instance.Succubi[_Boss]; } }
    [XmlAttribute("Boss")]
    public string _Boss { get; set; }

    [XmlIgnore]
    public List<SuccubusData> Succubi {
        get {
            List<SuccubusData> s = new List<SuccubusData>();
            foreach (string suc in _Succubi)
                s.Add(AssetDB.Instance.Succubi[suc]);
            return s;
        }
    }
    [XmlElement("Succubus")]
    public List<string> _Succubi { get; set; }

    [XmlElement("Shop")]
    public ShopData Shop { get; set; }

    [XmlIgnore]
    public DreamMapData Map { get { return AssetDB.Instance.DreamMaps[_Map]; } }
    [XmlAttribute("Map")]
    public string _Map { get; set; }


    [XmlIgnore]
    public List<DreamData> Nexts
    {
        get
        {
            List<DreamData> d = new List<DreamData>();
            foreach (string dream in _Nexts)
                d.Add(AssetDB.Instance.Dreams[dream]);
            return d;
        }
    }
    [XmlElement("Next")]
    public List<string> _Nexts { get; set; }

    [XmlIgnore]
    public List<DialogueData> Meditations
    {
        get
        {
            List<DialogueData> m = new List<DialogueData>();
            foreach (string medit in _Meditations)
                m.Add(AssetDB.Instance.Dialogues[medit]);
            return m;
        }
    }
    [XmlElement("Event")]
    public List<string> _Meditations { get; set; }
}
