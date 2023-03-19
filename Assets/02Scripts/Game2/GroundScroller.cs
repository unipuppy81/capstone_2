using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public SpriteRenderer[] tiles; // 바닥 오브젝트 배열
    public Sprite[] groundImg;

    public float speed;
    void Start()
    {
        tmp = tiles[0];
    }
    SpriteRenderer tmp;

    void Update()
    {
        if(GameManager.instance.isPlay)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (-3 >= tiles[i].transform.position.x)
                {
                    for (int j = 0; j < tiles.Length; j++)
                    {
                        if (tmp.transform.position.x < tiles[j].transform.position.x)
                            tmp = tiles[j];
                    }
                    tiles[i].transform.position = new Vector2(tmp.transform.position.x + 1, -1.5f);
                    tiles[i].sprite = groundImg[Random.Range(0, groundImg.Length)];
                }
            }
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * GameManager.instance.gameSpeed);
            }
        }
    }
}
