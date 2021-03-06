using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuccubusUI : MonoBehaviour
{
    [SerializeField] Succubus chara = new Succubus();
    public Succubus Character { get { return chara; } }

    [SerializeField] Image characterImage;
    [SerializeField] ImageUI characterImage2;
    [SerializeField] Sprite defaultImage;
    [SerializeField] TMP_Text characterName;
    [SerializeField] SimpleGauge gauge;
    [SerializeField] Button borrowed;

    [SerializeField] Transform abilityPanel;
    [SerializeField] List<AbilityUI> abilitiesUI;
    public int AbilityCount { get { return abilitiesUI.Count; } }

    [SerializeField] DiceHolderUI dice;
    public DiceHolderUI Dice { get { return dice; } }

    //[SerializeField] TraitsSheet traits;
    [SerializeField] TraitsSheetUI traits;
    public TraitsSheetUI Traits { get { return traits; } }

    private void Start()
    {
        chara.SuccubUI = this;
    }

    public void LoadCharacter(SuccubusData data)
    {
        characterImage2.Init(data.Image);

        /*if (data.Image == null)
            characterImage.sprite = defaultImage;
        else
            characterImage.sprite = data.Image;*/

        characterName.text = data.ID;

        traits.Clear();

        //borrowed.gameObject.SetActive(data.Source != null && data.Source != "");

        foreach (AbilityUI abi in abilitiesUI)
            abi.Remove();
    }

    public void FillGauge(int amount, int maxAmount)
    {
        if (amount > maxAmount)
            amount = maxAmount;
        gauge.Fill(amount, maxAmount);
    }

    public AbilityUI AbilityUI(int i)
    {
        if (abilitiesUI.Count > i)
            return abilitiesUI[i];
        else
            return null;
    }

    public void ClearAbilities()
    {
        foreach (AbilityUI abi in abilitiesUI)
            abi.Remove();
    }

    public void ToggleAbilities(bool toggle)
    {
        abilityPanel.gameObject.SetActive(toggle);
    }

    #region Auto play
    List<DieToSlot> toPlace;
    Queue<DieToSlot> slots;
    IDie currentSlot;
    RolledDie currentDie;

    public void PlayTurn(List<DieToSlot> toPlace)
    {
        StartCoroutine(AutoMoveDice(toPlace));
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
        if (GameManager.Instance.CheckBattleStatus())
            return;

        if (slots.Count == 0)
        {
            StartCoroutine(WaitEndTurn());
        }
        else
        {
            DieToSlot dts = slots.Dequeue();
            currentDie = dts.Die;
            currentSlot = dts.Slot;

            if (currentDie.DieUI == null)
            {
                int i = currentDie.Value;
                currentDie = dice.GetDieByID(currentDie.ID);
            }

            iTween.MoveTo(currentDie.GameObject, iTween.Hash(
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

    void SlotDie()
    {
        currentSlot.OnDrop(currentDie);
        PlaceNext();
    }

    IEnumerator WaitEndTurn()
    {
        yield return new WaitForSecondsRealtime(1);
        GameManager.Instance.BattleManager.NextRound();
    }
    #endregion
}
