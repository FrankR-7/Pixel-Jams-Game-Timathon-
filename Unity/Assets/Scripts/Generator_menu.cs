using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.AI;

public class Generator_menu : MonoBehaviour
{
    public GameObject floor;
    public GameObject wall;
    public GameObject grass;
    public GameObject parent;

    public static float size = 2.5f;
    private int m = 40, n = 40;

    void Start()
    {
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
                    case 3:
                        CreateGrass(i, x);
                        break;
                    case 2:                      
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:    
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        CreateFloor(i, x);
                        break;
                }
            }
        }
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
}
