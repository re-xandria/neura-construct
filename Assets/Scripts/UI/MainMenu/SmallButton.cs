using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SmallButton : VisualElement
{
    public VisualElement Root { get; }

    private readonly Button _btn;
    public Action OnClick { get; set; }
    private AudioClip buttonHoverSFX;
    private AudioClip buttonPressSFX;
    private AudioSource audioSource;

    public string Text
    {
        get => _btn?.text;
        set { if (_btn != null) _btn.text = value; }
    }

    public SmallButton(VisualElement element)
    {
        Root = element;
        Root.focusable = true;
        _btn = Root.Q<Button>();
        
        buttonPressSFX = Resources.Load<AudioClip>("SFX/select_007");
        buttonHoverSFX = Resources.Load<AudioClip>("SFX/glass_002");

        audioSource = GameObject.Find("SfxPlayer").GetComponent<AudioSource>();

        _btn.clicked += () => OnClick?.Invoke();
        _btn.RegisterCallback<PointerEnterEvent>(OnMouseEnter);
        // _btn.RegisterCallback<PointerDownEvent>(OnPress);

        // Ensure child layers don't block clicks
        Root.Query<VisualElement>().ForEach(child =>
        {
            if (child != Root && child != _btn) child.pickingMode = PickingMode.Ignore;
        });
    }

    public void OnMouseEnter(PointerEnterEvent evt)
    {
        audioSource.clip = buttonPressSFX;

        if (!audioSource.isPlaying) audioSource.Play();
        Debug.Log("Mouse Enter");
    }

    // public void OnPress(PointerDownEvent evt)
    // {
    //     audioSource.clip = buttonPressSFX;

    //     if (!audioSource.isPlaying) audioSource.Play();
    //     Debug.Log("Mouse Enter");
    // }
}
