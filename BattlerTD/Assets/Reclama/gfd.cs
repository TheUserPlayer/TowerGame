using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfd : MonoBehaviour
{
    void Update()
    {
        // ��������� ������������ ��'����
        Transform parent = transform.parent;

        if (parent != null)
        {
            // ��������� ��������� ������� ��'���� � ��������
            int currentIndex = transform.GetSiblingIndex();

            // ��������� ������� ���� ������������ ��'����
            int childCount = parent.childCount;

            // ���������� ��'���� ����, ���� �� �� � ������� � ��������
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
