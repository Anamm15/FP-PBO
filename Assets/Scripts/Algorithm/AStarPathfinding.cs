using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding {
    private float gridSize;

    public AStarPathfinding(float gridSize) {
        this.gridSize = gridSize;
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos) {
        HashSet<Vector2> closedSet = new HashSet<Vector2>();
        PriorityQueue<Vector2> openSet = new PriorityQueue<Vector2>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        Dictionary<Vector2, float> gScore = new Dictionary<Vector2, float>();
        Dictionary<Vector2, float> fScore = new Dictionary<Vector2, float>();

        openSet.Enqueue(startPos, 0);
        gScore[startPos] = 0;
        fScore[startPos] = HeuristicCostEstimate(startPos, endPos);

        while (openSet.Count > 0) {
            Vector2 current = openSet.Dequeue();

            if (current == endPos) {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (Vector2 neighbor in GetNeighbors(current)) {
                if (closedSet.Contains(neighbor)) {
                    continue;
                }

                float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbor);

                if (!openSet.Contains(neighbor)) {
                    openSet.Enqueue(neighbor, fScore.GetValueOrDefault(neighbor, float.MaxValue));
                } else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, float.MaxValue)) {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, endPos);
            }
        }

        return new List<Vector2>();
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current) {
        List<Vector2> path = new List<Vector2>();
        while (cameFrom.ContainsKey(current)) {
            path.Insert(0, current);
            current = cameFrom[current];
        }
        return path;
    }

    private float HeuristicCostEstimate(Vector2 start, Vector2 end) {
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }

    private List<Vector2> GetNeighbors(Vector2 current) {
        List<Vector2> neighbors = new List<Vector2>();

        Vector2[] directions = {
            new Vector2(0, gridSize), new Vector2(0, -gridSize),
            new Vector2(gridSize, 0), new Vector2(-gridSize, 0)
        };

        foreach (Vector2 direction in directions) {
            Vector2 neighbor = current + direction;
            neighbors.Add(neighbor);
        }

        return neighbors;
    }
}
