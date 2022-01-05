using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueUI;

public class GameManager : MonoBehaviour
{
    #region Start
    public static GameManager Instance { get; set; }
    #endregion

    [SerializeField] Loading loading;
    public Loading Loading { get { return loading; } }

    [SerializeField] PlayerManager playerManager;
    public PlayerManager PlayerManager { get { return playerManager; } }

    [SerializeField] BattleManager battleManager;
    public BattleManager BattleManager { get { return battleManager; } }

    [SerializeField] DreamManager dreamManager;
    public DreamManager DreamManager { get { return dreamManager; } }

    [SerializeField] DayManager dayManager;
    public DayManager DayManager { get { return dayManager; } }

    [SerializeField] CharacterTalkUI charaTalk;
    public CharacterTalkUI CharaTalk { get { return charaTalk; } }

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
    [SerializeField] Window introWindow;

    public GameStatus Status { get; private set; }
    public int Time { get { return dayManager.Time; } }

    /*[SerializeField] NightPrepUI nightPreps;
    public NightPreps NightPreps { get { return nightPreps.NightPreps; } }*/
    public NightPreps NightPreps { get { return dayManager.NightPreps; } }

    private void Start()
    {
        Instance = this;
        introWindow.FadeIn();
        Status = GameStatus.Day;
    }

    public void StartGame()
    {
        Loading.FadeIn();
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        yield return null;
        introWindow.FadeOut();
        dayManager.Open();
        Loading.FadeOut();
    }

    public void StartBattle(SuccubusData opponent)
    {
        dreamManager.FadeOut();
        battleManager.FadeIn();
        battleManager.StartBattle(opponent, playerManager.Abilities);
        Status = GameStatus.Battle;
    }

    public void StartDream(DreamData dData, SuccubusData pcData)
    {
        dayManager.FadeOut();

        if (dData == null)
            dData = AssetDB.Instance.Dreams[defaultDream];
        if (pcData == null)
            pcData = AssetDB.Instance.Succubi[defaultSuccubus];

        dreamManager.StartDream(dData, pcData);
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
        dayManager.NewDay();
        Status = GameStatus.Day;
    }

    public void StartDialogue(DialogueData dial, DialogueAction action)
    {
        dialogue.Open(dial, action);
    }

    public void Notify(string text)
    {
        text = parser.Parse(text);
        notifs.Notify(text);
    }

    public void Notify(string text, Color color)
    {
        text = parser.Parse(text);
        notifs.Notify(text, color);
    }
}

public enum GameStatus
{
    Day,
    Dream,
    Battle,
}