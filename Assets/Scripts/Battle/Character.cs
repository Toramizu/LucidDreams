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

    public int Arousal { get; set; }
    public int MaxArousal { get; set; }
    public bool Finished { get { return Arousal >= MaxArousal; } }

    //public int Level { get; set; }
    //CharacterLevel currentLevel;
    //public int Exp { get; set; }

    protected CharacterData Data { get; private set; }
    public int Crystals { get { return Data.Crystals; } }

    [SerializeField] Transform abilityPanel;
    [SerializeField] List<Ability> abilities;
    public List<Ability> Abilities { get { return abilities; } }
    public int Rolls { get; set; }
    [SerializeField] DiceHolder dice;
    public DiceHolder Dice { get { return dice; } }

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
        //currentLevel = data.Level[0];

        if (data.Image == null)
            characterImage.sprite = defaultImage;
        else
            characterImage.sprite = data.Image;

        characterName.text = data.Name;
        Arousal = 0;
        MaxArousal = data.MaxArousal;
        gauge.Fill(Arousal, MaxArousal);

        traits.Clear();

        borrowed.gameObject.SetActive(data.Source != null && data.Source != "");

        Rolls = data.Dice;
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
        if (slot < abilities.Count)
            abilities[slot].Init(data);
    }

    public void LoadAbilities(List<AbilityData> abis)
    {
        for (int i = 0; i < abis.Count && i < abilities.Count; i++)
            SetAbility(abis[i], i);
    }

    public virtual bool InflictDamage(int amount)
    {
        Arousal += amount;
        if (Arousal < 0)
            Arousal = 0;
        else if (Arousal > MaxArousal)
            Arousal = MaxArousal;

        gauge.Fill(Arousal, MaxArousal);

        return Arousal >= MaxArousal;
    }

    public void OpenBorrowed()
    {
        if (Data.Source != null && Data.Source != "")
            Application.OpenURL(Data.Source);
    }

    public void StartTurn()
    {
        ToggleAbilities(true);
        dice.Roll(Rolls, true, null);
        Traits.StartTurn(this);

    }

    public void EndTurn()
    {
        foreach (Ability abi in abilities)
            if (abi.isActiveAndEnabled)
                abi.ResetAbility();

        ResetDice();
        ToggleAbilities(false);
        Traits.EndTurn(this);
    }

    public void Roll(int amount, bool reset, DiceCondition condition)
    {
        dice.Roll(amount, reset, condition);
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


    public void ToggleAbilities(bool toggle)
    {
        abilityPanel.gameObject.SetActive(toggle);
    }

    #region Auto Play

    BattleAI ai = new BattleAI();

    public void PlayTurn()
    {
        /*List<RolledDie> rolled = new List<RolledDie>(dice.RolledDice).OrderByDescending(o => o.Value).ToList();
        for (int i = rolled.Count - 1; i >= 0; i--)
            if (rolled[i].Locked)
                rolled.Remove(rolled[i]);

        Dictionary<RolledDie, IDie> toPlace = new Dictionary<RolledDie, IDie>();
        foreach (Ability abi in abilities)
        {
            if (abi.isActiveAndEnabled)
                abi.TryFill(rolled, toPlace);

            if (rolled.Count == 0)
                break;

        
        StartCoroutine(AutoMoveDice(toPlace));
        }*/

        List<DieToSlot> next = ai.FindNextAction(abilities, dice.RolledDice);
        StartCoroutine(AutoMoveDice(next));
    }

    Dictionary<RolledDie, IDie> OLD_toPlace;
    List<DieToSlot> toPlace;
    Queue<RolledDie> OLD_slots;
    Queue<DieToSlot> slots;
    IDie currentSlot;
    RolledDie currentDie;

    IEnumerator AutoMoveDice(Dictionary<RolledDie, IDie> toPlace)
    {
        yield return null;
        this.OLD_toPlace = toPlace;
        OLD_slots = new Queue<RolledDie>(toPlace.Keys);

        OLD_PlaceNext();
    }

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
    }
    #endregion
} 