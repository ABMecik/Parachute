using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnBecameVisible()
    {
        SceneManager.Instance.endPath();
    }
}
