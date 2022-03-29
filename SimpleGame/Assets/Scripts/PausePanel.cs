using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    
    public void Animate()
    {
        Panel.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        LeanTween.scale(Panel, new Vector3(1f, 1f, 1f), 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic).setIgnoreTimeScale(true);
    }
}
