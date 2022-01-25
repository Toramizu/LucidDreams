using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DialogueUI;

public class AbilityPicker : Window
{
    [SerializeField] EndPanel endPanel;

    [SerializeField] List<AbilityUI> abilities;
    [SerializeField] TMP_Text stealingText;
    [SerializeField] List<string> stealingTexts;

    int crystals;
    DialogueAction onWin;

    public void Open(Succubus succubus, int crystals, DialogueAction onWin)
    {
        this.crystals = crystals;
        this.onWin = onWin;

        int i = 0;
        for(; i < abilities.Count && i < succubus.Abilities.Count; i++)
        {
            new Ability(succubus.Abilities[i].Data, abilities[i]);
        }
        for(; i< abilities.Count; i++)
        {
            abilities[i].Remove();
        }

        FadeIn();
    }

    public void PickAbility(int i)
    {
        if(i < abilities.Count)
        {
            GameManager.Instance.EndBattle(crystals, abilities[i].Data);
            Close();
        }
        else
            Skip();

    }

    public void Skip()
    {
        GameManager.Instance.EndBattle(crystals + 3, null);
        Close();
    }

    void Close()
    {
        FadeOut();
        endPanel.FadeOut();
        onWin?.Invoke();
    }
}
