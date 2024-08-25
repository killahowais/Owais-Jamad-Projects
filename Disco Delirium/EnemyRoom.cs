using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    [SerializeField] private Animator Animator1;
    [SerializeField] private Animator Animator2;

    [SerializeField] private GameObject Gate1;
    [SerializeField] private GameObject Gate2;

    [SerializeField] private Transform OpenPos;
    [SerializeField] private Transform ClosedPos;

    private float _doorSpeed = 4f ;
    private bool doorclosed;
    private bool dooropened;
    public bool isTriggerd;
                                       
    public GameObject[] EnemyArray;

    private void Update()
    {
        if (isTriggerd)
        {
            StartCoroutine(Closegate());
            if (EnemyArray != null) // if enemy array is not empty 
            {
               bool AllEnemiesDestroyed = true; 

                for (int i = 0; i < EnemyArray.Length; i++) // checking if all enemies are alive or not 
                {
                    if (EnemyArray[i] != null)
                    {
                        AllEnemiesDestroyed = false;
                        EnemyArray[i].SetActive(true);
                    }
                }

                if (AllEnemiesDestroyed) 
                {
                    StartCoroutine(Opengate());
                    Debug.Log("Alle enemies zijn dood");
                }
            }
            else
            {
                StartCoroutine(Opengate());
                Debug.Log("Enemy array is null");
            }
        }
    }
    public IEnumerator Closegate()  
    {
        Gate1.transform.position = Vector3.MoveTowards(Gate1.transform.position, ClosedPos.position, _doorSpeed * Time.deltaTime);

        if (!doorclosed)
        {
            Animator1.SetTrigger("GateClose");
            doorclosed = true;
        }
        yield return null;
    }

    public IEnumerator Opengate()
    {
        Gate2.transform.position = Vector3.MoveTowards(Gate2.transform.position, OpenPos.position, _doorSpeed * Time.deltaTime);
        if (!dooropened)
        {
            Animator2.SetTrigger("GateOpen");
            dooropened = true;
        }
        
        yield return null;
    }
}
