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

        if (Input.GetKeyDown(KeyCode.F5))
            Test5();
    }

    [SerializeField] OpponentData oData;
    [SerializeField] DreamData dData;
    [SerializeField] AbilityData aData;
    void Test1()
    {
        //GameManager.Instance.DreamManager.StartDream(dData);
        //GameManager.Instance.BattleManager.StartBattle(oData);
        GameManager.Instance.BattleManager.SetAbility(aData, 0);
        Debug.Log("Done");

    }

    void Test2()
    {
        int r = Random.Range(1, 7);
        Debug.Log(r);

        GameManager.Instance.BattleManager.InflictDamage(r);
    }

    void Test5()
    {
        dData.Nodes.Clear();

        DreamNodeData dnd1 = new DreamNodeData();
        dnd1.Coo = new Coordinate(0, 0);
        dData.Nodes.Add(dnd1);
        DreamNodeData dnd2 = new DreamNodeData();
        dnd2.Coo = new Coordinate(0, 1);
        dData.Nodes.Add(dnd2);
        DreamNodeData dnd3 = new DreamNodeData();
        dnd3.Coo = new Coordinate(0, 2);
        dData.Nodes.Add(dnd3);
        DreamNodeData dnd4 = new DreamNodeData();
        dnd4.Coo = new Coordinate(0, 3);
        dData.Nodes.Add(dnd4);
        DreamNodeData dnd5 = new DreamNodeData();
        dnd5.Coo = new Coordinate(0, -1);
        dData.Nodes.Add(dnd5);
        DreamNodeData dnd6 = new DreamNodeData();
        dnd6.Coo = new Coordinate(0, -2);
        dData.Nodes.Add(dnd6);
        DreamNodeData dnd7 = new DreamNodeData();
        dnd7.Coo = new Coordinate(0, -3);
        dData.Nodes.Add(dnd7);

        DreamNodeData dnd9 = new DreamNodeData();
        dnd9.Coo = new Coordinate(1,1);
        dData.Nodes.Add(dnd9);
        DreamNodeData dnd10 = new DreamNodeData();
        dnd10.Coo = new Coordinate(2, 1);
        dData.Nodes.Add(dnd10);
        DreamNodeData dnd11 = new DreamNodeData();
        dnd11.Coo = new Coordinate(2, 2);
        dData.Nodes.Add(dnd11);

        DreamNodeData dnd12 = new DreamNodeData();
        dnd12.Coo = new Coordinate(-1, 1);
        dData.Nodes.Add(dnd12);
        DreamNodeData dnd13 = new DreamNodeData();
        dnd13.Coo = new Coordinate(-2, 1);
        dData.Nodes.Add(dnd13);
        DreamNodeData dnd14 = new DreamNodeData();
        dnd14.Coo = new Coordinate(-2, 2);
        dData.Nodes.Add(dnd14);
        Debug.Log("Done");
    }
}