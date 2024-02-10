using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    public GameObject player;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int newScore = player.GetComponent<GameManager>().scoreTreasure;
        int enemyScore2 = player.GetComponent<GameManager>().enemyScore;
        text.text = "Enemies: " + enemyScore2;
        text.text = "Collected: " + newScore + " /7";
        
    }
}
