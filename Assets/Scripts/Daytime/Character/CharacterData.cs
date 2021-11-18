using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [SerializeField] string cName;
    public string CName { get { return cName; } }

    [SerializeField] ImageSet images;
    public ImageSet Images { get { return images; } }
    [SerializeField] Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField] SuccubusData succubus;
    public SuccubusData Succubus { get { return succubus; } }

    [SerializeField] List<InteractionData> defaultEvents;
    public List<InteractionData> DefaultEvents { get { return defaultEvents; } }
    [SerializeField] List<RelationData> relationEvents;
    public List<RelationData> RelationEvents { get { return relationEvents; } }
}

[System.Serializable]
public class RelationData
{
    [SerializeField] RelationEnum relationType;
    public RelationEnum RelationType { get { return relationType; } }

    [SerializeField] List<InteractionData> relationEvents;
    public List<InteractionData> RelationEvents { get { return relationEvents; } }
}