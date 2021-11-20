using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueElement
{
    //Return true if auto-play next element
    public abstract bool Play(DialogueUI dialUI);
}
