using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GMscript : MonoBehaviour
{
    public int targetsHit;
    public int score;
    public float timer;
    public GameObject showtimer;
    public GameObject scoreshow;
    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (score >= 11)
            {
                showtimer.GetComponent<TextMeshProUGUI>().text = "good job or smth";
            }
            else
            {
                showtimer.GetComponent<TextMeshProUGUI>().text = "loser";
            }
        }
        else
        {
            showtimer.GetComponent<TextMeshProUGUI>().text = timer.ToString();
        }
        scoreshow.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}