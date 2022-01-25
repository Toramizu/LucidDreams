using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Relationship
{
    public RelationshipData Data { get; set; }
    const int POINTS_PER_STAGE = 100;
    string id;
    public int Points {
        get { return Flags.Instance.GetFlag(id + "_Points"); }
        set { Flags.Instance.SetFlag(id + "_Points", value); }
    }
    public int Stage {
        get { return Flags.Instance.GetFlag(id + "_Stage"); }
        set { Flags.Instance.SetFlag(id + "_Stage", value); }
    }
    public int MaxStage { get { return Data.RelationEvents.Count; } }

    public int StageInPoints { get { return Stage * POINTS_PER_STAGE; } }
    public int PointsInStage { get { return Points / POINTS_PER_STAGE; } }

    public string Name { get { return Data.RelationName; } }
    public RelationType Type { get { return Data.Type; } }

    public Relationship() { }
    public Relationship(RelationshipData data, string charaID) {
        //Load saved
        Data = data;
        id = charaID + "_" + data.Type.ToString();
    }

    public ConditionalDialogue GetRelationshipEvent()
    {
        if (Points >= (Stage + 1) * POINTS_PER_STAGE && Data.RelationEvents[Stage].Check)
            return Data.RelationEvents[Stage];
        else
            return null;
    }

    public int AddPoints(int points, bool stageLimit)
    {
        if(points < 0)
        {
            if (stageLimit && Points + points < StageInPoints)
                points = StageInPoints - Points;
            else if (Points < -points)
                points = -Points;
        }

        Points += points;
        return points;
    }

    public void IncreaseRelationship(int level)
    {
        if(level >= Stage)
        {
            if (level > Stage + 1)
                GameManager.Instance.NotifyError("Unexpected relationship progress : From " + Stage + " to " + level);

            Stage = level;
            GameManager.Instance.Notify(Data.RelationName + " => " + Stage);
        }
    }
}
