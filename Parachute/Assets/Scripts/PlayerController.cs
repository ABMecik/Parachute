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
    bool isSlow;
    bool parachuteIsActive;

    #endregion

    #region Functions
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }


    #region Routines

    public void play()
    {
#if UNITY_ANDROID || UNITY_IOS
        StartCoroutine("moveRoutine");
#endif
        StartCoroutine("movement");
        isSlow = true;
        parachuteIsActive = false;
        velocityLimit = 20;
    }

    IEnumerator movement()
    {
        while (true)
        {
            StartCoroutine("velocityControl");
            if (Input.GetKeyDown(KeyCode.W) && !parachuteIsActive)
            {
                if (isSlow)
                    StartCoroutine("openParachute");
                else
                {
                    StartCoroutine("slowUp");
                    isSlow = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) && !parachuteIsActive)
            {
                StartCoroutine("dive");
                isSlow = false;
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
                                    if (isSlow)
                                        StartCoroutine("openParachute");
                                    else
                                    {
                                        StartCoroutine("slowUp");
                                        isSlow = true;
                                    }
                                }
                                else
                                {
                                    StartCoroutine("dive");
                                    isSlow = false;
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
            speed = rigid.velocity.y * 10;
            speed = (int)speed;
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
        velocityLimit = 20;
        yield return null;
    }

    IEnumerator dive()
    {
        velocityLimit = 30;
        yield return null;
    }


    IEnumerator openParachute()
    {
        parachuteIsActive = true;
        velocityLimit = 10;
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

    public float getSpeed()
    {
        return speed;
    }
    #endregion

    #endregion

}
