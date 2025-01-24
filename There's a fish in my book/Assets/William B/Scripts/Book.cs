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
    public bool IsOpen
    {
         get {return isOpen; }
    }

    // Reference
    [SerializeField] private TextMeshPro title, author;
    [SerializeField] private Sprite genre;
    [SerializeField] private Material color;
    [SerializeField] UnityEvent onOpenEvents;
    private InputDevice leftController, rightController;
    public Book(string nTitle, string nAuthor, Sprite nGenre, Color nColor, UnityEvent nEvents = null)
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
        // Kolla om knappen är nedtryckt på vänster kontroll
        if (leftController.isValid && leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            
        }
    }
}
