using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppreciateManager : MonoBehaviour
{
    //[SerializeField] Image appreciateUI;
    [SerializeField] Sprite appreciate_1, appreciate_2;
    [SerializeField] ParticleSystem particle_1, particle_2;
    [SerializeField] GameObject message;

    public void Appreciate(int grade)
    {
        StartCoroutine(AppreciateIE(grade));
    }
    
    IEnumerator AppreciateIE(int _grade)
    {
        ParticleSystem ps;

        switch(_grade)
        {
            case 1:
            ps = particle_1;
            //appreciateUI.sprite = appreciate_1;
            message.GetComponent<SpriteRenderer>().sprite = appreciate_1;
            AudioManager.instance.PlaySound("Celebrate");
            break;

            case 2:
            ps = particle_2;
            //appreciateUI.sprite = appreciate_2;
            message.GetComponent<SpriteRenderer>().sprite = appreciate_2;
            break;

            default:
            ps = particle_2;
            //appreciateUI.sprite = appreciate_1;
            message.GetComponent<SpriteRenderer>().sprite = appreciate_2;
            break;
        }
        
        ps.gameObject.SetActive(true);
        message.SetActive(true);
        
        //appreciateUI.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //appreciateUI.transform.gameObject.SetActive(false);
        message.SetActive(false);
    }
}
