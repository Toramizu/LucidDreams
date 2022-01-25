using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputUI : Window
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_InputField input;

    [SerializeField] Window confirmPanel;
    [SerializeField] TMP_Text confirmText;
    [SerializeField] string confirmDefault1 = "Is \"";
    [SerializeField] string confirmDefault2 = "\" okay? ";

    [SerializeField] DialogueUI dialUI;

    string toConfirm;
    string stringID;
    string defaultString;

    public void Open(string text, string id)
    {
        dialUI.Locked = true;
        this.text.text = text;
        stringID = id;
        defaultString = Flags.Instance.GetString(id);
        input.text = defaultString;
        StartCoroutine(Focus());
        FadeIn();
    }

    public void ConfirmInput1()
    {
        toConfirm = input.text;
        if (toConfirm != "")
        {
            confirmPanel.FadeIn();
            confirmText.text = confirmDefault1 + toConfirm + confirmDefault2;
        }
    }

    public void CancelInput1()
    {
        input.text = defaultString;
    }

    public void ConfirmInput2()
    {
        Debug.Log("Saving " + stringID + " as " + toConfirm);
        Flags.Instance.Strings[stringID] = toConfirm;
        dialUI.Locked = false;
        FadeOut();
        confirmPanel.FadeOut();
        dialUI.Next();
    }

    public void CancelInput2()
    {
        confirmPanel.FadeOut();
        StartCoroutine(Focus());
    }

    IEnumerator Focus()
    {
        yield return null;
        input.Select();
        input.ActivateInputField();
    }
}
