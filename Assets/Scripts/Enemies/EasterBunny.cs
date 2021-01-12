using UnityEngine;

public class EasterBunny : EnemyController 
{
    public GameObject slavePrefab;
    public GameObject respanPoint1;
    public GameObject respanPoint2;
    public GameObject respanPoint3;
    public GameObject respanPoint4;

    public int healtBarAmount { get; set; }

    public override void Update() 
    {
        base.Update();
    }
    public override void Start()
    {
        base.Start();
        timeStop = 15f;
    }

    public void healtControl(int danio)
    {
        healt = healt - danio;
        if (healt <= 0) 
        {
            if (healtBarAmount > 0)
            {
                healtBarAmount--;
                healt = 100;
            }
            else 
            {
                Die();
                enemyRespawnController.setBossStillAlive(false);
            }
        }      
    }

    public override void movePNJ() 
    {
        base.movePNJ();
        useSkill();
    }

    public void useSkill() 
    {
        instantiateRabbit(respanPoint1);
        instantiateRabbit(respanPoint2);
        instantiateRabbit(respanPoint3);
        instantiateRabbit(respanPoint4);
    }

    private void instantiateRabbit(GameObject randomPoint) 
    {
        Instantiate(slavePrefab, randomPoint.transform.position, randomPoint.transform.rotation);
    }

}