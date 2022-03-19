using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject settingUI;
    public int totalPoint;
    public int stagepoint;
    public int stageIndex;

    private void Start()
    {

    }
    public void NextStage()
    {
        stageIndex++;
        totalPoint += stagepoint;
        stagepoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            settingUI.SetActive(true);
        }


    }
}