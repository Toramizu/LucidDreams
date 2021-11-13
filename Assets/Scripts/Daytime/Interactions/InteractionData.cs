using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionData : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    [SerializeField] Vector2 position;
    public Vector2 Position { get { return position; } }
    [SerializeField] string text;
    public string Text { get { return text; } }

    [SerializeField] Condition condition;
    //public Condition Condition { get { return condition; } }
    //[SerializeField] DialogueData dialogue;
    [SerializeField] List<InteractionEventData> events;

    public bool Check { get { return condition == null || condition.Check; } }

    [SerializeField] Color background;
    public Color Background { get { return background; } }
    [SerializeField] Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }

    public void OnClick()
    {
        List<InteractionEventData> evnts = events.Where(e => e.Check).ToList();

        evnts[Random.Range(0, evnts.Count)].Play();
    }
}
