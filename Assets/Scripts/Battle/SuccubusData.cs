using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SuccubusData : ImageHaver, XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlIgnore]
    public override string Name { get { return ID; }set { ID = value; } }

    [XmlIgnore]
    public List<AbilityData> Abilities
        {
        get {
            List<AbilityData> abis = new List<AbilityData>();
            foreach (string s in _Abilities)
                abis.Add(AssetDB.Instance.Abilities[s]);
            return abis;
        }
    }
    [XmlElement("Ability")]
    public List<string> _Abilities { get; set; }

    [XmlAttribute("Dice")]
    public int Dice { get; set; }
    [XmlAttribute("MaxArousal")]
    public int MaxArousal { get; set; }

    [XmlAttribute("Crystals")]
    public int Crystals { get; set; }
}
