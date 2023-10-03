using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int s)
    {
        score += s;
        ScoreText.text = score.ToString();
    }

    public void GameOver()
    {

    }
}
