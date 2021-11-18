using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{
    public string ID { get { return data.ID; } }

    CharacterData data;

    List<int> relationPoints = new List<int>(3);

    public Character() { }
    public Character(CharacterData data) {
        //TODO : Load saved characters
        this.data = data;
        relationPoints = new List<int>() { 0, 0, 0 };
    }

    public InteractionData GetInteraction()
    {
        List<RelationData> rd = data.RelationEvents;
        for (int i = 0; i < relationPoints.Count && i < rd.Count; i++)
            if (rd[i].RelationEvents.Count > relationPoints[i] && rd[i].RelationEvents[relationPoints[i]].Check)
                return rd[i].RelationEvents[relationPoints[i]];

        List<InteractionData> evnts = data.DefaultEvents.Where(e => e.Check).ToList();

        if (evnts.Count == 0)
            return null;
        else
            return evnts[Random.Range(0, evnts.Count)];
    }
}

public enum RelationEnum
{
    Friendship,
    Love,
    Loss,
}
