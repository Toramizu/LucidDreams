using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] MiniAbility miniAbilityPrefab;
    [SerializeField] int minisStorageX = 5;
    [SerializeField] int minisStorageY = 5;

    [SerializeField] float miniX;
    [SerializeField] float miniY;

    [SerializeField] Ability abilityPreview;
    [SerializeField] Transform equipped;
    [SerializeField] Transform storage;

    List<MiniAbility> equippedAbi;
    List<MiniAbility> storedAbi;
    
    void Init()
    {
        equippedAbi = new List<MiniAbility>();
        storedAbi = new List<MiniAbility>();

        for (int i = 0; i < 6; i++)
        {
            MiniAbility slot = Instantiate(miniAbilityPrefab, equipped, true);
            equippedAbi.Add(slot);
            slot.EquipSlot = i;
            slot.DefaultPosition = new Vector3(miniX * (i % 2), -miniY * (i / 2), 0);
            slot.AbiUI = this;
            slot.gameObject.SetActive(true);
        }

        for (int i = 0; i < minisStorageX * minisStorageY; i++)
        {
            MiniAbility slot = Instantiate(miniAbilityPrefab, storage, true);
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
        abilityPreview.Init(abi);
    }
}
