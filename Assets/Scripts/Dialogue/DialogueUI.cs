using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Window
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogue;
    public string Dialogue
    {
        get { return dialogue.text; }
        set { dialogue.text = value; }
    }

    [SerializeField] DialogueSpeakerUI leftSpeaker;
    [SerializeField] DialogueSpeakerUI rightSpeaker;

    #region Dialogue
    DialogueData currentDialogue;
    Queue<DialogueElement> elements;

    public void Open(DialogueData data)
    {
        Open();
        currentDialogue = data;
        AddInFront(data.Elements);

        dialoguePanel.SetActive(false);

        Dialogue = "";
        leftSpeaker.Toggle(false);
        rightSpeaker.Toggle(false);

        choicesPanel.SetActive(false);

        Next();
    }

    void AddInFront(List<DialogueElement> newElements)
    {
        Queue<DialogueElement> elems = new Queue<DialogueElement>(newElements);

        if(elements != null)
            foreach (DialogueElement e in elements)
                elems.Enqueue(e);

        elements = elems;
    }

    void Next()
    {
        if (elements.Count > 0)
            elements.Dequeue().Play(this);
        else
            Close();
    }

    #endregion

    #region Dialogue lines
    Queue<DialogueLine> lines;

    public void Play(List<DialogueLine> lines)
    {
        this.lines = new Queue<DialogueLine>(lines);

        dialoguePanel.SetActive(true);

        PlayLine();
    }

    public void PlayLine()
    {
        if (lines == null)
            return;

        if (lines.Count == 0)
        {
            lines = null;
            Next();
        }
        else
        {
            DialogueLine line = lines.Dequeue();

            switch (line.SpeakerPosition)
            {
                case SpeakerPos.None:
                default:
                    if (line.RemoveSpeaker)
                    {
                        leftSpeaker.Toggle(false);
                        rightSpeaker.Toggle(false);
                    }
                    else
                    {
                        leftSpeaker.Focus = false;
                        rightSpeaker.Focus = false;
                    }
                    break;
                case SpeakerPos.Left:
                    if (line.RemoveSpeaker)
                    {
                        leftSpeaker.Toggle(false);
                    }
                    else
                    {
                        if (line.Speaker != null && line.Speaker != "")
                            leftSpeaker.Text = line.Speaker;

                        leftSpeaker.Focus = true;
                        rightSpeaker.Focus = false;
                    }
                    break;
                case SpeakerPos.Right:
                    if (line.RemoveSpeaker)
                    {
                        rightSpeaker.Toggle(false);
                    }
                    else
                    {
                        if (line.Speaker != null && line.Speaker != "")
                            rightSpeaker.Text = line.Speaker;

                        rightSpeaker.Focus = true;
                        leftSpeaker.Focus = false;
                    }
                    break;
            }

            if (line.Line != null && line.Line != "")
                Dialogue = line.Line;
            else
                PlayLine();
        }
    }
    #endregion

    #region Choices
    [SerializeField] TMP_Text choiceTitle;

    [SerializeField] GameObject choicesPanel;
    [SerializeField] Transform choicesDisplay;

    [SerializeField] DialogueChoiceUI choiceButtonPrefab;
    List<DialogueChoiceUI> displayedChoices = new List<DialogueChoiceUI>();

    public void Play(string title, List<DialogueChoice> choices)
    {
        choicesPanel.SetActive(true);

        if (title == null || title == "")
            choiceTitle.gameObject.SetActive(false);
        else
        {
            choiceTitle.text = title;
            choiceTitle.gameObject.SetActive(true);
        }

        foreach (DialogueChoice choice in choices)
        {
            DialogueChoiceUI dc = Instantiate(choiceButtonPrefab, choicesDisplay);
            dc.Init(choice, this);
            displayedChoices.Add(dc);
        }
    }

    public void Choose(DialogueChoice choice)
    {
        AddInFront(choice.Elements);

        foreach (DialogueChoiceUI diplayed in displayedChoices)
            Destroy(diplayed.gameObject);

        displayedChoices.Clear();
        choicesPanel.SetActive(false);

        Next();
    }
    #endregion
}