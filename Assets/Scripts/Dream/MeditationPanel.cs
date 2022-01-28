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

    public override void FadeIn()
    {
        base.FadeIn();
        DialogueData med = meditations[Random.Range(0, meditations.Count)];
        GameManager.Instance.StartDialogue(med, null);
    }
}
