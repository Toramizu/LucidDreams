using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DreamManager : MonoBehaviour, GridManager
{
    [SerializeField] DreamGrid grid;
    //[SerializeField] DreamToken tokenPrefab;

    [SerializeField] TMP_Text crystals;

    [SerializeField] DreamShop shop;

    [SerializeField] DreamToken playerToken;

    [SerializeField] AbilityUI abilities;

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
        /*ClearDream();
        DreamMapData map = data.Maps[Random.Range(0, data.Maps.Count)];

        Dictionary<NodeContent, List<DreamNode>> nodeEvents = new Dictionary<NodeContent, List<DreamNode>>();
        foreach (DreamNodeData nData in map.Nodes)
        {
            grid.SetNode(nData);

            if (!nodeEvents.ContainsKey(nData.Content))
                nodeEvents.Add(nData.Content, new List<DreamNode>());
            nodeEvents[nData.Content].Add(grid[nData.Coo]);
        }

        FillNodes(data, nodeEvents);

        currentNode = grid[map.Start];
        playerToken.SetCharacter(cData);
        currentNode.PlacePlayer(playerToken);

        crystals.text = "0";
        CanMove = true;*/
    }

    public void ContinueDream(DreamData data)
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

        FillNodes(data, nodeEvents);

        currentNode = grid[map.Start];
        currentNode.PlacePlayer(playerToken);
        //playerToken = Instantiate(tokenPrefab, transform);

        crystals.text = "0";
        CanMove = true;
    }

    void FillNodes(DreamData data, Dictionary<NodeContent, List<DreamNode>> nodeEvents)
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

        if (nodeEvents.ContainsKey(NodeContent.Boss) && nodeEvents[NodeContent.Boss].Count > 0)
        {
            nodeEvents[NodeContent.Boss][Random.Range(0, nodeEvents[NodeContent.Boss].Count)].SetCharacter(data.Boss);
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
                node.SetCharacter(rSucc);
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
        if (CanMove && grid.AreNeighbour(currentNode, node))
        {
            currentNode = node;
            node.MoveTo(playerToken);
        }
    }

    public void OpenShop(ShopData data)
    {
        shop.InitShop(data);
    }
}
