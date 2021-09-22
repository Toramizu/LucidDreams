using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DreamShop : MonoBehaviour
{
    [SerializeField] List<Ability> abilities;
    [SerializeField] List<TMP_Text> prices;
    [SerializeField] TMP_Text maxArousalPrice;
    [SerializeField] TMP_Text maxArousalText;
    [SerializeField] int maxArousalIncrement = 5;
    [SerializeField] Button maxArousalButton;

    ShopData data;
    
    public void InitShop(ShopData data)
    {
        gameObject.SetActive(true);

        if (this.data != data)
        {
            this.data = data;
            List<AbilityData> aDatas = new List<AbilityData>(data.Abilities);

            //foreach(Ability abi in abilities)
            for (int i = 0; i < abilities.Count; i++)
            {
                if (aDatas.Count == 0 || i >= data.MaxAbilities)
                    abilities[i].gameObject.SetActive(false);
                else
                {
                    abilities[i].gameObject.SetActive(true);
                    AbilityData aData = aDatas[Random.Range(0, aDatas.Count)];
                    aDatas.Remove(aData);
                    abilities[i].Init(aData);
                    prices[i].text = AbilityPrice(aData).ToString();
                }
            }

            maxArousalPrice.text = ArousalPrice().ToString();
            maxArousalText.text = "+" + maxArousalIncrement + " Max Arousal";

            maxArousalButton.gameObject.SetActive(true);
        }

        GameManager.Instance.DreamManager.CanMove = false;
    }

    public void BuyAbility(int id)
    {
        AbilityData aData = abilities[id].Data;
        Debug.Log(aData.Title);

        if (GameManager.Instance.PlayerManager.Crystals < AbilityPrice(aData))
        {
            Debug.Log("Not enough crystals");
        }
        else
        {
            GameManager.Instance.PlayerManager.LearnAbility(aData, AbilityPrice(aData));
            abilities[id].Hide();
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

    public void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.DreamManager.CanMove = true;
    }
}
