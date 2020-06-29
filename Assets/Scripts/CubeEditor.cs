using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] //в editor делаем снаппинг через скрипт
[SelectionBase] //чтобы выбирать весь куб, а не лейбл
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    //[SerializeField] [Range(1f, 20f)] float gridSize = 10f; //выбор значения для размера сетки, влияет на снаппинг - заменили на константу
    

    Waypoint waypoint;
    
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }
    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(waypoint.GetWaypointPos().x * gridSize, 0f, waypoint.GetWaypointPos().y * gridSize); // присваиваем занчение позиции объекту
    }
    private void UpdateLabel()
    {
        // Присваиваем тексту на каждом кубе значение координат (x,z)
        TextMesh labelTextMesh = GetComponentInChildren<TextMesh>();
        int gridSize = waypoint.GetGridSize();
        string labelText = 
            "[" + (waypoint.GetWaypointPos().x).ToString() + 
            "," + 
            (waypoint.GetWaypointPos().y).ToString() + "]";
        labelTextMesh.text = labelText;
        gameObject.name = labelText; // переименовываем куб
    }
}
