using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float Damage;
    public float ShInterval;
    public int Price;
    public GameObject Ball;
    public List<GameObject> _enemyInsideTrigger;

    private GameObject _createdBall;
    private CircleCollider2D _radiusTrigger;
    private GameObject _radiusSprite;
    private float _startTime;
    private bool _fight;


    void Start()
    {
        _enemyInsideTrigger = new List<GameObject>();
        _startTime = ShInterval;
    }

    void Update()
    {
        if (_fight)
        {
            _startTime -= Time.deltaTime;
            if (_startTime <= 0.0f)
            {
                Fire();
                _startTime = ShInterval;
            }
        }
    }

    private void Fire()
    {
        _createdBall = Instantiate(Ball, this.transform.position, Quaternion.identity) as GameObject;
        _createdBall.GetComponent<BallController>().Target = currTarget();
        _createdBall.GetComponent<BallController>().Damage = Damage;
        _createdBall.GetComponent<BallController>().Tower = this.gameObject;
    }

    private GameObject currTarget()
    {
        for (int i = 0; i < _enemyInsideTrigger.Count; i++)
        {
            if (_enemyInsideTrigger[i] != null)
                return _enemyInsideTrigger[i];
        }
        return null;
    }

    bool CheckTargetExist()
    {
        if (_enemyInsideTrigger.Count == 0)
            return false;
        return true;
    }

    public void RadiusController()
    {
        if (_radiusSprite.active)
            _radiusSprite.SetActive(false);
        else
            _radiusSprite.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _enemyInsideTrigger.Add(other.gameObject);
        _fight = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _enemyInsideTrigger.Remove(other.gameObject);
        _fight = CheckTargetExist();
    }
}
