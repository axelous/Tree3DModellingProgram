using System.IO;
using UnityEngine;
using AnotherFileBrowser.Windows;

public class TreesFileLoader : MonoBehaviour {
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private char[] separators = { '|' };
    
    public void OpenFileBrowser() {
        var bp = new BrowserProperties();
        new FileBrowser().OpenFileBrowser(bp, path => {
            Debug.Log(path);
            LoadTrees(path);
        });
    }

    private void LoadTrees(string path) {
        var text = File.ReadAllText(path);
        var coordinatesList = text.Split(separators);

        foreach (var coordinates in coordinatesList) {
            var components = coordinates.Split(',');
            
            var objCoordinates = new Vector3();
            objCoordinates.x = float.Parse(components[0]);
            objCoordinates.y = float.Parse(components[1]);
            objCoordinates.z = float.Parse(components[2]);

            Instantiate(objectToSpawn, objCoordinates, Quaternion.identity);
        }
    }
}