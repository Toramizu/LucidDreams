using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Character")]
public class CharacterData : ImageHaver
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [SerializeField] string cName;
    public override string Name { get { return cName; } }

    /*[SerializeField] ImageSet images;
    public ImageSet Images { get { return images; } }
    public ImageData Image { get { return images.Default; } }*/
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