using UnityEngine;
using UnityEngine.Rendering;

public class EasterBunny : EnemyController, IEasterBunny
{
    public GameObject slavePrefab { get; set; }
    public GameObject respanPoint1 { get; set; }
    public GameObject respanPoint2 { get; set; }
    public GameObject respanPoint3 { get; set; }
    public GameObject respanPoint4 { get; set; }

    public int healtBarAmount { get; set; }

    public override void Update() 
    {
        base.Update();
        this.healtControl();
    }
    public override void Start()
    {
        base.Start();
        healt = 100;
        timeStop = 15f;
    }

    public void healtControl() 
    {
        if (this.healtBarAmount > 0 && this.healt == 0) 
        {
            this.healtBarAmount--;
            this.healt = 100;
        }
    }

    public override void Die()
    {
        if (this.healtBarAmount == 0 && this.healt == 0)
        {
            base.Die();
        }
    }

    public override void movePNJ() 
    {
        base.movePNJ();
        Instantiate(slavePrefab, respanPoint1.transform.position, respanPoint1.transform.rotation);
        Instantiate(slavePrefab, respanPoint2.transform.position, respanPoint2.transform.rotation);
        Instantiate(slavePrefab, respanPoint3.transform.position, respanPoint3.transform.rotation);
        Instantiate(slavePrefab, respanPoint4.transform.position, respanPoint4.transform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
    }
}