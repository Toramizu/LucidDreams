using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Relationship
{
    public RelationshipData Data { get; set; }
    public int Points { get; set; }
    const int POINTS_PER_STAGE = 100;
    public int Stage { get; set; }
    public int MaxStage { get { return Data.RelationEvents.Count; } }

    public string Name { get { return Data.RelationName; } }

    public Relationship() { }
    public Relationship(RelationshipData data) {
        //Load saved
        Data = data;
    }

    public bool TryPlayInteraction()
    {
        if (Points >= Stage * POINTS_PER_STAGE && Data.RelationEvents[Stage + 1].Check)
        {
            Data.RelationEvents[Stage + 1].Play(IncreaseRelationship);
            return true;
        }
        return false;
    }

    /*public InteractionDialogue CheckInteraction()
    {
        if (Points >= stage * POINTS_PER_STAGE && relation.RelationEvents[stage + 1].Check)
            return relation.RelationEvents[stage + 1];
        return null;
    }*/

    public void IncreaseRelationship()
    {
        Stage++;
        GameManager.Instance.Notify(Data.RelationName + " => " + Stage);
    }
}
