using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.Networking;

public class BackendHandler : MonoBehaviour
{

    // JSON test

    const string jsonTestStr = "{ " +
    "\"scores\":[ " +
    "{\"id\":1, \"playername\":\"Antti\", \"score\":999, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":2, \"playername\":\"Arttu\", \"score\":30, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":3, \"playername\":\"Aleksi\", \"score\":40, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":4, \"playername\":\"Jere\", \"score\":50, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":5, \"playername\":\"Otto\", \"score\":60, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":6, \"playername\":\"Nikolas\", \"score\":70, \"playtime\": \"2022-15-11 08:20:00\"}, " +
    "{\"id\":7, \"playername\":\"Benjamin\", \"score\":80, \"playtime\": \"2022-15-11 08:20:00\"} " +
    "] }";


    const string urlBackendHighScoresFile = "http://172.30.139.22/~narvanen/api/v1/highscores.json";
    const string urlBackendHighScores = "http://172.30.139.22/~narvanen/api/v1/highscores.php";





    HighScores.HighScores hs = null;




    string log = "";
    int fetchCounter = 0;
    bool updateHighScoreTextArea = false;


    // UI elements:

    public UnityEngine.UI.Text loggingText;
    public UnityEngine.UI.Text highScoresTextArea;

    public UnityEngine.UI.InputField playerNameInput;
    public UnityEngine.UI.InputField scoreInput;


    void Start()
    {
        Debug.Log("BackendHandler started");

        //hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
        //Debug.Log("HighScores name: " + hs.scores[0].playername);
        //Debug.Log("HighScores as json: " + JsonUtility.ToJson(hs));

        InsertToLog("Game started");
    }


    void Update()
    {
        loggingText.text = log;
        if (updateHighScoreTextArea)
        {
            highScoresTextArea.text = CreateHighScoreList();
            updateHighScoreTextArea = false;
        }
    }

    string CreateHighScoreList()
    {
        string hsList = "";

        if (hs != null)
        {
            int len = (hs.scores.Length < 10) ? hs.scores.Length : 10;
            for (int i = 0; i < len; i++)
            {
                //hsList += hs.scores[i].playername + ": \t" +
                //    hs.scores[i].score + " \t" + hs.scores[i].playtime + "\n";

                hsList += string.Format("[ {0} ] {1,15} {2,5} {3,15}\n",
                    (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
            }
        }

        return hsList;
    }


    // BUTTONEIDEN KƒSITTELYƒ:
    // GetJSONFile
    public void FetchHighScoresJSONFile()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSONFile button clicked");
        StartCoroutine(GetRequestForHighScores(urlBackendHighScoresFile));
    }
    public void FetchHighScoresJSON()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSON button clicked");

        StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }


    public void PostGameResults()
    {
        HighScores.HighScore highScore = new HighScores.HighScore();
        if (playerNameInput.text.Length > 0 && scoreInput.text.Length > 0)
        {
            highScore.playername = playerNameInput.text;
            highScore.score = float.Parse(scoreInput.text);

            playerNameInput.text = "";
            scoreInput.text = "";

            Debug.Log("PostGameResults called " + playerNameInput.text + " with scores: " + scoreInput.text);
            StartCoroutine(PostRequestForHighScores(urlBackendHighScores, highScore));
        }

    }

    //logi:

    string InsertToLog(string s)
    {
        return log = "[" + fetchCounter + "]  " + s + "\n" + log; // j‰tet‰‰n vanha logi yl‰puolelle ja uusi alapuolelle
    }
    string GetLog()
    {
        return log;
    }


    IEnumerator GetRequestForHighScores(string uri)
    {
      using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
      { 
        //UnityWebRequest webRequest = new UnityWebRequest(uri);
        InsertToLog("Request sent to " + uri);
        // set downloadHandler for json
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");

        // Request and wait for reply
        yield return webRequest.SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            InsertToLog("Error encountered: " + webRequest.error);
            Debug.Log("Error: " + webRequest.error);


        }
        else
        {
            // get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

            //Debug.Log(resultStr);
            // create HighScore item from json string
            hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);

            updateHighScoreTextArea = true;

            InsertToLog("Response received succesfully ");
            Debug.Log("Received(UTF8): " + resultStr);
            Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
        }
      }
    }

    IEnumerator PostRequestForHighScores(string uri, HighScores.HighScore hsItem)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, JsonUtility.ToJson(hsItem)))
        {
            InsertToLog("POST Request sent to " + uri);

            // set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");

            // Request and wait for reply
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                InsertToLog("Error encountered in POST request: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // get raw data and convert it to string
                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

                InsertToLog("POST Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr);

            }
        }
    }




}
