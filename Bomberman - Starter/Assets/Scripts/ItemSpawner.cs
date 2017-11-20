using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    public void BoxDestroyed(Box box)
    {
        StartCoroutine(InstaniateRandomItem(box.transform.position));
    }

    private IEnumerator InstaniateRandomItem(Vector3 position)
    {
        int rInt = Random.Range(0, items.Length);

        yield return new WaitForSeconds(0.6f);

        Instantiate(items[rInt], new Vector3( Mathf.Round(position.x), 0.55f, Mathf.Round(position.z)), items[rInt].transform.rotation);
    }
}
