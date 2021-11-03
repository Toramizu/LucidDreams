using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoll", menuName = "Data/Dialogue/Roll")]
public class DialogueRoll : DialogueElement
{
    [SerializeField] int dice = 2;
    [SerializeField] string flagBonus;
    [SerializeField] string flagMalus;

    [SerializeField] List<RollValue> rolls;

    public override bool Play(DialogueUI dialUI)
    {
        string text = "";
        int total = 0;
        if (flagBonus != "" && GameManager.Instance.Flags.HasFlag(flagBonus))
        {
            int bonus = GameManager.Instance.Flags.GetFlag(flagBonus);
            total += bonus;
            text += "(" + bonus + ") + ";
        }

        if (flagMalus != "" && GameManager.Instance.Flags.HasFlag(flagMalus))
        {
            int bonus = GameManager.Instance.Flags.GetFlag(flagMalus);
            total -= bonus;
            text += "(" + -bonus + ") + ";
        }

        for (int i = 0; i < dice; i++)
        {
            int r = Random.Range(1, 7);
            total += r;
            if (text != "")
                text += "+ ";
            text += "_" + r + " ";
        }

        foreach(RollValue val in rolls)
        {
            if (total >= val.Value)
            {
                text += "= " + total + " => " + val.ShownName;
                dialUI.AddInFront(val.Elements);
                break;
            }
        }

        GameManager.Instance.Notify(text);

        return true;
    }
}

[System.Serializable]
public class RollValue
{
    [SerializeField] int value;
    public int Value { get { return value; } }
    [SerializeField] string shownName = "Passed";
    public string ShownName { get { return shownName; } }
    [SerializeField] List<DialogueElement> elements;
    public List<DialogueElement> Elements { get { return elements; } }
}