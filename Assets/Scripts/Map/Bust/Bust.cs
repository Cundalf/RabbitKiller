using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Bust : MonoBehaviour
{
    public int healtbust { set; get; }
    public float timeReloadBust { set; get; }
    public float time { set; get; }
    private BustController bustController { set; get; }
    private Animator animationComponent;

    public string typeOfBust;
    void Start()
    {
        bustController = (BustController)FindObjectOfType(typeof(BustController));
        animationComponent = GetComponent<Animator>();
        animationComponent.SetBool("Activate", false);
        timeReloadBust = 300;
        healtbust = 2;
    }
    void Update()
    {
        if (healtbust == 0)
        {
            if (time >= timeReloadBust)
            {
                reloadBust();
            }
            else
            {
                time += Time.deltaTime;
            }
        }
    }

    public virtual void appliBust() 
    {
        if (typeOfBust!="" && typeOfBust != null)
        {
            bustController.appliBust(typeOfBust);
            animationComponent.SetBool("Activate",true);
        }
        else { return; }
        
    }

    public virtual void reloadBust() 
    {
        animationComponent.SetBool("Activate", false);
        healtbust = 2;
        time = 0;
    }
}

