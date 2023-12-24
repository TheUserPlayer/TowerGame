using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfd : MonoBehaviour
{
    void Update()
    {
        // Отримання батьківського об'єкта
        Transform parent = transform.parent;

        if (parent != null)
        {
            // Отримання поточного індексу об'єкта в ієрархії
            int currentIndex = transform.GetSiblingIndex();

            // Отримання кількості дітей батьківського об'єкта
            int childCount = parent.childCount;

            // Переміщення об'єкта вниз, якщо він не є останнім в ієрархії
            if (currentIndex < childCount - 1)
            {
                transform.SetSiblingIndex(currentIndex + 1);
                Debug.Log("Object lowered in the hierarchy.");
            }
            else
            {
                Debug.Log("Object is already at the bottom of the hierarchy.");
            }
        }
        else
        {
            Debug.Log("Object has no parent.");
        }
    }
}
