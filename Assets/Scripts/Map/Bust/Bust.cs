using UnityEngine;

[RequireComponent(typeof(Animator))]
public  class Bust : MonoBehaviour
{
    [SerializeField]
    private int healtbust;
    [SerializeField]
    private float timeReloadBust;
    private float time;
    [SerializeField]
    private BustController bustController;
    private Animator _anim;

    public string typeOfBust;
    void Start()
    {
        _anim = GetComponent<Animator>();
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

    public virtual void applyBust() 
    {
        if (typeOfBust!="" && typeOfBust != null)
        {
            bustController.applyBust(typeOfBust);
            _anim.SetBool("Activate",true);
        }
        else { return; }
        
    }

    public virtual void reloadBust() 
    {
        _anim.SetBool("Activate", false);
        healtbust = 2;
        time = 0;
    }
}

