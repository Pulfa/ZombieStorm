using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalKills = 0;
    public int money = 0;
    public event Action OnScoreChanged; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddKill(int reward)
    {
        totalKills += reward;
        money += reward;
        OnScoreChanged?.Invoke();
    }
}
