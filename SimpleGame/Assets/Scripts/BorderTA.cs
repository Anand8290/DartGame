using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTA : MonoBehaviour
{
    public GameObject Player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "dart")
        {
            Destroy(other.gameObject);
            Player.GetComponent<PlayerTA>().UpdateScore(0);
        }
    }
}
