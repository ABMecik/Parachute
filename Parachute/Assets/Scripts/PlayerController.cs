using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    #region  Variables



    #endregion

    #region Functions

    public Vector3 getPosition()
    {
        return transform.position;
    }

    #endregion

}
