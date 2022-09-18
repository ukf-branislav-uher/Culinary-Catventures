using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStun : CardEffectWithDebuff
{
    public override void Effect(GameManager gm, RaycastHit2D hit)
    {
        Customer target = hit.transform.GetComponent<CustomerView>().Customer;
        GiveFlavourful(gm, target);
    }

    public void GiveFlavourful(GameManager gm, Customer target)
    {
        GameObject temp = Instantiate(_buffable);
        Stun tempBuff = temp.GetComponent<Stun>();
        tempBuff.increaseTimer(_duration);
        gm.SetBuff(tempBuff, target);
    }
}
