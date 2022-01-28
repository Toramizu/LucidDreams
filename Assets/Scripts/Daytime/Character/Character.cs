using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueUI;

public class Character : XmlAsset
{
    public string ID { get { return Data.ID; } }
    public CharacterData Data { get; set; }

    Dictionary<RelationType, Relationship> relationDict = new Dictionary<RelationType, Relationship>();
    public List<Relationship> Relationships { get { return relationDict.Values.ToList(); } }
    List<int> relationPoints = new List<int>(3);
    List<int> relationStage = new List<int>(3);
    const int POINTS_PER_STAGE = 100;

    public bool Check { get { return Data.Check; } }
    public bool NightCheck { get { return Data.NightCheck && Data._Succubus != null; } }

    public List<CharacterLocation> Locations { get { return Data.Locations; } }

    public int LastInteraction { get; set; } = 1;
    public bool IsImportant { get { return Data.IsImportant; } }

    public Character() { }
    public Character(CharacterData data) {
        //TODO : Load saved characters
        this.Data = data;
        //Relationships = new List<Relationship>();
        relationDict = new Dictionary<RelationType, Relationship>();
        foreach (RelationshipData rd in data.Relationships)
            relationDict[rd.Type] = new Relationship(rd, ID);
            //Relationships.Add(new Relationship(rd));
    }

    public void PlayDialogue(DialogueAction action)
    {
        if(LastInteraction < GameManager.Instance.DayManager.Day)
        {
            LastInteraction = GameManager.Instance.DayManager.Day;
            AddRelationPoints(0, 1, false);
        }

        /*foreach(Relationship relationship in Relationships)
        {
            if (relationship.TryPlayInteraction())
                return;
        }*/

        List<ConditionalDialogue> evnts = Data.Events.Where(e => e.Check).ToList();

        if (evnts.Count > 0)
            evnts[Random.Range(0, evnts.Count)].Play(action);
    }

    public ConditionalDialogue GetRelationshipEvent()
    {
        foreach (Relationship relationship in Relationships)
            return relationship.GetRelationshipEvent();
        return null;
    }

    public void IncreaseRelationship(RelationType relation, int level)
    {
        if(relationDict.ContainsKey(relation))
            relationDict[relation].IncreaseRelationship(level);
        else
            GameManager.Instance.NotifyError("No relationship " + relation + " for " + Data.ID);

        /*if (Relationships.Count <= relation)
        {
            GameManager.Instance.NotifyError("No relationship " + relation + " for " + Data.ID);
            return;
        }

        Relationships[relation].IncreaseRelationship(level);*/
    }

    public void AddRelationPoints(RelationType relation, int points, bool stageLimit)
    {
        if (relationDict.ContainsKey(relation))
        {
            relationDict[relation].AddPoints(points, stageLimit);

            if (points > 0)
                GameManager.Instance.Notify(Data.Name + " +" + points + " " + relationDict[relation].Name, Data.Color);
            else if (points < 0)
                GameManager.Instance.Notify(Data.Name + " " + points + " " + relationDict[relation].Name, Data.Color);
        }
        /*else
            GameManager.Instance.NotifyError("No relationship " + relation + " for " + Data.ID);*/


        /*if (Relationships.Count <= relation)
        {
            GameManager.Instance.NotifyError("No relationship " + relation + " for " + Data.ID);
            return;
        }

        int i = Relationships[relation].AddPoints(points, stageLimit);

        if (points > 0)
            GameManager.Instance.Notify(Data.Name + " +" + points + " " + Relationships[relation].Name, Data.Color);
        else if (points < 0)
            GameManager.Instance.Notify(Data.Name + " " + points + " " + Relationships[relation].Name, Data.Color);*/
    }

    public void DailyChecks(int day)
    {
        if (day > LastInteraction && relationDict.ContainsKey(RelationType.Friendship))
        {
            AddRelationPoints(RelationType.Friendship, LastInteraction - day, true);
            if (relationDict.ContainsKey(RelationType.Love) && relationDict[RelationType.Love].Stage > relationDict[RelationType.Friendship].Stage)
                AddRelationPoints(RelationType.Love, Variables.loveDecayRate, false);
        }
        
        if(relationDict.ContainsKey(RelationType.Love) && relationDict.ContainsKey(RelationType.Loss))
        {
            if (relationDict[RelationType.Love].Stage > relationDict[RelationType.Loss].Stage)
                AddRelationPoints(RelationType.Loss, Variables.lossDecayByLove, false);
            else if (relationDict[RelationType.Love].Stage < relationDict[RelationType.Loss].Stage)
                AddRelationPoints(RelationType.Love, Variables.loveDecayByLoss, false);
        }

        
        /*if (day > LastInteraction)
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
            AddRelationPoints(1, Variables.loveDecayByLoss, false);*/
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
