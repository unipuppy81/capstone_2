using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGroundScrollor : MonoBehaviour
{
    public SpriteRenderer[] tiles; // 바닥 오브젝트 배열

    public float speed;
    GameManager gm;
    void Start()
    {
        gm = GameManager.instance;
        tmp = tiles[0];
    }
    SpriteRenderer tmp;

    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (-4.6 >= tiles[i].transform.position.x)
                {
                    for (int j = 0; j < tiles.Length; j++)
                    {
                        if (tmp.transform.position.x < tiles[j].transform.position.x)
                            tmp = tiles[j];
                    }
                    tiles[i].transform.position = new Vector2(tmp.transform.position.x + 4.1f, 0.5f);
                    tiles[i].sprite = gm.stages[gm.curStage].grounds[Random.Range(0, gm.stages[gm.curStage].grounds.Length)];
                }
            }
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * GameManager.instance.gameSpeed);
            }
        }
    }
}
