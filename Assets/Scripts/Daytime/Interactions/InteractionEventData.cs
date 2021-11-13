using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEventData : ScriptableObject
{
    [SerializeField] Condition condition;
    public Condition Condition { get { return condition; } }
    [SerializeField] DialogueData dialogue;
    public DialogueData Dialogue { get { return dialogue; } }

    public bool Check { get { return condition == null || condition.Check; } }

    public void Play()
    {
        GameManager.Instance.StartDialogue(dialogue);
    }
}
