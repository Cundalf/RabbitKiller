using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject frTime;
    public GameObject frPoints;
    public GameObject frGameOver;

    public Text txtTimeGO;
    public Text txtPointsGO;
    public Text txtTime;
    public Text txtPoints;
    public Text currentHorde;
    public Text enemisCurrentHorde;
    public Text txtRabbitFeet;

    public Image frPlayer;
    public Image Bullet1;
    public Image Bullet2;

    public Sprite BulletON;
    public Sprite BulletOFF;

    public Sprite CompleteLife;
    public Sprite HalfLife;
    public Sprite Die;
    
    private int points;
    private float timeControl;

    public void ShowPoint(int points)
    {
        txtPoints.text = points.ToString();
    }
    public void BulletsControl(int cantBullets)
    {
        if(cantBullets == 2)
        {
            Bullet1.sprite = BulletON;
            Bullet2.sprite = BulletON;
        }

        if (cantBullets == 1)
        {
            Bullet1.sprite = BulletON;
            Bullet2.sprite = BulletOFF;
        }

        if (cantBullets == 0)
        {
            Bullet1.sprite = BulletOFF;
            Bullet2.sprite = BulletOFF;
        }
    }

    public void updateOrdeInfo(int cantRabbitInOrde, int currentOrdeNumber) 
    {
        currentHorde.text = "Orde:" + currentOrdeNumber.ToString();
        enemisCurrentHorde.text = "Rabbits:" + cantRabbitInOrde.ToString();
    }

    private void FixedUpdate()
    {
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;

        timeControl += Time.fixedDeltaTime;

        txtTime.text = GetHora();
    }

    public void PointsControl()
    {
        points++;
        txtPoints.text = points.ToString();
    }

    public void GameOver()
    {
        Bullet1.gameObject.SetActive(false);
        Bullet2.gameObject.SetActive(false);
        frTime.SetActive(false);
        frPoints.SetActive(false);
        frGameOver.SetActive(true);

        txtTimeGO.text = GetHora();
        txtPointsGO.text = points.ToString();
        txtRabbitFeet.GetComponent<TextAnimator>().SetIncrement(GameManager.SharedInstance.GetCantRabbitFeet(points));
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.LOSE);
    }

    private string GetHora()
    {
        string minutes = Mathf.Floor(timeControl / 60).ToString("00");
        string seconds = (timeControl % 60).ToString("00");

        return minutes + ":" + seconds;
    }

    public void HealthControl(int healthPoint)
    {
        if(healthPoint > 2)
        {
            frPlayer.sprite = CompleteLife;
        }

        if (healthPoint > 0 && healthPoint <= 2)
        {
            frPlayer.sprite = HalfLife;
        }

        if (healthPoint <= 0)
        {
            frPlayer.sprite = Die;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
