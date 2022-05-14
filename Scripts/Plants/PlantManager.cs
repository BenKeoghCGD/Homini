using System.Collections.Generic;
using UnityEngine;
using NMaterial;

public class PlantManager : MonoBehaviour
{
    int bounds = 1000;
    public GameObject[] prefabs;
    int spawnCount = 1000;
    int spawnCountSmall = 500;
    public List<STree> _trees = new List<STree>();
    Vector3 spawnPosition = Vector3.zero;

    void Start()
    {
        StartCoroutine(FindObjectOfType<TickTimer>().beginTickTimer());

        for(int i = 0; i < spawnCount; i++)
        {
            bool completed = false;
            while(!completed)
            {
                spawnPosition = new Vector3(Random.Range(0f, bounds), 0f, Random.Range(0f, bounds));
                if (Terrain.activeTerrain.SampleHeight(spawnPosition) > 15f) completed = true;
                spawnPosition = new Vector3(spawnPosition.x, Terrain.activeTerrain.SampleHeight(spawnPosition), spawnPosition.z);
            }

            GeneratePlant(spawnPosition, false);
        }
        for (int i = 0; i < spawnCountSmall; i++)
        {
            bool completed = false;
            while (!completed)
            {
                spawnPosition = new Vector3(Random.Range(0f, bounds), 0f, Random.Range(0f, bounds));
                if (Terrain.activeTerrain.SampleHeight(spawnPosition) > 15f) completed = true;
                spawnPosition = new Vector3(spawnPosition.x, Terrain.activeTerrain.SampleHeight(spawnPosition), spawnPosition.z);
            }

            GenerateSmall(spawnPosition, Random.Range(1, 10) <= 5);
        }
    }

    float f = 1f;
    public void OnTick()
    {
        f += Time.deltaTime * 0.1f;

        foreach(STree t in _trees)
        {
            if (t.gameObject.transform.localScale.x >= t.maxScale)
                continue;

            Vector3 newScale = t.gameObject.transform.localScale;
            newScale *= f;

            t.gameObject.transform.localScale = newScale;

            if (t.gameObject.transform.localScale.x >= 0.1f) t.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            else t.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void GenerateSmall(Vector3 position, bool stone = false)
    {
        GameObject obj = Instantiate(Mat.getIID(stone ? Materials.STONE : Materials.WOOD).prefab, position, new Quaternion());
        if (stone) obj.GetComponent<StoneItem>().Init(1);
        else obj.GetComponent<WoodItem>().Init(1);
    }

    public void GeneratePlant(Vector3 position, bool newTree = true)
    {
        STree element = new STree();

        element.gameObject = Instantiate(prefabs[Random.Range(0, prefabs.Length-1)], position, new Quaternion());
        element.maxYield = (int)Random.Range(10f, 50f);
        element.maxScale = Random.Range(0.75f, 2f);
        element.growthMultiplier = Random.Range(1f, 5f);

        if (newTree) element.gameObject.transform.localScale = Vector3.one * 0.01f;
        else element.gameObject.transform.localScale *= (element.maxScale * 0.5f);

        _trees.Add(element);

        element.gameObject.GetComponent<Tree>().Init(element);
    }

    public void HarvestTree(GameObject obj)
    {
        int index = _trees.FindIndex(x => x.gameObject == obj);
        if (index >= 1)
        {
            STree t = _trees[index];
            Destroy(t.gameObject);
            _trees.Remove(t);
        }
    }
}
