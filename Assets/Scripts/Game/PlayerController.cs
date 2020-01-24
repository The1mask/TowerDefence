using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float Heach;
    public int Money;
    public int MaxWaves;
    public int CurrWave;
    public TextMeshProUGUI[] Text;

    void Start()
    {
        Text[0].text = Heach.ToString();
        Text[1].text = Money.ToString();
        Text[2].text = string.Format("{0} {1} {2}", CurrWave.ToString(), "/", MaxWaves);
    }

    public void SetText()
    {
        Text[0].text = Heach.ToString();
        Text[1].text = Money.ToString();
        Text[2].text = string.Format("{0} {1} {2}", CurrWave.ToString(), "/", MaxWaves);
    }
}
