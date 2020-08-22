using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    public GameObject map;
    private int rot_speed = 5;

    public GameObject loading_screen;
    public Text loading;

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
        //initialise empty inventory here 

        Door.entities = new List<Transform>();
        Enemy1.player = null;
        CameraMovement.target = null;
        loading.text = "Generating and Loading Level " + Player.level.ToString();
        loading_screen.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
