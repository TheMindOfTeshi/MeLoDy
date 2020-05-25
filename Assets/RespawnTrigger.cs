using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnLocation;

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = respawnLocation.transform.position;
    }

}
