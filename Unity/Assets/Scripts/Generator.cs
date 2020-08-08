using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

public class Generator : MonoBehaviour
{
    public GameObject myPrefab;
    public GameObject parent;

    [SerializeField] float size;
    [SerializeField] int m, n;

    void Start()
    {
        System.Diagnostics.Process p = System.Diagnostics.Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Level Generation", "main.exe"), m.ToString()+" "+n.ToString());
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
                        CreateWall(i, x);
                        break;
                    case 1:
                        break;
                }
            }
        }
    }

    private void CreateWall(int i, int x)
    {
        GameObject block = Instantiate(myPrefab, new Vector3(x * size, size*2, i * size), Quaternion.identity);
        block.isStatic = true;
        block.transform.localScale = new Vector3(size, size*5, size);
        block.transform.SetParent(parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
