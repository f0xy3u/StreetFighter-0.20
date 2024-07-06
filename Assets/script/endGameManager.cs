using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class endGameManager : MonoBehaviour
{   
    public TMP_Text annoucmentPlace;
    int id;

    public void gameRepeat() {
        SceneManager.LoadScene("player1SetUp");
    }

    public void quitGame() {
        Application.Quit();
    }
    void Start() {
        id = PlayerPrefs.GetInt("idWon");

        switch(id) {
            case 1:
                annoucmentPlace.text = $"1. hrac ({PlayerPrefs.GetString("player1Name")}) vyhrál!";
                break;
            case 2:
                annoucmentPlace.text = $"2. hrac ({PlayerPrefs.GetString("player2Name")}) vyhrál!";
                break;
        }
    }
}
