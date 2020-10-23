using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public ShootController shootController;
    public float timeReload;
    public int cantMaxBullet;
    public Texture2D InGameCursor;

    [SerializeField]
    private int cantBullet;

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
        cantBullet = cantMaxBullet;

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

        if (cantBullet == 0)
        {
            time += Time.deltaTime;

            if(time >= timeReload)
            {
                cantBullet = cantMaxBullet;
                uiManager.BulletsControl(cantBullet);
                time = 0;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
            }
        }
        else
        {
            if (!Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) return;

            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) return;
            
            shootController.Shoot();
            cantBullet--;
            uiManager.BulletsControl(cantBullet);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
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