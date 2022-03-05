using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //인트로에서 게임 씬으로 넘어감
    public void IntroTOGame()
    {
        SceneManager.LoadScene(1);
    }
}
