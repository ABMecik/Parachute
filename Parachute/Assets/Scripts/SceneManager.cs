using System.Collections;
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

    private void Start()
    {
        player = PlayerController.Instance;
        pool = ObstaclePool.Instance;
        ground = GameObject.FindGameObjectWithTag("Ground");

        startPath();
    }

    public void startPath()
    {

        startPos = player.getPosition() - new Vector3(0, SafeZoneRadius, 0);
        endPos = ground.transform.position + new Vector3(0, SafeZoneRadius, 0);
        currentPos = startPos;

        StartCoroutine("path");
    }

    public void endPath()
    {
        StopCoroutine("path");
    }

    IEnumerator path() {
        while (true)
        {

        }
    }
}
