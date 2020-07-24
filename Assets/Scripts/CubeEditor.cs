using UnityEngine;

[ExecuteInEditMode] //в editor делаем снаппинг через скрипт
[SelectionBase] //чтобы выбирать весь куб, а не лейбл
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()
    {
        SnapToGrid();
        //UpdateLabel();
    }
    private void SnapToGrid() //привязываем куб к сетке, чтобы при перемещении двигать по этой сетке координат
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(waypoint.GetWaypointPos().x * gridSize, 0f, waypoint.GetWaypointPos().y * gridSize); // присваиваем значение позиции объекту
    }
    /*private void UpdateLabel() // Присваиваем тексту на каждом кубе значение координат (x,z)
    {
        TextMesh labelTextMesh = GetComponentInChildren<TextMesh>();
        string labelText = 
            "[" + (waypoint.GetWaypointPos().x).ToString() + 
            "," + 
            (waypoint.GetWaypointPos().y).ToString() + "]";
        labelTextMesh.text = labelText;
        gameObject.name = labelText; // переименовываем сам куб
    }*/
}
