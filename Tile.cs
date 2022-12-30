using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlaceable;

    private PathFinder pathFinder;
    private GridManager gridManager;
    private Vector2Int coordinates = new Vector2Int();

    public bool IsPlaceable => isPlaceable;

    private void Awake()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        gridManager = FindObjectOfType<GridManager>();
        if (pathFinder == null)
            throw new Exception("PathFinder hasn't been found!");

        if (gridManager == null)
            throw new Exception("GridManager hasn't been found!");
    }

    private void Start()
    {
        coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

        if (!IsPlaceable)
            gridManager.BlockNode(coordinates);
    }

    private void OnMouseDown()
    {
        var node = gridManager.GetNode(coordinates);
        if (node.isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
