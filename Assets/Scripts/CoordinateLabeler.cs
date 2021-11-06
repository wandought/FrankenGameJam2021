using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
//[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour {

    private Color defaultColor = Color.green;
    private Color blockedColor = Color.red;

    private TextMeshPro label;
    private Vector2Int coord = new Vector2Int();
    private Waypoint waypoint;

    private void Awake() {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();

        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateName();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    private void ColorCoordinates()
    {
        if (waypoint.IsPlaceable)
            label.color = defaultColor;
        else
            label.color = blockedColor;
    }

    private void DisplayCoordinates()
    {
        coord.x = Mathf.RoundToInt(transform.parent.position.x / Administrator.GridSize);
        coord.y = Mathf.RoundToInt(transform.parent.position.z / Administrator.GridSize);

        label.text = $"({coord.x}, {coord.y})";
    }

    void UpdateName()
    {
        transform.parent.name = coord.ToString();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.H))
            label.enabled = !label.IsActive();
    }
}
