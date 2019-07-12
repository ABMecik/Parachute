using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private List<float> lines;
    PlayerController player;
    SceneManager scene;
    [SerializeField] bool canFly;

    private void Start()
    {
        player = PlayerController.Instance;
        scene = SceneManager.Instance;
        lines = new List<float>(player.getLinePositionX());
    }

    private void OnBecameVisible()
    {
        if (scene.ObstacleCanMove.Equals("Yes") || (scene.ObstacleCanMove.Equals("Release") && canFly))
        {
            startFly();
        }
        
    }

    private void OnBecameInvisible()
    {
        SceneManager.Instance.removeObs(gameObject);
    }


    private void startFly()
    {
        gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(lines[UnityEngine.Random.Range(0, lines.Count)], transform.position.y+5, transform.position.z));
    }

}
