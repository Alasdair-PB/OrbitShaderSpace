using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    static GameData s_GameData;
    public static GameData gameData
    {
        get
        {
            if (s_GameData == null)
                s_GameData = FindObjectOfType<GameData>();
            return s_GameData;
        }
    }
    void Awake()
    {
        s_GameData = this;
    }
}


/*
 * My latest pattern is to make single instance game management stuff not a monobehaviour. I use a static class instead. Rather than use Awake() I use the class constructor for setup. That way I don't have to worry about scene references. (edited) 
 */