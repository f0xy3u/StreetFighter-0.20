using System;
using System.Data.Common;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// >>>Ondro, až bude vše funkční, pokusím se kód usušit, ale chci, aby první vše fungovalo.<<<
// >>>Jestli by se k tobě dostala tato verze, promiň, že ti způsobím bolest tímto kódem. ;D<<<

public class gameScript : MonoBehaviour
{   
//Setup proměnných
    public TMP_Text p1NamePlaceholder;
    public TMP_Text p2NamePlaceholder;
    public TMP_Text whoPlay;
    public Slider p1HealthStatus;
    public TMP_Text p1HpSatusNum;
    public Slider p2HealthStatus;
    public TMP_Text p2HpSatusNum;
    public Slider p1VitStatus;
    public TMP_Text p1VitSatusNum;
    public Slider p2VitStatus;
    public TMP_Text p2VitSatusNum;

    public Image p1AttackForm;
    public Image p1OziveniForm;
    public Image p2AttackForm;
    public Image p2OziveniForm;
    public TMP_InputField p1AttackInput;
    public TMP_InputField p1OziveniInput;
    public TMP_InputField p2AttackInput;
    public TMP_InputField p2OziveniInput;
    public TMP_Text p1AttackUpozorneni;
    public TMP_Text p1OziveniUpozorneni;
    public TMP_Text p2AttackUpozorneni;
    public TMP_Text p2OziveniUpozorneni;
    public Button p1AttackButton;
    public Button p1OziveniButton;
    public Button p1SkipButton;
    public Button p2AttackButton;
    public Button p2OziveniButton;
    public Button p2SkipButton;
    public TMP_Text p1ReviveAmnout;
    public TMP_Text p2ReviveAmnout;


    public string player1Name;
    public string player2Name;
    public float player1Hp;
    public float player2Hp;
    public float player1Vit;
    public float player2Vit;

    int p1ReviveAbility = 5;
    int p2ReviveAbility = 5;

    bool p1Skipped = false;
    bool p2Skipped = false;
    bool p1Played = false;
    bool p2Played = false;
    bool roundComplete = false;

    //Attack and revive
    public void attack(int id, int dmg) {
        Debug.Log(dmg);
        switch(id) {
            case 1:
                player2Hp -= math.ceil(dmg * (player1Vit / 100));
                Debug.Log(staminaCalc(dmg));
                player1Vit -= staminaCalc(dmg);
                p1AttackUpozorneni.text = "Nemáš dostatek staminy!";
                break;
            case 2:
                player1Hp -=math.ceil(dmg * (player2Vit / 100));
                Debug.Log(staminaCalc(dmg));
                player2Vit -= staminaCalc(dmg);
                p2AttackUpozorneni.text = "Nemáš dostatek staminy!";
                break;
        }
    }

    public void revive(int id, int amnout) {
        Debug.Log($"id oziveni: {id}");
        Debug.Log($"pocet oziveni: {amnout}");
        //tohle později dát do jedné funkce (dry)
        if(amnout >=9 && amnout <= 15) {
            switch(id) {
                case 1:
                    if (p1ReviveAbility >=2) {
                        p1ReviveAbility -= 2;
                        if (player1Hp + amnout < PlayerPrefs.GetFloat("player1Hp")) {
                           player1Hp += amnout;
                        } else  {
                            player1Hp += amnout - ((player1Hp + amnout) - PlayerPrefs.GetFloat("player1Hp"));
                        }
                        p1OziveniUpozorneni.text = "";
                        p2OziveniUpozorneni.text = "";
                        closeForm();
                    } else {
                        p1OziveniUpozorneni.text = "Nemáš dostatek počtů oživení!";
                    }
                    break;
                case 2:
                    if (p2ReviveAbility >=2) {
                        p2ReviveAbility -= 2;
                        Debug.Log("odecitam 2");
                        if (player2Hp + amnout < PlayerPrefs.GetFloat("player2Hp")) {
                            player2Hp += amnout;
                        } else  {
                            player2Hp += amnout - ((player2Hp + amnout) - PlayerPrefs.GetFloat("player2Hp"));
                        }
                        p1OziveniUpozorneni.text = "";
                        p2OziveniUpozorneni.text = "";
                        closeForm();
                    } else {
                        p2OziveniUpozorneni.text = "Nemáš dostatek počtů oživení!";
                    }
                    break;
            }
        } else if(amnout <= 8) {
            switch(id) {
                case 1:
                    if (p1ReviveAbility >=1) {
                        p1ReviveAbility -= 1;
                        if (player1Hp + amnout < PlayerPrefs.GetFloat("player1Hp")) {
                            player1Hp += amnout;
                        } else  {
                            player1Hp += amnout - ((player1Hp + amnout) - PlayerPrefs.GetFloat("player1Hp"));
                        }
                        p1OziveniUpozorneni.text = "";
                        p2OziveniUpozorneni.text = "";
                        closeForm();
                    } else {
                        p1OziveniUpozorneni.text = "Nemáš dostatek počtů oživení!";
                    }
                    break;
                case 2:
                    if (p2ReviveAbility >=1) {
                        p2ReviveAbility -= 1;
                        Debug.Log("odecitam 1");
                        if (player2Hp + amnout < PlayerPrefs.GetFloat("player2Hp")) {
                            player2Hp += amnout;
                        } else  {
                            player2Hp += amnout - ((player2Hp + amnout) - PlayerPrefs.GetFloat("player2Hp"));
                        }
                        p1OziveniUpozorneni.text = "";
                        p2OziveniUpozorneni.text = "";
                        closeForm();
                    } else {
                        p2OziveniUpozorneni.text = "Nemáš dostatek počtů oživení!";
                    }
                    break;
            }
        } else {
            Debug.Log(amnout);
            p1OziveniUpozorneni.text = "Max. se můžeš oživit o 15 životů!";
            p2OziveniUpozorneni.text = "Max. se můžeš oživit o 15 životů!";
        }
    }

