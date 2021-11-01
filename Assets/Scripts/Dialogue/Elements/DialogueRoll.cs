using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoll", menuName = "Data/Dialogue/Roll")]
public class DialogueRoll : DialogueElement
{
    [SerializeField] int dice = 2;
    [SerializeField] string shownName = "Fail";
    [SerializeField] string flagBonus;

    [SerializeField] List<DialogueElement> ifLess;
    [SerializeField] List<RollValue> values;

    public override bool Play(DialogueUI dialUI)
    {
        string text;
        int total;
        if (GameManager.Instance.Flags.HasFlag(flagBonus))
        {
            total = GameManager.Instance.Flags.GetFlag(flagBonus);
            text = "(" + total + ") + ";
        }
        else
        {
            text = "";
            total = 0;
        }

        for(int i = 0; i < dice; i++)
        {
            int r = Random.Range(1, 7);
            total += r;
            if (i > 0)
                text += "+ ";
            text += "_" + r + " ";
        }

        bool found = false;
        foreach(RollValue val in values)
        {
            if(val.Value >= total)
            {
                text += ">= " + total + " => "+ val.ShownName;
                found = true;
                dialUI.AddInFront(val.Elements);
                break;
            }
        }

        if(!found)
        {
            dialUI.AddInFront(ifLess);
            text += "< " + total + " => " + shownName;
            dialUI.AddInFront(ifLess);
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