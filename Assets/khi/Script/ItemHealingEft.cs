using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healing = 0;
    // Start is called before the first frame update
    public override bool ExcuteRole()
    {
        Debug.Log("PlayerHP ADD: " + healing);
        return true;
    }
}
