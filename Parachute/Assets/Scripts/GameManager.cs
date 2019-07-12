using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    PlayerController player;
    SceneManager scene;
    UIManager UI;
    public float vh = 100f;

    private void Start()
    {
        scene = SceneManager.Instance;
        player = PlayerController.Instance;
        UI = UIManager.Instance;
    }

    public void play()
    {
        player.transform.position = new Vector3(player.transform.position.x, vh, player.transform.position.z);
        scene.play();
        player.play();
    }

    public void gameover()
    {
        resetGame();
    }

    public void levelUp()
    {
        vh += 10;
        resetGame();
    }

    private void resetGame()
    {
        player.transform.position = new Vector3(player.transform.position.x, vh, player.transform.position.z);
        player.stopAllRoutines();
        scene.stop();
        UI.stop();
    }
}
