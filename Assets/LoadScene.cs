using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;

public class LoadScene : MonoBehaviour
{

    GameObject loadingScreen;

    public string loadThisScene;

    SaveSystemMethods saveSystemMethods;

    private void Awake()
    {
        saveSystemMethods = FindAnyObjectByType<SaveSystemMethods>();
        loadingScreen = GameObject.Find("Loading Screen");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            LoadTheScene();
        }
    }

    public void LoadTheScene()
    {
        loadingScreen.SetActive(true);
        saveSystemMethods.LoadScene(loadThisScene);
    }
}
