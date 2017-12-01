using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ProbabilityTest : MonoBehaviour {

    class TestGachaData {
        [CsvColumn(0, 0)]
        public int Id { get; set; }

        [CsvColumn(1, "NoName")]
        public string Name { get; set; }

        [CsvColumn(2, 100)]
        public int Probability;

        public override string ToString()
        {
            return string.Format("Id={0}, Name={1}, Probability={2} %", Id, Name, Probability);
        }

    }

    public int totalCount = 0;


    // Use this for initialization
    void Start () {



        Run();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Run")]
    private void Run()
    {
        var reader = new CSVReader<TestGachaData>("CSV/gacha_table", true);
        var gachalist = reader.ToList();

        Dictionary<string, float> gachaPerDict = new Dictionary<string, float>();
        Dictionary<string, int> gachacount = new Dictionary<string, int>();

        for (var i = 0; i < gachalist.Count; i++)
        {
            gachaPerDict.Add(gachalist[i].Name, gachalist[i].Probability);
            gachacount.Add(gachalist[i].Name, 0);
        }

        Debug.Log(totalCount + "回");
        for (var i = 0; i < totalCount; i++)
        {
            gachacount[ProbabilityCalclator.DetermineFromDict(gachaPerDict)]++;
        }
        string logText = "抽選の精度確認\n試行回数 : " + totalCount.ToString() + "回\n";

        foreach (KeyValuePair<string, float> pair in gachaPerDict)
        {
            logText += pair.Key.ToString() + " 目標 : " + ((pair.Value/65536f)*100f).ToString() + "%, 実際 : "
              + ((gachacount[pair.Key]) / (float)totalCount * 100f).ToString() + "%\n";
        }

        Debug.Log(logText);
    }




}
