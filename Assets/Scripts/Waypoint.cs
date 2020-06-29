using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false; //для алгоритма поиска пути, чтоб не просчитывать уже отработанные вэйпойнты
    public Waypoint exploredFrom;

    const int gridSize = 10;
    Vector2Int waypointPos;
   
    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetWaypointPos()
    {
        return new Vector2Int(
        waypointPos.x = Mathf.RoundToInt(transform.position.x / gridSize), /*берем значение по х, делим на размер сетки (он равен размеру Scale, но может быть меньше, 
                                                                                   тогда между кубами будет пространство), округляем до целого и умножаем на размер сетки. Получаем снаппинг*/
        waypointPos.y = Mathf.RoundToInt(transform.position.z / gridSize));
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

}
