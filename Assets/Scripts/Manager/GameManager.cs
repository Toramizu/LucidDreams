using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueUI;

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

    [SerializeField] DayManager dayManager;
    public DayManager DayManager { get { return dayManager; } }

    [SerializeField] Parser parser;
    public Parser Parser { get { return parser; } }

    [SerializeField] GameAssets assets;
    public GameAssets Assets { get { return assets; } }

    [SerializeField] SoundManager sound;
    public SoundManager Sound { get { return sound; } }

    public Flags Flags { get; private set; } = new Flags();

    [SerializeField] NotificationsUI notifs;
    [SerializeField] DialogueUI dialogue;


    [SerializeField] string defaultSuccubus;
    [SerializeField] string defaultDream;
    [SerializeField] Window dayTmp;

    public GameStatus Status { get; private set; }
    public int Time { get { return dayManager.Time; } }

    [SerializeField] NightPrepUI nightPreps;
    public NightPreps NightPreps { get { return nightPreps.NightPreps; } }

    private void Start()
    {
        Instance = this;
        dayTmp.FadeIn();
        Status = GameStatus.Day;
    }

    public void StartGame()
    {
        dayTmp.FadeOut();
        dayManager.Open(0);
        //StartDream(null, null);
    }

    public void StartBattle(SuccubusData opponent)
    {
        dreamManager.FadeOut();
        battleManager.FadeIn();
        battleManager.StartBattle(opponent, playerManager.Abilities);
        Status = GameStatus.Battle;
    }

    public void StartNightTime()
    {
        nightPreps.FadeIn();
        /*dayManager.FadeOut();
        dreamManager.StartDream(nightPreps.Default);
        Status = GameStatus.Dream;*/
    }

    public void StartDream(DreamData dData, SuccubusData pcData)
    {
        dayManager.FadeOut();

        if (dData == null)
            dData = AssetDB.Instance.Dreams[defaultDream];
        if (pcData == null)
            pcData = AssetDB.Instance.Succubi[defaultSuccubus];

        dreamManager.StartDream(dData, pcData);
        //playerManager.UpdateGauge();
        //battleManager.Close();
        Status = GameStatus.Dream;
    }

    public void StartDream(NightStat nightStat)
    {
        dayManager.FadeOut();

        if (nightStat == null)
            NextDay();
        else
        {
            dreamManager.StartDream(nightStat);
            //playerManager.UpdateGauge();
            Status = GameStatus.Dream;
        }
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
        dreamManager.FadeOut();
        NextDay();
    }

    public void NextDay()
    {
        Debug.Log("It's a new day!");
        dayManager.NewDay();
        dayTmp.FadeIn();
        Status = GameStatus.Day;
    }

    public void StartDialogue(DialogueData dial, DialogueAction action)
    {
        dialogue.Open(dial, action);
    }

    public void Notify(string text)
    {
        text = parser.ParseDescription(text, Flags.Strings);
        notifs.Notify(text);
    }

    public void Notify(string text, Color color)
    {
        text = parser.ParseDescription(text, Flags.Strings);
        notifs.Notify(text, color);
    }
}

public enum GameStatus
{
    Day,
    Dream,
    Battle,
}