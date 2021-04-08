using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;



class EnemyTable : MonoBehaviour
{
    public List<float> DebuffProbabilities = new List<float>();
    public List<float> AttackProbabilities = new List<float>();
    public List<float> ManaBuffProbabilities = new List<float>();
    public List<float> HealthBuffProbabilities = new List<float>();
    public List<float> HealingProbabilities = new List<float>();
    public List<float> DefenseBuffProbabilities = new List<float>();

    public List<List<float>> Probabilities = new List<List<float>>();
    void Start()
    {
        Probabilities.Add(DebuffProbabilities);
        Probabilities.Add(AttackProbabilities);
        Probabilities.Add(ManaBuffProbabilities);
        Probabilities.Add(HealthBuffProbabilities);
        Probabilities.Add(HealingProbabilities);
        Probabilities.Add(DefenseBuffProbabilities);
        string filePath = Application.dataPath + "/Scripts/Cards/Enemy Cards/BayesianCardTable.csv";
        //@"C:\Users\Farhana\Documents\GitHub\CS-4900---Team-6\Assets\Scripts\EnemyCards\BayesianCardTable.csv";
        //StreamReader sr = new StreamReader(filePath);
        //string[][] data = File.ReadLines(filePath).Where(line => line != "").Select(x => x.Split(',')).ToArray();

        //var lines = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();

        //StreamReader sr = new StreamReader(filePath);
        //var lines = new List<string[]>();
        //int Row = 0;
        //while (!sr.EndOfStream)
        //{
        //    string[] Line = sr.ReadLine().Split(',');
        //    lines.Add(Line);
        //    Row++;
        //}

        int Row = 0;
        int Column = 0;
        string[][] data = File.ReadLines(filePath).Where(line => line != "").Select(x => x.Split(',')).ToArray();
        foreach (string[] s in data)
        {

            foreach (string t in s)
            {
                Probabilities[Row].Add(float.Parse(t));
                Debug.Log("row: " + Row + " col: " + Column + " Value: " + Probabilities[Row][Column]);
                Column++;
            }
            Column = 0;
            Row++;

        }



        //var data = lines.ToArray();





        //int Row = 0;
        //int Column = 0;
        //float[,] probs = new float[6,10];
        //foreach (string[] s in data)
        //{ 
        //    foreach (string t in s)
        //    {

        //       probs[Row, Column] = float.Parse("t");
        //       Debug.Log("this is t" + probs[Row, Column]);
        //        Column++;

        //    }
        //    Row++;
        //}

    }




}
       





