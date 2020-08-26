using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    public GameObject map;
    private int rot_speed = 5;

    public GameObject loading_screen;
    public Text loading;
    public Text tip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        map.transform.Rotate(0, rot_speed * Time.deltaTime, 0);
    }

    public void Play()
    {
        Player.level = 1;
        Player.max_health = 100;
        Player.health = Player.max_health;
        Player.attack = 50;
        Player.keys = 0;
        Player.inv = new Dictionary<Item.ItemType, int>();
        Player.isInvisible = false;
        Player.nextNotInvisible = 0f;
        Player.isDewed = false;
        Player.nextNotDewed = 0;

        Door.entities = new List<Transform>();
        Enemy1.player = null;
        CameraMovement.target = null;

        loading.text = "Generating and Loading Level " + Player.level.ToString();
        System.Random rand = new System.Random();
        string[] tips = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "tips.txt"));
        tip.text = "Tip: " + tips[rand.Next(tips.Length)];
        loading_screen.SetActive(true);

        SceneManager.LoadScene(1);
    }
}