    //Vypocet sebrani staminy
    int staminaCalc(int sila) {
        Debug.Log(sila);
        switch(sila) {
            case <=0:
                return(0);
            case <=10:
                return(6);
            case <=20:
                return(8);
            case <=30:
                return(18);
            default:
                return(0);
        }
    }

    //Formuláře

    public void p1AttackFormOpener() {
        closeForm();
        p1AttackForm.gameObject.SetActive(true);
    }
    public void p1OziveniFormOpener() {
        closeForm();
        p1ReviveAmnout.text = $"{p1ReviveAbility}/5 oziveni";
        p1OziveniForm.gameObject.SetActive(true);
    }
    public void p2AttackFormOpener() {
        closeForm();
        p2AttackForm.gameObject.SetActive(true);
    }
    public void p2OziveniFormOpener() {
        closeForm();
        p2ReviveAmnout.text = $"{p2ReviveAbility}/5 oziveni";
        p2OziveniForm.gameObject.SetActive(true);
    }

    public void p1BtnOff() {
        p1AttackButton.interactable = false;
        p1OziveniButton.interactable = false;
        p1SkipButton.interactable = false;
        p1Played = true;
    }

    public void p2BtnOff() {
        p2AttackButton.interactable = false;
        p2OziveniButton.interactable = false;
        p2SkipButton.interactable = false;
        p2Played = true;
    }

    public void checkForm(string id) {
        switch(id) {
            case "1a":
                int.TryParse(p1AttackInput.text, out int velikost);
                if(velikost <= 30 && velikost > 0) {
                    if (player1Vit > staminaCalc(velikost)) {
                        attack(1, velikost);
                        p1AttackUpozorneni.text = "";
                        closeForm();
                        p1BtnOff();
                    } else {
                        p1AttackUpozorneni.text = "Nemáš dostatek staminy!";
                    }
                } else if (velikost > 30) {
                    p1AttackUpozorneni.text = "Můžeš max. 30!";
                } else {
                    p1AttackUpozorneni.text = "Zadej platnou hodnotu!";
                }
                break;
            case "2a":
                int.TryParse(p2AttackInput.text, out int velikost2);
                if(velikost2 <= 30 && velikost2 > 0) {
                    if (player2Vit > staminaCalc(velikost2)) {
                        attack(2, velikost2);
                        p2AttackUpozorneni.text = "";
                        closeForm();
                        p2BtnOff();
                    } else {
                        p2AttackUpozorneni.text = "Nemáš dostatek staminy!";
                    }
                } else if (velikost2 > 30) {
                    p2AttackUpozorneni.text = "Můžeš max. 30!";
                } else {
                    p2AttackUpozorneni.text = "Zadej platnou hodnotu!";
                }
                break;
            case "1u":
                int.TryParse(p1OziveniInput.text, out int velikost3);
                revive(1, velikost3);
                p1BtnOff();
                break;
            case "2u":
                int.TryParse(p2OziveniInput.text, out int velikost4);
                revive(2, velikost4);
                p2BtnOff();
                break;
        }
    }
    public void closeForm() {
        p1AttackForm.gameObject.SetActive(false);
        p1OziveniForm.gameObject.SetActive(false);
        p2AttackForm.gameObject.SetActive(false);
        p2OziveniForm.gameObject.SetActive(false);
    }

