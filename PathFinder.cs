using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return DestinationCoordinates; } }

    private Node currentSearchNode;
    private Node startNode;
    private Node destinationNode;
    private GridManager gridManager;

    private Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    private Queue<Node> frontier = new Queue<Node>();

    [Header("Road tiles path")]
    [SerializeField] private bool withPathFinder = true;
    [SerializeField] private List<Tile> roadPath = new List<Tile>();


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }


    void Start()
    {
        StartCoroutine(Create_RoadTiles_Path());
        GetNewPath();
        
    }

    private IEnumerator Create_RoadTiles_Path()
    {
        yield return new WaitForSeconds(0.5f);
        if (withPathFinder)
        {
            foreach (Node node1 in reached.Values)
            {
                node1.isWalkable = false;
                //node1.isPath = false;
            }      

            foreach (Tile tile in roadPath)
            {
                reached[gridManager.GetCoordinatesFromPosition(tile.transform.position)].isWalkable = true;
                reached[gridManager.GetCoordinatesFromPosition(tile.transform.position)].isPath = true;
                
            }
            //NotifyReceivers();
        }
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }


    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }


    void ExploreNaighbores()
    {
        List<Node> neighbores = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int searchCoordinates = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(searchCoordinates))
            {
                neighbores.Add(grid[searchCoordinates]);
            }

        }


        foreach (Node naighbore in neighbores)
        {
            if(!reached.ContainsKey(naighbore.coordinates) && naighbore.isWalkable)
            {
                naighbore.conncetedTo = currentSearchNode;
                reached.Add(naighbore.coordinates, naighbore);
                frontier.Enqueue(naighbore);
            }
        }
        
    }


    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNaighbores();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }

    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.conncetedTo != null)
        {
            currentNode = currentNode.conncetedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }



    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
