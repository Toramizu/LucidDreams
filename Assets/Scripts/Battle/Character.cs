using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] Sprite defaultImage;
    [SerializeField] TMP_Text characterName;
    [SerializeField] SimpleGauge gauge;
    [SerializeField] Button borrowed;

    protected int Arousal { get; set; }
    public bool Finished { get { return Arousal >= Data.MaxArousal; } }

    protected CharacterData Data { get; private set; }

    [SerializeField] Transform abilityPanel;
    [SerializeField] List<Ability> abilities;
    public int Rolls { get; set; }
    [SerializeField] DiceHolder dice;

    [SerializeField] TraitsSheet traits;
    public TraitsSheet Traits { get { return traits; } }

    public bool NoAbilityRemaining
    {
        get
        {
            foreach (Ability abi in abilities)
                if (abi.isActiveAndEnabled)
                    return false;

            return true;
        }
    }

    public void LoadCharacter(CharacterData data)
    {
        Data = data;

        if (data.Image == null)
            characterImage.sprite = defaultImage;
        else
            characterImage.sprite = data.Image;

        characterName.text = data.Name;
        Arousal = 0;
        gauge.Fill(Arousal, data.MaxArousal);

        traits.Clear();

        borrowed.gameObject.SetActive(data.Source != null && data.Source != "");

        Rolls = data.Rolls;
        for (int i = 0; i < abilities.Count; i++)
        {
            if (i < data.Abilities.Count)
                SetAbility(data.Abilities[i], i);
            else
                abilities[i].gameObject.SetActive(false);
        }
    }

    public void SetAbility(AbilityData data, int slot)
    {
        //Debug.Log("Ability? " + slot + " <= " + slot);
        if (slot <= abilities.Count)
        {
            abilities[slot].gameObject.SetActive(true);
            abilities[slot].Init(data);
        }
    }

    public virtual bool InflictDamage(int amount)
    {
        Arousal += amount;
        if (Arousal < 0)
            Arousal = 0;
        else if (Arousal > Data.MaxArousal)
            Arousal = Data.MaxArousal;

        gauge.Fill(Arousal, Data.MaxArousal);

        return Arousal >= Data.MaxArousal;
    }

    public void OpenBorrowed()
    {
        if (Data.Source != null && Data.Source != "")
            Application.OpenURL(Data.Source);
    }

    public void StartTurn()
    {
        abilityPanel.gameObject.SetActive(true);
        Traits.StartTurn(this);
        dice.Roll(Rolls, true);

        foreach (Ability abi in abilities)
            if (abi.isActiveAndEnabled)
                abi.ResetAbility();
    }

    public void EndTurn()
    {
        ResetDice();
        abilityPanel.gameObject.SetActive(false);
        Traits.EndTurn(this);
    }

    public void Roll(int amount, bool reset)
    {
        dice.Roll(amount, reset);
    }

    public void Give(int value)
    {
        dice.Give(value);
    }

    public void ResetDice()
    {
        dice.ResetDice();
    }

    public void ResetDicePosition()
    {
        dice.ResetDicePosition();
    }

    public void PlayTurn()
    {
        List<RolledDie> rolled = new List<RolledDie>(dice.RolledDice).OrderByDescending(o => o.Value).ToList();
        //List<RolledDie> SortedList = rolled.OrderBy(o => o.Value).ToList();

        string log = "Opponent's turn :";
        foreach (RolledDie die in rolled)
            log += " " + die.Value;
        Debug.Log(log);

        foreach (Ability abi in abilities)
            if(abi.isActiveAndEnabled)
                abi.TryFill(rolled);
    }
}
