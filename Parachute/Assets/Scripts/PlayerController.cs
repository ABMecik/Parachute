using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel;

public class PlayerController : MonoSingleton<PlayerController>
{
    #region  Variables

    [Header("Swipe Variables")]
    [SerializeField] private float minimumSwipeDistanceY;
    [SerializeField] private float minimumSwipeDistanceX;
    [SerializeField] private float timeDifferenceLimit = 0.5f;
    [SerializeField] private float horizontalMovementTime = 0.5f;
    [SerializeField] private float horizontalMovementDuration = 0.5f;

    [Header("Line Variables")]
    [SerializeField] private float[] line = { -2.75f, 0, 2.75f };


    [Header("Speed Variables")]
    [SerializeField] float speed;
    [Range(20, 70)] [SerializeField] float velocityLimit = 30;

    Rigidbody rigid;
    private Touch touch = default(Touch);
    private Vector2 startPosition = Vector2.zero;
    private float startTime;

    #endregion

    #region Functions
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        StartCoroutine("moveRoutine");
#endif
        StartCoroutine("movement");
    }

    #region Routines

    IEnumerator movement()
    {
        while (true)
        {
            StartCoroutine("velocityControl");
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine("moveLeft");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("moveRight");
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator moveRoutine()
    {
        StartCoroutine("velocityControl");
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

    IEnumerator velocityControl()
    {
        while (true)
        {
            Vector3 velocity = rigid.velocity;

            if (velocity.y < -velocityLimit)
            {
                rigid.velocity = new Vector3(velocity.x, -velocityLimit, velocity.z);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator moveLeft()
    {
        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerRotation.y = -35;

        rigid.DOMoveX(line[0], horizontalMovementTime / 2, false);
        rigid.DORotate(playerRotation, horizontalMovementTime / 2);

        yield return new WaitForSeconds(horizontalMovementDuration);

        playerRotation.y = 0;

        rigid.DOMoveX(line[1], horizontalMovementTime, false);
        rigid.DORotate(playerRotation, horizontalMovementTime);
    }

    IEnumerator moveRight()
    {
        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerRotation.y = 35;

        rigid.DOMoveX(line[2], horizontalMovementTime / 2, false);
        rigid.DORotate(playerRotation, horizontalMovementTime / 2);

        yield return new WaitForSeconds(horizontalMovementDuration);

        playerRotation.y = 0;

        rigid.DOMoveX(line[1], horizontalMovementTime, false);
        rigid.DORotate(playerRotation, horizontalMovementTime);
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

    public float[] getLinePositionX()
    {
        return line;
    }
    #endregion

    #endregion

}
