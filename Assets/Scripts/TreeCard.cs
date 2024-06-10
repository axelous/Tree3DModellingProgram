using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TreeCard : MonoBehaviour {
    public Tree TreeInfo;

    public TMP_Text WoodType;
    public TMP_Text Age;
    public TMP_Text Diameter;
    public TMP_Text Height;

    public GameObject Tree = null;

    public static UnityAction GotSelectTreeCardEvent;

    [SerializeField] private Color originalCardColor = new Color(31, 185, 190, 1.0f);
    [SerializeField] private Color selectedCardColor = new Color(190, 31, 84, 1.0f);

    [SerializeField] private Color originalTreeColor = new Color(204, 204, 204, 1.0f);
    [SerializeField] private Color selectedTreeColor = new Color(137, 23, 69, 1.0f);

    private bool _selected = false;

    public void Awake() {
        WoodType = transform.Find("Wood Type")?.GetComponent<TMP_Text>();
        Age = transform.Find("Age")?.GetComponent<TMP_Text>();
        Diameter = transform.Find("Diameter")?.GetComponent<TMP_Text>();
        Height = transform.Find("Height")?.GetComponent<TMP_Text>();

        GotSelectTreeCardEvent += OnSelectTreeCardEvent;
    }

    public void SubscribeToPrimaryButtonDown() {
        InputHandler.GotPrimaryMouseButtonDown += OnPrimaryMouseButtonDown;
    }

    public Color GetOriginalCardColor() {
        return originalCardColor;
    }

    public Color GetSelectedCardColor() {
        return selectedCardColor;
    }

    public Color GetOriginalTreeColor() {
        return originalTreeColor;
    }

    public Color GetSelectedTreeColor() {
        return selectedTreeColor;
    }

    public void SelectTreeCard() {
        GotSelectTreeCardEvent?.Invoke();
        GetComponent<Image>().color = selectedCardColor;
        Tree.GetComponent<MeshRenderer>().material.color = selectedTreeColor;
        _selected = true;
    }

    public void SelectTree() {
        if (Time.timeScale == 0) {
            return;
        }
        
        GetComponent<MeshRenderer>().material.color = Tree.GetComponent<TreeCard>().GetSelectedTreeColor();
        Tree.GetComponent<Image>().color = Tree.GetComponent<TreeCard>().GetSelectedCardColor();
        _selected = false;
    }

    public void OnDestroy() {
        GotSelectTreeCardEvent -= OnSelectTreeCardEvent;
        InputHandler.GotPrimaryMouseButtonDown -= OnPrimaryMouseButtonDown;
    }

    private void OnSelectTreeCardEvent() {
        if (Time.timeScale == 0) {
            return;
        }
        
        _selected = false;
        if (!CompareTag("Tree")) {
            GetComponent<Image>().color = originalCardColor;
            if (Tree == null) return;
            Tree.GetComponent<MeshRenderer>().material.color = originalTreeColor;
        }
        else {
            GetComponent<MeshRenderer>().material.color = Tree.GetComponent<TreeCard>().GetOriginalTreeColor();
            if (Tree == null) return;
            Tree.GetComponent<Image>().color = Tree.GetComponent<TreeCard>().GetOriginalCardColor();
        }
    }

    private void OnPrimaryMouseButtonDown() {
        OnSelectTreeCardEvent();
        if (!CompareTag("Tree")) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        if (hit.transform.gameObject == transform.gameObject) {
            SelectTree();
        }
    }
}