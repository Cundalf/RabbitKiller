using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXType : MonoBehaviour
{
    public enum SoundType
    {
        SWITCH_OPTION, 
        COUNTDOWN, 
        LOSE, 
        FIRE, 
        RELOAD, 
        RABBIT_RESPAWN, 
        RABBIT_RESPAWN_ALT, 
        RABBIT_DEATH,
        RABBIT_DUPLICATE,
        RABBIT_BITE,
        RABBIT_BITE_ALT,
        PLAYER_DEATH
    }

    public SoundType type;
}
