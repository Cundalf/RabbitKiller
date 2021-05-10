using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public void PlayCountDownSFX()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.COUNTDOWN);
    }

    public void Finish()
    {
        Destroy(gameObject);
        GameManager.sharedInstance.changeGameState(GameManager.GameState.IN_GAME);
    }
}
