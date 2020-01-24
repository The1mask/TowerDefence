using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public float[] Radius;
    public float[] AttackSpeed;
    public float[] Damage;
    public int[] Cost;

    public GameObject Tower;
    public MenuController MenuButton;
    public Transform CurrPoint;
    public Sprite[] Sprite;

    private PlayerController _player;
    private GameObject _createdTower;
    private int _currentButton;
    private GameObject[] _towers;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerControl").GetComponent<PlayerController>();
    }


    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero);
            if (rayHit.transform != null)
            {
                if (rayHit.transform.gameObject.layer == 9)
                {
                    _currentButton = int.Parse(rayHit.transform.gameObject.name);
                    if (_player.Money >= Cost[_currentButton])
                    {
                        BuildTower();
                        MenuButton.CurrentPoint.GetComponent<PointData>().Build = true;
                        MenuButton.CloseTMenu();
                        MenuButton.CurrentPoint.GetComponent<PointData>().Tower = _createdTower;
                    }
                }
            }
        }
    }

    void BuildTower()
    {
        _createdTower = Instantiate(Tower, new Vector3(CurrPoint.position.x, CurrPoint.position.y, CurrPoint.position.z-0.01f), Quaternion.identity);
        _createdTower.GetComponent<SpriteRenderer>().sprite = Sprite[_currentButton];
        _createdTower.GetComponent<CircleCollider2D>().radius = Radius[_currentButton];
        _createdTower.transform.GetChild(0).localScale = _createdTower.transform.GetChild(0).localScale*Radius[_currentButton];
        _createdTower.transform.GetChild(0).gameObject.SetActive(false);
        _createdTower.GetComponent<TowerController>().ShInterval = AttackSpeed[_currentButton];
        _createdTower.GetComponent<TowerController>().Price = Cost[_currentButton];
        _createdTower.GetComponent<TowerController>().Damage = Damage[_currentButton];
        _player.Money -= Cost[_currentButton];
        _player.SetText();
    }
}
