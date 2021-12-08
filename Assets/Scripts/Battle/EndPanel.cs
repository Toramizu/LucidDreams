using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : Window
{
    [SerializeField] Window victory;
    [SerializeField] TMP_Text victoryText;
    [SerializeField] TMP_Text newAbilityText;
    [SerializeField] List<AbilityStolenFlavourText> newAbilitySynonym = new List<AbilityStolenFlavourText>();
    [SerializeField] AbilityUI abilityPanel;

    [SerializeField] Window loss;
    [SerializeField] TMP_Text lossText;
    [SerializeField] List<string> wakeUpText;

    int crystals;
    SuccubusData opponent;
    AbilityData ability;
    
    public void Victory(int crystals, SuccubusData opponent)
    {
        FadeIn();
        victory.FadeIn();
        loss.FadeOut();

        this.crystals = crystals;
        victoryText.text = "+" + crystals;

        this.opponent = opponent;
        ability = opponent.Abilities[Random.Range(0, opponent.Abilities.Count)];
        newAbilityText.text = newAbilitySynonym[Random.Range(0, newAbilitySynonym.Count)].BuildText(ability.ID);
        new Ability(ability, abilityPanel);
        //abilityPanel.Init(ability, null);
    }

    public void Loss()
    {
        GameManager.Instance.DreamManager.LoseDream();
        FadeIn();
        victory.FadeOut();
        loss.FadeIn();

        lossText.text = wakeUpText[Random.Range(0, wakeUpText.Count)];
    }

    void NewAbility(List<AbilityData> abilities)
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
    }

    public void VictoryClose()
    {
        GameManager.Instance.EndBattle(crystals, ability);
        victory.FadeOut();
        FadeOut();
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
