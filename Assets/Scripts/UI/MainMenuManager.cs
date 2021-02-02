using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject ConfigPanel;
    public GameObject CreditsPanel;
    public GameObject TutorialPanel;
    public GameObject ShopPanel;
    public Slider AudioSlider;
    public Slider SFXSlider;
    public GameObject MusicOnButton;
    public GameObject MusicOffButton;
    public GameObject SFXOnButton;
    public GameObject SFXOffButton;
    public Text txtRabbitFeet;

    //Animeted
    public Animator _playerAnime;
    public Animator _rabbitAnime;
    public bool animeted = true;
    private float time;
    private System.Random random;

    [SerializeField]
    private GameManager gManager;

    void Start()
    {
        random = new System.Random(859633);
    }

    public void ShowConfig()
    {
        ConfigPanel.SetActive(true);
        UpdateConfig();
    }

    public void ToggleStore()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
        transform.Find("frOptions").gameObject.SetActive(!ShopPanel.activeSelf);
        transform.Find("Banner").gameObject.SetActive(!ShopPanel.activeSelf);
        transform.Find("btnShop").gameObject.SetActive(!ShopPanel.activeSelf);
        transform.Find("RabbitFeetFrame").gameObject.SetActive(!ShopPanel.activeSelf);
        transform.Find("btnExit").gameObject.SetActive(!ShopPanel.activeSelf);
        transform.Find("btnConfig").gameObject.SetActive(!ShopPanel.activeSelf);
    }

    private void UpdateConfig()
    {
        AudioSlider.value = AudioVolumeManager.SharedInstance.CurrentAudioVolume;
        SFXSlider.value = AudioVolumeManager.SharedInstance.CurrentSFXVolume;
        MusicOnButton.SetActive(!AudioVolumeManager.SharedInstance.AudioMute);
        MusicOffButton.SetActive(AudioVolumeManager.SharedInstance.AudioMute);
        SFXOnButton.SetActive(!AudioVolumeManager.SharedInstance.SFXMute);
        SFXOffButton.SetActive(AudioVolumeManager.SharedInstance.SFXMute);
    }

    public void HideConfig()
    {
        ConfigPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        CreditsPanel.SetActive(false);
    }

    public void ShowTutorial()
    {
        animeted = false;
        TutorialPanel.SetActive(true);
    }

    public void StartGame()
    {
        gManager.nexMap();
    }

    void Update()
    {
        if (animeted)
        {
            time += Time.deltaTime;
            if ((int)time == 5)
            {
                var ran = random.Next(0, 5);
                time = 0;
                switch (ran)
                {
                    case 1:
                        _rabbitAnime.SetTrigger("LookCarrot");
                        break;
                    case 2:
                        _playerAnime.SetTrigger("UpWepon");
                        break;
                    default:
                        _playerAnime.SetTrigger("LookRabbit");
                        break;
                }
            }
        }
    }

    public void UpdateRabbitFeet(int cant)
    {
        txtRabbitFeet.text = cant.ToString();
    }

    public void setGameManager(GameManager gManager) 
    {
        this.gManager = gManager;
    }

}
