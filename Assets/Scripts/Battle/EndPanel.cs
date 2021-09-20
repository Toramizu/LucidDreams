using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] GameObject victory;
    [SerializeField] TMP_Text victoryText;

    int crystals;

    private void Awake()
    {
        Close();
    }

    public void Victory(int crystals)
    {
        this.crystals = crystals;
        gameObject.SetActive(true);
        victory.SetActive(true);
        victoryText.text = "+" + crystals;
    }

    public void VictoryClose()
    {
        GameManager.Instance.EndBattle(crystals);
        Close();
    }

    void Close()
    {
        gameObject.SetActive(false);
        victory.SetActive(false);
    }
}
