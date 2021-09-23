using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Ability : Hidable
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    [SerializeField] List<DieSlot> DiceSlots;

    [SerializeField] TMP_Text countText;

    int? count;
    public int? Count {
        get {return count; }
        set { count = value;
            if(count == null)
            {
                countText.gameObject.SetActive(false);
            }
            else if (count <= 0)
            {
                countText.gameObject.SetActive(true);
                countText.text = "0";
            }
            else
            {
                countText.gameObject.SetActive(true);
                countText.text = value.ToString();
            }
        }
    }

    LinkedValue link;

    public AbilityData Data { get; private set; }
    public int Uses { get; set; }
    List<AbilityEffect> effects = new List<AbilityEffect>();

    /*int locked;
    public int Lock {
        get { return locked; }
        set {
            locked = value;

        }
    }*/

    [SerializeField] GameObject lockTransform;
    [SerializeField] LockSlot lockSlot;
    public LockSlot LockSlot { get { return lockSlot; } }
    bool locked;
    public bool Locked {
        get { return locked; }
        set
        {
            LockAbility(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        foreach (DieSlot slot in DiceSlots)
            slot.Ability = this;
    }

    public void Init(AbilityData data)
    {
        this.Data = data;

        if (data == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        title.text = data.Title;
        description.text = GameManager.Instance.Parser.ParseDescription(data.Description);

        if (data.EqualDice)
            link = new LinkedValue();
        else
            link = null;

        for (int i = 0; i < DiceSlots.Count; i++)
        {
            if (i < data.Conditions.Count)
            {
                DiceSlots[i].gameObject.SetActive(true);
                DiceSlots[i].SetCondition(data.Conditions[i].ToCondition()); ;
                DiceSlots[i].Linked = link;
            }
            else
                DiceSlots[i].gameObject.SetActive(false);
        }

        if (data.Total <= 0)
            Count = null;
        else
            Count = data.Total;

        effects.Clear();
        foreach (EffectData effect in data.Effects)
            effects.Add(effect.ToEffect());

        Locked = false;

        ResetAbility();
    }

    public void ResetAbility()
    {
        Show();
        Uses = Data.Uses;
        if (link != null)
        {
            link.Value = 0;
            link.Count = 0;
        }

        if (Count <= 0)
            Count = Data.Total;

        //Locked = false;
    }

    public void Check()
    {
        if (count == null)
        {
            foreach (DieSlot slot in DiceSlots)
            {
                if (slot.isActiveAndEnabled && !slot.Slotted)
                    return;
            }
            PlayAbility();
        }
        else
        {
            foreach (DieSlot slot in DiceSlots)
                if(slot.Slotted)
                {
                    Count -= slot.SlottedDie.Value;
                    slot.SlottedDie.gameObject.SetActive(false);
                }

            if(count <= 0)
                PlayAbility();
        }
    }

    public void LockAbility(bool toggle)
    {
        locked = toggle;
        lockTransform.SetActive(toggle);
    }

    void PlayAbility()
    {
        int total = 0;
        foreach (DieSlot slot in DiceSlots)
        {
            if (slot.SlottedDie != null)
            {
                total += slot.Value;
                slot.SlottedDie.gameObject.SetActive(false);
            }
        }

        foreach (AbilityEffect effect in effects)
            effect.Play(total);
        
        Uses--;
        if (Uses <= 0)
            Hide();
    }

    public /*List<RolledDie>*/ void TryFill(List<RolledDie> dice, Dictionary<RolledDie, IDie> toPlace) // TODO : Manage locked dice
    {
        if (dice.Count == 0) return;

        //Keep dice fitting conditions
        Dictionary<RolledDie, IDie> placed = new Dictionary<RolledDie, IDie>();
        int? count = this.count;

        foreach (IDie slot in DiceSlots)
        {
            if (slot.IsActive)
            {
                foreach (RolledDie die in dice)
                {
                    //If a die fits
                    if (slot.Check(die.Value) && !placed.ContainsKey(die))
                    {
                        //Keep die & go to next slot
                        placed.Add(die, slot);

                        if(count != null)
                        {
                            count -= die.Value;
                            if (count <= 0)
                                break;
                        }
                        else
                            break;
                    }
                    else
                    {
                        //If no die found, skip ability
                        return; //dice;
                    }
                }
            }
        }


        //Place die in slot & Remove placed dice from remaining
        foreach(RolledDie die in placed.Keys)
        {
            toPlace.Add(die, placed[die]);
            dice.Remove(die);
        }
    }
}

public enum AbilityStatus
{
    Weak = -1,
    Normal = 0,
    Strong = 1
}
