using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode] //в editor делаем снаппинг через скрипт
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f; //выбор значения для размера сетки, влияет на снаппинг
    
    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize; /*берем значение по х, делим на размер сетки (он равен размеру Scale, но может быть меньше, 
                                                                                   тогда между кубами будет пространство), округляем до целого и умножаем на размер сетки. Получаем снаппинг*/
        snapPos.y = 0f; // по y всё поле будет на 1 уровне
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z); // присваиваем занчение позиции объекту
    }
}
