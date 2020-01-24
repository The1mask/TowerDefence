using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject CurrentPoint;
    public Transform cam;
    public GameObject BuildMenu;
    public GameObject DestroyMenu;
    public GameObject CurrentTower;

    private bool _open;
    private GameObject CurrentMenu;
    private Vector2 _localPoint;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        CurrentMenu = BuildMenu;
    }


    void Update()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero);
            if (rayHit.transform != null)
            {
                if (rayHit.transform.gameObject.layer == 8)
                {
                    
                    if (CheckAnother(rayHit.transform.gameObject))
                        CloseTMenu();
                    CurrentPoint = rayHit.transform.gameObject;
                    if (CheckBuild())
                        CurrentMenu = DestroyMenu;
                    else
                        CurrentMenu = BuildMenu;
                        CurrentTower = CurrentPoint.GetComponent<PointData>().Tower;
                        if (!_open)
                        {
                            _localPoint = cam.InverseTransformPoint(rayHit.transform.position);
                            OpenMenu();
                        if (CurrentMenu == DestroyMenu)
                            CurrentTower.GetComponent<TowerController>().transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else
                        {
                            CloseTMenu();
                        }
                    
                }
            } 
        }
    }
    bool CheckAnother(GameObject NewPoint)
    {
        if (CurrentPoint == NewPoint)
        return false;
        else
        return true;
    }

    bool CheckBuild()
    {
        return CurrentPoint.GetComponent<PointData>().Build; 
    }

    void OpenMenu()
    {
        _open = true;
        CurrentMenu.transform.position = new Vector3(_localPoint.x, _localPoint.y, -0.01f);
        CurrentMenu.SetActive(true);
        if (CurrentMenu == DestroyMenu)
        {
            CurrentMenu.transform.GetComponent<DestroyMenu>().Tower = CurrentTower;
        }
        if (CurrentMenu == BuildMenu)
        {
            CurrentMenu.transform.GetComponent<BuildMenu>().CurrPoint = CurrentPoint.transform;
        }
    }

    public void CloseTMenu()
    {
        _open = false;
        CurrentMenu.gameObject.SetActive(false);
        if (CurrentMenu == DestroyMenu)
            CurrentTower.GetComponent<TowerController>().transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ChangeMenu()
    {
        if (CurrentMenu == DestroyMenu)
            CurrentMenu = BuildMenu;
        else
        if (CurrentMenu == BuildMenu)
            CurrentMenu = DestroyMenu;
    }

}
