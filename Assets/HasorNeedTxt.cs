using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HasorNeedTxt : MonoBehaviour
{
    public bool has;
    public bool needs;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController");
    }

    // Update is called once per frame
    void Update()
    {
        if (has)
        {
            GetComponent<TextMeshPro>().text = player.GetComponent<player>().fishHeld.ToString();
        }
        if (needs)
        {
            GetComponent<TextMeshPro>().text = (player.GetComponent<player>().difficulty + 5).ToString();
        }
        if (player.GetComponent<player>().fishHeld >= player.GetComponent<player>().difficulty + 5)
        {
            GetComponent<TextMeshPro>().color = Color.green;
        } else
        {
            GetComponent<TextMeshPro>().color = Color.red;
        }
    }
}
