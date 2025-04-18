using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;

public class BoxSpawner : MonoBehaviour 
{
    public static BoxSpawner instance;

    private ObjectPool<Box> boxes;

    public GameObject prefab;
    public Transform spawnPos;

    public int initCount = 3;
    public int maxCount = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);            

        boxes = new ObjectPool<Box>(CreateBox, ActivateBox, DisableBox, DestroyBox, false, initCount, maxCount);
        GetBox();
    }

    private Box CreateBox()
    {
        return Instantiate(prefab, spawnPos.position, Quaternion.identity).GetComponent<Box>();
    }

    private void ActivateBox(Box box)
    {
        box.gameObject.SetActive(true);
        box.transform.position = spawnPos.position;
    }

    private void DisableBox(Box box)
    {
        box.gameObject.SetActive(false);
        box.transform.position = Vector3.zero;
    }

    private void DestroyBox(Box box)
    {
        Destroy(box.gameObject);
    }

    public Box GetBox()
    {
        Box box = null;
        if (boxes.CountInactive >= maxCount)
        {
            box = CreateBox();
            box.tag = "OverBox";
        }
        else
        {
            box = boxes.Get();
        }
        return box;
    }

    public void ReleaseBox(Box box)
    {
        if (box.CompareTag("OverBox"))
            Destroy(box);
        else
            boxes.Release(box);
    }
}
