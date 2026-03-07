using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Begin : MonoBehaviour
{
    public bool started = false;
    public bool secondtime = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && started == false)
        //{
        //    SceneManager.LoadScene("SampleScene");
        //    started = true;
        //}

    }
    public void startgame()
    { 
        SceneManager.LoadScene("SampleScene");
        started = true;
    }
    public void testgen()
    {
        SceneManager.LoadScene("Level gen");
        started = true;
    }
}