using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Singelton
    private GameManager() { }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }

    }
    #endregion
}
