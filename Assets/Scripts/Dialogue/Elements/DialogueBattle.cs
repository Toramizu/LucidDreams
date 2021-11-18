using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattle", menuName = "Data/Dialogue/Battle")]
public class DialogueBattle : DialogueElement
{
    [SerializeField] SuccubusData opponent;

    public override bool Play(DialogueUI dialUI)
    {
        GameManager.Instance.StartBattle(opponent);
        return true;
    }
}
