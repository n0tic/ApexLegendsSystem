using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public Syringe syringe;
    public Medkit medkit;
    public ShieldCell shieldCell;
    public ShieldBattery shieldBattery;
    public Phoenixkit phoenixKit;

    public Inventory()
    {
        syringe = new Syringe();
        medkit = new Medkit();
        shieldCell = new ShieldCell();
        shieldBattery = new ShieldBattery();
        phoenixKit = new Phoenixkit();
    }
}

[System.Serializable]
public class Syringe
{
    public Sprite icon;
    public string itemName = "Syringe";
    public float consumeTime = 3.7f;
    public float healAmount = 25f;
}

[System.Serializable]
public class Medkit
{
    public Sprite icon;
    public string itemName = "Medkit";
    public float consumeTime = 6f;
    public float healAmount = 100f;
}

[System.Serializable]
public class ShieldCell
{
    public Sprite icon;
    public string itemName = "Shield Cell";
    public float consumeTime = 2.2f;
    public float shieldAmount = 25f;
}

[System.Serializable]
public class ShieldBattery
{
    public Sprite icon;
    public string itemName = "Shield Battery";
    public float consumeTime = 4f;
    public float shieldAmount = 100f;
}

[System.Serializable]
public class Phoenixkit
{
    public Sprite icon;
    public string itemName = "Phoenix Kit";
    public float consumeTime = 8f;
    public float healAmount = 100f;
    public float shieldAmount = 100f;
}