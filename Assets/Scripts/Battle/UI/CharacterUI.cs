using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] Character chara = new Character();
    public Character Character { get { return chara; } }

    [SerializeField] Image characterImage;
    [SerializeField] Sprite defaultImage;
    [SerializeField] TMP_Text characterName;
    [SerializeField] SimpleGauge gauge;
    [SerializeField] Button borrowed;

    [SerializeField] Transform abilityPanel;
    [SerializeField] List<AbilityUI> abilitiesUI;

    [SerializeField] DiceHolderUI dice;
    public DiceHolderUI Dice { get { return dice; } }

    [SerializeField] TraitsSheet traits;
    public TraitsSheet Traits { get { return traits; } }

    private void Start()
    {
        chara.CharaUI = this;
    }

    public void LoadCharacter(CharacterData data)
    {
        if (data.Image == null)
            characterImage.sprite = defaultImage;
        else
            characterImage.sprite = data.Image;

        characterName.text = data.Name;

        traits.Clear();

        borrowed.gameObject.SetActive(data.Source != null && data.Source != "");

        foreach (AbilityUI abi in abilitiesUI)
            abi.Remove();
    }

    public void FillGauge(int amount, int maxAmount)
    {
        gauge.Fill(amount, maxAmount);
    }

    public AbilityUI AbilityUI(int i)
    {
        if (abilitiesUI.Count > i)
            return abilitiesUI[i];
        else
            return null;
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
        if (slots.Count == 0)
        {
            StartCoroutine(WaitEndTurn());
        }
        else
        {
            DieToSlot dts = slots.Dequeue();
            currentDie = dts.Die;
            currentSlot = dts.Slot;

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
