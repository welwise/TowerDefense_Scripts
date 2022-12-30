using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;
    [SerializeField] private Color exploredColor = Color.white;
    [SerializeField] private Color pathColor = Color.grey;

    private Vector2Int coordiantes = new Vector2Int();
    private TextMeshPro label;
    private GridManager gridManager;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        if (label == null)
            throw new Exception("Label is null!");

        if (gridManager == null)
            throw new Exception("GridManager is null!");

        DisplayCoordinates();
        UpdateParentName();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateParentName();
        }

        UpdateLabelColor();
        CheckToggleLabels();
    }
    

    private void CheckToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void UpdateLabelColor()
    {
        Node node = gridManager.GetNode(coordiantes);

        if (node == null)
            throw new Exception("Failed to get node! Node is null");

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordiantes.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordiantes.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordiantes.x + "," + coordiantes.y;
    }

    private void UpdateParentName()
    {
        transform.parent.name = coordiantes.ToString();
    }
}

