using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject ConfigPanel;
    public GameObject CreditsPanel;
    public GameObject TutorialPanel;
    //Animeted
    public Animator _playerAnime;
    public Animator _rabbitAnime;
    public bool animeted = true;
    private float time;
    private System.Random random;

    void Start()
    {
        random = new System.Random(859633);
    }

    public void ShowConfig()
    {
        ConfigPanel.SetActive(true);
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
        SceneManager.LoadScene("Stage1");
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
        else { return; }
    }
}
