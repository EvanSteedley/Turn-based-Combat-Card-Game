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
    public static void Main()
    {

        string filePath = @"C:\Users\Farhana\Documents\GitHub\CS-4900---Team-6";
        StreamReader reader = new StreamReader(filePath);
        List<string> listA = new List<string>();


        //    string line;
        while ((reader.ReadLine()) != null)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            Console.WriteLine(line);
            foreach (var item in values)
            {
                listA.Add(item);
            }
            foreach (var column1 in listA)
            {
                Console.WriteLine(column1);
            }
        }
    }
}
       





