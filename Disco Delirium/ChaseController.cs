using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    [SerializeField] EnemyRoom EnemyRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyRoom.isTriggerd = true;
            Debug.Log("Collisiongworks");
        }
    }
}
