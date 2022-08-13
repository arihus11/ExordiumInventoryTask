using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    private int _oldRandom = 0;

    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Space))
        {
                int randomPrefab = ExsclusiveRandom(_oldRandom,ItemPrefabs.Length);
                _oldRandom = randomPrefab;

                float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
     
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                GameObject obj = Instantiate(ItemPrefabs[randomPrefab], spawnPosition, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find("Items").gameObject.transform);
        }
    }

    private int ExsclusiveRandom(int exc, int len)
    {
        int num =  Random.Range(0,len);
        while(num == exc)
        {
            num =  Random.Range(0,len);
        }
        return num;
    }
}
