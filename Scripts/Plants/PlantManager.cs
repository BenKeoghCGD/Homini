using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    int bounds = 1000;
    public GameObject prefab = null, item_Wood = null;
    int spawnCount = 1000;
    public List<Tree> _trees = new List<Tree>();
    Vector3 spawnPosition = Vector3.zero;

    void Start()
    {
        StartCoroutine(FindObjectOfType<TickTimer>().beginTickTimer());

        for(int i = 0; i < spawnCount; i++)
        {
            /*spawnPosition = new Vector3(Random.Range(0f, bounds), 0f, Random.Range(0f, bounds));
            spawnPosition = new Vector3(spawnPosition.x, Terrain.activeTerrain.SampleHeight(spawnPosition), spawnPosition.z);

            GeneratePlant(spawnPosition, false);*/
            bool completed = false;
            while(!completed)
            {
                spawnPosition = new Vector3(Random.Range(0f, bounds), 0f, Random.Range(0f, bounds));
                if (Terrain.activeTerrain.SampleHeight(spawnPosition) > 15f) completed = true;
                spawnPosition = new Vector3(spawnPosition.x, Terrain.activeTerrain.SampleHeight(spawnPosition), spawnPosition.z);
            }

            GeneratePlant(spawnPosition, false);
        }
    }

    float f = 1f;
    public void OnTick()
    {
        f += Time.deltaTime * 0.1f;

        foreach(Tree t in _trees)
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

    public void GeneratePlant(Vector3 position, bool newTree = true)
    {
        Debug.Log("Logged");
        Tree element = new Tree();

        element.gameObject = Instantiate(prefab, position, new Quaternion());
        element.maxYield = (int)Random.Range(10f, 50f);
        element.maxScale = Random.Range(0.75f, 2f);
        element.growthMultiplier = Random.Range(1f, 5f);

        if (newTree) element.gameObject.transform.localScale = Vector3.one * 0.01f;
        else element.gameObject.transform.localScale *= (element.maxScale * 0.5f);

        _trees.Add(element);
    }

    public void HarvestTree(GameObject obj)
    {
        // based on StackOverflow example: https://stackoverflow.com/a/12676351
        int index = _trees.FindIndex(x => x.gameObject == obj);
        if (index >= 1)
        {
            // Tree Exists
            Tree t = _trees[index];
            int yield = (int)(t.maxYield * (t.gameObject.transform.localScale.x / t.maxScale));

            //for (int i = 0; i < yield; i++) Instantiate(item_Wood, t.gameObject.transform.position, new Quaternion());
            Debug.Log("Dropped " + yield + " wood. MAX: " + t.maxYield + ".");
            Destroy(t.gameObject);
            _trees.Remove(t);
        }
    }
}
