using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class mobs
{
    public string name;
    public int nextMove;
    public int HP;
    public int attack;
    public int Exp;
    public int Money;

    public mobs(string name, int nextMove, int hP, int attack, int exp, int money)
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
    public List<mobs> data = new List<mobs>();

    [ContextMenu("To json Data")]
    void save()
    {


        data.Add(new mobs("bunny", 0, 1584, 55, 14, 158));
        data.Add(new mobs("alligator", 0, 2112, 75, 34, 211));
        data.Add(new mobs("wolf", 0, 2640, 95, 86, 264));
        data.Add(new mobs("wisp", 0, 3168, 115, 213, 317));
        data.Add(new mobs("harpy", 0, 3696, 134, 530, 370));
        data.Add(new mobs("werewolf", 0, 8553, 154, 123, 855));
        data.Add(new mobs("Goblin", 0, 10137, 184, 307, 1014));
        data.Add(new mobs("griffin", 0, 11721, 213, 763, 1172));

        mobs mobdata = new mobs("bunny", 0, 1584, 55, 14, 158);

        string jsondata  = JsonUtility.ToJson(mobdata, true);
        print(jsondata);
        string path = Application.dataPath + "/mobs.json";
        File.WriteAllText(path, jsondata);
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
