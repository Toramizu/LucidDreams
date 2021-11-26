using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterData : ImageHaver, XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("DisplayName")]
    public override string Name { get; set; }

    [XmlIgnore]
    public Sprite Icon { get {
            Debug.Log(_Icon);
            return AssetDB.Instance.Images[_Icon]; } }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }

    [XmlAttribute("Succubus")]
    public string _Succubus { get; set; }
    [XmlIgnore]
    public SuccubusData Succubus { get { return AssetDB.Instance.Succubi[_Succubus]; } }

    [XmlElement("Event")]
    public List<ConditionalDialogue> Events { get; set; }

    [XmlElement("Relationship")]
    public List<RelationshipData> Relationships { get; set; }

    [XmlElement("Unlock")]
    public MultCondition Condition { get; set; }

    [XmlElement("Location")]
    public List<CharacterLocation> Locations { get; set; }
}

public class RelationData
{
    [SerializeField] RelationEnum relationType;
    public RelationEnum RelationType { get { return relationType; } }

    [SerializeField] List<ConditionalDialogue> relationEvents;
    public List<ConditionalDialogue> RelationEvents { get { return relationEvents; } }
}

public class CharacterLocation
{
    [XmlAttribute("Time")]
    public int Time { get; set; }
    [XmlElement("Location")]
    public List<string> Locations { get; set; }
}