using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    #region Variables
    // Variables to hold our players health, shield and maxShield which is based on the armor tier.
    public float health = 100, shield = 0f, currentMaxShield = 0f;
    public enum tierList
    {
        //Comments are segments /+ "health"
        None = 0,           // 0 - 0
        White = 1,          // 2 - 50
        Blue = 2,           // 3 - 75
        Purple = 3,         // 4 - 100
        Gold = 4            // 4 - 100
    }
    //We can use the same tierlist for both armor and helmet.
    public tierList equippedShield = tierList.None;
    public tierList equippedHelmet = tierList.None;

    //A list of pre-defined colors for our tiers. Element 0 would be nothing which we leave as white.
    [HideInInspector] public List<Color> tierColor = new List<Color> { new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 1f), new Color(0.09927912f, 0.6678277f, 0.9150943f, 1f), new Color(0.754717f, 0.09611962f, 0.7459197f, 1f), new Color(0.8490566f, 0.82941f, 0.07609467f, 1f) };

    //A variable containing our inventory.
    public Inventory inventory;

    //Couple of events we can subscribe to.
    public event EventHandler OnHealthChange;
    public event EventHandler OnArmorChange;
    public event EventHandler OnHelmetChange;
    #endregion

    // All our get functions are inside this region.
    #region Get Functions

    //Calculate and return the number of shield segments we should use.
    public int GetSegmentCount()
    {
        int ret;
        switch(equippedShield)
        {
            case tierList.None:
                ret = 0;
                break;
            case tierList.White:
                ret = 2;
                break;
            case tierList.Blue:
                ret = 3;
                break;
            case tierList.Purple:
                ret = 4;
                break;
            case tierList.Gold:
                ret = 4;
                break;
            default: 
                ret = 0;
                break;
        }

        return ret;
    }
    #endregion

    // All our void functions
    #region Void Functions

    //Class constructor always creates a new inventory to follow.
    public PlayerData() { inventory = new Inventory(); }

    /*
     * Take a float value as damage and calculate if the damage should affect both shield and health.
     * We also add a subscription event on which we subscribe to on the GameManager.
    */
    public void CalculateDamage(float dmg)
    {
        if (dmg < shield)
            this.shield -= dmg;
        else
        {
            health -= dmg - shield;
            shield = 0;
        }

        if (health <= 0)
            GameOver();

        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }

    /*
     * Take a float value as health and heals the player.
     * We also add a subscription event on which we subscribe to on the GameManager.
    */
    public void HealPlayer(float amount)
    {
        health += amount;
        if (health > 100f)
            health = 100f;

        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }

    /*
     * Take a float value as shield and add shield to the player.
     * We also add a subscription event on which we subscribe to on the GameManager.
    */
    public void AddShield(float amount)
    {
        if (shield + amount > currentMaxShield)
            shield = currentMaxShield;
        else
            shield += amount;

        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }

    /*
     * Here we can change the tier of the armor on the player.
     * We also add a subscription event on which we subscribe to on the GameManager.
    */
    public void ChangeArmor(tierList tier)
    {
        switch(tier)
        {
            case tierList.None:
                equippedShield = tierList.None;
                shield = 0f;
                currentMaxShield = 0f;
                break;
            case tierList.White:
                equippedShield = tierList.White;
                shield = 50f;
                currentMaxShield = 50f;
                break;
            case tierList.Blue:
                equippedShield = tierList.Blue;
                shield = 75f;
                currentMaxShield = 75f;
                break;
            case tierList.Purple:
                equippedShield = tierList.Purple;
                shield = 100f;
                currentMaxShield = 100f;
                break;
            case tierList.Gold:
                equippedShield = tierList.Gold;
                shield = 100f;
                currentMaxShield = 100f;
                break;
        }
        if (OnArmorChange != null) OnArmorChange(this, EventArgs.Empty);
    }

    /*
     * Here we can change the tier of the helmet on the player.
     * We also add a subscription event on which we subscribe to on the GameManager.
    */
    public void ChangeHelmet(tierList tier)
    {
        switch (tier)
        {
            case tierList.None:
                equippedHelmet = tierList.None;
                break;
            case tierList.White:
                equippedHelmet = tierList.White;
                break;
            case tierList.Blue:
                equippedHelmet = tierList.Blue;
                break;
            case tierList.Purple:
                equippedHelmet = tierList.Purple;
                break;
            case tierList.Gold:
                equippedHelmet = tierList.Gold;
                break;
        }

        if (OnHelmetChange != null) OnHelmetChange(this, EventArgs.Empty);
    }

    //Simple game over.
    public void GameOver()
    {
        health = 0f;
        Debug.LogWarning("Game Over!");
    }
    #endregion
}
