using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int health { set; get; }
    public Texture2D InGameCursor;
    public UIManager uiManager;

    [SerializeField]
    private string[] validTag;

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits
    private void Start()
    {
        health = 4;
        uiManager = FindObjectOfType<UIManager>();
        uiManager.HealthControl(health);
        Cursor.SetCursor(InGameCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            if (isAObjectValid(cameraRayHit.transform.tag))
            {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;

        health -= 1;
        uiManager.HealthControl(health);

        if (health <= 0)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameManager.SharedInstance.ChangeGameState(GameManager.GameState.GAME_OVER);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.PLAYER_DEATH);
            uiManager.GameOver();
        }
    }

    private bool isAObjectValid(string objectIsTag)
    {
        foreach (string tag in this.validTag) 
        {
            if (objectIsTag == tag) 
            {
                return true;
            }
        }
        return false;
    }
}