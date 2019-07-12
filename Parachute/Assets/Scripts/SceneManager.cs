﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{

    enum Optional
    {
        Release,
        Yes,
        No
    };

    PlayerController player;
    ObstaclePool pool;
    GameObject ground;

    [SerializeField] float SafeZoneRadius = 4f;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 endPos;
    private List<float> lines;

    [SerializeField] float minObsDistance = 2f;
    [SerializeField] float maxObsDistance = 10f;

    [SerializeField] Optional ObstacleCanMove = Optional.Release;

    private List<GameObject> calledObjects;


    private void Start()
    {
        player = PlayerController.Instance;
        pool = ObstaclePool.Instance;
        ground = GameObject.FindGameObjectWithTag("Ground");
        lines = new List<float>(player.getLinePositionX());
        calledObjects = new List<GameObject>();
        startPath();
    }

    public void startPath()
    {

        startPos = player.getPosition() - new Vector3(0, SafeZoneRadius, 0);
        endPos = ground.transform.position + new Vector3(0, SafeZoneRadius, 0);
        currentPos = startPos;
    }

    public void play()
    {
        StartCoroutine("path");
    }

    public void cleanPath()
    {
        foreach(GameObject go in calledObjects)
        {
            go.SetActive(false);
        }
        calledObjects.Clear();
    }

    public void removeObs(GameObject obs)
    {
        calledObjects.Remove(obs);
        obs.SetActive(false);
    }

    IEnumerator path() {
        while (currentPos.y > endPos.y)
        {
            float addDistance = UnityEngine.Random.Range(minObsDistance, maxObsDistance);
            float line = lines[UnityEngine.Random.Range(0,lines.Count)];
            currentPos = currentPos - new Vector3(line,addDistance,0);
            if(currentPos.y <= endPos.y) { break; }

            GameObject newObs = pool.SpawnFromPool("Bird", currentPos) as GameObject;
            calledObjects.Add(newObs);

            yield return null;
        }
    }
}
