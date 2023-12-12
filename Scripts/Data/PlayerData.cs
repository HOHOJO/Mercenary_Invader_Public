using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public float hp = 500;
    public float attack = 10;
    public float def = 10;
    public float sp = 20;
    public int coin = 100;

    public List<bool> levelUnlocked = new List<bool> { true, false, false };
}
