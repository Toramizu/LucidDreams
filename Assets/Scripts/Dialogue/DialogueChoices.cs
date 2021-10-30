using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChoices", menuName = "Data/Dialogue/Choices")]
public class DialogueChoices : DialogueElement
{
    [SerializeField] string choiceTitle;
    [SerializeField] List<DialogueChoice> choices;

    public override void Play(DialogueUI dialUI)
    {
        dialUI.Play(choiceTitle, choices);
    }
}

[System.Serializable]
public class DialogueChoice
{
    [SerializeField] string text;
    public string Text { get { return text; } }

    [SerializeField] List<DialogueElement> elements;
    public List<DialogueElement> Elements { get { return elements; } }
}
