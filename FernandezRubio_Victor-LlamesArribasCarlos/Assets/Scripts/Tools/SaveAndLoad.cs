using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveAndLoad {

    //Save the game (levels info(stars and lock), rubies and rayPowerUp) at a document in a path
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.txt");
        bf.Serialize(file, GameManager.gameManagerInstace.GetLevels());
        bf.Serialize(file, GameManager.gameManagerInstace.GetRuby());
        bf.Serialize(file, GameManager.gameManagerInstace.GetNRayPowerUp());
        file.Close();
    }

    //If exist a saved game, load the info saved before, and give them to the gameManager to update the info
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.txt", FileMode.Open);
            Level[] readLevel = (Level[])bf.Deserialize(file);
            int readRuby = (int)bf.Deserialize(file);
            int readRayPowerUp = (int)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < GameManager.gameManagerInstace.GetLevels().Length; i++)
            {
                GameManager.gameManagerInstace.GetLevels()[i] = readLevel[i];
            }

            GameManager.gameManagerInstace.SetRuby(readRuby);
            GameManager.gameManagerInstace.SetNRayPowerUp(readRayPowerUp);
        }
    }
}
