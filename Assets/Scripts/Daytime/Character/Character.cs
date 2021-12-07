using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueUI;

public class Character : XmlAsset
{
    public string ID { get { return Data.ID; } }
    public CharacterData Data { get; set; }

    public List<Relationship> Relationships { get; set; }
    List<int> relationPoints = new List<int>(3);
    List<int> relationStage = new List<int>(3);
    const int POINTS_PER_STAGE = 100;

    public bool Check { get { return Data.Check; } }
    public bool NightCheck { get { return Data.NightCheck; } }

    public List<CharacterLocation> Locations { get { return Data.Locations; } }

    public Character() { }
    public Character(CharacterData data) {
        //TODO : Load saved characters
        this.Data = data;
        Relationships = new List<Relationship>();
        foreach (RelationshipData rd in data.Relationships)
            Relationships.Add(new Relationship(rd));
    }

    public void PlayDialogue(DialogueAction action)
    {
        foreach(Relationship relationship in Relationships)
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
        Relationships[relation].Points += points;

        if (points > 0)
            GameManager.Instance.Notify(Data.ID + " +" + points + " " + Relationships[relation].Name, Data.Color.Color);
        else if (points < 0)
            GameManager.Instance.Notify(Data.ID + " -" + (-points) + " " + Relationships[relation].Name, Data.Color.Color);
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
