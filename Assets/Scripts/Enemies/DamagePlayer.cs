using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float timeStopDamage = 2f;
    public int damage = 1;
    private float timeDamage;

    [SerializeField]
    private bool canHit = true;

    void Update()
    {
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;
        if (!canHit)
        {
            timeDamage += Time.deltaTime;
            if (timeDamage >= timeStopDamage)
            {
                canHit = true;
                timeDamage = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canHit)
        {
            other.gameObject.GetComponent<PlayerController>().Hit(this.damage);
            canHit = false;

            int randomSFX = Random.Range(0, 2);
            if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_BITE);
            if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_BITE_ALT);
        }
    }
}
