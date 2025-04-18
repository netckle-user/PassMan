using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 10ÃÊ¸¶´Ù ¾ç ¿·ÀÇ Æ÷Å»µéÀ» ¼ÅÇÃÇÑ´Ù.
/// </summary>
public class PortalManager : MonoBehaviour
{
    public List<GameObject> leftPortals = new List<GameObject>();
    public List<GameObject> rightPortals = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ShufflePortalOrder());
    }

    public IEnumerator ShufflePortalOrder()
    {
        ShuffleList(leftPortals);
        ShuffleList(rightPortals);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine(ShufflePortalOrder());
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; i++)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
}
