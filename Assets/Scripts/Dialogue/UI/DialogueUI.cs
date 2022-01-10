using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Window
{
    [SerializeField] Button dialoguePanel;
    [SerializeField] TMP_Text dialogue;
    public string Dialogue
    {
        get { return dialogue.text; }
        set { dialogue.text = value; }
    }

    [SerializeField] DialogueSpeakerUI leftSpeaker;
    [SerializeField] DialogueSpeakerUI left2Speaker;
    [SerializeField] DialogueSpeakerUI rightSpeaker;
    [SerializeField] DialogueSpeakerUI right2Speaker;


    [SerializeField] Window hidable;

    #region Dialogue
    DialogueData currentDialogue;
    Queue<DialogueElement> elements;
    DialogueAction action;

    public void Open(DialogueData data, DialogueAction action)
    {
        if(data == null)
        {
            FadeOut();
            Debug.Log("No dialogue...");
            action();
            return;
        }

        this.action = action;
        FadeIn();
        hidable.FadeIn();
        currentDialogue = data;
        AddInFront(data.Elements);

        dialoguePanel.gameObject.SetActive(false);

        Dialogue = "";
        //leftSpeaker.Toggle(false);
        leftSpeaker.Data = GameManager.Instance.PlayerManager.Player; ;
        leftSpeaker.Focus = false;
        rightSpeaker.Toggle(false);
        left2Speaker.Toggle(false);
        right2Speaker.Toggle(false);
        centerImage.Toggle(false);

        choicesPanel.SetActive(false);

        Next();
    }

    public void AddInFront(List<DialogueElement> newElements)
    {
        Queue<DialogueElement> elems = new Queue<DialogueElement>(newElements);

        if(elements != null)
            foreach (DialogueElement e in elements)
                elems.Enqueue(e);

        elements = elems;
    }

    public void Next()
    {
        if (elements.Count > 0)
        {
            if (elements.Dequeue().Play(this))
                Next();
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        FadeOut();
        action?.Invoke();
    }

    Dictionary<string, Queue<DialogueElement>> loops = new Dictionary<string, Queue<DialogueElement>>();
    public void AddLoop(string id)
    {
        loops[id] = new Queue<DialogueElement>(elements);
    }

    public void GoTo(string id)
    {
        if (loops.ContainsKey(id))
            elements = new Queue<DialogueElement>(loops[id]);
    }

    #endregion

    #region Dialogue lines
    public void PlayLine(DialogueLine line)
    {
        dialoguePanel.gameObject.SetActive(true);
        Dialogue = GameManager.Instance.Parser.Parse(line.Line);

        if (line.Focus != SpeakerPos.NoChange)
        {
            leftSpeaker.Focus = false;
            left2Speaker.Focus = false;
            rightSpeaker.Focus = false;
            right2Speaker.Focus = false;

            switch (line.Focus)
            {
                case SpeakerPos.Left:
                    leftSpeaker.Focus = true;
                    break;
                case SpeakerPos.Left2:
                    left2Speaker.Focus = true;
                    break;
                case SpeakerPos.Right:
                    rightSpeaker.Focus = true;
                    break;
                case SpeakerPos.Right2:
                    right2Speaker.Focus = true;
                    break;
            }
        }
    }

    public void SetSpeaker(DialogueSpeaker speaker)
    {
        switch (speaker.SpeakerPosition)
        {
            case SpeakerPos.Left:
                leftSpeaker.SetSpeaker(speaker);
                break;
            case SpeakerPos.Left2:
                left2Speaker.SetSpeaker(speaker);
                break;
            case SpeakerPos.Right:
                rightSpeaker.SetSpeaker(speaker);
                break;
            case SpeakerPos.Right2:
                right2Speaker.SetSpeaker(speaker);
                break;
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
        dialoguePanel.interactable = false;

        if (title == null || title == "")
            choiceTitle.gameObject.SetActive(false);
        else
        {
            choiceTitle.text = title;
            choiceTitle.gameObject.SetActive(true);
        }

        bool anyChoice = false;
        foreach (DialogueChoice choice in choices)
        {
            if (choice.Check)
            {
                DialogueChoiceUI dc = Instantiate(choiceButtonPrefab, choicesDisplay);
                dc.Init(choice, this);
                displayedChoices.Add(dc);
                anyChoice = true;
            }
        }

        if (!anyChoice)
        {
            GameManager.Instance.NotifyError("No choice available in " + currentDialogue.ID);
            CloseChoices();
        }
    }

    public void Choose(DialogueChoice choice)
    {
        AddInFront(choice.Elements);
        
        CloseChoices();
    }

    void CloseChoices()
    {
        foreach (DialogueChoiceUI diplayed in displayedChoices)
            Destroy(diplayed.gameObject);

        displayedChoices.Clear();
        choicesPanel.SetActive(false);
        dialoguePanel.interactable = true;

        Next();
    }
    #endregion

    #region Images
    [SerializeField] ImageUI centerImage;

    [SerializeField] Vector2 centerMaxSize;

    public void CenterImage(ImageData image)
    {
        FadeIn();

        centerImage.Init(image);

        int w = image.Image.texture.width;
        int h = image.Image.texture.height;

        float ratio = centerMaxSize.x / centerMaxSize.y;
        //Debug.Log(w + " / " + h + " = " + ratio + " => " + w * ratio + " & " + h * ratio);

        if (w  > h * ratio)
            ratio = w / centerMaxSize.x;
        else
            ratio = h / centerMaxSize.y;

        Vector2 v = new Vector2(
            w / ratio,
            h / ratio
            );

        //Debug.Log(w + " / " + h + " = " + ratio + " => " + v);

        centerImage.RectTransform.sizeDelta = v;
    }
    #endregion

    public delegate void DialogueAction();
}