using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _PlayerHealth : MonoBehaviour
{
    private int Maxhealth = 10;
    public int _currentHealth;
    public float  _timeBeforeStrike;
    public Slider _playerHealthSlider;

    public bool _damageEffectRunning = false;

    public Animator animatordeath;
    void Start()
    {     
      _currentHealth = Maxhealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
          StartCoroutine(PlayerDeath());    
                              
        } 
        _playerHealthSlider.value = _currentHealth;
    }

    public IEnumerator PlayerDamage(int Damage)
    {
        yield return new WaitForSeconds(_timeBeforeStrike);
        _currentHealth -= Damage;
    }

    public IEnumerator PlayerDeath()
    {
        // Play the death animation
        animatordeath.Play("DeathAnimation");

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Switch to the "GameOver" scene
        SceneManager.LoadScene("_GameOver");
    }
    // resettign health for spawning new wave 
    public IEnumerator ResetHealth()
    {
        _currentHealth = Maxhealth;
        yield return null;
    }
}
