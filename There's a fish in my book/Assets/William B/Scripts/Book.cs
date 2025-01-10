using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Book : MonoBehaviour
{
    // Attributes
    

    // Variables
    private bool isOpen = false;
    public bool IsOpen
    {
         get {return isOpen; }
    }

    // Reference
    [SerializeField] private TextMeshPro title, author;
    [SerializeField] private Sprite genre;
    [SerializeField] private Material color;
    [SerializeField] UnityEvent onOpenEvents;

    private Book(TextMeshPro nTitle, TextMeshPro nAuthor, Sprite nGenre, Material nColor, UnityEvent newEvents = null)
    {
        title = nTitle;
        author = nAuthor;
        genre = nGenre;
        color = nColor;
        onOpenEvents = newEvents;
    }

    /// <summary>
    /// Gets triggered when the book is opened, not when picked up
    /// </summary>
    public void OnOpen()
    {

        isOpen = true;

        // If the book i sa  fake (if it has events), trigger said events
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
