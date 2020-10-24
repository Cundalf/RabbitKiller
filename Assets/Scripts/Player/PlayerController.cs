using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Wepons
    public int ammoInCharge;
    public float timeReload;
    public ShootController shootController;
    public Texture2D InGameCursor;
    public string weponInUse = "ShootGun";

    [SerializeField]
    private int ammo;

    [SerializeField]
    private int health = 4;

    private UIManager uiManager;
    private Animator _anim;
    private float time;

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        _anim = GetComponent<Animator>();
        
        uiManager.HealthControl(health);

        UnityEngine.Cursor.SetCursor(InGameCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (GameManager.SharedInstance.actualGameState != GameManager.GameState.IN_GAME) return;

        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
            if (cameraRayHit.transform.tag == "Ground" || cameraRayHit.transform.tag == "Enemy")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
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

    public void shoot()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) return;

        shootController.Shoot();
        ammo--;
        uiManager.BulletsControl(ammo);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
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

    public void Hit()
    {
        health -= 1;
        uiManager.HealthControl(health);

        if (health <= 0)
        {
            UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameManager.SharedInstance.actualGameState = GameManager.GameState.GAME_OVER;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.PLAYER_DEATH);
            uiManager.GameOver();
        }
    }
}