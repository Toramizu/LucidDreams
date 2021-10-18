using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character //: MonoBehaviour
{
    public CharacterUI CharaUI { get; set; }
    
    public int Arousal { get; set; }
    public int MaxArousal { get; set; }
    public bool Finished { get { return Arousal >= MaxArousal; } }
    public int Missing { get { return MaxArousal - Arousal; } }
    
    public CharacterData Data { get; private set; }
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

    public void LoadCharacter(CharacterData data)
    {
        if (CharaUI != null)
        {
            CharaUI.LoadCharacter(data);
            Dice = new DiceHolder(CharaUI.Dice);
            traits.TraitsUI = CharaUI.Traits;
        }
        else
        {
            Dice = new DiceHolder(null);
            traits.TraitsUI = null;
        }

        Data = data;
        Arousal = 0;
        MaxArousal = data.MaxArousal;
        CharaUI.FillGauge(Arousal, MaxArousal);

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
        if (CharaUI != null)
            aUI = CharaUI.AbilityUI(slot);
        else
            aUI = null;
        return new Ability(data, aUI);
    }

    public void SetAbility(AbilityData data, int slot)
    {
        if(CharaUI != null)
        {
            AbilityUI aUI = CharaUI.AbilityUI(slot);
            if(aUI != null)
                Abilities[slot].Init(data, CharaUI.AbilityUI(slot));
        }
    }

    public void LoadAbilities(List<AbilityData> abis)
    {
        Abilities.Clear();
        for (int i = 0; i < abis.Count; i++)
        {
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

        if(CharaUI != null)
        {
            CharaUI.FillGauge(Arousal, MaxArousal);
            GameManager.Instance.BattleManager.CheckBattleStatus();
        }
    }

    public void OpenBorrowed()
    {
        if (Data.Source != null && Data.Source != "")
            Application.OpenURL(Data.Source);
    }

    public void StartTurn()
    {
        CharaUI.ToggleAbilities(true);
        Dice.Roll(Rolls, null);
        Traits.StartTurn(this);

    }

    public void EndTurn()
    {
        foreach (Ability abi in Abilities)
            if (abi.IsActive)
                abi.ResetAbility();

        ResetDice();
        CharaUI.ToggleAbilities(false);
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
        CharaUI.ToggleAbilities(toggle);
    }

    public Character Clone()
    {
        Character c = new Character();
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
        CharaUI.PlayTurn(next);
        
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
} 