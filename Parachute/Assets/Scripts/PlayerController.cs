﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
   
   public Vector3 getPosition()
   {
       return transform.position;
   }
}