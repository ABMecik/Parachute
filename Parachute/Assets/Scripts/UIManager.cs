using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<ObstaclePool>
{

    [SerializeField] Slider altimeter;

    PlayerController player;
    GameObject ground;


    private void Start()
    {
        player = PlayerController.Instance;
        ground = GameObject.FindGameObjectWithTag("Ground");
    }

    IEnumerator play()
    {
        while (true)
        {
            yield return null;
        }
    }
}
