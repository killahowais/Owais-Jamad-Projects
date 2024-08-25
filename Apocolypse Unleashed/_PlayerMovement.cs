using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Player;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float Stamina;
    [SerializeField] private float MaxStamina;

    private Vector3 position;
    private bool IsRunning;
    public Slider Staminaslider;

    // audio clip
   public  AudioSource RunningSound;

    void Update()
    {   
        position.x = Input.GetAxisRaw("Horizontal");
        position.z = Input.GetAxisRaw("Vertical");

        Player.transform.Translate(position.x * _movementSpeed* Time.deltaTime,0, position.z * _movementSpeed * Time.deltaTime);

        // Checking for Jumping input
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            IsRunning = true;

        }
        else
        {
            IsRunning = false;
            _movementSpeed = 5f;
        }


        if (!IsRunning)
        {
            RunningSound.Stop();
        }

        // if the player is running
        if (IsRunning)
        {
            if (!RunningSound.isPlaying) // Controleer of het geluid niet al aan het afspelen is
            {
                RunningSound.Play(); // Speel het geluid af als het niet al aan het afspelen is
            }


            Stamina -= Time.deltaTime;
            _movementSpeed = 10f;
            //if player is out of stamina 

            if (Stamina < 0)
            {
                _movementSpeed = 5f;
                Stamina = 0;
                IsRunning = false;
                RunningSound.Stop();
            }
        }

        else if (Stamina < MaxStamina)
        {
            Stamina += Time.deltaTime;
        }
        Staminaslider.value = Stamina;
    }

    //resetting stamina when new wave
    public IEnumerator ResetStamina()
    {
        Stamina = MaxStamina;

        yield return null;
    }
}
