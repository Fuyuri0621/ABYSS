using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [Header("Drop Settings")]
    public GameObject[] dropItems; // 可掉落的物件陣列
    public int minDrops = 1; 
    public int maxDrops = 3; 
    public Vector2 dropRange = new Vector2(1f, 1f);

    void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            DropItems();
        }
    }

    void DropItems()
    {
        if (dropItems.Length == 0) return;

        int dropCount = Random.Range(minDrops, maxDrops + 1);
        for (int i = 0; i < dropCount; i++)
        {
            GameObject itemToDrop = dropItems[Random.Range(0, dropItems.Length)];
            Vector3 dropPosition = transform.position + new Vector3(
                Random.Range(-dropRange.x, dropRange.x),
                Random.Range(-dropRange.y, dropRange.y),
                0f
            );
            Instantiate(itemToDrop, dropPosition, Quaternion.identity);
        }
    }
}

