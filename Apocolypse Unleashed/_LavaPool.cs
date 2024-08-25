using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _LavaPool : MonoBehaviour
{
   [SerializeField] _PlayerHealth playerhealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(playerhealth.PlayerDamage(1));
        }
    }
}

