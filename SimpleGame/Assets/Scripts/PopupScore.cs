using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScore : MonoBehaviour
{
    public void Animate()
    {
        transform.localScale = new Vector3(0f, 0f, 1f);
        LeanTween.scale(this.gameObject, new Vector3(1f, 1f, 1f), 1f)
                .setEase(LeanTweenType.easeOutElastic)
                .setOnComplete(DisableGameobject);
    }

    private void DisableGameobject()
    {
        this.gameObject.SetActive(false);
    }
}
