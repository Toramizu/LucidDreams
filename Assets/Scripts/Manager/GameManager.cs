using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Start
    public static GameManager Instance { get; set; }
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

    [SerializeField] SoundManager sound;
    public SoundManager Sound { get { return sound; } }

    public Flags Flags { get; private set; } = new Flags();

    [SerializeField] NotificationsUI notifs;
    [SerializeField] DialogueUI dialogue;


    [SerializeField] CharacterData pData;
    [SerializeField] DreamData dData;
    [SerializeField] Window dayTmp;

    public GameStatus Status { get; private set; }

    private void Start()
    {
        Instance = this;
        dayTmp.Open();
        Status = GameStatus.Day;
    }

    public void StartGame()
    {
        StartDream(dData, pData);
        dayTmp.FadeOut();
    }

    public void StartBattle(CharacterData opponent)
    {
        dreamManager.FadeOut();
        battleManager.FadeIn();
        battleManager.StartBattle(opponent, playerManager.Abilities);
        Status = GameStatus.Battle;
    }

    public void StartDream(DreamData data, CharacterData pcData)
    {
        dreamManager.StartDream(data, pcData);
        playerManager.UpdateGauge();
        //battleManager.Close();
        Status = GameStatus.Dream;
    }

    public void EndBattle(int crystals, AbilityData newAbility)
    {
        playerManager.Crystals += crystals;
        if(newAbility != null)
            playerManager.LearnAbility(newAbility, 0);
        playerManager.UpdateGauge();

        battleManager.FadeOut();
        dreamManager.FadeIn();
    }

    public void EndDream()
    {

    }

    public void NextDay()
    {
        Debug.Log("It's a new day!");
        dayTmp.FadeIn();
        Status = GameStatus.Day;
    }

    public void StartDialogue(DialogueData dial)
    {
        dialogue.Open(dial);
    }

    public void Notify(string text)
    {
        text = parser.ParseDescription(text, Flags.Strings);
        notifs.Notify(text);
    }
}

public enum GameStatus
{
    Day,
    Dream,
    Battle,
}