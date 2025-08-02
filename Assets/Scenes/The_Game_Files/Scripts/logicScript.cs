using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
{
    public static int playerScore = 0;
    public Text scoreText;
    public int spawnNumber = 0;
    //public Image playerHeart, playerHeart_1, playerHeart_2;

    //public static logicScript instance;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GameObject.Find("scoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int amount)
    {
        spawnNumber += amount;
        playerScore += amount;
        scoreText.text = playerScore.ToString();
    }
}