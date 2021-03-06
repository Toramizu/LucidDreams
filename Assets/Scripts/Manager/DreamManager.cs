using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DreamManager : Window, GridManager
{
    [SerializeField] NodePanel nodePanel;

    [SerializeField] TMP_Text crystals;

    [SerializeField] DreamShop shop;
    List<DialogueData> dreamMeditations;
    List<SuccubusData> remainingSuccubi;

    [SerializeField] DreamToken playerToken;

    [SerializeField] Window wakeUpWindow;

    [SerializeField] string defaultDream;

    DreamData data;
    NightStat nightStats;

    public bool CanMove { get; set; }
    int level;
    public bool IsBossfight { get; set; }
    
    DreamNode currentNode;
    public override void FadeIn()
    {
        base.FadeIn();
        CanMove = true;
    }

    public override void FadeOut()
    {
        base.FadeOut();
        CanMove = false;
    }

    public void StartDream(DreamData data, SuccubusData cData)
    {
        FadeIn();
        GameManager.Instance.PlayerManager.SetPlayer(cData);

        level = 0;
        ContinueDream(data);

        playerToken.SetCharacter(cData);
    }

    public void StartDream(NightStat stats)
    {
        nightStats = stats;
        FadeIn();
        GameManager.Instance.PlayerManager.SetPlayer(stats);
        
        level = 0;
        ContinueDream(AssetDB.Instance.Dreams[defaultDream]);

        playerToken.SetCharacter(stats.Succubus);
    }

    public void OpenWakeUpWindown()
    {
        wakeUpWindow.FadeIn();
    }

    public void NextDream()
    {

        /*if (next == null)
            GameManager.Instance.DreamManager.WinDream();
        else
            GameManager.Instance.DreamManager.ContinueDream(next);*/
    }

    public void ContinueDream(DreamData data)
    {
        this.data = data;
        DreamMapData map = data.Map;

        Dictionary<NodeContent, List<DreamNode>> nodes = nodePanel.AddNodes(map.Nodes, this);
        FillNodes(nodes, map, data);

        dreamMeditations = data.Meditations;
        CanMove = true;
        IsBossfight = false;
        
        level++;
    }

    void FillNodes(Dictionary<NodeContent, List<DreamNode>> nodes, DreamMapData map, DreamData data)
    {
        if (nodes.ContainsKey(NodeContent.Start))
        {
            List<DreamNode> starts = nodes[NodeContent.Start];
            DreamNode start = starts[Random.Range(0, starts.Count)];
            start.SetType(NodeContent.Start);
            currentNode = start;
            currentNode.PlacePlayer(playerToken);
        }
        else
            Debug.LogError("No start found for map " + map.ID);

        if (nodes.ContainsKey(NodeContent.Exit))
        {
            List<DreamNode> exits = new List<DreamNode>(nodes[NodeContent.Exit]);

            if (data.Nexts == null || data.Nexts.Count == 0)
            {
                DreamNode node = exits[Random.Range(0, exits.Count)];

                node.SetExit(null);
            }
            else
            {
                List<DreamData> nexts = new List<DreamData>(data.Nexts);
                while (nexts.Count > 0 && exits.Count > 0)
                {
                    DreamData next = nexts[Random.Range(0, nexts.Count)];
                    nexts.Remove(next);
                    DreamNode node = exits[Random.Range(0, exits.Count)];
                    exits.Remove(node);

                    node.SetExit(next);
                }
            }
        }

        if (nodes.ContainsKey(NodeContent.Succubus))
        {
            List<DreamNode> succubi = new List<DreamNode>(nodes[NodeContent.Succubus]);
            remainingSuccubi = new List<SuccubusData>(data.Succubi);

            for (int i = 0; i < data.SuccubiCount && succubi.Count > 0 && remainingSuccubi.Count > 0; i++)
            {
                SuccubusData rSucc = remainingSuccubi[Random.Range(0, remainingSuccubi.Count)];
                remainingSuccubi.Remove(rSucc);
                DreamNode node = succubi[Random.Range(0, succubi.Count)];
                succubi.Remove(node);
                node.SetCharacter(rSucc, false);
            }
        }

        if (nodes.ContainsKey(NodeContent.Boss))
        {
            List<DreamNode> bosses = nodes[NodeContent.Boss];
            bosses[Random.Range(0, bosses.Count)].SetCharacter(data.Boss, true);
        }

        if (nodes.ContainsKey(NodeContent.Shop))
        {
            List<DreamNode> shops = nodes[NodeContent.Shop];
            shops[Random.Range(0, shops.Count)].SetShop(data.Shop);
        }

        if (nodes.ContainsKey(NodeContent.Meditation))
        {
            List<DreamNode> medit = nodes[NodeContent.Meditation];
            medit[Random.Range(0, medit.Count)].SetMeditation();
        }
    }
        
    public void Clicked(DreamNode node)
    {
        if (CanMove)
        {
            StartMove(Pathfinder.FindPath(currentNode, node));
        }
    }
    public void OpenShop(ShopData data)
    {
        shop.InitShop(data, remainingSuccubi);
    }

    public void Meditate()
    {
        List<DialogueData> meds = new List<DialogueData>(data.Meditations);
        meds.AddRange(dreamMeditations);
        DialogueData med = meds[Random.Range(0, meds.Count)];
        GameManager.Instance.StartDialogue(med, null);
        //medit.Open();
    }

    public void GainFriendshipPoints()
    {
        nightStats.Character.AddRelationPoints(RelationType.Friendship, nightStats.GetRelationValue(0, Variables.friendshipPerLevel) * level, false);
    }

    public void WinDream()
    {
        EndDream();

        GameManager.Instance.Notify("You wake up victorious!");
        nightStats.Character.AddRelationPoints(RelationType.Love, (int) (level * Variables.lovePerLevel * Variables.bossLoveLossMod), false);
    }

    public void FleeDream()
    {
        GameManager.Instance.Notify("You wake up quite aroused.");
        nightStats.Character.AddRelationPoints(RelationType.Love, level * Variables.lovePerLevel, false);
        EndDream();
    }

    public void LoseDream()
    {
        EndDream();

        if (IsBossfight)
        {
            GameManager.Instance.Notify("You wake up, heart pounding in your chest...");
            //nightStats.Character.AddRelationPoints(2, nightStats.GetRelationValue(2, Variables.lossOnDefeatBoss), false);
            nightStats.Character.AddRelationPoints(RelationType.Loss, (int)(level * Variables.lossPerLevel * Variables.bossLoveLossMod), false);
        }
        else
        {
            GameManager.Instance.Notify("You wake up ashamed...");
            //nightStats.Character.AddRelationPoints(2, nightStats.GetRelationValue(2, Variables.lossOnDefeatBoss), false);
            nightStats.Character.AddRelationPoints(RelationType.Loss, level * Variables.lossPerLevel, false);
        }
    }

    void EndDream()
    {
        GameManager.Instance.NextDay();
        wakeUpWindow.FadeOut();
        level = 0;
        FadeOut();
    }

    #region Movement
    Queue<PathNode> path;

    void StartMove(List<PathNode> path)
    {
        if (path == null)
            path = new List<PathNode>() { currentNode };

        this.path = new Queue<PathNode>(path);
        CheckAndMove(currentNode);
    }

    void CheckAndMove(DreamNode current)
    {
        currentNode = current;
        if (path == null || path.Count == 0 || current.PathStop)
        {
            currentNode.OnEnter();
        }
        else
        {
            MoveTo((DreamNode)path.Dequeue());
        }
    }

    void MoveTo(DreamNode node)
    {
        iTween.MoveTo(playerToken.gameObject, iTween.Hash(
            "x", node.TokenXPos,
            "y", node.TokenYPos,
            "time", .8f,
            "easeType", iTween.EaseType.easeOutSine,
            "onComplete", "CheckAndMove",
            "oncompleteparams", node,
            "onCompleteTarget", gameObject
            ));
    }

    #endregion
}
