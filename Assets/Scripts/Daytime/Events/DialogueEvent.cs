using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueUI;

public class DialogueEvent : InteractionEvent
{
    public ConditionalDialogue Dialogue { get; set; }
    public DialogueAction Action { get; set; }

    public DialogueEvent() { }
    public DialogueEvent(ConditionalDialogue dialogue, DialogueAction action) {
        Dialogue = dialogue;
        Action = action;
    }

    public override void Play()
    {
        Dialogue.Play(Action);
    }
}
