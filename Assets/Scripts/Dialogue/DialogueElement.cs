using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueElement : ScriptableObject
{
    public abstract void Play(DialogueUI dialUI);
}
