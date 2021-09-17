using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamManager : MonoBehaviour
{
    [SerializeField] DreamGrid grid;
    [SerializeField] DreamToken tokenPrefab;

    DreamToken playerToken;
    List<DreamToken> placedTokens = new List<DreamToken>();


    DreamNode currentNode;

    private void Start()
    {
        grid.Init(this);
    }

    public void StartDream(DreamData data, CharacterData cData)
    {
        ClearDream();

        Dictionary<NodeContent, List<DreamNode>> nodeEvents = new Dictionary<NodeContent, List<DreamNode>>();
        foreach (DreamNodeData nData in data.Nodes)
        {
            grid.SetNode(nData);

            if (!nodeEvents.ContainsKey(nData.Content))
                nodeEvents.Add(nData.Content, new List<DreamNode>());
            nodeEvents[nData.Content].Add(grid[nData.Coo]);
        }

        FillNodes(data, nodeEvents);

        currentNode = grid[data.Start];
        playerToken = Instantiate(tokenPrefab, transform);
        playerToken.Character = cData;
        currentNode.PlaceAt(playerToken, true);
    }

    void FillNodes(DreamData data, Dictionary<NodeContent, List<DreamNode>> nodeEvents)
    {
        if (nodeEvents.ContainsKey(NodeContent.Boss) && nodeEvents[NodeContent.Boss].Count > 0)
        {
            DreamToken boss = Instantiate(tokenPrefab, transform);
            boss.Character = data.Boss;
            placedTokens.Add(boss);
            nodeEvents[NodeContent.Boss][Random.Range(0, nodeEvents[NodeContent.Boss].Count)].PlaceAt(boss, false);
        }

        if (nodeEvents.ContainsKey(NodeContent.Succubus) && nodeEvents[NodeContent.Succubus].Count > 0)
        {
            List<CharacterData> succubi = new List<CharacterData>(data.Succubi);
            List<DreamNode> nodes = new List<DreamNode>(nodeEvents[NodeContent.Succubus]);

            for(int i = 0; i < data.SuccubiCount && succubi.Count > 0; i++)
            {
                DreamToken succubus = Instantiate(tokenPrefab, transform);
                succubus.Character = succubi[Random.Range(0, succubi.Count)];
                succubi.Remove(succubus.Character);
                placedTokens.Add(succubus);
                DreamNode node = nodes[Random.Range(0, nodes.Count)];
                node.PlaceAt(succubus, false);
                nodes.Remove(node);
            }
        }

        if (nodeEvents.ContainsKey(NodeContent.Exit) && nodeEvents[NodeContent.Exit].Count > 0)
        {

        }
    }

    void ClearDream()
    {
        grid.Clear();

        if(playerToken != null)
            Destroy(playerToken.gameObject);

        foreach (DreamToken token in placedTokens)
            Destroy(token.gameObject);
        placedTokens.Clear();
    }

    public void Clicked(DreamNode node)
    {
        if (grid.AreNeighbour(currentNode, node))
        {
            currentNode = node;
            node.MoveTo(playerToken);
        }
    }
}
