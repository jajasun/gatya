using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kizuna : MonoBehaviour {

    // ベースの数
    float baseNum = 65536;



    // 個数
    [SerializeField]
    int[] rare_probability;

    // 高確移行率（役によって移行率を変える）
    int[] _koukakuWeight;
    // BC当選率
    int[] _BCmakimono = {
        10000,
        30000
    };



    // ATループ率当選（25 = 75, 66 = 24, 80 = 0.1）
    int[] _ATroopWeight;

    
    // 回転数
    public int c = 10;
    // 投入枚数
    int inputcoin = 3;
    // 出玉
    int payout = 0;
    // 総出玉
    int totalpay = 0;
    // 差枚
    //int total = 0;

    // 今のコイン
    int nowCoin = 0;

    int okane = 0;
    // 出玉率
    float payout_rate = 0;


    int bc_osijun = 0;

    enum scene {
        tujo,
        bc,
        bt
    }
    scene s = scene.tujo;

    // Use this for initialization
    void Start () {
        rare_probability = new int[]
        {
            // 押し順
            36409,
            // リプレイ
            25206,
            // 弱チェ
            1652,
            // チャンスリプレイ
            981,
            // 共通ベル
            825,
            // 強チェ
            410,
            // チャンス目
            327,
            // プレミアムリプレイ
            1,
        };
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < c; i++)
            {

                switch (s)
                {
                    case scene.tujo:
                        tujo();
                        break;
                    case scene.bc:
                        baziliskChance();
                        break;
                    case scene.bt:
                        baziliskTime();
                        break;
                    default:
                        break;
                }
            }
            payout_rate = ((float)(c * inputcoin + totalpay - (Mathf.Abs(okane) / 20)) / (c * inputcoin)) * 100.0f;
            Debug.Log("出玉率：" + payout_rate + "%");
            Debug.Log(nowCoin + "枚");

        }
    }

    public int tujo()
    {
        if (nowCoin <= 2)
        {
            okane -= 1000;
            nowCoin += 50;
        }
        nowCoin -= inputcoin;
        Debug.Log(c + "回転(通常)");
        switch (DrawLot(rare_probability))
        {
            case 0:
                Debug.Log("押し順");
                payout = 0;
                break;
            case 1:
                Debug.Log("リプレイ");
                payout = 3;
                break;
            case 2:
                Debug.Log("弱チェ");
                payout = 2;
                break;
            case 3:
                Debug.Log("巻物");
                payout = 3;
                if(GetRandomIndex(_BCmakimono) == 0)
                {
                    Debug.Log("BC当選");
                    s = scene.bc;
                }
                break;
            case 4:
                Debug.Log("共通ベル");
                payout = 8;
                break;
            case 5:
                Debug.Log("強チェリー");
                payout = 2;
                break;
            case 6:
                Debug.Log("チャンス目");
                payout = 1;
                break;
            case 7:
                Debug.Log("プレミアムリプレイ");
                payout = 3;
                break;
            default:
                Debug.Log("外れ");
                payout = 0;
                break;
        }
        nowCoin += payout;
        totalpay += payout - inputcoin;

        return 0;
    }
    public int baziliskChance()
    {


        if (nowCoin <= 2)
        {
            okane -= 1000;
            nowCoin += 50;
        }
        nowCoin -= inputcoin;
        Debug.Log(c + "回転(BC)");
        switch (DrawLot(rare_probability))
        {
            case 0:
                Debug.Log("押し順");
                payout = 8;
                bc_osijun++;
                break;
            case 1:
                Debug.Log("リプレイ");
                payout = 3;
                break;
            case 2:
                Debug.Log("弱チェ");
                payout = 2;
                break;
            case 3:
                Debug.Log("巻物");
                payout = 3;
                break;
            case 4:
                Debug.Log("共通ベル");
                payout = 8;
                break;
            case 5:
                Debug.Log("強チェリー");
                payout = 2;
                break;
            case 6:
                Debug.Log("チャンス目");
                payout = 1;
                break;
            case 7:
                Debug.Log("プレミアムリプレイ");
                payout = 3;
                break;
            default:
                Debug.Log("外れ");
                payout = 0;
                break;
        }
        nowCoin += payout;
        totalpay += payout - inputcoin;
        if(bc_osijun > 8)
        {
            s = scene.tujo;
        }
    
        return 0;
    }
    public int baziliskTime()
    {
        if (nowCoin <= 2)
        {
            okane -= 1000;
            nowCoin += 50;
        }
        nowCoin -= inputcoin;
        Debug.Log(c + "回転(BT)");
        switch (DrawLot(rare_probability))
        {
            case 0:
                Debug.Log("押し順");
                payout = 8;
                break;
            case 1:
                Debug.Log("リプレイ");
                payout = 3;
                break;
            case 2:
                Debug.Log("弱チェ");
                payout = 2;
                break;
            case 3:
                Debug.Log("巻物");
                payout = 3;
                break;
            case 4:
                Debug.Log("共通ベル");
                payout = 8;
                break;
            case 5:
                Debug.Log("強チェリー");
                payout = 2;
                break;
            case 6:
                Debug.Log("チャンス目");
                payout = 1;
                break;
            case 7:
                Debug.Log("プレミアムリプレイ");
                payout = 3;
                break;
            default:
                Debug.Log("外れ");
                payout = 0;
                break;
        }
        nowCoin += payout;
        totalpay += payout - inputcoin;

        //payout_rate = ((float)25500/24000)*100.0f;
        //payout_rate = ((float)(c * inputcoin + totalpay - (Mathf.Abs(okane) / 20)) / (c * inputcoin)) * 100.0f;
        //payout_rate = ((float)(1000 * 3 + 0 - 100) / (1000 * 3)) * 100.0f;

        return 0;
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
                //Debug.Log(i + "当選！");
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
