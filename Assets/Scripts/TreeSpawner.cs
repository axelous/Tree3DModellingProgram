using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TreeSpawner : MonoBehaviour {
    public static TreeSpawner Instance;

    public static event UnityAction GotTreesLoaded;

    [SerializeField] private TreeList treeList;

    private List<Tree> _trees;

    public List<Tree> GetTrees() {
        return _trees;
    }

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Initialize(List<Tree> trees) {
        _trees = trees;
    }

    public void SpawnTrees() {
        if (_trees == null) return;
        foreach (var tree in _trees) {
            var treeGO = tree.WoodTypeIndex > -1
                ? treeList.treeObjects[tree.WoodTypeIndex]
                : treeList.CustomTreeObjects[tree.WoodType];
            tree.TreeGO = Instantiate(treeGO, tree.Coordinates, Quaternion.identity);
            tree.TreeGO.gameObject.tag = "Tree";
            tree.TreeGO.AddComponent<CapsuleCollider>();

            Debug.Log("Порода: " + tree.WoodType + " Возраст: " + tree.Age + " Диаметр: " +
                      tree.Diameter + " Высота: " + tree.Height);

            tree.TreeGO.transform.localScale = new Vector3(tree.Diameter, tree.Height / 10, tree.Diameter);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name != "MainScene") return; // todo
        SpawnTrees();
        GotTreesLoaded?.Invoke();
    }
}