using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    public float[] Damage;
    public float[] Heach;
    public float[] Speed;
    public float SpawnSpeed;

    public int currWave;
    public GameObject Enemy;
    public Sprite[] EnemySprite;
    public GameObject SpawnPoint;
    public PlayerController Player;
    public float WavesTime;
    public float PauseTime;
    public int MaxWaves;

    private GameObject _createdEnemy;
    private float _startTime;
    private int _currEnemy;
    private bool _pause;
    private float _pauseTime;
    private GameObject[] pivot;
    private bool win;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerControl").GetComponent<PlayerController>();
        pivot = new GameObject[6];
        pivot[0] = GameObject.FindGameObjectWithTag("MovePoint");
        pivot[1] = GameObject.FindGameObjectWithTag("mp1");
        pivot[2] = GameObject.FindGameObjectWithTag("mp2");
        pivot[3] = GameObject.FindGameObjectWithTag("mp3");
        pivot[4] = GameObject.FindGameObjectWithTag("mp4");
        pivot[5] = GameObject.FindGameObjectWithTag("mp5");

        _currEnemy = 0;
        _pause = true;
        _pauseTime = PauseTime;
        win = false;
        MaxWaves = Player.MaxWaves;
    }

    void Update()
    {
        if (!win)
        {
            if (currWave > MaxWaves)
            {
                win = true;
                SceneManager.LoadScene(0);
            }
            else
            {
                _pauseTime -= Time.deltaTime;
                if (_pauseTime <= 0.0f)
                {
                    if (_pause)
                    {
                        currWave++;
                        Player.CurrWave = currWave;
                        Player.SetText();
                        _pauseTime = WavesTime;
                        _pause = false;
                    }
                    else
                    {
                        _pause = true;
                        _pauseTime = PauseTime;
                    }
                }
                if (!_pause)
                {
                    NewWave();
                }
            }
        }
    }

    void CurrentEnemy()
    {
        if(currWave<4)
        _currEnemy = Random.RandomRange(0, currWave);
        else
        _currEnemy = Random.RandomRange(2, 4);
}

    void NewEnemy()
    {
        _createdEnemy = Instantiate(Enemy, SpawnPoint.transform.position, Quaternion.identity);
        _createdEnemy.GetComponent<EnemyController>().NewEnemy(EnemySprite[_currEnemy], Heach[_currEnemy], Speed[_currEnemy], Damage[_currEnemy], pivot);
    }

    void NewWave()
    {
        _startTime -= Time.deltaTime;
        if (_startTime <= 0.0f)
        {
            CurrentEnemy();
            NewEnemy();
            _startTime = SpawnSpeed;
        }
    }
}
