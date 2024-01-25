using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    public List<GameObject> activateThese;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            foreach (GameObject go in activateThese)
            {
                if (go != null)
                {
                    go.SetActive(true);
                }
            }
        }
    }
}
