using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{

    public List<GameObject> activateThese = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayThis", 1f);
    }

   void DelayThis()
    {
        foreach (GameObject go in activateThese)
        {
            go.SetActive(true);
        }
    }
}
