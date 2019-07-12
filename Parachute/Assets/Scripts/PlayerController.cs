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
                                    Debug.Log("Up");
                                }
                                else
                                {
                                    Debug.Log("Down");
                                }
                            }
                        }
                        else
                        {
                            if (!timeOut)
                            {
                                if (positionDelta.x > 0 && Mathf.Abs(positionDelta.x) > minimumSwipeDistanceX && !timeOut)//Right
                                {
                                    Debug.Log("Right");
                                }
                                else//left
                                {
                                    Debug.Log("Left");
                                }

                            }
                        }
                        startTime = 0;
                        break;
                }
            }
            yield return null;
        }
    }


    public Vector3 getPosition()
    {
        return transform.position;
    }

    #endregion

}
