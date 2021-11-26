using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : InteractionEvent
{
    Character chara;

    public CharacterEvent(Character chara)
    {
        this.chara = chara;
    }

    public override void Play()
    {
        chara.PlayDialogue(EndEvent);
    }
}
