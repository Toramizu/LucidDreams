using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DreamManager : MonoBehaviour, GridManager
{
    [SerializeField] DreamGrid grid;
    [SerializeField] NodePanel nodePanel;

    [SerializeField] TMP_Text crystals;

    [SerializeField] DreamShop shop;

    [SerializeField] DreamToken playerToken;

    public bool CanMove { get; set; }

    DreamNode currentNode;

    private void Start()
    {
        grid.Init(this);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        CanMove = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        CanMove = false;
    }

    public void StartDream(DreamData data, CharacterData cData)
    {
        Open();
        GameManager.Instance.PlayerManager.SetPlayer(cData);

        ContinueDream(data);

        playerToken.SetCharacter(cData);
    }

    public void ContinueDream(DreamData data)
    {
        DreamMapData map = data.Maps[Random.Range(0, data.Maps.Count)];

        Dictionary<NodeContent, List<DreamNode>> nodes = nodePanel.AddNodes(map.Nodes, this);
        FillNodes(nodes, map, data);

        crystals.text = "0";
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

            while (nexts.Count > 0 && exits.Count > 0)
            {
                DreamData next = nexts[Random.Range(0, nexts.Count)];
                nexts.Remove(next);
                DreamNode node = exits[Random.Range(0, exits.Count)];
                exits.Remove(node);

                node.SetExit(next);
            }
        }

        if (nodes.ContainsKey(NodeContent.Succubus))
        {
            List<DreamNode> succubi = new List<DreamNode>(nodes[NodeContent.Succubus]);
            List<CharacterData> opponents = new List<CharacterData>(data.Succubi);

            for (int i = 0; i < data.SuccubiCount && succubi.Count > 0 && opponents.Count > 0; i++)
            {
                CharacterData rSucc = opponents[Random.Range(0, opponents.Count)];
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
    }

    public void OLD_ContinueDream(DreamData data)
    {
        ClearDream();
        DreamMapData map = data.Maps[Random.Range(0, data.Maps.Count)];

        Dictionary<NodeContent, List<DreamNode>> nodeEvents = new Dictionary<NodeContent, List<DreamNode>>();
        foreach (DreamNodeData nData in map.Nodes)
        {
            grid.SetNode(nData);

            if (!nodeEvents.ContainsKey(nData.Content))
                nodeEvents.Add(nData.Content, new List<DreamNode>());
            nodeEvents[nData.Content].Add(grid[nData.Coo]);
        }

        currentNode = null;
        OLD_FillNodes(data, nodeEvents);

        if(currentNode == null)
        {
            Debug.Log("No starting node for map " + map.ID);
        }
        //currentNode = grid[map.Start];
        currentNode.PlacePlayer(playerToken);
        //playerToken = Instantiate(tokenPrefab, transform);

        crystals.text = "0";
        CanMove = true;
    }

    void OLD_FillNodes(DreamData data, Dictionary<NodeContent, List<DreamNode>> nodeEvents)
    {
        if (nodeEvents.ContainsKey(NodeContent.Exit) && nodeEvents[NodeContent.Exit].Count > 0)
        {
            List<DreamData> nexts = new List<DreamData>(data.Nexts);
            List<DreamNode> nodes = new List<DreamNode>(nodeEvents[NodeContent.Exit]);

            while(nexts.Count > 0 && nodes.Count > 0)
            {
                DreamData next = nexts[Random.Range(0, nexts.Count)];
                nexts.Remove(next);
                DreamNode node = nodes[Random.Range(0, nodes.Count)];
                nodes.Remove(node);

                node.SetExit(next);
            }

            //nodeEvents[NodeContent.Exit][Random.Range(0, nodeEvents[NodeContent.Exit].Count)].SetExit(data.Boss, data.);
        }

        if (nodeEvents.ContainsKey(NodeContent.Start) && nodeEvents[NodeContent.Start].Count > 0)
        {
            currentNode = nodeEvents[NodeContent.Start][Random.Range(0, nodeEvents[NodeContent.Start].Count)];
        }


        if (nodeEvents.ContainsKey(NodeContent.Boss) && nodeEvents[NodeContent.Boss].Count > 0)
        {
            nodeEvents[NodeContent.Boss][Random.Range(0, nodeEvents[NodeContent.Boss].Count)].SetCharacter(data.Boss, true);
        }

        if (nodeEvents.ContainsKey(NodeContent.Succubus) && nodeEvents[NodeContent.Succubus].Count > 0)
        {
            List<CharacterData> succubi = new List<CharacterData>(data.Succubi);
            List<DreamNode> nodes = new List<DreamNode>(nodeEvents[NodeContent.Succubus]);

            for(int i = 0; i < data.SuccubiCount && succubi.Count > 0; i++)
            {
                CharacterData rSucc = succubi[Random.Range(0, succubi.Count)];
                succubi.Remove(rSucc);
                DreamNode node = nodes[Random.Range(0, nodes.Count)];
                node.SetCharacter(rSucc, false);
                nodes.Remove(node);
            }
        }

        if (nodeEvents.ContainsKey(NodeContent.Shop) && nodeEvents[NodeContent.Shop].Count > 0)
        {
            nodeEvents[NodeContent.Shop][Random.Range(0, nodeEvents[NodeContent.Shop].Count)].SetShop(data.Shop);
        }

        /*if (nodeEvents.ContainsKey(NodeContent.Exit) && nodeEvents[NodeContent.Exit].Count > 0)
        {

        }*/
    }

    void ClearDream()
    {
        grid.Clear();
    }
    
    public void Clicked(DreamNode node)
    {
        if (CanMove)
        {
            StartMove(Pathfinder.FindPath(currentNode, node));
            /*List<PathNode> path = Pathfinder.FindPath(currentNode, node);
            currentNode = node;
            node.MoveTo(playerToken);*/
        }
    }
    public void OpenShop(ShopData data)
    {
        shop.InitShop(data);
    }

    #region Movement
    Queue<PathNode> path;

    void StartMove(List<PathNode> path)
    {
        if (path == null)
            currentNode.OnEnter();
        else
        {
            this.path = new Queue<PathNode>(path);
            CheckAndMove();
        }
    }

    void CheckAndMove()
    {
        if (path == null || path.Count == 0 || currentNode.PathStop)
            currentNode.OnEnter();
        else
        {
            currentNode = (DreamNode)path.Dequeue();
            MoveTo(currentNode);
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
            "onCompleteTarget", gameObject
            ));
    }

    #endregion
}
