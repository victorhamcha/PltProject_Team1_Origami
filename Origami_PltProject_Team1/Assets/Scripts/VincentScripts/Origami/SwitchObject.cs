using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    private int index = 0;
    [SerializeField] private List<GameObject> listObjectToDisplayByOrder = new List<GameObject>();

    private void Start()
    {
        if (listObjectToDisplayByOrder.Count > 0)
            listObjectToDisplayByOrder[index].SetActive(true);
    }

    public void DisplayNextObject()
    {
        if (listObjectToDisplayByOrder.Count <= 0)
            return;
        listObjectToDisplayByOrder[index].SetActive(false);
        if (index + 1 < listObjectToDisplayByOrder.Count)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        listObjectToDisplayByOrder[index].SetActive(true);
    }

    public void DisplayPreviousObject()
    {
        if (listObjectToDisplayByOrder.Count <= 0)
            return;
        listObjectToDisplayByOrder[index].SetActive(false);
        if (index - 1 >= 0)
        {
            index--;
        }
        else
        {
            index = listObjectToDisplayByOrder.Count - 1;
        }
        listObjectToDisplayByOrder[index].SetActive(true);
    }

}
