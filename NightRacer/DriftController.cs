using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DriftController : MonoBehaviour
{
    // text components
    public Rigidbody playerRB;
    public TMP_Text totalScoreText;
    public TMP_Text currentScoreText;
    public TMP_Text factorText;
    public TMP_Text driftAngleText;

    // tweakble components for drift mode 
    private float speed = 0;
    private float driftAngle=0;
    private float driftFactor;
    private float currentScore;
    private float totalScore;

    private bool isDrifting= false;

    private CarControllers carController;
    // variables for it to detect if you are drifting or not 
    public float minimumSpeed = 5;
    public float minimumAngle = 10;
    public float driftingDelay = 0.2f;
    public GameObject DriftingObject;
    private IEnumerator stopDriftingCoroutine = null;

    // color for the text 
    public Color normalDriftColor;
    public Color NearStopColor;
    public Color driftEndedColor;

    private bool isDriftSoundPlaying = false;
    public AudioSource driftSound;


    void Start()
    {
        driftSound = GetComponent<AudioSource>();
        DriftingObject.SetActive(false);
        carController = GetComponent<CarControllers>();
    }

    void Update()
    {
        ManageDrift();
        ManageUI();
    }
    void ManageDrift() 
    {
        // calculating speed
        speed = playerRB.velocity.magnitude;
        // angle for the drifting being calulated
        driftAngle = Vector3.Angle(playerRB.transform.forward, (playerRB.velocity + playerRB.transform.forward).normalized);
        if (driftAngle>120)
        {
            driftAngle = 0;
        }
        if (driftAngle >= minimumAngle && speed > minimumSpeed)
        {
            if (!isDrifting || stopDriftingCoroutine != null)
            {
                StartDrift();
                PlayDriftSound();

            }
        }
        else
        {
            if (isDrifting && stopDriftingCoroutine == null)
            {
                StopDrifting();
                StopDriftSound();

            }
        }
        if (isDrifting)
        {
            currentScore += Time.deltaTime* driftAngle *driftFactor;
            driftFactor += Time.deltaTime;
            DriftingObject.SetActive(true);
        }
    }
    async void StartDrift()
    {
        if (!isDrifting)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * driftingDelay));
            driftFactor = 1;
        }
        if (stopDriftingCoroutine != null)
        {
            StopCoroutine(stopDriftingCoroutine);
            stopDriftingCoroutine = null;
        }
        currentScoreText.color = normalDriftColor;
        isDrifting = true;
    }
    public void StopDrifting() 
    {
        stopDriftingCoroutine = StoppingDrift();
        StartCoroutine(stopDriftingCoroutine);
    }
    private IEnumerator StoppingDrift()
    {
        yield return new WaitForSeconds(0.1f);
        currentScoreText.color = NearStopColor;
        yield return new WaitForSeconds(driftingDelay * 4f);
        totalScore += currentScore;
        carController.connectedPlayer.SetDriftPoints(totalScore);
        isDrifting = false;
        currentScoreText.color = driftEndedColor;
        yield return new WaitForSeconds(0.5f);
        currentScore = 0;
        DriftingObject.SetActive(false);
    }
    void ManageUI()
    {
        // alpha for the text 
        normalDriftColor.a= 255;
        NearStopColor.a = 255;
        driftEndedColor.a = 255;

        totalScoreText.text = "Total: " + (totalScore).ToString("###,###,000");
        factorText.text = driftFactor.ToString("###,###,##0.0") + "X";
        currentScoreText.text = currentScore.ToString("###,###,000");
        driftAngleText.text = driftAngle.ToString("###,##0") + "�";
    }
    // sounds
    void PlayDriftSound()
    {
        if (!isDriftSoundPlaying && driftSound != null)
        {
            driftSound.Play();
            isDriftSoundPlaying = true;
        }
    }
    // sounds
    void StopDriftSound()
    {
        if (isDriftSoundPlaying && driftSound != null)
        {
            driftSound.Stop();
            isDriftSoundPlaying = false;
        }
    }
    // stop drifting 
    private void OnCollisionExit(Collision other)
    {
        StopDrifting();
    }
}
