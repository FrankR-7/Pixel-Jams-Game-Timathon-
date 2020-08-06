using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] float size;
    public GameObject myPrefab;
    public GameObject parent;
    void Start()
    {
        List<List<int>> sampleMap = new List<List<int>>()
        {
            new List<int>(){0, 0, 1, 0, 0},
            new List<int>(){0, 0, 1, 0, 0},
            new List<int>(){0, 1, 1, 0, 0},
            new List<int>(){0, 1, 0, 0, 0},
            new List<int>(){0, 1, 0, 0, 0},
        };

        GenerateMap(sampleMap);
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
