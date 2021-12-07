using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Succubus //: MonoBehaviour
{
    public SuccubusUI SuccubUI { get; set; }

    int arousal;
    public int Arousal {
        get { return arousal; }
        set
        {
            if (value < 0)
                arousal = 0;
            else
                arousal = value;

            if (SuccubUI != null)
                SuccubUI.FillGauge(arousal, maxArousal);
        }
    }
    int maxArousal;
    public int MaxArousal {
        get { return maxArousal; }
        set
        {
            maxArousal = value;
            if(SuccubUI != null)
                SuccubUI.FillGauge(arousal, maxArousal);
        }
    }
    public bool Finished { get { return Arousal >= MaxArousal; } }
    public int Missing { get { return MaxArousal - Arousal; } }
    
    public SuccubusData Data { get; private set; }
    public int Crystals { get { return Data.Crystals; } }

    //[SerializeField] Transform abilityPanel;
    //[SerializeField] List<Ability> Abilities = new List<Ability>();
    public List<Ability> Abilities { get; set; } = new List<Ability>();
        //{ get { return Abilities; } }
    public int Rolls { get; set; }

    //[SerializeField] DiceHolder dice;
    public DiceHolder Dice { get; private set; }

    [SerializeField] TraitsSheet traits;
    public TraitsSheet Traits { get { return traits; } }

    public bool NoAbilityRemaining
    {
        get
        {
            foreach (Ability abi in Abilities)
                if (abi.IsActive)
                    return false;

            return true;
        }
    }

    public void LoadCharacter(NightStat stats)
    {
        SuccubusData data = stats.Succubus;
        if (SuccubUI != null)
        {
            SuccubUI.LoadCharacter(data);
            Dice = new DiceHolder(SuccubUI.Dice);
            traits.TraitsUI = SuccubUI.Traits;
        }
        else
        {
            Dice = new DiceHolder(null);
            traits.TraitsUI = null;
        }

        Data = data;
        Arousal = 0;
        MaxArousal = stats.FinalArousal;
        SuccubUI.FillGauge(Arousal, MaxArousal);

        traits.Clear();

        Rolls = data.Dice;
        LoadAbilities(data.Abilities);
    }

    public void LoadCharacter(SuccubusData data)
    {
        if (SuccubUI != null)
        {
            SuccubUI.LoadCharacter(data);
            Dice = new DiceHolder(SuccubUI.Dice);
            traits.TraitsUI = SuccubUI.Traits;
        }
        else
        {
            Dice = new DiceHolder(null);
            traits.TraitsUI = null;
        }

        Data = data;
        Arousal = 0;
        MaxArousal = data.MaxArousal;
        SuccubUI.FillGauge(Arousal, MaxArousal);

        traits.Clear();

        Rolls = data.Dice;
        LoadAbilities(data.Abilities);
    }

    public void Reset(List<AbilityData> abilities)
    {
        LoadAbilities(abilities);
        traits.Clear();
        Dice.ResetDice();
    }

    Ability NewAbility(AbilityData data, int slot)
    {
        AbilityUI aUI;
        if (SuccubUI != null)
            aUI = SuccubUI.AbilityUI(slot);
        else
            aUI = null;
        return new Ability(data, aUI);
    }

    public void SetAbility(AbilityData data, int slot)
    {
        if(SuccubUI != null)
        {
            AbilityUI aUI = SuccubUI.AbilityUI(slot);
            if(aUI != null)
                Abilities[slot].Init(data, SuccubUI.AbilityUI(slot));
        }
    }

    public void LoadAbilities(List<AbilityData> abis)
    {
        Abilities.Clear();
        if (SuccubUI != null)
            SuccubUI.ClearAbilities();

        for (int i = 0; i < abis.Count && i < SuccubUI.AbilityCount; i++)
        {
            if(abis[i] != null)
                Abilities.Add(NewAbility(abis[i], i));
        }
    }

    public void InflictDamage(int amount)
    {
        Arousal += amount;
        if (Arousal < 0)
            Arousal = 0;
        else if (Arousal > MaxArousal)
            Arousal = MaxArousal;

        if (SuccubUI != null)
        {
            SuccubUI.FillGauge(Arousal, MaxArousal);
            GameManager.Instance.BattleManager.CheckBattleStatus();
        }
    }

    public int InflictDamageProportion(float proportion)
    {
        int amount = (int)(maxArousal * proportion);
        if (amount < arousal)
            amount = arousal;

        InflictDamage(amount);

        return amount;
    }

    public void OpenBorrowed()
    {
        /*if (Data.Source != null && Data.Source != "")
            Application.OpenURL(Data.Source);*/
    }

    public void StartTurn()
    {
        SuccubUI.ToggleAbilities(true);
        Dice.Roll(Rolls, null);
        Traits.StartTurn(this);
    }

    public void EndTurn()
    {
        foreach (Ability abi in Abilities)
            if (abi.IsActive)
                abi.ResetAbility();

        ResetDice();
        SuccubUI.ToggleAbilities(false);
        Traits.EndTurn(this);
    }

    public void Roll(int amount, DiceCondition condition)
    {
        Dice.Roll(amount, condition);
    }

    public void Give(int value)
    {
        Dice.Give(value);
    }

    public void ResetDice()
    {
        Dice.ResetDice();
    }

    public void ResetDicePosition()
    {
        Dice.ResetDicePosition();
    }


    public void ToggleAbilities(bool toggle)
    {
        SuccubUI.ToggleAbilities(toggle);
    }

    public Succubus Clone()
    {
        Succubus c = new Succubus();
        c.Data = Data;
        c.Arousal = Arousal;
        c.MaxArousal = MaxArousal;
        c.Abilities = new List<Ability>();
        foreach (Ability a in Abilities)
            c.Abilities.Add(a.Clone());

        c.traits = traits.Clone();

        c.Dice = Dice.Clone();

        return c;
    }

    #region Auto Play

    BattleAI ai = new BattleAI();
    
    public void PlayTurn()
    {
        //List<DieToSlot> next = ai.FindNextAction(Abilities, Dice.RolledDice, this, GameManager.Instance.BattleManager.Other(this));
        List<DieToSlot> next = ai.FindNextAction(this, GameManager.Instance.BattleManager.Other(this));
        SuccubUI.PlayTurn(next);
        
        //StartCoroutine(AutoMoveDice(next));
    }

    /*Dictionary<RolledDie, IDie> OLD_toPlace;
    List<DieToSlot> toPlace;
    Queue<RolledDie> OLD_slots;
    Queue<DieToSlot> slots;
    IDie currentSlot;
    RolledDie currentDie;
    
    IEnumerator AutoMoveDice(List<DieToSlot> toPlace)
    {
        yield return null;
        this.toPlace = toPlace;
        slots = new Queue<DieToSlot>(toPlace);

        PlaceNext();
    }

    void PlaceNext()
    {
        if (slots.Count == 0)
        {
            StartCoroutine(WaitEndTurn());
        }
        else
        {
            DieToSlot dts = slots.Dequeue();
            currentDie = dts.Die;
            currentSlot = dts.Slot;

            iTween.MoveTo(currentDie.gameObject, iTween.Hash(
                "x", currentSlot.X,
                "y", currentSlot.Y,
                //"speed", 500f,
                "time", .8f,
                "easeType", iTween.EaseType.easeOutSine,
                "onComplete", "SlotDie",
                "onCompleteTarget", gameObject
                ));
        }
    }

    void OLD_PlaceNext()
    {
        if (OLD_slots.Count == 0)
        {
            StartCoroutine(WaitEndTurn());
        }
        else
        {
            currentDie = OLD_slots.Dequeue();
            currentSlot = OLD_toPlace[currentDie];

            iTween.MoveTo(currentDie.gameObject, iTween.Hash(
                "x", currentSlot.X,
                "y", currentSlot.Y,
                //"speed", 500f,
                "time", .8f,
                "easeType", iTween.EaseType.easeOutSine,
                "onComplete", "SlotDie",
                "onCompleteTarget", gameObject
                ));
        }
    }

    IEnumerator WaitEndTurn()
    {
        yield return new WaitForSecondsRealtime(1);
        GameManager.Instance.BattleManager.NextRound();
    }

    void SlotDie()
    {
        currentSlot.OnDrop(currentDie);
        PlaceNext();
        //OLD_PlaceNext();
    }*/

    const float DMG_VALUE = 10;
    const float KO_VALUE = 1000;

    public float AIValue
    {
        get
        {
            float val = Arousal * DMG_VALUE;
            if (Arousal > MaxArousal)
                val += KO_VALUE;

            val += traits.AIValue;

            return val;
        }
    }
    #endregion

    #region Cheats
    public void FullHeal()
    {
        Arousal = 0;
        //MaxArousal = Data.MaxArousal;
        SuccubUI.FillGauge(Arousal, MaxArousal);
    }
    #endregion
}