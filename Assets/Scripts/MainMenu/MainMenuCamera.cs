using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>();
    }

    public void ShowStoreUI()
    {
        mainMenuManager.ShowStoreUI();
    }
    public void RestoreMenu()
    {
        mainMenuManager.RestoreMainMenu();
    }
}
