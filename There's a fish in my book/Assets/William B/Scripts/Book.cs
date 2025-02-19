using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using TMPro;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour
{
    // Attributes
    [SerializeField] private float buffer = 0.05f;

    // Variables
    private bool isOpen = false;

    // Reference
    public TextMeshProUGUI title { get; private set; }
    public TextMeshProUGUI author { get; private set; }
    public Sprite genre { get; private set; }
    public Material color { get; private set; }
    [SerializeField] private UnityEvent onOpenEvents;
    [SerializeField] private UnityEvent onCloseEvents;

    public InputActionReference openInput;

    // Function-like
    public bool IsOpen
    {
        get { return isOpen; }
    }


    // Start is called before the first frame update
    void Start()
    {
        // References
        Transform canvas = transform.Find("Canvas").transform;
        title = canvas.Find("title").GetComponent<TextMeshProUGUI>();
        author = canvas.Find("author").GetComponent<TextMeshProUGUI>();
        color = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        // author.rectTransform.position = new Vector3(author.rectTransform.position.x, title.textBounds.center.y - title.textBounds.extents.y - buffer, author.rectTransform.position.z);
    }

    private void OnEnable()
    {
        openInput.action.Enable();

        openInput.action.performed += OpenBook;
    }

    public void OpenBook(InputAction.CallbackContext ctx)
    {
        isOpen = true;

        // If the book is a fake (if it has events), trigger said events
        if (onOpenEvents != null)
        {
            onOpenEvents.Invoke();
        }
    }

    public void Initialize(string nTitle, string nAuthor, Sprite nGenre, Color nColor, UnityEvent nEvents = null)
    {
        title.text = nTitle;
        author.text = nAuthor;
        genre = nGenre;
        color.color = nColor;
        onOpenEvents = nEvents;
    }

    /// <summary>
    /// Gets triggered when the book is opened, not when picked up
    /// </summary>
    public void OnOpen()
    {
    }
}
