using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class mobData
{
    public string name;
    public int nextMove;
    public int HP;
    public int attack;
    public int Exp;
    public int Money;

    public mobData(string name, int nextMove, int hP, int attack, int exp, int money)
    {
        this.name = name;
        this.nextMove = nextMove;
        HP = hP;
        this.attack = attack;
        Exp = exp;
        Money = money;
    }
}

public class make_mob : MonoBehaviour
{
    [SerializeField]
    [ContextMenu("To json Data")]
    void ToJson()
    {

        save(new mobData("mobs", 0, 100, 100, 10, 100));
        save(new mobData("bunny", 0, 1584, 55, 14, 158));
        save(new mobData("alligator", 0, 2112, 75, 34, 211));
        save(new mobData("wolf", 0, 2640, 95, 86, 264));
        save(new mobData("wisp", 0, 3168, 115, 213, 317));
        save(new mobData("harpy", 0, 3696, 134, 530, 370));
        save(new mobData("werewolf", 0, 8553, 154, 123, 855));
        save(new mobData("Goblin", 0, 10137, 184, 307, 1014));
        save(new mobData("griffin", 0, 11721, 213, 763, 1172));
    }

    public void save(mobData data)
    {
        //Json파일로 변환
        string jsondata = JsonUtility.ToJson(data);

        //저장 경로지정
        string sPath = string.Format("/nmy/{0}.json", data.name);
        System.IO.FileInfo file = new System.IO.FileInfo(sPath);
        file.Directory.Create();
        File.WriteAllText(file.FullName, jsondata);
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
