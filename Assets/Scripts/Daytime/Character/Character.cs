using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{
    public string ID { get { return data.ID; } }

    CharacterData data;

    List<Relationship> relationships;
    List<int> relationPoints = new List<int>(3);
    List<int> relationStage = new List<int>(3);
    const int POINTS_PER_STAGE = 100;

    public Character() { }
    public Character(CharacterData data) {
        //TODO : Load saved characters
        this.data = data;
        relationships = new List<Relationship>();
        foreach (RelationshipData rd in data.Relationships)
            relationships.Add(new Relationship(rd));
    }

    public void PlayInteraction()
    {
        bool found = false;
        foreach (Relationship rela in relationships)
        {
            found = rela.PlayInteraction();
            if (found)
                return;
        }

        List<InteractionDialogue> evnts = data.DefaultEvents.Where(e => e.Check).ToList();

        if (evnts.Count >= 0)
            evnts[Random.Range(0, evnts.Count)].Play(null);
    }

    /*public InteractionDialogue GetInteraction()
    {
        InteractionDialogue evnt = null;
        foreach (Relationship rela in relationships)
        {
            evnt = rela.CheckInteraction();
            if (evnt != null)
                return evnt;
        }

        List<InteractionDialogue> evnts = data.DefaultEvents.Where(e => e.Check).ToList();

        if (evnts.Count == 0)
            return null;
        else
            return evnts[Random.Range(0, evnts.Count)];
    }*/

    public void AddRelationPoints(int relation, int points)
    {
        relationships[relation].Points += points;

        if (points > 0)
            GameManager.Instance.Notify(data.ID + " +" + points + " " + relationships[relation].Name);
        else if (points < 0)
            GameManager.Instance.Notify(data.ID + " -" + (-points) + " " + relationships[relation].Name);
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
