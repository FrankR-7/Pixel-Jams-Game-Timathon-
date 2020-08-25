using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text level;
    public Text key;
    public Slider healthbar;
    public GameObject loading_screen;
    public Text loading;
    public Text tip;

    // Start is called before the first frame update
    void Start()
    {
        level.text = Player.level.ToString();
        key.text = Player.keys.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = Player.health / Player.max_health;
    }

    public void Trigger_Loading()
    {
        loading.text = "Generating and Loading Level " + Player.level.ToString();
        System.Random rand = new System.Random();
        string[] tips = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "tips.txt"));
        tip.text = "Tip: " + tips[rand.Next(tips.Length)];
        loading_screen.SetActive(true);
    }
}
