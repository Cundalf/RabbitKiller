using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int initialHealth = 4;
    [SerializeField]
    private string[] validCameraRayTag;

    // Player health
    private int _health;
    public int health
    {
        get
        {
            return _health;
        }
    }

    // UI Manager para actualizar la vida
    private UIManager uiManager;
    // The ray that is cast from the camera to the mouse position
    private Ray cameraRay;
    // The object that the ray hits
    private RaycastHit cameraRayHit;

    private void Start()
    {
        GameManager.sharedInstance.actualPlayer = gameObject;
        _health = initialHealth;
        uiManager = FindObjectOfType<UIManager>();
        uiManager.HealthControl(_health);
    }

    void Update()
    {
        if (GameManager.sharedInstance.ActualGameState != GameManager.GameState.IN_GAME)
            return;

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

    public void Hit(int damage = 1)
    {
        if (GameManager.sharedInstance.ActualGameState != GameManager.GameState.IN_GAME) 
            return;

        _health -= damage;
        uiManager.HealthControl(_health);

        if (_health <= 0)
        {   
            GameManager.sharedInstance.changeGameState(GameManager.GameState.GAME_OVER);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.PLAYER_DEATH);
            uiManager.GameOver();
        }
    }

    public void heal(int healAmount = 1)
    {
        _health += healAmount;
        
        if(_health > initialHealth)
        {
            _health = initialHealth;
        }
    }

    private bool isAObjectValid(string objectIsTag)
    {
        foreach (string tag in this.validCameraRayTag) 
        {
            if (objectIsTag == tag) 
            {
                return true;
            }
        }
        return false;
    }
}