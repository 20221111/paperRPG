using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagepoint;
    public int stageIndex;
    public int HP;
    public void NextStage()
    {
        stageIndex++;
        totalPoint += stagepoint;
        stagepoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
