using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : InteractionEvent
{
    Character chara;

    public CharacterEvent(Character chara)
    {
        CharacterData data = chara.Data;
        this.chara = chara;
        Name = data.Name;
        _Icon = data._Icon;
        _BackgroundColor = data.Color;

        TimeSpent = 1;
    }

    public override void Play()
    {
        chara.PlayDialogue(EndEvent);
    }
}