    //Funkce na skip
    public void skip(int id) {
        switch(id) {
            case 1:
                p1AttackButton.interactable = false;
                p1OziveniButton.interactable = false;
                p1SkipButton.interactable = false;
                p1Played = true;
                p1Skipped = true;
                closeForm();
                break;
            case 2:
                p2AttackButton.interactable = false;
                p2OziveniButton.interactable = false;
                p2SkipButton.interactable = false;
                p2Played = true;
                p2Skipped = true;
                closeForm();
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        //Nastavení proměnných
        player1Name = PlayerPrefs.GetString("player1Name");
        player2Name = PlayerPrefs.GetString("player2Name");
        player1Hp = PlayerPrefs.GetFloat("player1Hp");
        player2Hp = PlayerPrefs.GetFloat("player2Hp");
        player1Vit = PlayerPrefs.GetFloat("player1Vit");
        player2Vit = PlayerPrefs.GetFloat("player2Vit");

        p1HealthStatus.maxValue = player1Hp;
        p2HealthStatus.maxValue = player2Hp;
        p1VitStatus.maxValue = player1Vit;
        p2VitStatus.maxValue = player2Vit;

        //Nastavení placeholderů na jména a kdo hraje
        p1NamePlaceholder.text = player1Name;
        p2NamePlaceholder.text = player2Name;
        whoPlay.text = $"{player1Name} je na tahu!";

        //Vypnutí na začátku interakce pro druhého hráče
        p2AttackButton.interactable = false;
        p2OziveniButton.interactable = false;
        p2SkipButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Aktualizace status barů
        p1HealthStatus.value = player1Hp;
        p2HealthStatus.value = player2Hp;
        p1VitStatus.value = player1Vit;
        p2VitStatus.value = player2Vit;

        p1HpSatusNum.text = player1Hp.ToString();
        p2HpSatusNum.text = player2Hp.ToString();
        p1VitSatusNum.text = Convert.ToString(math.ceil(player1Vit));
        p2VitSatusNum.text = Convert.ToString(math.ceil(player2Vit));

        //Endgame checker
        if (player1Hp < 0) {
            PlayerPrefs.SetInt("idWon", 2);
            SceneManager.LoadScene("endGame");
        } else if (player2Hp < 0) {
            PlayerPrefs.SetInt("idWon", 1);
            SceneManager.LoadScene("endGame");
        }

        //roundChecker
        if (p1Played == true) {
            Debug.Log("p1 odehral");
            p2AttackButton.interactable = true;
            p2OziveniButton.interactable = true;
            p2SkipButton.interactable = true;
            whoPlay.text = $"{player2Name} je na tahu!";
            p1Played = false;
            p2Played = false;
        }
        if (p2Played == true) {
            Debug.Log("p2 odehral");
            p1AttackButton.interactable = true;
            p1OziveniButton.interactable = true;
            p1SkipButton.interactable = true;
            whoPlay.text = $"{player1Name} je na tahu!";
            p1Played = false;
            p2Played = false;
            roundComplete = true;
        }
        if (roundComplete == true) {
            float p1Add;
            float p2Add;
            if (p1Skipped == false) {
                p1Add = PlayerPrefs.GetFloat("player1Vit") / 20;
            } else {
                p1Add = PlayerPrefs.GetFloat("player1Vit") / 10;
            }
            if (p2Skipped == false) {
                p2Add = PlayerPrefs.GetFloat("player1Vit") / 20;
            } else {
                p2Add = PlayerPrefs.GetFloat("player1Vit") / 10;
            }

            if (player1Vit + p1Add < PlayerPrefs.GetFloat("player1Vit")) {
                player1Vit += p1Add;
            } else {
                player1Vit += p1Add - ((player1Vit + p1Add) - PlayerPrefs.GetFloat("player1Vit"));
            }
            if (player2Vit + p2Add < PlayerPrefs.GetFloat("player2Vit")) {
                player2Vit += p2Add;
            } else {
                player2Vit += p2Add - ((player2Vit + p2Add) - PlayerPrefs.GetFloat("player2Vit"));
            }
            roundComplete = false;
        }
    }
}
