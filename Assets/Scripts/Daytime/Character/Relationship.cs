using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Relationship
{
    RelationshipData relation;
    public int Points { get; set; }
    const int POINTS_PER_STAGE = 100;
    int stage;

    public string Name { get { return relation.RelationName; } }

    public Relationship() { }
    public Relationship(RelationshipData data) {
        //Load saved
        relation = data;
    }

    public bool PlayInteraction()
    {
        if (Points >= stage * POINTS_PER_STAGE && relation.RelationEvents[stage + 1].Check)
        {
            relation.RelationEvents[stage + 1].Play(IncreaseRelationship);
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
        stage++;
        GameManager.Instance.Notify(relation.RelationName + " => " + stage);
    }
}
