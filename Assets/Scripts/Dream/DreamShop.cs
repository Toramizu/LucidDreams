using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DreamShop : Window
{
    [SerializeField] List<AbilityUI> abilities;
    [SerializeField] List<TMP_Text> prices;
    [SerializeField] TMP_Text maxArousalPrice;
    [SerializeField] TMP_Text maxArousalText;
    [SerializeField] int maxArousalIncrement = 5;
    [SerializeField] Button maxArousalButton;

    ShopData data;
    
    public void InitShop(ShopData data)
    {
        Open();

        if (this.data != data)
            FirstLoad(data);
        else
            RefreshLoad();

        GameManager.Instance.DreamManager.CanMove = false;
    }

    void FirstLoad(ShopData data)
    {
        this.data = data;
        List<AbilityData> aDatas = new List<AbilityData>(data.Abilities);
        int abiCount;
        if (data.MinAbilities == 0)
            abiCount = data.MaxAbilities;
        else
            abiCount = Random.Range(data.MinAbilities, data.MaxAbilities + 1);

        //foreach(Ability abi in abilities)
        for (int i = 0; i < abilities.Count; i++)
        {
            if (aDatas.Count == 0 || i >= abiCount)
                abilities[i].gameObject.SetActive(false);
            else
            {
                AbilityData aData = aDatas[Random.Range(0, aDatas.Count)];
                aDatas.Remove(aData);

                if (GameManager.Instance.PlayerManager.Abilities.Contains(aData))
                {
                    i--;
                }
                else
                {
                    abilities[i].gameObject.SetActive(true);
                    //abilities[i].Init(aData, null);
                    new Ability(aData, abilities[i]);
                    prices[i].text = AbilityPrice(aData).ToString();
                }
            }
        }

        maxArousalPrice.text = ArousalPrice().ToString();
        maxArousalText.text = "+" + maxArousalIncrement + " Max Arousal";

        maxArousalButton.gameObject.SetActive(true);
    }

    public void RefreshLoad()
    {
        foreach(AbilityUI abi in abilities)
            if (abi.isActiveAndEnabled && GameManager.Instance.PlayerManager.Abilities.Contains(abi.Data))
                abi.gameObject.SetActive(false);
    }

    public void BuyAbility(int id)
    {
        AbilityData aData = abilities[id].Data;

        if (GameManager.Instance.PlayerManager.Crystals < AbilityPrice(aData))
        {
            Debug.Log("Not enough crystals");
        }
        else
        {
            GameManager.Instance.PlayerManager.LearnAbility(aData, AbilityPrice(aData));
            abilities[id].Close();
        }
    }

    public void BuyMaxArousal()
    {
        if (GameManager.Instance.PlayerManager.Crystals < ArousalPrice())
        {
            Debug.Log("Not enough crystals");
        }
        else
        {
            GameManager.Instance.PlayerManager.IncreaseMaxArousal(maxArousalIncrement, ArousalPrice());
            maxArousalButton.gameObject.SetActive(false);
        }
    }

    int AbilityPrice(AbilityData abi)
    {
        return (int)abi.Price * data.AbilityMod;
    }

    int ArousalPrice()
    {
        return (int)maxArousalIncrement * data.ArousalMod;
    }

    public override void Close()
    {
        base.Close();
        GameManager.Instance.DreamManager.CanMove = true;
    }
}