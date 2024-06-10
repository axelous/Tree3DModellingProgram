using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimeSimulator : MonoBehaviour {
    [SerializeField] private int defaultMaxAge = 200;

    [SerializeField] private TreeExplorer treeExplorer;
    [SerializeField] private TreeList treeList;

    [SerializeField] private Slider slider;

    public static int count = 0;

    public void Start() {
        treeList = FindObjectOfType<TreeList>();

        slider.onValueChanged.AddListener(delegate { OnTimeSliderValueChanged(); });
    }

    private void OnTimeSliderValueChanged() {
        count = 0;
        foreach (var treeCard in treeExplorer.GetTreeCards()) {
            ++count;
            Debug.Log(treeCard.WoodType.text + count);
            var age = treeCard.TreeInfo.Age + slider.value;
            treeCard.Age.text = "Возраст: " + age.ToString(CultureInfo.InvariantCulture);

            var maxAge = Array.IndexOf(treeList.treeNames, treeCard.TreeInfo.WoodType);
            if (maxAge == -1) {
                maxAge = defaultMaxAge;
            }
            else {
                maxAge = treeList.treeMaxAges[maxAge];
            }

            if (age >= maxAge) {
                treeCard.Age.text = "Мертвое дерево";
                treeCard.Tree.SetActive(false);
                continue;
            }

            treeCard.Tree.SetActive(true);

            var diameter = treeCard.TreeInfo.Diameter + (slider.value * 0.01f);
            var height = treeCard.TreeInfo.Height / 10 + (slider.value * 0.01f);
            treeCard.Tree.transform.localScale = new Vector3(diameter, height, diameter);
            treeCard.Diameter.text = "Диаметр ствола: " + diameter.ToString(CultureInfo.InvariantCulture);
            treeCard.Height.text = "Высота дерева: " + height.ToString(CultureInfo.InvariantCulture);
        }
    }
}