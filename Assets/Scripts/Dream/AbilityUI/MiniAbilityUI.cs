using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAbilityUI : MonoBehaviour
{
    [SerializeField] MiniAbility miniAbilityPrefab;
    [SerializeField] int minisStorageX = 5;
    [SerializeField] int minisStorageY = 5;

    [SerializeField] float miniX;
    [SerializeField] float miniY;

    [SerializeField] AbilityUI abilityPreview;
    [SerializeField] Transform equipped;
    [SerializeField] Transform storage;

    List<MiniAbility> equippedAbi;
    List<MiniAbility> storedAbi;

    AbilityData displayed;
    
    void Init()
    {
        equippedAbi = new List<MiniAbility>();
        storedAbi = new List<MiniAbility>();

        for (int i = 0; i < 6; i++)
        {
            MiniAbility slot = Instantiate(miniAbilityPrefab, equipped, false);
            equippedAbi.Add(slot);
            slot.EquipSlot = i;
            slot.DefaultPosition = new Vector3(miniX * (i % 2), -miniY * (i / 2), 0);
            slot.AbiUI = this;
            slot.gameObject.SetActive(true);
        }

        for (int i = 0; i < minisStorageX * minisStorageY; i++)
        {
            MiniAbility slot = Instantiate(miniAbilityPrefab, storage, false);
            storedAbi.Add(slot);
            slot.DefaultPosition = new Vector3(miniX * (i % minisStorageX), -miniY * (i / minisStorageX), 0);
            slot.AbiUI = this;
            slot.gameObject.SetActive(true);
        }
    }

    public void Open()
    {
        if (equippedAbi == null)
            Init();

        Queue<AbilityData> toSlot = new Queue<AbilityData>(GameManager.Instance.PlayerManager.Abilities);

        foreach(MiniAbility abi in equippedAbi)
        {
            if (toSlot.Count > 0)
                abi.Ability = toSlot.Dequeue();
            else
                abi.Ability = null;
        }

        foreach (MiniAbility abi in storedAbi)
        {
            if (toSlot.Count > 0)
                abi.Ability = toSlot.Dequeue();
            else
                abi.Ability = null;
        }

        gameObject.SetActive(true);
        DisplayAbility(null);
    }

    public void Close()
    {
        List<AbilityData> abis = new List<AbilityData>();
        foreach (MiniAbility mAbi in equippedAbi)
            abis.Add(mAbi.Ability);

        foreach (MiniAbility mAbi in storedAbi)
            abis.Add(mAbi.Ability);
        GameManager.Instance.PlayerManager.Abilities = abis;

        gameObject.SetActive(false);
    }

    public void DisplayAbility(AbilityData abi)
    {
        if(abi == displayed || abi == null)
        {
            displayed = null;
            abilityPreview.gameObject.SetActive(false);
        }
        else
        {
            displayed = abi;
            abilityPreview.gameObject.SetActive(true);
            new Ability(abi, abilityPreview);
        }

        //abilityPreview.Init(abi, null);
    }
}
