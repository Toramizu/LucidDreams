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


    [SerializeField] Trait locked;
    
    void Test1()
    {
        GameManager.Instance.BattleManager.GetCharacter(true).Traits.AddTrait(locked, 1);
    }

    void Test2()
    {
        //GameManager.Instance.BattleManager.R(1);
        //Debug.Log(GameManager.Instance.BattleManager.GetCharacter(true).CumulativeBonus);
    }

    void Test3()
    {
        //GameManager.Instance.BattleManager.R(1);
        GameManager.Instance.BattleManager.Give(0);
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
