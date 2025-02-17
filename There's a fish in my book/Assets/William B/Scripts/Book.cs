using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using TMPro;

public class Book : MonoBehaviour
{
    // Attributes
    

    // Variables
    private bool isOpen = false;

    // Reference
    public TextMeshProUGUI title { get; private set; }
    public TextMeshProUGUI author { get; private set; }
    public Sprite genre { get; private set; }
    public Material color { get; private set; }
    [SerializeField] UnityEvent onOpenEvents;

    // Function-like
    public bool IsOpen
    {
        get { return isOpen; }
    }

    public void Initialize(string nTitle, string nAuthor, Sprite nGenre, Color nColor, UnityEvent nEvents = null)
    {
        // References
        Transform canvas = transform.Find("Canvas").transform;
        title = canvas.Find("title").GetComponent<TextMeshProUGUI>();
        author = canvas.Find("author").GetComponent<TextMeshProUGUI>();

        // Assignment
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
        isOpen = true;

        // If the book is a fake (if it has events), trigger said events
        if (onOpenEvents != null)
        {
            onOpenEvents.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
