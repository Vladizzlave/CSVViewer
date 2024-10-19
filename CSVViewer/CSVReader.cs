
using System;
using System.IO;

class CSVReader
{
    public static void ReadCSV(string filePath)
    {
        
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            
            while ((line = reader.ReadLine()) != null)
            {
                
                string[] values = line.Split(';'); 
                foreach (var value in values)
                {
                    Console.Write(value + "\t"); 
                }
                Console.WriteLine(); 
            }
        }
    }

    static void Pyth (string[] args)
    {
       
        string filePath = @"C:C:\Users\st53\Desktop\Csv.csv";
        ReadCSV(filePath); 
    }

}

