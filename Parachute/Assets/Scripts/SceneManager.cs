using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{

    public enum Optional
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
    [SerializeField]private List<float> lines;

    [SerializeField] float minObsDistance = 2f;
    [SerializeField] float maxObsDistance = 10f;

    public Optional ObstacleCanMove = Optional.Release;

    private List<GameObject> calledObjects;


    private void Start()
    {
        player = PlayerController.Instance;
        pool = ObstaclePool.Instance;
        ground = GameObject.FindGameObjectWithTag("Ground");

        lines = new List<float>(player.getLinePositionX());
        calledObjects = new List<GameObject>();
        ground.SetActive(false);
    }

    internal void stop()
    {
        cleanPath();
        ground.SetActive(false);
    }

    public void play()
    {
        ground.SetActive(true);
        startPos = player.getPosition() - new Vector3(0, SafeZoneRadius, 0);
        endPos = ground.transform.position + new Vector3(0, SafeZoneRadius, 0);
        currentPos = startPos;
        StartCoroutine("path");
    }

    public void cleanPath()
    {
        while (calledObjects.Count != 0)
        {
            calledObjects[0].SetActive(false);
            calledObjects.RemoveAt(0);
        }
        calledObjects.Clear();
    }

    public void removeObs(GameObject obs)
    {
        calledObjects.Remove(obs);
        obs.SetActive(false);
    }

    IEnumerator path()
    {
        while (currentPos.y > endPos.y)
        {
            float addDistance = UnityEngine.Random.Range(minObsDistance, maxObsDistance);
            float line = lines[UnityEngine.Random.Range(0, lines.Count)];
            currentPos = new Vector3(line, currentPos.y-addDistance, currentPos.z);
            if (currentPos.y <= endPos.y) { break; }

            GameObject newObs = pool.SpawnFromPool("Bird", currentPos) as GameObject;
            calledObjects.Add(newObs);

            yield return null;
        }
    }
}
