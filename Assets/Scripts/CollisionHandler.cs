using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    void StartDeathSequence()
    {
        gameObject.SendMessage("OnPlayerDeath"); // запуск метода в другом скрипте, который применен к тому же объекту
        //FindObjectOfType<PlayerController>().OnPlayerDeath(); // запуск метода в скрипте PlayerController, но метод должен быть public!
        //FindObjectOfType<RunCamera>().ChangeLiveStatus(); //использовалось когда была камера от Синемашин (до Таймлайнов)
    }
}
