using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject loadingScreen;

    void Start()
    {
       
        Invoke("DelayStart", 0.5f);
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.3f);
        //half a second delay for start
    }

  
}
