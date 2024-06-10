using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject mainPageGroup;
    [SerializeField] private GameObject builtInModelsPageGroup;
    [SerializeField] private GameObject customMadeModelsPageGroup;

    public void GoToBuiltInModelsPage() {
        builtInModelsPageGroup.SetActive(true);
        mainPageGroup.SetActive(false);
    }

    public void GoToCustomMadeModelsPage() {
        customMadeModelsPageGroup.SetActive(true);
        mainPageGroup.SetActive(false);
    }

    public void GoToMainPage() {
        mainPageGroup.SetActive(true);
        customMadeModelsPageGroup.SetActive(false);
        builtInModelsPageGroup.SetActive(false);
    }
}