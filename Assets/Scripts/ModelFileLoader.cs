using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AnotherFileBrowser.Windows;
using Dummiesman;
using UnityEngine.SceneManagement;

public class ModelFileLoader : MonoBehaviour {
    public static ModelFileLoader Instance;

    [SerializeField] private TreeList treeList;

    // public static event UnityAction GotSuccessfulTreeListLoad;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    public void OnEnable() {
        TreeSpawner.GotTreesLoaded += OnTreesLoaded;
    }

    public void OpenFileBrowser() {
        var bp = new BrowserProperties();
        bp.filter = "obj files (*.obj)|*.obj|All Files (*.*)|*.*";
        new FileBrowser().OpenMultiSelectFileBrowser(bp, paths => {
            foreach (var model in paths) {
                Debug.Log(model);
            }

            LoadTrees(paths);
        });
    }

    private void LoadTrees(string[] paths) {
        treeList.CustomTreeObjects = new Dictionary<string, GameObject>();

        foreach (var file in paths) {
            var fileName = Path.GetFileNameWithoutExtension(file);
            Debug.Log(fileName);

            var obj = new OBJLoader().Load(file);
            DontDestroyOnLoad(obj);

            treeList.CustomTreeObjects.Add(fileName, obj);
        }
    }

    private void OnTreesLoaded() {
        foreach (var tree in treeList.CustomTreeObjects) {
            Destroy(tree.Value);
        }
    }
}