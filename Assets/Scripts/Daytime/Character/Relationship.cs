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

    public int StageInPoints { get { return Stage * POINTS_PER_STAGE; } }
    public int PointsInStage { get { return Points / POINTS_PER_STAGE; } }

    public string Name { get { return Data.RelationName; } }

    public Relationship() { }
    public Relationship(RelationshipData data) {
        //Load saved
        Data = data;
    }

    public bool TryPlayInteraction()
    {
        if (Points >= (Stage + 1) * POINTS_PER_STAGE && Data.RelationEvents[Stage + 1].Check)
        {
            Data.RelationEvents[Stage].Play(IncreaseRelationship);
            return true;
        }
        return false;
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

    public void IncreaseRelationship()
    {
        Stage++;
        GameManager.Instance.Notify(Data.RelationName + " => " + Stage);
    }
}
