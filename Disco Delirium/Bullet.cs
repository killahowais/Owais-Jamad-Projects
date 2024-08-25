using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public GameObject Player;
     private Rigidbody2D rb;
     private float timer;

    [SerializeField] private float _ShootSpeed;
    [SerializeField] private int _playerDamage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * _ShootSpeed;

        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer> 10 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out IDamagable iDamagable))
        {
            iDamagable.Hit(_playerDamage);
            Destroy(gameObject);
        }
    }
}
