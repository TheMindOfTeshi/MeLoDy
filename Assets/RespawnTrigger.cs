using System.Collections;

using UnityEngine;


public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnLocation;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            player.transform.position = respawnLocation.transform.position;
        }
        
    }

}
