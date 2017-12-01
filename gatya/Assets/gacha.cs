using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class gacha : MonoBehaviour
{


    // 個数
    [SerializeField]
    int[] rare_probability;

    // 星５重み
    int[] _weightTable =
    {
        // ★５のかなりレア
        10000,
        // ★５のレア
        50000,
        // ★５よくでる
        100000
    };



    float baseNum = 65536;





    public int total_count = 10;



    // Use this for initialization
    void Start()
    {
        rare_probability = new int[]
        {
           GetProbability(5),
           GetProbability(15),
           GetProbability(80),
        };
        _weightTable = new int[]
        {
            // ★５のかなりレア
            10000,
            // ★５のレア
            50000,
            // ★５よくでる
            100000
        };
    }

    // Update is called once per frame
    void Update()
    {
        gasha();
    }


    void gasha()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            for (int i = 0; i < total_count; i++)
            {
                Debug.Log(total_count + "連！");
                if (DrawLot(rare_probability) == 0)
                {

                    int result = GetRandomIndex(_weightTable);
                    switch (result)
                    {
                        case 0:
                            Debug.Log("激レア！！！！！！");
                            break;
                        case 1:
                            Debug.Log("レア！！！");
                            break;
                        case 2:
                            Debug.Log("そこそこ");
                            break;
                        default:
                            Debug.Log("????????");
                            break;
                    }
                }
            }

        }
    }



    public int GetProbability(float percent)
    {
        float p = percent / 100.0f;
        float r = baseNum * p;
        return (int)r;
    }


    //渡された重み付け配列からIndexを得る
    public static int GetRandomIndex(params int[] weightTable)
    {
        int totalWeight = 0;
        for (int i = 0; i < weightTable.Length; i++)
        {
            totalWeight += weightTable[i];
        }
        var value = Random.Range(1, totalWeight + 1);
        var retIndex = -1;
        for (var i = 0; i < weightTable.Length; ++i)
        {
            if (weightTable[i] >= value)
            {
                retIndex = i;
                break;
            }
            value -= weightTable[i];
        }
        return retIndex;
    }


    public int DrawLot(int[] probabilityTable)
    {
        var r = Random.value;
        var r2 = r * baseNum;
        var rnd = Mathf.Round(r2);

        for (var i = 0; i < probabilityTable.Length; i++)
        {
            if (probabilityTable[i] >= rnd)
            {
                Debug.Log(i + "当選！");
                return i;
            }
            else
            {
                rnd -= probabilityTable[i];
                continue;
            }
        }
        return -1;
    }



}
