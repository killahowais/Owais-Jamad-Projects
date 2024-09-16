using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCar : MonoBehaviour
{
    [SerializeField] private GameObject Car;
    private float FlipTimer = 0f;
    private Quaternion originalRotation;
    CarControllers CarController;
    void Start()
    {
        originalRotation = Car.transform.rotation;
        CarController = GetComponent<CarControllers>();
    }

    void Update()
    {
        FlipTimer -= Time.deltaTime;
        if (FlipTimer <= 0)
        {
            FlipTimer = 0;
        }
        if (Input.GetKey(KeyCode.P) && FlipTimer <= 0 && CarController.speed <= 30)
        {
            FlipTimer = 3;
            Car.transform.rotation = originalRotation;
            Vector3 newPosition = Car.transform.position + new Vector3(0, 10.0f, 0);
            Car.transform.position = newPosition;
        }
    }

}
