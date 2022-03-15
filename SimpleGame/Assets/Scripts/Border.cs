using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public GameObject Player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "dart")
        {
            Destroy(other.gameObject);
            Player.GetComponent<PlayerController>().UpdateScore(0);
        }
    }

}
