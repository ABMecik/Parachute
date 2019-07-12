using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnBecameVisible()
    {
        
    }  

    private void OnBecameInvisible()
    {
        Debug.Log("agdsga");
        SceneManager.Instance.removeObs(gameObject);
    }
}
