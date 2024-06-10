using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextBasedGame : MonoBehaviour
{

    public TextMeshProUGUI DashBoard;
    public TextMeshProUGUI EnemyHP;

    int player_points = 10;
    int ai_points = 10;
    // Start is called before the first frame update
    void Start()
    {
        DashBoard.text = "You start!"; 
        EnemyHP.text = "Enemy HP: 10";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
