using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemUseController : MonoBehaviour
{
    float heal, shield;

    bool start;
    float time;
    float countDownTimer;

    [SerializeField] UIController UI;
    [SerializeField] Slider progressBar;
    [SerializeField] Image itemIcon;
    [SerializeField] Text progressText;
    [SerializeField] Text itemName;

    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void StartUsage(float _time = 0f, float _heal = 0f, float _shield = 0f, Sprite _itemIcon = null, string _itemName = "")
    {
        this.gameObject.SetActive(true);

        if(_shield > 0) UI.UpdateExpectationsShieldsToSegments(_shield);
        if(_heal > 0) UI.HealthExpectation(_heal);

        FixImageWidth(_itemIcon);
        _time = CalculateLegendaryEffect(_time);

        itemIcon.sprite = _itemIcon;
        itemName.text = _itemName;
        heal = _heal;
        shield = _shield;
        progressBar.maxValue = _time;
        countDownTimer = _time;
        start = true;
    }

    private void FixImageWidth(Sprite _itemIcon)
    {
        if (_itemIcon.name == "Phoenix")
            itemIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(25, itemIcon.GetComponent<RectTransform>().sizeDelta.y);
        else
            itemIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(50, itemIcon.GetComponent<RectTransform>().sizeDelta.y);
    }

    private float CalculateLegendaryEffect(float _time)
    {
        float pitchInc = .25f;

        GameManager.instance.gameObject.GetComponent<AudioSource>().pitch = 1f;

        //We do not care about speeding up the sound.
        float timeIncrease = 0f;
        if ((int)GameManager.instance.player.equippedShield > 3)
        {
            GameManager.instance.gameObject.GetComponent<AudioSource>().pitch += pitchInc + .1f;
            timeIncrease = _time * pitchInc;
            _time -= timeIncrease;
        }
        return _time;
    }

    void Update()
    {
        if(start)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameManager.instance.gameObject.GetComponent<AudioSource>().Stop();
                this.gameObject.SetActive(false);
            }

            countDownTimer -= Time.deltaTime;
            progressText.text = Math.Round(countDownTimer, 1).ToString();
            time += Time.deltaTime;
            progressBar.value = time;

            if(time > progressBar.maxValue)
            {
                UI.ConsumeItem(heal, shield);
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        heal = 0f;
        itemIcon.sprite = null;
        itemName.text = "";
        shield = 0f;
        progressBar.maxValue = 10;
        progressBar.value = 0;
        start = false;
        time = 0f;
        UI.HideExpectations();
    }
}
