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

    public int LastInteraction { get; set; } = 1;

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
        if(LastInteraction < GameManager.Instance.DayManager.Day)
        {
            LastInteraction = GameManager.Instance.DayManager.Day;
            AddRelationPoints(0, 1, false);
        }

        foreach(Relationship relationship in Relationships)
        {
            if (relationship.TryPlayInteraction())
                return;
        }

        List<ConditionalDialogue> evnts = Data.Events.Where(e => e.Check).ToList();

        if (evnts.Count > 0)
            evnts[Random.Range(0, evnts.Count)].Play(action);
    }

    public ConditionalDialogue GetRelationshipEvent(out DialogueAction action)
    {
        ConditionalDialogue evnt;
        foreach (Relationship relationship in Relationships)
        {
            evnt = relationship.GetRelationshipEvent();
            if (evnt != null)
            {
                action = relationship.IncreaseRelationship;
                return evnt;
            }
        }
        action = null;
        return null;
    }

    public void AddRelationPoints(int relation, int points, bool stageLimit)
    {
        if (Relationships.Count <= relation)
            return;

        //Relationships[relation].Points += points;
        int i = Relationships[relation].AddPoints(points, stageLimit);

        if (points > 0)
            GameManager.Instance.Notify(Data.Name + " +" + points + " " + Relationships[relation].Name, Data.Color);
        else if (points < 0)
            GameManager.Instance.Notify(Data.Name + " " + points + " " + Relationships[relation].Name, Data.Color);
    }

    public void DailyChecks(int day)
    {
        if(day > LastInteraction)
        {
            AddRelationPoints(0, LastInteraction - day, true);
            if(Relationships.Count > 2 && Relationships[1].Stage > Relationships[0].Stage)
                AddRelationPoints(2, Variables.loveDecayRate, false);
        }

        if (Relationships.Count < 3)
            return;

        if (Relationships[1].Stage > Relationships[2].Stage)
            AddRelationPoints(2, Variables.lossDecayByLove, false);
        else if (Relationships[1].Stage < Relationships[2].Stage)
            AddRelationPoints(1, Variables.loveDecayByLoss, false);
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
