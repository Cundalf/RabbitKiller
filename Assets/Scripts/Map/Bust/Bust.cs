using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Bust : MonoBehaviour
{
    [SerializeField]
    private int healtbust;
    [SerializeField]
    private float timeReloadBust;
    private float time;
    [SerializeField]
    private BustController bustController;
    [SerializeField]
    private Animator _Anim;

    public string typeOfBust;
    void Start()
    {
        bustController = (BustController)FindObjectOfType(typeof(BustController));
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
            _Anim.SetBool("Activate",true);
        }
        else { return; }
        
    }

    public virtual void reloadBust() 
    {
        _Anim.SetBool("Activate", false);
        healtbust = 2;
        time = 0;
    }
}

