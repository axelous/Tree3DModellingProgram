using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TreeExplorer : MonoBehaviour {
    [SerializeField] private TreeSpawner treeSpawner;
    [SerializeField] private GameObject treeCard;

    private List<TreeCard> _treeCards = new List<TreeCard>();

    public void Start() {
        treeSpawner = FindObjectOfType<TreeSpawner>();

        SpawnCards();
    }

    public List<TreeCard> GetTreeCards() {
        return _treeCards;
    }

    private void SpawnCards() {
        if (treeSpawner.GetTrees() == null) return;

        foreach (var tree in treeSpawner.GetTrees()) {
            var card = Instantiate(treeCard, transform);
            var cardContent = card.GetComponent<TreeCard>();
            _treeCards.Add(cardContent);

            cardContent.TreeInfo = tree;
            
            cardContent.WoodType.text = tree.WoodType;
            cardContent.Age.text = "Возраст: " + tree.Age.ToString(CultureInfo.InvariantCulture);
            cardContent.Diameter.text = "Диаметр ствола: " + tree.Diameter.ToString(CultureInfo.InvariantCulture);
            cardContent.Height.text = "Высота дерева: " + tree.Height.ToString(CultureInfo.InvariantCulture);

            cardContent.Tree = tree.TreeGO;

            cardContent.Tree.AddComponent<TreeCard>().Tree = cardContent.gameObject;
            cardContent.Tree.GetComponent<TreeCard>().enabled = true;

            cardContent.Tree.GetComponent<TreeCard>().SubscribeToPrimaryButtonDown();
        }
    }
}