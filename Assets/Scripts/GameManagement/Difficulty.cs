using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameController gameController;
    [SerializeField] private int difficulty = 1;
    void Start()
    {
        button.onClick.AddListener(SetDifficulty);
    }

    


    private void SetDifficulty()
    {
        gameController.StartGame(difficulty);
    }
}
