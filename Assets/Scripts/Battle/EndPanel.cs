using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] GameObject victory;
    [SerializeField] TMP_Text victoryText;
    [SerializeField] TMP_Text newAbilityText;
    [SerializeField] List<AbilityStolenFlavourText> newAbilitySynonym = new List<AbilityStolenFlavourText>();
    [SerializeField] AbilityUI abilityPanel;

    int crystals;
    CharacterData opponent;
    AbilityData ability;
    
    public void Victory(int crystals, CharacterData opponent)
    {
        gameObject.SetActive(true);
        victory.SetActive(true);

        this.crystals = crystals;
        victoryText.text = "+" + crystals;

        this.opponent = opponent;
        ability = opponent.Abilities[Random.Range(0, opponent.Abilities.Count)];
        newAbilityText.text = newAbilitySynonym[Random.Range(0, newAbilitySynonym.Count)].BuildText(ability.Title);
        new Ability(ability, abilityPanel);
        //abilityPanel.Init(ability, null);
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
                newAbilityText.text = newAbilitySynonym[Random.Range(0, newAbilitySynonym.Count)].BuildText(ability.Title);
                abilityPanel.Init(ability, null);
            }
        }
    }

    public void VictoryClose()
    {
        GameManager.Instance.EndBattle(crystals, ability);
        Close();
    }

    void Close()
    {
        gameObject.SetActive(false);
        victory.SetActive(false);
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
