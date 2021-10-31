using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueChoiceUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public string Text {
        get { return text.text; }
        private set { text.text = value; }
    }

    [SerializeField] DialogueChoice choice;
    public DialogueChoice Choice { get { return choice; } }

    [SerializeField] DialogueUI ui;
    public DialogueUI UI { get { return ui; } }

    public void Init(DialogueChoice choice, DialogueUI ui)
    {
        this.choice = choice;
        Text = choice.Text;
        this.ui = ui;
    }

    public void Click()
    {
        ui.Choose(choice);
    }
}
