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
        DisplayCoordinates();
        UpdateParentName();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateParentName();
        }

        UpdateLabelColor();
        CheckToggleLabels();
    }
    

    void CheckToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void UpdateLabelColor()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordiantes);

        if (node == null) { return; }

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

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordiantes.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordiantes.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordiantes.x + "," + coordiantes.y;
    }

    void UpdateParentName()
    {
        transform.parent.name = coordiantes.ToString();
    }
}

