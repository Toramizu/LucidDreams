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

    [XmlIgnore]
    [SerializeField] List<InteractionDialogue> defaultEvents = new List<InteractionDialogue>();
    [XmlIgnore]
    public List<InteractionDialogue> DefaultEvents { get { return defaultEvents; } }
    [XmlIgnore]
    [SerializeField] List<RelationData> relationEvents = new List<RelationData>();
    [XmlIgnore]
    public List<RelationData> RelationEvents { get { return relationEvents; } }

    [XmlIgnore]
    [SerializeField] List<RelationshipData> relationShips = new List<RelationshipData>();
    [XmlIgnore]
    public List<RelationshipData> Relationships { get { return relationShips; } }
}

[System.Serializable]
public class RelationData
{
    [SerializeField] RelationEnum relationType;
    public RelationEnum RelationType { get { return relationType; } }

    [SerializeField] List<InteractionData> relationEvents;
    public List<InteractionData> RelationEvents { get { return relationEvents; } }
}