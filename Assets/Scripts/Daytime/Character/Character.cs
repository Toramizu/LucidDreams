using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueUI;

public class Character : XmlAsset
{
    public string ID { get { return Data.ID; } }
    public CharacterData Data { get; set; }

    List<Relationship> relationships;
    List<int> relationPoints = new List<int>(3);
    List<int> relationStage = new List<int>(3);
    const int POINTS_PER_STAGE = 100;

    public bool Check { get { return Data.Check; } }

    public List<CharacterLocation> Locations { get { return Data.Locations; } }

    public Character() { }
    public Character(CharacterData data) {
        //TODO : Load saved characters
        this.Data = data;
        relationships = new List<Relationship>();
        foreach (RelationshipData rd in data.Relationships)
            relationships.Add(new Relationship(rd));
    }

    public void PlayDialogue(DialogueAction action)
    {
        foreach(Relationship relationship in relationships)
        {
            if (relationship.TryPlayInteraction())
                return;
        }

        List<ConditionalDialogue> evnts = Data.Events.Where(e => e.Check).ToList();

        if (evnts.Count > 0)
            evnts[Random.Range(0, evnts.Count)].Play(action);
    }

    public void AddRelationPoints(int relation, int points)
    {
        relationships[relation].Points += points;

        if (points > 0)
            GameManager.Instance.Notify(Data.ID + " +" + points + " " + relationships[relation].Name);
        else if (points < 0)
            GameManager.Instance.Notify(Data.ID + " -" + (-points) + " " + relationships[relation].Name);
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
