using System.Collections.Generic;
using UnityEngine;

public class TreeList : MonoBehaviour {
    public GameObject[] treeObjects;
    public string[] treeNames = { "Дуб", "Ясень", "Береза" };
    public int[] treeMaxAges = { 500, 300, 120 };

    public Dictionary<string, GameObject> CustomTreeObjects;
}