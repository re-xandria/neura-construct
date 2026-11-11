using System.Collections.Generic;
using System.Linq;                // Required for .Where() and .ToList()
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecordLevel()
    {
        MenuCanvas.SetActive(false);

        Transform levelManager = Instantiate(levelManagerPrefab, GameCanvas.GetComponent<Transform>());
        LevelManager levelManagerScript = levelManager.GetComponent<LevelManager>();

        GameCanvas.SetActive(true);

        levelManagerScript.conductorController = GameObject.Find("Conductor").GetComponent<ConductorController>();
        levelManagerScript.noteParent = GameObject.Find("NoteHolderParent").GetComponent<Transform>();
        levelManagerScript.noteCanvasButtons = Component.FindObjectsByType<Transform>().Where(x => x.name.Contains("NoteButton")).ToList();
        // need to attach level manager script to the buttons 
        foreach (Transform btn in levelManagerScript.noteCanvasButtons)
        {
            btn.GetComponent<ButtonController>().levelManager = levelManagerScript;
        }
        levelManagerScript.judgementLine = GameObject.Find("JudgementLine");
        levelManagerScript.timer = GameObject.Find("Time");

        levelManagerScript.enabled = true;
        conductorController.ToggleRecord();
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
            NoteList jsonData = JsonUtility.FromJson<NoteList>(jsonString);
            levelManagerScript.NoteList = jsonData.noteList;

            foreach (NoteData note in jsonData.noteList)
            {
                Debug.Log($"Loaded Note: {note.noteId}");
            }

            GameCanvas.SetActive(true);

            // set necessary game object components
            levelManagerScript.conductorController = GameObject.Find("Conductor").GetComponent<ConductorController>();
            levelManagerScript.noteParent = GameObject.Find("NoteHolderParent").GetComponent<Transform>();
            levelManagerScript.noteCanvasButtons = Component.FindObjectsByType<Transform>().Where(x => x.name.Contains("NoteButton")).ToList();
            levelManagerScript.noteCanvasButtons.Sort((a, b) => a.name.CompareTo(b.name));
            // need to attach level manager script to the buttons 
            foreach (Transform btn in levelManagerScript.noteCanvasButtons)
            {
                btn.GetComponent<ButtonController>().levelManager = levelManagerScript;
            }
            levelManagerScript.judgementLine = GameObject.Find("JudgementLine");
            levelManagerScript.timer = GameObject.Find("Time");

            levelManagerScript.enabled = true;
            conductorController.playGame = true;
        }
        else
        {
            Debug.LogError($"Cannot find JSON file at: {filePathDev} or {filePathBuild}");
        }
    }
}
