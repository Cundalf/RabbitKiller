using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private int health = 4;
    public Texture2D InGameCursor;
    public UIManager uiManager;

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
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