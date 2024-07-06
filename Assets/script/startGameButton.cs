using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameButton : MonoBehaviour
{
    public void gameStartButton() {
        SceneManager.LoadScene("vysvetlivky");
    }
}