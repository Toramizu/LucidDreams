using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeditationPanel : Window
{
    [SerializeField] TMP_Text quote;

    //[SerializeField] List<string> meditationQuotes;
    [SerializeField] List<DialogueData> meditations;

    public bool CanMeditate { get; set; }

    public override void Open()
    {
        base.Open();
        DialogueData med = meditations[Random.Range(0, meditations.Count)];
        GameManager.Instance.StartDialogue(med);

        /*if (CanMeditate)
            quote.text = meditationQuotes[Random.Range(0, meditationQuotes.Count)];
        else
            quote.text = "You already took a rest here, it became too dangerous to rest again...";*/
    }

    public void Meditate()
    {
        if (CanMeditate)
            GameManager.Instance.PlayerManager.Meditate();
        Close();
    }
}
