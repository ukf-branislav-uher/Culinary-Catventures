using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customers/Customer")]
public class CustomerData : ScriptableObject
{
    public string Name;
    public int MaxHunger;
    public int TurnsLeft;
    public int Turns;
    public int Money;
    public int Reputation;
    public List<Sprite> Sprites;
}
