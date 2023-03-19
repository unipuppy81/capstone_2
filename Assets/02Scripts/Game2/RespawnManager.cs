using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject>ObPool = new List<GameObject>();
    public GameObject[] Obs;
    public int objCnt = 1;

    void Awake()
    {
        for(int i = 0; i < Obs.Length; i++) 
        {
            for(int j = 0; j < objCnt; j++)
            {
                ObPool.Add(CreatObj(Obs[i], transform));
            }
        }
    }
    private void Start()
    {
        GameManager.instance.onPlay += PlayGame;
    }
    void PlayGame(bool isplay)
    {
        if (isplay) 
        {
            for(int i = 0; i<ObPool.Count; i++)
            {
                if (ObPool[i].activeSelf)
                    ObPool[i].SetActive(false);
            }
            StartCoroutine(CreateOb());
        }
        else
        {
            StopAllCoroutines();
        }
    }
    IEnumerator CreateOb()
    {
        yield return new WaitForSeconds(0.5f);
        while(GameManager.instance.isPlay)
        {
            ObPool[DeactiveOb()].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    int DeactiveOb()
    {
        List<int> num = new List<int>();
        for(int i = 0; i<ObPool.Count; i++) 
        {
            if (!ObPool[i].activeSelf)
            {
                num.Add(i);
            }
        }
        int x = 0;
        if(num.Count > 0) 
        {
            x = num[Random.Range(0,num.Count)];
        }
        return x;
    }

    GameObject CreatObj(GameObject obj, Transform parent) //장애물 생성
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
