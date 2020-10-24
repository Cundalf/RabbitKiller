using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeponController : MonoBehaviour
{
    [SerializeField]
    public int ammoCharge;
    public int ammo;
    public string weponInUse;
    
    public float timeReload;
    private UIManager uiManager;
    private Animator _anim;
    private float time;

    public ShootController shootController;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        _anim = GetComponent<Animator>();
        ammo = ammoCharge;
        
    }

    // Update is called once per frame
    void Update()
    {
        wepon();
    }

    public void wepon()
    {
        switch (weponInUse)
        {
            case ("ShootGun"):
                if (ammo == 0)
                {
                    reloadWepon(2, 5);
                }
                else
                {
                    if (!Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) return;
                    shoot();
                }
                break;
            case ("MachineGun"):
                if (ammo == 0)
                {
                    reloadWepon(10, 7);
                }
                else
                {
                    if (!Input.GetMouseButtonDown((int)MouseButton.LeftMouse)) return;
                    shoot();
                }
                break;
            default:
                return;
        }
    }

    public void reloadWepon(int ammoInCharge, int timeReload)
    {
        time += Time.deltaTime;
        if (time >= timeReload)
        {
            ammo = ammoInCharge;
            uiManager.BulletsControl(ammo);
            time = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
        }
    }

    public void shoot()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) return;

        shootController.Shoot();
        ammo--;
        uiManager.BulletsControl(ammo);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
    }


}
