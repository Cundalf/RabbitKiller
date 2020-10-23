using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    public void OnPointerEnter(PointerEventData ped)
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SWITCH_OPTION);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SWITCH_OPTION);
    }
}