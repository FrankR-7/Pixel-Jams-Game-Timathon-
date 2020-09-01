using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.AI;

public class Generator : MonoBehaviour
{
    public GameObject start;
    public GameObject floor;
    public GameObject end;
    public GameObject wall;
    public GameObject grass;
    public GameObject door;
    public GameObject parent;
    public GameObject enemy1;
    public GameObject player;
    public GameObject chest;
    public GameObject str_potion;
    public GameObject invisib_potion;
    public GameObject heal_potion;
    public GameObject dewflask;
    public GameObject scrap;
    public GameObject key;

    public static float size = 2.5f;
    private int m, n;

    void Start()
    {
        m = 30; //+ (Player.level - 1) * 2;
        n = 30; //+ (Player.level - 1) * 2;

        Process p = Process.Start(Path.Combine(Application.streamingAssetsPath, "level-gen.exe"), m.ToString()+" "+n.ToString());
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;

        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        string error = p.StandardError.ReadToEnd();
        p.WaitForExit();
        int exitCode = p.ExitCode;

        Dictionary<string, List<List<int>>> dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<int>>>>(output);
        List<List<int>> map = dict["map"];
        GenerateMap(map);
    }

    private void GenerateMap(List<List<int>> sampleMap)
    {
        for (int i = 0; i < sampleMap.Count; i++)
        {
            for (int x = 0; x < sampleMap[i].Count; x++)
            {
                switch (sampleMap[i][x])
                {
                    case 0:
                        break;
                    case 1:
                        CreateWall(i, x);
                        break;
                    case 2:
                        CreateFloor(i, x);
                        break;
                    case 3:
                        CreateGrass(i, x);
                        break;
                    case 4:
                        CreateDoor(i, x);
                        break;
                    case 5:
                        CreateStart(i, x);
                        break;
                    case 6:
                        CreateEnd(i, x);
                        break;
                    case 7:
                        CreateFloor(i, x);
                        break;
                    case 8:
                        CreateFloor(i, x);
                        break;
                    case 9:
                        CreateGrass(i, x);
                        break;
                    case 10:
                        CreateFloor(i, x);
                        break;
                    case 11:
                        CreateFloor(i, x);
                        break;
                    case 12:
                        CreateFloor(i, x);
                        break;
                    case 13:
                        CreateGrass(i, x);
                        break;
                    case 14:
                        CreateFloor(i, x);
                        break;
                    default:
                        break;
                }
            }
        }
        GameObject.FindObjectOfType<NavMeshSurface>().GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void CreateWall(int i, int x)
    {
        for (int j = 1; j <= 3; ++j)
        {
            GameObject block = Instantiate(wall, new Vector3(x * size, j * size, i * size), Quaternion.identity);
            block.isStatic = true;
            block.transform.localScale = new Vector3(size, size, size);
            block.transform.SetParent(parent.transform);
        }
    }

    private void CreateFloor(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);
    }

    private void CreateGrass(int i, int x)
    {
        GameObject block = Instantiate(grass, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);
    }

    private void CreateDoor(int i, int x)
    {
        GameObject block1 = Instantiate(wall, new Vector3(x * size, 3*size, i * size), Quaternion.identity);
        block1.isStatic = true;
        block1.transform.localScale = new Vector3(size, size, size);
        block1.transform.SetParent(parent.transform);

        GameObject block2 = Instantiate(door, new Vector3(x * size, -size/2, i * size), Quaternion.identity);
        block2.transform.localScale = new Vector3(size, 2*size, size);
        block2.transform.SetParent(parent.transform);
    }

    private void CreateStart(int i, int x)
    {
        GameObject block = Instantiate(start, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        player.transform.position = block.transform.position + new Vector3(0,player.transform.localScale.y + 1,0);
    }

    private void CreateEnd(int i, int x)
    {
        GameObject block = Instantiate(end, new Vector3(x * size, size, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);
    }

    private void CreateChest(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(chest, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateScrap(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(scrap, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(7f, 7f, 7f);
        go.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateStrengthPotion(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(str_potion, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateHealPotion(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(heal_potion, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateInvisibilityPotion(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(invisib_potion, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateDewFlask(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(dewflask, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(7f, 7f, 7f);
        go.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        go.transform.SetParent(parent.transform);
    }

    private void CreateMob(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);
    }

    private void CreateChestKey(int i, int x)
    {
        GameObject block = Instantiate(floor, new Vector3(x * size, 0, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size, size);
        block.transform.SetParent(parent.transform);

        GameObject go = Instantiate(key, new Vector3(x * size, size, i * size), Quaternion.identity);
        go.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        go.transform.SetParent(parent.transform);
    }
}
