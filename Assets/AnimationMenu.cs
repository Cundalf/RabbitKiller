using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class AnimationMenu : MonoBehaviour
{
    public Animator _playerAnime;
    public Animator _rabbitAnime;
    private float time;
    private System.Random random;

    void Start() 
    {
        random = new System.Random(859633);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if ((int)time == 5)
        {
            var ran = random.Next(0, 5);
            UnityEngine.Debug.Log(ran);
            time = 0;
            switch (ran)
            {
                case 1:
                    _rabbitAnime.SetTrigger("LookCarrot");
                    UnityEngine.Debug.Log("LookCarrot"); 
                    break;
                case 2:
                    _playerAnime.SetTrigger("UpWepon");
                    UnityEngine.Debug.Log("UpWepon");
                    break;
                default:
                    _playerAnime.SetTrigger("LookRabbit");
                    UnityEngine.Debug.Log("LookRabbit");
                    break;
            }
        }
    }
}
