using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public float Damage;
    public float Heach;
    public float Speed;

    public GameObject Player;
    public GameObject[] pivot;
    public TextMeshProUGUI Text;
    public GameObject HeachBar;
    private int cout;
    private Vector3 targetDirection;
    private Vector3 selfDir;
    private Vector3 newDirection;
    private float _trueHeach;

    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("PlayerControl"); 
        cout = 0;
        _trueHeach = 1 / Heach;
        ChangeHeach();
    }

    void Update()
    {
        MoveTo();
    }

    public void NewEnemy(Sprite EnemySprite, float NewHeach, float NewSpeed, float NewDamage, GameObject[] NewPivot)
    {
        this.GetComponent<SpriteRenderer>().sprite = EnemySprite;
        Heach = NewHeach;
        Speed = NewSpeed;
        Damage = NewDamage;
        pivot = NewPivot;
    }

    public void MoveTo()
    {
        if (cout == pivot.Length)
        {
            Player.GetComponent<PlayerController>().Heach -= Damage;
            Player.GetComponent<PlayerController>().SetText();
            Destroy(this.gameObject);
            if (Player.GetComponent<PlayerController>().Heach<=0)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            targetDirection = pivot[cout].transform.position;
            selfDir = transform.position;
            newDirection = targetDirection - selfDir;
            this.transform.position = Vector3.MoveTowards(transform.position, targetDirection, Speed);
            if (this.transform.position == targetDirection)
            {
                cout = cout + 1;
            }
        }
    }

    public void ChangeHeach()
    {
        HeachBar.transform.localScale = new Vector3(_trueHeach * Heach, HeachBar.transform.localScale.y, HeachBar.transform.localScale.z);
    }
}
