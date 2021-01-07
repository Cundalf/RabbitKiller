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
        healt = 100;
        timeStop = 15f;
    }

    public void healtControl(int danio)
    {
        this.healt = this.healt - danio;
        if (this.healt <= 0) 
        {
            if (this.healtBarAmount > 0)
            {
                this.healtBarAmount--;
                this.healt = 100;
            }
            else 
            {
                this.Die();
                this.enemyRespawnController.setBossStillAlive(false);
            }
        }      
    }

    public override void movePNJ() 
    {
        base.movePNJ();
        this.useSkill();
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