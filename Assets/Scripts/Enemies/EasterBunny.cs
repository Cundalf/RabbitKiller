using UnityEngine;
using UnityEngine.Rendering;

public class EasterBunny : EnemyController
{
    public GameObject slavePrefab;
    public GameObject respanPoint1;
    public GameObject respanPoint2;
    public GameObject respanPoint3;
    public GameObject respanPoint4;

    public override void Start()
    {
        base.Start();
        healt = 100;
        timeStop = 15f;
    }


    public override void movePNJ() {
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