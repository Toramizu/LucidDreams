using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Test1();
        if (Input.GetKeyDown(KeyCode.F2))
            Test2();
        if (Input.GetKeyDown(KeyCode.F3))
            Test3();

        if (Input.GetKeyDown(KeyCode.F5))
            Test5();
        if (Input.GetKeyDown(KeyCode.F6))
            Test6();
        if (Input.GetKeyDown(KeyCode.F7))
            Test7();
        if (Input.GetKeyDown(KeyCode.F8))
            Test8();

        if (Input.GetKeyDown(KeyCode.F9))
            Test9();
    }

    [SerializeField] List<DialogueData> dials;
    void Test1()
    {
        Debug.Log("Done");
    }

    void Test2()
    {
        SuccubusData sd = AssetDB.Instance.Succubi["Holy"];
        Debug.Log(sd.ID + " - " + sd.MaxArousal);
    }

    void Test3()
    {
        DialogueData d = AssetDB.Instance.Dialogues["M_Bed"];
        GameManager.Instance.StartDialogue(d, null);
        Debug.Log("Done");
    }

    void Test5()
    {
        GameManager.Instance.PlayerManager.Crystals = 99;
    }

    void Test6()
    {
        GameManager.Instance.BattleManager.Roll(1, null);
    }

    void Test7()
    {
        GameManager.Instance.BattleManager.FullHeal();
    }

    void Test8()
    {
        GameManager.Instance.BattleManager.EndBattle(true);
    }

    [SerializeField] Window mapCreator;

    void Test9()
    {
        mapCreator.Toggle();
    }
}
