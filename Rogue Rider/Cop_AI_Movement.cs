using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_AI_Movement : MonoBehaviour
{
    PlayerMovement Playermovement;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform car;

    [Header("Variables")] // variables
     
    public float _speedGear;
    public float _speed;
   
    private bool _isChangingLane;
    private int _currentLane= 1;

    public bool _MirrorCheck_L = true;
    public bool _MirrorCheck_R = true;

    void Start()
    {
        Playermovement = GetComponent<PlayerMovement>();
    }

    

    void Update()
    {
        EnemyMove();    
        SpeedSwitch();
        DebugFunction();

        #region summery
        // if a NPC Car comes to close to the NPC from the front it wil iniate a lane change if there are no cars left or right
        // also the speed is changed when a NPC is close to the AI

        #endregion    
        if (Physics.Raycast(enemy.transform.position + Vector3.up, Vector3.forward, out RaycastHit raycastHit, 3))
        { 
            if (_MirrorCheck_L && _MirrorCheck_R == true)
            {
                switch (_currentLane)
                {
                    case 1:
                        StartChangeLane(2);
                        break;

                    case 2:
                        StartChangeLane(1);
                        break;
                }
            }
            if (raycastHit.collider.CompareTag("NPC"))
            {
                _speedGear = 2;
            }
        }
        else
        {
            _speedGear = 1;
        }
        // checks for cars left and right 
        if (Physics.Raycast(enemy.transform.position + Vector3.back * 2 + Vector3.up + Vector3.left * 3f, Vector3.forward, out RaycastHit rayLeft, 10))
        {
            _MirrorCheck_L = false;
        }
        else
        {
            _MirrorCheck_L = true;
        }
        if (Physics.Raycast(enemy.transform.position + Vector3.back * 2 + Vector3.up + Vector3.right * 3f, Vector3.forward, out RaycastHit rayRight, 10))
        {
            _MirrorCheck_R = false;
        }
        else
        {
            _MirrorCheck_R = true;
        }
    }

    // Enemy Movement 
    private void EnemyMove()
        {
           car.position += Vector3.forward * _speed / 3.6f * Time.deltaTime;
        }

    // Speed Gear makes car go fast or slow depending on the gear 
    private void SpeedSwitch() 
    {
        switch (_speedGear)
        {
            case 1:
                _speed = 70;
                break;
            case 2:
                _speed = 30;
                break;       
        }
    }

    /// <summary>
    /// Initiates a lane change for the car.
    /// </summary>
    /// 
    /// <param name="lane">Lane to change to</param>
    private void StartChangeLane(int lane)
    {
        if (_currentLane == lane) return;

        if (!_isChangingLane)
            StartCoroutine(LaneChangeMovement(lane, 1f));
    }

    /// <summary>
    /// Do the actual lane change movement.
    /// </summary>
    /// <param name="lane">Lane to change to</param>
    /// <param name="time">Time it will take to change lane</param>
    /// <returns></returns>
    private IEnumerator LaneChangeMovement(int lane, float time)
    {
        // Set the changing of lanes to true so that you can't change lanes while changing lanes.
        _isChangingLane = true;

        // Decide the amount to move based on the lane you want to move to.
        int moveAmount = lane == 2 ? 3 : -3;

        // Start and final position for the lerp.
        Vector3 startPos = transform.position;
        Vector3 finalPos = new Vector3(startPos.x + moveAmount, startPos.y, startPos.z);

        // Start and final rotation for having a bit of an animation when changing lanes.
        Vector3 startRotation = Vector3.zero;
        Vector3 finalRotation = Vector3.up * (moveAmount * 2);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            // Make car move to the side
            transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / time);

            // Make car rotate to the side
            if (elapsedTime < time / 2)
                car.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, finalRotation, elapsedTime / (time / 2)));
            else
                car.rotation = Quaternion.Euler(Vector3.Lerp(finalRotation, startRotation, (elapsedTime - time / 2) / (time / 2)));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set these values to current state.
        _currentLane = lane;
        _isChangingLane = false;
    }

    // a fucntion just for debugging and testing 
    private void DebugFunction() 
    {
        //Debug.Log(_MirrorCheck_R);
        //Debug.Log(_MirrorCheck_L);

        //Debug.DrawRay(enemy.transform.position + Vector3.up, Vector3.forward * 3, Color.green, 0.1f);
        //Debug.DrawRay(enemy.transform.position + Vector3.back * 2 + Vector3.up + Vector3.left * 3f, Vector3.forward * 10, Color.green, 0.1f);
        //Debug.DrawRay(enemy.transform.position + Vector3.back * 2 + Vector3.up + Vector3.right * 3f, Vector3.forward * 10, Color.green, 0.1f);

    }
}
