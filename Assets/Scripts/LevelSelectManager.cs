using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

    private Button[] levelSelect;
    public GameObject levelSelectHolder;

    public ConductorController conductorController;
    public Transform levelManagerPrefab;

    public GameObject MenuCanvas;
    public GameObject GameCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelSelect = levelSelectHolder.GetComponentsInChildren<Button>();
        Debug.Log(levelSelect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel(string levelName)
    {
        MenuCanvas.SetActive(false);
        Transform levelManager = Instantiate(levelManagerPrefab, GameCanvas.GetComponent<Transform>());
        LevelManager levelManagerScript = levelManager.GetComponent<LevelManager>();

        string filePathDev = Path.Combine(Application.dataPath, $"Scripts/LevelData/{levelName}.json");
        string filePathBuild = Path.Combine(Application.persistentDataPath, $"Assets/Scripts/LevelData/{levelName}.json");

        if (File.Exists(filePathDev) || File.Exists(filePathBuild))
        {
            string jsonString = "";
            if (File.Exists(filePathBuild)) 
            {
                jsonString = File.ReadAllText(filePathBuild); 
            }
            else 
            {
                jsonString = File.ReadAllText(filePathDev);
            }
            NoteList nlist = JsonUtility.FromJson<NoteList>(jsonString);
            // Fix loop on single object
            // Debug.Log(nlist);
            // foreach (NoteData ndata in nlist)
            // {
            //     Debug.Log($"Loaded Note: {ndata.noteId}");
            // }
            // levelManagerScript.NoteList = nlist;

            // set necessary game object components

            conductorController.playGame = true;
            levelManager.GetComponent<GameObject>().SetActive(true);
            GameCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError($"Cannot find JSON file at: {filePathDev} or {filePathBuild}");
        }
    }
}
