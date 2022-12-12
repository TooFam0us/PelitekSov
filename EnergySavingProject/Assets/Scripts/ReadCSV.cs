using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    string fileName = Application.dataPath + "/StreamingAssets/EnergyPricesData.csv";
    // Start is called before the first frame update

    public IEnumerator ReadCSVFile()
    {
        StreamReader strReader = new StreamReader(fileName, Encoding.Default);
        bool eof = false;
        bool started = true;
        int day;
        int lastDay = 0;
        int dayIndex = 0;
        int hour;
        float price;
        while (!eof)
        {
            string dataString = strReader.ReadLine();
            if(dataString == null)
            {
                eof = true;
                break;
            }
            string[] dataValues = dataString.Split(';');
            if(dataValues[0] == "DateTime")
            {
                continue;
            }
            string[] dateValues = dataValues[0].Split('/');
            string[] timeValues = dataValues[0].Split(' ');
            string[] hourValues = timeValues[1].Split('.');
            bool success = int.TryParse(dateValues[0], out day);
            bool successTwo = int.TryParse(hourValues[0], out hour);
            bool successThree = float.TryParse(dataValues[1], out price);
            if(success && successTwo && successThree)
            {
                if(day > lastDay)
                {
                    lastDay = day;
                    if(!started)
                        dayIndex++;
                    started = false;

                }
                GameManager.Instance.AddDataToPriceTable(dayIndex, hour, price/10000);
            }
        }
        strReader.Close();
        yield return null;
    }
}
