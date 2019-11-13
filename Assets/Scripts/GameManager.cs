using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Make this available publicly from all scripts. (Singleton)
    public static GameManager instance = null;

    UIController UI; // A reference to the UI-Controller
    public PlayerData player; // Our player-data holder.

    void Awake()
    {
        //This is where we check for multiple instances or create an instance.
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject); // If this is a new instance, dont destroy this object if we load in to another scene.

        //Create a new playerdata object with subscriptions to its methods.
        player = new PlayerData();
        player.OnHealthChange += player_OnHealthChange;
        player.OnArmorChange += player_OnArmorChange;
        player.OnHelmetChange += player_OnHelmetChange;

        SetupItemIcons(player.inventory);

        //Get a reference to the UI-Controller and do a initial setup of player-data.
        UI = GetComponent<UIController>();
        UI.FirstSetup(player);
    }

    private void SetupItemIcons(Inventory inventory)
    {
        inventory.syringe.icon = Resources.Load("Syringe", typeof(Sprite)) as Sprite;
        inventory.medkit.icon = Resources.Load("Medkit", typeof(Sprite)) as Sprite;
        inventory.shieldCell.icon = Resources.Load("Shield_Cell", typeof(Sprite)) as Sprite;
        inventory.shieldBattery.icon = Resources.Load("Shield_Battery", typeof(Sprite)) as Sprite;
        inventory.phoenixKit.icon = Resources.Load("Phoenix", typeof(Sprite)) as Sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        //UI.SetHealth(player.health);
        //UI.SetShield((int)player.eShield);
        //UI.UpdateShieldAmount(player.shield);
        //UI.SetHelmet((int)player.eHelmet);
    }

    void player_OnHealthChange(object sender, EventArgs e)
    {
        UI.UpdatePlayerHealth();
        UI.UpdateShieldsToSegments(player);
    }
    private void player_OnHelmetChange(object sender, EventArgs e)
    {
        UI.UpdateHelmetAndColor((int)player.equippedHelmet);
    }

    private void player_OnArmorChange(object sender, EventArgs e)
    {
        UI.UpdateShieldSegmentsAndColor((int)player.equippedShield);
        UI.UpdateShieldsToSegments(player);
    }

    void Update()
    {
        
    }
}
