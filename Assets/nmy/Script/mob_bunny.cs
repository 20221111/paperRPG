using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_bunny : Mobs
{
    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        name = "bunny";

        HP =100;
        attack = 100;
        Exp = 100;
        Money = 10;

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
