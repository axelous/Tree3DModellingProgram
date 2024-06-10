using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using AnotherFileBrowser.Windows;
using UnityEngine.Events;
using UnityEngine.UI;

public class TreesFileLoader : MonoBehaviour {
    [SerializeField] private char[] separators = { '|' };
    [SerializeField] private TreeList treeList;
    [SerializeField] private TreeSpawner treeSpawner;

    [SerializeField] private Button createForestButton; 

    private List<Tree> _trees;

    public static event UnityAction GotSuccessfulTreeListLoad;

    public void Start() {
        createForestButton.interactable = false;
    }

    public void OpenFileBrowser() {
        var bp = new BrowserProperties();
        bp.filter = "csv files (*.csv)|*.csv|All Files (*.*)|*.*";
        new FileBrowser().OpenFileBrowser(bp, path => {
            Debug.Log(path);
            LoadTrees(path);

            treeSpawner = FindObjectOfType<TreeSpawner>();
            if (treeSpawner != null) {
                treeSpawner.Initialize(_trees);
            }

            createForestButton.interactable = true;

            GotSuccessfulTreeListLoad?.Invoke();
        });
    }

    private void LoadTrees(string path) {
        _trees = new List<Tree>();

        var text = File.ReadAllText(path);
        var treeInfo = text.Split(separators);

        foreach (var tree in treeInfo) {
            var components = tree.Split(',');

            var objCoordinates = new Vector3 {
                x = float.Parse(components[4], CultureInfo.InvariantCulture),
                y = 0f,
                z = float.Parse(components[5], CultureInfo.InvariantCulture)
            };

            var woodTypeIndex = -1;
            var contains = false;
            if (treeList.CustomTreeObjects != null) {
                contains = treeList.CustomTreeObjects.ContainsKey(components[0]);
            }

            if (!contains) {
                woodTypeIndex = Array.IndexOf(treeList.treeNames, components[0]);
            }

            // todo: error check

            _trees.Add(new Tree(
                objCoordinates,
                components[0],
                woodTypeIndex == -1 ? -1 : woodTypeIndex,
                float.Parse(components[1], CultureInfo.InvariantCulture),
                float.Parse(components[2], CultureInfo.InvariantCulture),
                float.Parse(components[3], CultureInfo.InvariantCulture)));
        }
    }
}