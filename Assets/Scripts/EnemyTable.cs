using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;




public class EnemyTable : MonoBehaviour
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


        int Row = 0;
        int Column = 0;
        string[][] data = File.ReadLines(filePath).Where(line => line != "").Select(x => x.Split(',')).ToArray();
        foreach (string[] s in data)
        {

            foreach (string t in s)
            {
                Probabilities[Row].Add(float.Parse(t));
                //Debug.Log("row: " + Row + " col: " + Column + " Value: " + Probabilities[Row][Column]);
                Column++;
            }
            Column = 0;
            Row++;

        }

    }

    public String GetResponseCard(List<String> PossibleCards, Card playerCard)
    {
        //10 strings that represent each column for enemy card type 

        String ColumnZero = "AttackCard";
        String ColumnOne = "BuffAttack";
        String ColumnTwo = "BuffDefense";
        String ColumnThree = "BuffHealth";
        String ColumnFour = "DefenseDown";
        String ColumnFive = "Healing";
        String ColumnSix = "Poison";
        String ColumnSeven = "StrongAttack";
        String ColumnEight = "Stun";
        List<String> responseCards = new List<String>();
        responseCards.Add(ColumnZero);
        responseCards.Add(ColumnOne);
        responseCards.Add(ColumnTwo);
        responseCards.Add(ColumnThree);
        responseCards.Add(ColumnFour);
        responseCards.Add(ColumnFive);
        responseCards.Add(ColumnSix);
        responseCards.Add(ColumnSeven);
        responseCards.Add(ColumnEight);
        List<bool> validColumns = new List<bool>();
        bool MatchZero = false;
        bool MatchOne = false;
        bool MatchTwo = false;
        bool MatchThree = false;
        bool MatchFour = false;
        bool MatchFive = false;
        bool MatchSix = false;
        bool MatchSeven = false;
        bool MatchEight = false;
        float probToAdd = 0;

        int NumColumns = 9;
        int Row = 0;
        List<float> LocalProbabilities = Probabilities[Row];

        DebuffCard debuff = playerCard.transform.GetComponent<DebuffCard>();
        AttackCard attack = playerCard.transform.GetComponent<AttackCard>();
        ManaBuffCard manabuff = playerCard.transform.GetComponent<ManaBuffCard>();
        HealthBuffCard healthbuff = playerCard.transform.GetComponent<HealthBuffCard>();
        HealingCard healing = playerCard.transform.GetComponent<HealingCard>();
        DefenseBuffCard defensebuff = playerCard.transform.GetComponent<DefenseBuffCard>();

        //Determine which row we are working with
        if (debuff != null)
        {
            Row = 0;
        }

        if (attack != null)
        {
            Row = 1;
        }
        if (manabuff != null)

        {
            Row = 2;
        }
        if (healthbuff != null)

        {
            Row = 3;
        }
        if (healing != null)

        {
            Row = 4;
        }
        if (defensebuff != null)

        {
            Row = 5;
        }


        //Determine valid columns
        foreach (String s in PossibleCards)
        {
            if (s == ColumnZero)
            {
                MatchZero = true;
            }
            if (s == ColumnOne)
            {
                MatchOne = true;
            }
            if (s == ColumnTwo)
            {
                MatchTwo = true;
            }
            if (s == ColumnThree)
            {
                MatchThree = true;
            }
            if (s == ColumnFour)
            {
                MatchFour = true;
            }
            if (s == ColumnFive)
            {
                MatchFive = true;
            }
            if (s == ColumnSix)
            {
                MatchSix = true;
            }
            if (s == ColumnSeven)
            {
                MatchSeven = true;
            }
            if (s == ColumnEight)
            {
                MatchEight = true;
            }
        }

        validColumns.Add(MatchZero);
        validColumns.Add(MatchOne);
        validColumns.Add(MatchTwo);
        validColumns.Add(MatchThree);
        validColumns.Add(MatchFour);
        validColumns.Add(MatchFive);
        validColumns.Add(MatchSix);
        validColumns.Add(MatchSeven);
        validColumns.Add(MatchEight);

        //If the column is invalid, add its value to probToAdd
        if (!MatchZero)
        {
            probToAdd += LocalProbabilities[0];
            NumColumns--;
        }
        if (!MatchOne)
        {
            probToAdd += LocalProbabilities[1];
            NumColumns--;
        }
        if (!MatchTwo)
        {
            probToAdd += LocalProbabilities[2];
            NumColumns--;
        }
        if (!MatchThree)
        {
            probToAdd += LocalProbabilities[3];
            NumColumns--;
        }
        if (!MatchFour)
        {
            probToAdd += LocalProbabilities[4];
            NumColumns--;
        }
        if (!MatchFive)
        {
            probToAdd += LocalProbabilities[5];
            NumColumns--;
        }
        if (!MatchSix)
        {
            probToAdd += LocalProbabilities[6];
            NumColumns--;
        }
        if (!MatchSeven)
        {
            probToAdd += LocalProbabilities[7];
            NumColumns--;
        }
        if (!MatchEight)
        {
            probToAdd += LocalProbabilities[8];
            NumColumns--;
        }

        //Divide the probability to add by number of valid columns, NumColumns represents the # of valid columns at this point
        probToAdd /= NumColumns;

        //Add probabilities back to valid columns
        if (MatchZero)
        {
            LocalProbabilities[0] += probToAdd;
        }
        if (MatchOne)
        {
            LocalProbabilities[1] += probToAdd;
        }
        if (MatchTwo)
        {
            LocalProbabilities[2] += probToAdd;
        }
        if (MatchThree)
        {
            LocalProbabilities[3] += probToAdd;
        }
        if (MatchFour)
        {
            LocalProbabilities[4] += probToAdd;
        }
        if (MatchFive)
        {
            LocalProbabilities[5] += probToAdd;
        }
        if (MatchSix)
        {
            LocalProbabilities[6] += probToAdd;
        }
        if (MatchSeven)
        {
            LocalProbabilities[7] += probToAdd;
        }
        if (MatchEight)
        {
            LocalProbabilities[8] += probToAdd;
        }

        //Determine which card to play with what is left after filtering, based on a random number:
        float rand = UnityEngine.Random.Range(0, 1);
        float probabilitySum = 0;
        for (int i = 0; i < 9; i++)
        {
            if (validColumns[i])
            {
                probabilitySum += LocalProbabilities[i];
                if (rand <= probabilitySum)
                {
                    return responseCards[i];
                }
            }
        }

        return null;

    }
}






