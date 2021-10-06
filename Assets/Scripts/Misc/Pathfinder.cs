using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinder
{
    public static List<PathNode> FindPath(PathNode start, PathNode end)
    {
        List<PathStep> steps = new List<PathStep>();

        FindNextStep(steps,
            new PathStep(end, null, 0),
            start);

        //Debug.Log(steps.Count);
        List<PathStep> s = steps.Where(x => x.Node == start).ToList();

        if (s.Count == 0)
            return null;

        List<PathNode> path = new List<PathNode>();
        PathStep current = s[0].Previous;
        while (current != null)
        {
            path.Add(current.Node);
            current = current.Previous;
        }

        return path;
    }

    static void FindNextStep(List<PathStep> steps, PathStep current, PathNode start)
    {
        //Debug.Log(current.Cost);
        if (current.Node == start)
            return;

        foreach (PathNode neighbour in current.Neighbours)
        {
            List<PathStep> s = steps.Where(x => x.Node == neighbour).ToList();
            if(s.Count == 0)
            {
                PathStep step = new PathStep(neighbour, current, current.Cost + neighbour.PCost);
                steps.Add(step);
                FindNextStep(steps, step, start);
            }
            else
            {
                int cost = current.Cost + neighbour.PCost;
                if(s[0].Cost > cost)
                {
                    s[0].Previous = current;
                    s[0].Cost = cost;
                    FindNextStep(steps, s[0], start);
                }
            }
        }
    }
}

class PathStep
{
    public PathNode Node { get; set; }
    public PathStep Previous { get; set; }
    public int Cost { get; set; }

    public List<PathNode> Neighbours { get { return Node.PNeighbours; } }

    public PathStep() { }
    public PathStep(PathNode node, PathStep previous, int cost) {
        Node = node;
        Previous = previous;
        Cost = cost;
    }
}

public interface PathNode
{
    int PCost { get; }
    List<PathNode> PNeighbours { get; }
}
