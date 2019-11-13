using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Image healthSlider, shieldSlider1, shieldSlider2, shieldSlider3, shieldSlider4;
    [SerializeField] Image expectHealthSlider, expectShieldSlider1, expectShieldSlider2, expectShieldSlider3, expectShieldSlider4;
    [SerializeField] GameObject armorHolder, helmetHolder;
    [SerializeField] GameObject itemUse;
    [SerializeField] List<AudioClip> clips;

    public void UpdateShieldSegmentsAndColor(int armor)
    {
        switch(armor)
        {
            case 0:
                armorHolder.SetActive(false);
                shieldSlider1.transform.parent.gameObject.SetActive(false);
                shieldSlider2.transform.parent.gameObject.SetActive(false);
                shieldSlider3.transform.parent.gameObject.SetActive(false);
                shieldSlider4.transform.parent.gameObject.SetActive(false);
                break;
            case 1:
                armorHolder.SetActive(true);
                armorHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[1];
                shieldSlider1.color = GameManager.instance.player.tierColor[1];
                shieldSlider2.color = GameManager.instance.player.tierColor[1];
                shieldSlider1.transform.parent.gameObject.SetActive(true);
                shieldSlider2.transform.parent.gameObject.SetActive(true);
                shieldSlider3.transform.parent.gameObject.SetActive(false);
                shieldSlider4.transform.parent.gameObject.SetActive(false);
                break;
            case 2:
                armorHolder.SetActive(true);
                armorHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[2];
                shieldSlider1.color = GameManager.instance.player.tierColor[2];
                shieldSlider2.color = GameManager.instance.player.tierColor[2];
                shieldSlider3.color = GameManager.instance.player.tierColor[2];
                shieldSlider1.transform.parent.gameObject.SetActive(true);
                shieldSlider2.transform.parent.gameObject.SetActive(true);
                shieldSlider3.transform.parent.gameObject.SetActive(true);
                shieldSlider4.transform.parent.gameObject.SetActive(false);
                break;
            case 3:
                armorHolder.SetActive(true);
                armorHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[3];
                shieldSlider1.color = GameManager.instance.player.tierColor[3];
                shieldSlider2.color = GameManager.instance.player.tierColor[3];
                shieldSlider3.color = GameManager.instance.player.tierColor[3];
                shieldSlider4.color = GameManager.instance.player.tierColor[3];
                shieldSlider1.transform.parent.gameObject.SetActive(true);
                shieldSlider2.transform.parent.gameObject.SetActive(true);
                shieldSlider3.transform.parent.gameObject.SetActive(true);
                shieldSlider4.transform.parent.gameObject.SetActive(true);
                break;
            case 4:
                armorHolder.SetActive(true);
                armorHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[4];
                shieldSlider1.color = GameManager.instance.player.tierColor[4];
                shieldSlider2.color = GameManager.instance.player.tierColor[4];
                shieldSlider3.color = GameManager.instance.player.tierColor[4];
                shieldSlider4.color = GameManager.instance.player.tierColor[4];
                shieldSlider1.transform.parent.gameObject.SetActive(true);
                shieldSlider2.transform.parent.gameObject.SetActive(true);
                shieldSlider3.transform.parent.gameObject.SetActive(true);
                shieldSlider4.transform.parent.gameObject.SetActive(true);
                break;
        }
    }

    public void ConsumeItem(float _heal, float _shield)
    {
        if(_heal > 0f) GameManager.instance.player.HealPlayer(_heal);
        if (_shield > 0f) GameManager.instance.player.AddShield(_shield);
    }

    public void HealthExpectation (float _health)
    {
        float health = (GameManager.instance.player.health + _health) / 100;
        expectHealthSlider.fillAmount = health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            itemUse.SetActive(!itemUse.activeSelf);
    }

    public void UpdateHelmetAndColor(int helmet)
    {
        switch (helmet)
        {
            case 0:
                helmetHolder.SetActive(false);
                break;
            case 1:
                helmetHolder.SetActive(true);
                helmetHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[1];
                break;
            case 2:
                helmetHolder.SetActive(true);
                helmetHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[2];
                break;
            case 3:
                helmetHolder.SetActive(true);
                helmetHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[3];
                break;
            case 4:
                helmetHolder.SetActive(true);
                helmetHolder.GetComponent<Image>().color = GameManager.instance.player.tierColor[4];
                break;
        }
    }

    public void UpdateShieldsToSegments(PlayerData player)
    {
        for (int i = 0; i < player.GetSegmentCount(); i++)
        {
            int shieldSegmentsMin = i * 25;
            int shieldSegmentsMax = (i + 1) * 25;

            if (player.shield <= shieldSegmentsMin)
                ResolveShield(i).fillAmount = 0f;
            else
            {
                if (player.shield >= shieldSegmentsMax)
                    ResolveShield(i).fillAmount = 1f;
                else
                {
                    float fillAmount = (float)(player.shield - shieldSegmentsMin) / 25;
                    ResolveShield(i).fillAmount = fillAmount;
                }
            }
        }
    }

    public void UpdateExpectationsShieldsToSegments(float _shield)
    {
        float shield = GameManager.instance.player.shield + _shield;

        for (int i = 0; i < GameManager.instance.player.GetSegmentCount(); i++)
        {
            int shieldSegmentsMin = (i) * 25;
            int shieldSegmentsMax = (i + 1) * 25;

            if (shield <= shieldSegmentsMin)
                ResolveExpectationShield(i).fillAmount = 0f;
            else
            {
                if (shield >= shieldSegmentsMax)
                    ResolveExpectationShield(i).fillAmount = 1f;
                else
                {
                    float fillAmount = (float)(shield - shieldSegmentsMin) / 25;
                    ResolveExpectationShield(i).fillAmount = fillAmount;
                }
            }
        }
    }

    Image ResolveShield(int slider)
    {
        switch(slider)
        {
            case 0:
                return shieldSlider1;
            case 1:
                return shieldSlider2;
            case 2:
                return shieldSlider3;
            case 3:
                return shieldSlider4;
        }
        return null;
    }

    Image ResolveExpectationShield(int slider)
    {
        switch (slider)
        {
            case 0:
                return expectShieldSlider1;
            case 1:
                return expectShieldSlider2;
            case 2:
                return expectShieldSlider3;
            case 3:
                return expectShieldSlider4;
        }
        return null;
    }

    public void FirstSetup(PlayerData player)
    {
        GetComponent<AudioSource>().PlayOneShot(clips[6]);
        healthSlider.fillAmount = player.health / 100;
        UpdateShieldSegmentsAndColor((int)player.equippedShield);
        UpdateHelmetAndColor((int)player.equippedHelmet);
        UpdateShieldsToSegments(player);
    }

    public void HideExpectations()
    {
        expectHealthSlider.fillAmount = 0f;
        expectShieldSlider1.fillAmount = 0f;
        expectShieldSlider2.fillAmount = 0f;
        expectShieldSlider3.fillAmount = 0f;
        expectShieldSlider4.fillAmount = 0f;
    }

    public void UpdatePlayerHealth()
    {
        healthSlider.fillAmount = GameManager.instance.player.health / 100;
    }

    private PlayerData.tierList ResolveTier(int tier)
    {
        switch (tier)
        {
            case 0:
                return PlayerData.tierList.None;
            case 1:
                return PlayerData.tierList.White;
            case 2:
                return PlayerData.tierList.Blue;
            case 3:
                return PlayerData.tierList.Purple;
            case 4:
                return PlayerData.tierList.Gold;
        }
        return PlayerData.tierList.None;
    }

    public void ButtonSyringe() // (25 health, 5 seconds)
    {
        PlayerData player = GameManager.instance.player;
        Inventory inventory = player.inventory;
        if (player.health < 100)
        {
            itemUse.GetComponent<UIItemUseController>().StartUsage(inventory.syringe.consumeTime, inventory.syringe.healAmount, 0, inventory.syringe.icon, inventory.syringe.itemName);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clips[1]);
        }
    }

    public void ButtonMedkit() // (100 health, 8 seconds)
    {
        PlayerData player = GameManager.instance.player;
        Inventory inventory = player.inventory;
        if (player.health < 100)
        {
            itemUse.GetComponent<UIItemUseController>().StartUsage(inventory.medkit.consumeTime, inventory.medkit.healAmount, 0, inventory.medkit.icon, inventory.medkit.itemName);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clips[2]);
        }
    }

    public void ButtonShieldCell() // (25 shields, 3 seconds)
    {
        PlayerData player = GameManager.instance.player;
        Inventory inventory = player.inventory;
        if (player.shield < player.currentMaxShield)
        {
            itemUse.GetComponent<UIItemUseController>().StartUsage(inventory.shieldCell.consumeTime, 0, inventory.shieldCell.shieldAmount, inventory.shieldCell.icon, inventory.shieldCell.itemName);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clips[3]);
        }
    }

    public void ButtonShieldBattery() // (100 shields, 5 seconds)
    {
        PlayerData player = GameManager.instance.player;
        Inventory inventory = player.inventory;
        if (player.shield < player.currentMaxShield)
        {
            itemUse.GetComponent<UIItemUseController>().StartUsage(inventory.shieldBattery.consumeTime, 0, inventory.shieldBattery.shieldAmount, inventory.shieldBattery.icon, inventory.shieldBattery.itemName);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clips[4]);
        }
    }

    public void ButtonPhoenix() // (100 shields and 100 health, 10 seconds)
    {
        PlayerData player = GameManager.instance.player;
        Inventory inventory = player.inventory;
        if (player.shield < player.currentMaxShield || player.health < 100)
        {
            itemUse.GetComponent<UIItemUseController>().StartUsage(inventory.phoenixKit.consumeTime, inventory.phoenixKit.healAmount, inventory.phoenixKit.shieldAmount, inventory.phoenixKit.icon, inventory.phoenixKit.itemName);
            gameObject.GetComponent<AudioSource>().PlayOneShot(clips[5]);
        }
    }

    public void ButtonDamage(float dmg)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(clips[0]);
        GameManager.instance.player.CalculateDamage(dmg);
    }

    public void ButtonChangeArmor(int tier)
    {
        GameManager.instance.player.ChangeArmor(ResolveTier(tier));
    }

    public void ButtonChangeHelmet(int tier)
    {
        GameManager.instance.player.ChangeHelmet(ResolveTier(tier));
    }
}
