using UnityEngine;

public class NoteController : MonoBehaviour
{

    public float targetBeat;

    private Vector3 spawnPosition;
    private float spawnBeat;
    private float travelBeats;
    private float destroyBeat;

    public ConductorController conductorController;
    public LevelManager levelManager;
    public GameObject judgementLine;

    public bool legacyNotebehavior;

    public Transform laneButton;
    public NoteData ndata;

    public void InitializeLane(NoteData noteData, Transform buttonTransform)
    {
        ndata = noteData;
        laneButton = buttonTransform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D button)
    {
        ButtonController collidedButton = button.collider.GetComponentInParent<ButtonController>();
        if (!collidedButton) return;

        ndata.isColliding = true;
    }

    void OnCollisionStay2D(Collision2D button)
    {
        ButtonController collidedButton = button.collider.GetComponentInParent<ButtonController>();
        if (collidedButton != null) collidedButton.HandleButtonPress(ndata);
    }

}
