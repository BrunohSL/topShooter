using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using Pathfinding;

public class TutorialWaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING}

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Transform player;
    private GameObject chestObj;
    private Chest chestController;

    public Wave[] waves;
    public Transform[] spawnPoints;
    private int nextWave = 0;

    public Canvas canvas;
    public GameObject fadeTextPrefab;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountdown = timeBetweenWaves;

        chestObj = GameObject.FindGameObjectWithTag("chest");
        chestController = chestObj.GetComponent<Chest>();

        if (spawnPoints.Length == 0) {
            Debug.LogError("No Spawn points detected!");
        }

        if (waves.Length == 0) {
            Debug.LogError("No waves detected!");
        }
    }

    void Update() {
        if (state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                WaveCompleted();
                return;
            } else {
                return;
            }
        }

        if (chestController.getCollected()) {
            // show message "Monsters are comming"
            if (waveCountdown <= 0) {
                if (state != SpawnState.SPAWNING) {
                    showFadeText("Monsters incoming", 30f, -110f, 0f);

                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            } else {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    void showFadeText(string text, float x, float y, float z) {
        GameObject fadeTextPrefabObj = Instantiate(fadeTextPrefab, new Vector3(x, y, z), Quaternion.identity);
        fadeTextPrefabObj.transform.SetParent(canvas.transform, false);
        fadeTextPrefabObj.GetComponent<Text>().text = text;
    }

    void WaveCompleted() {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            // show tutorial completed message
            // create a coroutine to complete the tutorial

            // call game scene
            // SceneManager.LoadScene("MainCity");
            Debug.Log("Completed all waves. Looping");
        } else {
            showFadeText("Wave Clear", 50f, -110f, 0f);

            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f) {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("enemy") == null) {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave (Wave _wave) {
        state = SpawnState.SPAWNING;

        if (_wave.name == "wave 3") {
            Debug.Log("Last Wave");
        }

        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy (Transform _enemy) {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform newEnemy = Instantiate(_enemy, _sp.position, _sp.rotation);
        newEnemy.GetComponent<EnemyAI>().setTarget(player);
    }
}
