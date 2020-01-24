using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenu : MonoBehaviour
{
    public MenuController CurrButton;
    public Transform CurrPoint;
    public GameObject Tower;

    private GameObject[] _towers;
    private PlayerController _player;

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
                    CurrButton.CloseTMenu();
                    CurrButton.ChangeMenu();
                    Debug.Log(rayHit.transform.gameObject.layer);
                    Destroy(Tower);
                    CurrButton.CurrentPoint.GetComponent<PointData>().Build = false;
                    _player.Money += Tower.GetComponent<TowerController>().Price;
                    _player.SetText();
                }
            }
        }
    }
}
