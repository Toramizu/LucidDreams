using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Start
    public static GameManager Instance { get; set; }

    private void Start()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] PlayerManager playerManager;
    public PlayerManager PlayerManager { get { return playerManager; } }

    [SerializeField] BattleManager battleManager;
    public BattleManager BattleManager { get { return battleManager; } }

    [SerializeField] DreamManager dreamManager;
    public DreamManager DreamManager { get { return dreamManager; } }

    [SerializeField] Parser parser;
    public Parser Parser { get { return parser; } }

    [SerializeField] GameAssets assets;
    public GameAssets Assets { get { return assets; } }


    [SerializeField] CharacterData pData;
    [SerializeField] DreamData dData;
    [SerializeField] GameObject dayTmp;

    private void Awake()
    {
        battleManager.Open();
        dreamManager.Open();
        dayTmp.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        StartDream(dData, pData);
        dayTmp.SetActive(false);
    }

    public void StartBattle(CharacterData opponent)
    {
        dreamManager.Close();
        battleManager.StartBattle(opponent, playerManager.Abilities);
    }

    public void StartDream(DreamData data, CharacterData pcData)
    {
        dreamManager.StartDream(data, pcData);
        battleManager.Close();
    }

    public void EndBattle(int crystals, AbilityData newAbility)
    {
        playerManager.Crystals += crystals;
        playerManager.LearnAbility(newAbility, 0);

        battleManager.Close();
        dreamManager.Open();
    }

    public void NextDay()
    {
        Debug.Log("It's a new day!");
        dayTmp.SetActive(true);
    }
}
