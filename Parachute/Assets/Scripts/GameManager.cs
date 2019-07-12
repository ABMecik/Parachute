using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    PlayerController player;
    SceneManager scene;
    public float vh = 100f;

    private void Start()
    {
        scene = SceneManager.Instance;
        player = PlayerController.Instance;
        player.transform.position = new Vector3(player.transform.position.x, vh, player.transform.position.z);
    }

    public void play()
    {
        scene.play();
        player.play();
    }
}
