using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    private GameObject Player;
    private float timer;
    [SerializeField] private float _Shootspeed;
    [SerializeField] private AudioSource audio;
    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);  

        if (distance < 8)
        {
            timer += Time.deltaTime;
            
            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }  
    }
    void Shoot() 
    {
        Instantiate(bullet, bulletPos.position , Quaternion.identity);
       // audio.Play();
    }
}


