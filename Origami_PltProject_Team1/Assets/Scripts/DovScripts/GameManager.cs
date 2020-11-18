using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            //ce qui se passe en pause
        }
        else
        {
            //resume
        }
    }
}
