using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public CatalogController catalogController;
    public GameObject tutorialPanel;
    public Text txtRabbitFeet;

    // Config panel components
    public Slider audioSlider;
    public Slider sfxSlider;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject sfxOnButton;
    public GameObject sfxOffButton;

    private Animator _animator;
    private Animator mainMenuCameraAnimator;

    void Start()
    {
        //random = new System.Random(859633);
        _animator = GetComponent<Animator>();
        mainMenuCameraAnimator = GameObject.Find("MainCamera").GetComponent<Animator>();
    }

    public void OpenConfig()
    {
        _animator.SetTrigger("ExitMenu");
        _animator.SetTrigger("EnterConfig");
    }

    public void CloseConfig()
    {
        _animator.SetTrigger("ExitConfig");
    }

    public void OpenStore()
    {
        _animator.SetTrigger("ExitMenu");
        mainMenuCameraAnimator.SetTrigger("GoToStore");
    }

    public void ShowStoreUI()
    {
        _animator.SetTrigger("EnterStore");
    }

    public void RestoreMainMenu()
    {
        _animator.SetTrigger("EnterMenu");
    }

    public void CloseStore()
    {
        _animator.SetTrigger("ExitStore");
        mainMenuCameraAnimator.SetTrigger("ReturnFromStore");
    }

    public void OpenCredits()
    {
        _animator.SetTrigger("ExitMenu");
        _animator.SetTrigger("EnterCredits");
    }

    public void CloseCredits()
    {
        _animator.SetTrigger("ExitCredits");
        GameManager.SharedInstance.changeMap();
    }

    public void LoadShopData()
    {
        catalogController.LoadShopData();
        _animator.SetTrigger("StoreLoaded");
    }

    public void UpdateConfig()
    {
        audioSlider.value = AudioVolumeManager.SharedInstance.CurrentAudioVolume;
        sfxSlider.value = AudioVolumeManager.SharedInstance.CurrentSFXVolume;
        musicOnButton.SetActive(!AudioVolumeManager.SharedInstance.AudioMute);
        musicOffButton.SetActive(AudioVolumeManager.SharedInstance.AudioMute);
        sfxOnButton.SetActive(!AudioVolumeManager.SharedInstance.SFXMute);
        sfxOffButton.SetActive(AudioVolumeManager.SharedInstance.SFXMute);
    }

    //TODO: Hay que definir como sera el tutorial para desarrollarlo y cambiar esto.
    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void UpdateRabbitFeet(int cant)
    {
        txtRabbitFeet.text = cant.ToString();
    }

}
