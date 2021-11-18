using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DreamManager : Window, GridManager
{
    [SerializeField] NodePanel nodePanel;

    [SerializeField] TMP_Text crystals;

    [SerializeField] DreamShop shop;
    //[SerializeField] MeditationPanel medit;
    [SerializeField] List<DialogueData> meditations;
    List<DialogueData> dreamMeditations;

    [SerializeField] DreamToken playerToken;

    public bool CanMove { get; set; }

    DreamNode currentNode;
    public override void Open()
    {
        base.Open();
        CanMove = true;
    }

    public override void Close()
    {
        base.Close();
        CanMove = false;
    }

    public void StartDream(DreamData data, SuccubusData cData)
    {
        Open();
        GameManager.Instance.PlayerManager.SetPlayer(cData);

        ContinueDream(data);

        //medit.CanMeditate = true;
        playerToken.SetCharacter(cData);
    }

    public void ContinueDream(DreamData data)
    {
        DreamMapData map = data.Maps[Random.Range(0, data.Maps.Count)];

        Dictionary<NodeContent, List<DreamNode>> nodes = nodePanel.AddNodes(map.Nodes, this);
        FillNodes(nodes, map, data);

        dreamMeditations = data.Meditations;
        CanMove = true;
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
            List<DreamData> nexts = new List<DreamData>(data.Nexts);

            if (nexts.Count == 0)
            {
                while (nexts.Count > 0 && exits.Count > 0)
                {
                    DreamNode node = exits[Random.Range(0, exits.Count)];
                    exits.Remove(node);

                    node.SetExit(null);
                }
            }
            else
            {
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
            List<SuccubusData> opponents = new List<SuccubusData>(data.Succubi);

            for (int i = 0; i < data.SuccubiCount && succubi.Count > 0 && opponents.Count > 0; i++)
            {
                SuccubusData rSucc = opponents[Random.Range(0, opponents.Count)];
                opponents.Remove(rSucc);
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
            //iTween.Stop(playerToken.gameObject);
            StartMove(Pathfinder.FindPath(currentNode, node));
        }
    }
    public void OpenShop(ShopData data)
    {
        shop.InitShop(data);
    }

    public void Meditate()
    {
        List<DialogueData> meds = new List<DialogueData>(meditations);
        meds.AddRange(dreamMeditations);
        DialogueData med = meds[Random.Range(0, meds.Count)];
        GameManager.Instance.StartDialogue(med);
        //medit.Open();
    }

    #region Movement
    Queue<PathNode> path;

    void StartMove(List<PathNode> path)
    {
        if (path == null)
            path = new List<PathNode>() { currentNode };

        this.path = new Queue<PathNode>(path);
        CheckAndMove(currentNode);

        /*if (path == null)
        {
            
            currentNode.OnEnter();
        }
        else
        {
            this.path = new Queue<PathNode>(path);
            CheckAndMove(currentNode);
        }*/
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
            //currentNode = (DreamNode)path.Dequeue();
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
