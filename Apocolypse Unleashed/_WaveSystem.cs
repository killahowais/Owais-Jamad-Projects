using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _WaveSystem : MonoBehaviour
{

    [System.Serializable]
    public class WaveContent
    {
        [SerializeField][NonReorderable] GameObject[] MonsterSpawn;

        public GameObject[] GetMonsterSpawnList()
        {
            return MonsterSpawn;
        }
    }
   [SerializeField][NonReorderable] WaveContent[] Waves;

    int CurrentWave = 0;
    float spawnRange = 10;
    public int EnemiesKilled;
    public float delay = 2f;

    public Animator Animator;
    public List <GameObject> _currentMonster;
    private bool isSpawning;

    // scripts 
  [SerializeField]  _PlayerHealth _PlayerHealthScript;
  [SerializeField]  _Gun _GunScript;
  [SerializeField] _PlayerMovement _PlayerMovementScript;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNextWaveWithDelay(delay));
    }

    // Update is called once per frame
    void Update()
    {
        //Starting new Wave and reseting Ammo,Health,Stamina
        if (_currentMonster.Count == 0 && !isSpawning)
        {
              Animator.SetTrigger("WaveFade");
           // Animator.Play("Fade_In_Out");
            CurrentWave++;
            StartCoroutine(SpawnNextWaveWithDelay(delay));
            //Reloading gun 
             StartCoroutine(_GunScript.Reload());
            //Resetting Health 
            StartCoroutine(_PlayerHealthScript.ResetHealth());
            //Reset Stamina 
            StartCoroutine(_PlayerMovementScript.ResetStamina());

        }
    }

    IEnumerator SpawnNextWaveWithDelay(float delay)
    {
        isSpawning = true;
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < Waves[CurrentWave].GetMonsterSpawnList().Length; i++)
        {
            GameObject newspawn = Instantiate(Waves[CurrentWave].GetMonsterSpawnList()[i], FindSpawnLoc(), Quaternion.identity);
            _currentMonster.Add(newspawn);

            _EnemyMovement monster = newspawn.GetComponent<_EnemyMovement>();
             monster.SetSpawner(this);
        }
        isSpawning = false;
        if (CurrentWave >= Waves.Length -1)
        {
            // Als dit de laatste golf is, laad dan een andere scène

            Won();

        }
    }
    Vector3 FindSpawnLoc() 
    {
        Vector3 SpawnPos ;

        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = transform.position.y;

        SpawnPos = new Vector3(xLoc, yLoc, zLoc);
        
        if (Physics.Raycast(SpawnPos, Vector3.down,5))
        {
            return SpawnPos;
        }
        else
        {
            return FindSpawnLoc();
        }
    }

    public void Won() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("_WinScreen");

    }

    public void RemoveMonster()
    {

    }

}
