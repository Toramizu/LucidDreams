using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DialogueUI;

public class EndPanel : Window
{
    [SerializeField] Window victory;
    [SerializeField] TMP_Text victoryText;

    [SerializeField] Window loss;
    [SerializeField] TMP_Text lossText;
    [SerializeField] List<string> wakeUpText;

    [SerializeField] AbilityPicker abilityPicker;

    int crystals;
    Succubus opponent;

    [SerializeField] GameObject edgedText;
    bool edged;

    DialogueAction onWin;
    DialogueAction onLoss;

    public void Victory(int crystals, Succubus opponent, bool edged, DialogueAction onWin)
    {
        this.edged = edged;
        edgedText.SetActive(edged);

        FadeIn();
        victory.FadeIn();
        loss.FadeOut();

        this.crystals = crystals;
        victoryText.text = "+" + crystals;

        this.opponent = opponent;
        this.onWin = onWin;
    }

    public void Loss(DialogueAction onLoss)
    {
        GameManager.Instance.DreamManager.LoseDream();
        FadeIn();
        victory.FadeOut();
        loss.FadeIn();

        lossText.text = wakeUpText[Random.Range(0, wakeUpText.Count)];
        this.onLoss = onLoss;
    }

    /*void NewAbility(List<AbilityData> abilities)
    {
        if (abilities.Count == 0)
        {
            newAbilityText.gameObject.SetActive(false);
            abilityPanel.gameObject.SetActive(false);
        }
        else {
            ability = opponent.Abilities[Random.Range(0, opponent.Abilities.Count)];
            if (GameManager.Instance.PlayerManager.Abilities.Contains(ability))
            {
                abilities.Remove(ability);
                NewAbility(abilities);
            }
            else
            {
                newAbilityText.gameObject.SetActive(true);
                abilityPanel.gameObject.SetActive(true);
                newAbilityText.text = newAbilitySynonym[Random.Range(0, newAbilitySynonym.Count)].BuildText(ability.ID);
                abilityPanel.Init(ability, null);
            }
        }
    }*/

    public void VictoryClose()
    {
        if (edged)
        {
            abilityPicker.Open(opponent, crystals, onWin);
            victory.FadeOut();
        }
        else
        {
            GameManager.Instance.EndBattle(crystals, null);
            victory.FadeOut();
            FadeOut();
            onWin?.Invoke();
        }
    }

    public void DefeatClose()
    {
        onLoss?.Invoke();
    }
}

[System.Serializable]
public class AbilityStolenFlavourText
{
    [SerializeField] string textBefore;
    [SerializeField] string textAfter;

    public string BuildText(string abilityName)
    {
        return textBefore + " " + abilityName + " " + textAfter;
    }
}
