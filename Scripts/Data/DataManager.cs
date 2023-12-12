using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private const string xorKey = "YourXORKey";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    private string XORCipher(string input)
    {
        char[] inputArray = input.ToCharArray();
        char[] keyArray = xorKey.ToCharArray();
        int keyLength = keyArray.Length;

        for (int i = 0; i < inputArray.Length; i++)
        {
            inputArray[i] = (char)(inputArray[i] ^ keyArray[i % keyLength]);
        }

        return new string(inputArray);
    }

    public void SaveData()
    { 
        string data = JsonUtility.ToJson(nowPlayer);
        string encryptedData = XORCipher(data);
        File.WriteAllText(path + nowSlot.ToString(), encryptedData);
    }

    public void LoadData()
    {
        string encryptedData = File.ReadAllText(path + nowSlot.ToString());
        string decryptedData = XORCipher(encryptedData);
        nowPlayer = JsonUtility.FromJson<PlayerData>(decryptedData);
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    public void DeleteData()
    {
        string filePath = path + nowSlot.ToString();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Data in slot " + nowSlot + " deleted.");
            nowSlot = -1;
            nowPlayer = new PlayerData();
        }
    }

}
