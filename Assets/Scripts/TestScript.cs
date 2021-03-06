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

        if (Variables.debugMode)
        {
            if (Input.GetKeyDown(KeyCode.F5))
                Test5();
            if (Input.GetKeyDown(KeyCode.F6))
                Test6();
            if (Input.GetKeyDown(KeyCode.F7))
                Test7();
            if (Input.GetKeyDown(KeyCode.F8))
                Test8();


        }

        if (Input.GetKeyDown(KeyCode.F9))
            Test9();
        if (Input.GetKeyDown(KeyCode.F12))
                Test12();
    }

    void Test1()
    {
        DialogueAddArousal da1 = new DialogueAddArousal();
        da1.Amount = 20;
        da1.Play(null);
        GameManager.Instance.Notify("Done");
    }

    void Test2()
    {
        DialogueAddArousal2 da2 = new DialogueAddArousal2();
        da2.Proportion = -.5f;
        da2.Play(null);
        GameManager.Instance.Notify("Done");
    }

    void Test3()
    {
        DialogueData d = AssetDB.Instance.Dialogues["M_Bed"];
        GameManager.Instance.StartDialogue(d, null);
        Debug.Log("Done");
    }

    void Test5()
    {
        if (GameManager.Instance.Status == GameStatus.Dream || GameManager.Instance.Status == GameStatus.Battle)
        {
            GameManager.Instance.PlayerManager.Crystals = 99;
            GameManager.Instance.Notify("Cheat : Lust Crystals +99");
        }
    }

    void Test6()
    {
        if (GameManager.Instance.Status == GameStatus.Battle)
        {
            GameManager.Instance.BattleManager.Roll(1, null);
            GameManager.Instance.Notify("Cheat : Free roll");
        }
    }

    void Test7()
    {
        if (GameManager.Instance.Status == GameStatus.Dream || GameManager.Instance.Status == GameStatus.Battle)
        {
            GameManager.Instance.BattleManager.FullHeal();
            GameManager.Instance.Notify("Cheat : Full heal");
        }
    }

    void Test8()
    {
        if (GameManager.Instance.Status == GameStatus.Battle)
        {
            GameManager.Instance.BattleManager.EndBattle(true, true);
            GameManager.Instance.Notify("Cheat : Free win");
        }
    }

    [SerializeField] Window mapCreator;

    void Test9()
    {
        mapCreator.Toggle();
    }

    void Test12()
    {
        Variables.debugMode = !Variables.debugMode;
        GameManager.Instance.Notify("Debug Mode : " + Variables.debugMode);
    }
}
