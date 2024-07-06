using UnityEngine;
using UnityEngine.UI;

public class hintMan : MonoBehaviour
{
    public Image hintWindow;
    public void hintOpener() {
        hintWindow.gameObject.SetActive(true);
    }

    public void hintCloser() {
        hintWindow.gameObject.SetActive(false);
    }
}
