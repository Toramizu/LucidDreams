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

        if (Input.GetKeyDown(KeyCode.F9))
            Test9();
    }

    [SerializeField] AbilityData ad;
    [SerializeField] AbilityData ad2;

    void Test1()
    {
        GameManager.Instance.PlayerManager.LearnAbility(ad, 0);
        GameManager.Instance.PlayerManager.LearnAbility(ad2, 0);
        //dmd.nodes = dd.Nodes;
        //dmd.start = dd.Start;
        Debug.Log("Done");

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

    [SerializeField] GameObject mapCreator;

    void Test9()
    {
        mapCreator.SetActive(!mapCreator.activeInHierarchy);
    }
}
