using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text level;
    public Slider healthbar;
    public GameObject loading_screen;
    public Text loading;

    // Start is called before the first frame update
    void Start()
    {
        level.text = Player.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = Player.health / Player.max_health;
    }

    public void Trigger_Loading()
    {
        loading.text = "Generating and Loading Level " + Player.level.ToString();
        loading_screen.SetActive(true);
    }
}
