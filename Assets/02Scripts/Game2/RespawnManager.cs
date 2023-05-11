using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StageMob
{
    public List<GameObject> obs= new List<GameObject>();
}
public class RespawnManager : MonoBehaviour
{
    public List<StageMob>ObPool = new List<StageMob>();

    public int objCnt = 1;
    GameManager gm;
    void Awake()
    {
        gm = GameManager.instance;
        for(int i = 0; i <gm.stages.Length; i++) 
        {
            StageMob stage = new StageMob();
            for(int x = 0; x < gm.stages[i].mobs.Length; x++)
            {
                for(int q = 0; q < objCnt; q++)
                {
                    stage.obs.Add(CreatObj(gm.stages[i].mobs[x], transform));
                }
            }
            ObPool.Add(stage);
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
                for(int x = 0; x < ObPool[i].obs.Count; x++)
                {
                    if (ObPool[i].obs[x].activeSelf)
                        ObPool[i].obs[x].SetActive(false);
                }
                
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
            ObPool[gm.curStage].obs[DeactiveOb(ObPool[gm.curStage].obs)].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    int DeactiveOb(List<GameObject>obs)
    {
        List<int> num = new List<int>();
        for(int i = 0; i<obs.Count; i++) 
        {
            if (!obs[i].activeSelf)
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
