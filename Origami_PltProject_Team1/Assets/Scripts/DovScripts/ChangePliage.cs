using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePliage : MonoBehaviour
{
    [SerializeField] private List<GameObject> listPliage = new List<GameObject>();
    [SerializeField] private SwitchModePlayerOrigami switchModePlayerOrigami = null;
    private PliageManager pliageManager = null;

    private GameObject GetPliage(string name)
    {
        for (int i = 0; i <= listPliage.Count; i++)
        {
            if(name == listPliage[i].name)
            {
                return listPliage[i];
            }
        }
        return null;
    }

    public void SetUpPliage(string name)
    {
        switchModePlayerOrigami._pliageToDo = GetPliage(name);
        pliageManager = switchModePlayerOrigami._pliageToDo.GetComponent<PliageManager>();
        switchModePlayerOrigami.ActiveOrigami();
    }
}
