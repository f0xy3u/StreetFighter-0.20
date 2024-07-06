using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class player1Script : MonoBehaviour
{

    public TMP_InputField inputName;
    public TMP_InputField inputHp;
    public TMP_InputField inputVit;
    public Button confirmButton;
    public TMP_Text zdraviUpozorneni;
    public TMP_Text staminaUozorneni;
    public TMP_Text overLimitUpozorneni;
    public TMP_Text nameUpozorneni;

    public bool setUpDone = false;
    public void getPlayerName() {
        nameUpozorneni.text = "";
        if(inputName.text.Length <=10) {
            PlayerPrefs.SetString("player1Name", inputName.text);
        } else {
            nameUpozorneni.text = "Máš příliš dlouhé jméno! (max. 10 char.)";
        }
    }

    public void getPlayerHp() {
        zdraviUpozorneni.text = "";
        float.TryParse(inputHp.text, out float zivot);
        if(zivot <= 100 && zivot >= 20) {
            PlayerPrefs.SetFloat("player1Hp", zivot);
        } else if (string.IsNullOrEmpty(inputHp.text)) {
            PlayerPrefs.SetFloat("player1Hp", 0);
            zdraviUpozorneni.text = "";
        } else if (zivot > 100){
            PlayerPrefs.SetFloat("player1Hp", 0);
            zdraviUpozorneni.text = "Max. počet životů je 100!";
        } else {
            PlayerPrefs.SetFloat("player1Hp", 0);
            zdraviUpozorneni.text = "Min. počet životů je 20!";
        }
    }

    public void getPlayerVit() {
        staminaUozorneni.text = "";
        float.TryParse(inputVit.text, out float stamina);
        if (stamina <= 80) {
            PlayerPrefs.SetFloat("player1Vit", stamina);
        } else if(string.IsNullOrEmpty(inputVit.text)) {
            PlayerPrefs.SetFloat("player1Vit", 0);
            staminaUozorneni.text = "";
        } else if (stamina > 80){
            PlayerPrefs.SetFloat("player1Vit", 0);
            zdraviUpozorneni.text = "Max. počet staminy je 80!";
        } else {
            PlayerPrefs.SetFloat("player1Vit", 0);
            zdraviUpozorneni.text = "Min. počet staminy je 20!";
        }
}

    public void confirmSetUp() {
        nameUpozorneni.text = "";
        overLimitUpozorneni.text = "";
        if(PlayerPrefs.GetFloat("player1Hp") + PlayerPrefs.GetFloat("player1Vit") <= 150) {
            if(string.IsNullOrEmpty(PlayerPrefs.GetString("player1Name"))) {
                nameUpozorneni.text = "Napiš platné jméno!";
            } else if (PlayerPrefs.GetFloat("player1Hp") != 0 && PlayerPrefs.GetFloat("player1Hp") <= 100 && PlayerPrefs.GetFloat("player1Vit") != 0 && PlayerPrefs.GetFloat("player1Vit") <= 80) {
                setUpDone = true;
                SceneManager.LoadScene("player2SetUp");
            }
        } else {
            overLimitUpozorneni.text = "Překročil jsi limit staminy a životů!";
        }
    }
}
