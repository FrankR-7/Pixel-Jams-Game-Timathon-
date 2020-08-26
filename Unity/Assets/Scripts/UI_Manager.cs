﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text level;
    public Text key;
    public Slider healthbar;
    public GameObject loading_screen;
    public Text loading;
    public Text tip;
    public GameObject inventorybar;
    public GameObject exitbar;

    public Sprite img_strengthpotion;
    public Sprite img_healthpotion;
    public Sprite img_invisibpotion;
    public Sprite img_dewflask;
    public Sprite img_scrap;
    public Sprite img_scroll;

    public GameObject itemcontainer;
    private List<GameObject> items = new List<GameObject>();

    private bool inExit;
    private bool inInv;

    // Start is called before the first frame update
    void Start()
    {
        inExit = false;
        inInv = false;
            
        level.text = Player.level.ToString();
        key.text = Player.keys.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = Player.health / Player.max_health;
    }

    public void ToggleInventory()
    {
        if (inventorybar.activeSelf)
        {
            inInv = false;
            inventorybar.SetActive(false);
        }
        else if (!inExit)
        {
            inInv = true;
            RefreshInv();
            inventorybar.SetActive(true);
        }
    }

    public void ToggleExit()
    {
        if (exitbar.activeSelf)
        {
            inExit = false;
            exitbar.SetActive(false);
        }
        else if (!inInv)
        {
            inExit = true;
            exitbar.SetActive(true);
        }
    }

    public void Exit()
    {
        loading.text = "Loading Menu...";
        System.Random rand = new System.Random();
        string[] tips = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "tips.txt"));
        tip.text = "Tip: " + tips[rand.Next(tips.Length)];
        loading_screen.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void Trigger_Loading()
    {
        loading.text = "Generating and Loading Level " + Player.level.ToString();
        System.Random rand = new System.Random();
        string[] tips = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "tips.txt"));
        tip.text = "Tip: " + tips[rand.Next(tips.Length)];
        loading_screen.SetActive(true);
    }

    public void RefreshInv()
    {
        foreach (GameObject go in items)
        {
            Destroy(go);
        }
        items = new List<GameObject>();

        int x = 0, y = 0;
        float size = 130f;
        foreach (KeyValuePair<Item.ItemType, int> kvp in Player.inv)
        {
            GameObject go = Instantiate(itemcontainer.transform, inventorybar.transform).gameObject;

            if (kvp.Key != Item.ItemType.Scrap)
                go.GetComponent<Button>().onClick.AddListener(delegate { Player.UseItem(kvp.Key); });

            switch (kvp.Key)
            {
                case Item.ItemType.StrengthPotion:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_strengthpotion;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 54.76f);
                    break;
                case Item.ItemType.HealPotion:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_healthpotion;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 54.76f);
                        break;
                case Item.ItemType.InvisibilityPotion:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_invisibpotion;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 54.76f);
                        break;
                case Item.ItemType.DewFlask:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_dewflask;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(17f, 53.5f);
                    break;
                case Item.ItemType.Scrap:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_scrap;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(21f, 24f);
                    break;
                case Item.ItemType.AttackScroll:
                    go.transform.GetChild(0).GetComponent<Image>().sprite = img_scroll;
                    go.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(44.85f, 55.6f);
                    break;
            }

            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(-193.2f + x * size, 28f + y * size / 1.5f);
            ++x;
            if (x==4)
            {
                x = 0;
                --y;
            }

            go.transform.GetChild(1).GetComponent<Text>().text = kvp.Value > 1 ? kvp.Value.ToString() : "";

            items.Add(go);
            go.SetActive(true);
        }
    }
}
