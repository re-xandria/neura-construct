using UnityEngine;

public class NoteController : MonoBehaviour
{

    public float targetBeat;

    private Vector3 spawnPosition;
    private float spawnBeat;
    private float travelBeats;
    private float destroyBeat;

    public ConductorController conductorController;
    public GameObject judgementLine;

    public bool legacyNotebehavior;

    private ConductorController _ConductorController;

    private Transform laneButton;
    private NoteData.BUTTONTYPE laneType;

    public void InitializeLane(NoteData.BUTTONTYPE type, Transform buttonTransform)
    {
        laneType = type;
        laneButton = buttonTransform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate() 
    {

    }

    void OnCollisionEnter2D(Collision2D button)
    {
        ButtonController collidedButton = button.collider.GetComponentInParent<ButtonController>();
        if (!collidedButton) return;

        Debug.Log("Colliding with button " + laneButton);
    }
}
