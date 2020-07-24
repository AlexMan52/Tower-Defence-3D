using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false; //для алгоритма поиска пути, чтоб не просчитывать уже отработанные вэйпойнты
    public Waypoint exploredFrom; //для определения клетки, с которой нашли соседние
    const int gridSize = 10;
    Vector2Int waypointPos;
    Dictionary<Vector2, Tower> listOfPlacedTowers = new Dictionary<Vector2, Tower>();

    public int GetGridSize()
    {
        return gridSize;
    }
    public Vector2Int GetWaypointPos() //получаем округленную позицию вэйпойнта
    {
        return new Vector2Int(
        waypointPos.x = Mathf.RoundToInt(transform.position.x / gridSize), /*берем значение по х, делим на размер сетки (он равен размеру Scale, но может быть меньше, 
                                                                                   тогда между кубами будет пространство), округляем до целого. Получаем снаппинг*/
        waypointPos.y = Mathf.RoundToInt(transform.position.z / gridSize));
    }

    public void SetTopColor(Color color) //устанавливаем цвет верха кубика вэйпойнта
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    
}
