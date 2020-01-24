using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallController : MonoBehaviour
{
    public float Damage;
    public GameObject Tower;
    public GameObject Target;

    private Vector3 _targetDirection;
    private Vector3 _selfDir;
    private Vector3 _newDirection;
    private PlayerController Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerControl").GetComponent<PlayerController>();
    }


    void Update()
    {
        CheckEnemyExist();
        MoveTo();
    }

    void DoDamage()
    {
        Target.GetComponent<EnemyController>().Heach -= Damage;
        Target.GetComponent<EnemyController>().ChangeHeach();
        CheckKill();
    }

    void CheckEnemyExist()
    {
        try
        {
            if (Target.GetComponent<EnemyController>().Heach <= 0)
                Destroy(this.gameObject);
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
    }

    void CheckKill()
    {
        if (Target.GetComponent<EnemyController>().Heach <= 0)
        {
            Player.Money += UnityEngine.Random.Range(1, (int)(Target.GetComponent<EnemyController>().Heach/10));
            Player.SetText();
            Destroy(Target.gameObject);
            Tower.GetComponent<TowerController>()._enemyInsideTrigger.Remove(Target.gameObject);
        }
    }

    void MoveTo()
    {
        _targetDirection = Target.transform.position;
        _selfDir = transform.position;
        _newDirection = _targetDirection - _selfDir;
        this.transform.position = Vector3.MoveTowards(transform.position, _targetDirection, 0.1f);
        DestroySelf(_targetDirection);
    }

    void DestroySelf(Vector3 targetDirection)
    {
        if (this.transform.position == targetDirection)
        {
            DoDamage();
            Destroy(this.gameObject);
        }
    }
}
