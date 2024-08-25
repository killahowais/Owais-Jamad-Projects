using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    // [SerializeField] private string ScoreText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject player;
    [SerializeField] private float _playerScore;
    
    void Update()
    {
        _playerScore = player.transform.position.z;
        ScoreText.text = "Score: " + ((int) _playerScore).ToString();
    }
}
