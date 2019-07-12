using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    #region  Variables

    [Header("Swipe Variables")]
    [SerializeField] private float minimumSwipeDistanceY;
    [SerializeField] private float minimumSwipeDistanceX;
    [SerializeField] private float timeDifferenceLimit = 0.5f;

    private Touch touch = default(Touch);
    private Vector2 startPosition = Vector2.zero;
    private float startTime;

    #endregion

    #region Functions

    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        StartCoroutine("moveRoutine");
#endif

#if UnityEngine
        StartCoroutine("movement");
#endif

    }

    #region Routines

    IEnumerator movement()
    {
        while (true)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {

            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator moveRoutine()
    {
        while (true)
        {
            if (Input.touches.Length > 0)
            {
                touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPosition = touch.position;
                        startTime = Time.time;
                        break;
                    case TouchPhase.Moved:
                        Vector3 positionDelta = (Vector2)touch.position - startPosition;

                        float timeDifference = Time.time - startTime;
                        bool timeOut = timeDifference > timeDifferenceLimit;

                        if (Mathf.Abs(positionDelta.y) > Mathf.Abs(positionDelta.x))
                        {
                            if (!timeOut)
                            {
                                if (positionDelta.y > 0 && Mathf.Abs(positionDelta.y) > minimumSwipeDistanceY)//Up
                                {
                                    StartCoroutine("slowUp");
                                }
                                else
                                {
                                    StartCoroutine("dive");
                                }
                            }
                        }
                        else
                        {
                            if (!timeOut)
                            {
                                if (positionDelta.x > 0 && Mathf.Abs(positionDelta.x) > minimumSwipeDistanceX && !timeOut)//Right
                                {
                                    StartCoroutine("moveRight");
                                }
                                else//left
                                {
                                    StartCoroutine("moveLeft");
                                }

                            }
                        }
                        startTime = 0;
                        break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator moveLeft()
    {

        yield return null;
    }

    IEnumerator moveRight()
    {

        yield return null;
    }

    IEnumerator slowUp()
    {

        yield return null;
    }

    IEnumerator dive()
    {

        yield return null;
    }

    IEnumerator openParachute()
    {

        yield return null;
    }

    #endregion

    #region Get Functions

    public Vector3 getPosition()
    {
        return transform.position;
    }
    #endregion

    #endregion

}
