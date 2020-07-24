using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWaypoint : MonoBehaviour
{
    public bool isPlaceable = true;
   
    private void OnMouseOver() //устанавливаем башню
    {
        if (isPlaceable & Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
