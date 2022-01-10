using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnd : DialogueElement
{
    public override bool Play(DialogueUI dialUI)
    {
        dialUI.Close();
        return false;
    }
}
