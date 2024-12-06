using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // time and game progression
    [SerializeField] private int day = 1;
    [SerializeField] private float time = 0;
    // amount of books the game will generate
    [SerializeField] private int nrBooks = 1;
    [SerializeField] private int nrRealBooks = 1;
    // number of correct and incorrect books the player has collected
    [SerializeField] private int nrCorrectBooks = 0;
    [SerializeField] private int nrIncorrectBooks = 0;

    [SerializeField] private TempBook[] generatedBooks;
    [SerializeField] private TempBook[] correctBooks;
    // possible combination of books that will be generated
    [SerializeField] private string[] titles;
    [SerializeField] private string[] authorName;
    [SerializeField] private Sprite[] genres;
    [SerializeField] private Color[] colors;
    [SerializeField] private UnityEvent possibleEvents;

    private GameObject gameOverPanel = null;


    // Start is called before the first frame update
    void Start()
    {
        // makes this a singelton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
    }

    public int Score
    {
        get
        {
            return 0;
        }
    }

    public void StartDay()
    {
    }

    public void SetBooks()
    {

    }

    public void ResetGame()
    {
    }

    public void EndDay()
    {

    }

    public void GameOver()
    {

    }

    [System.Serializable]
    public class TempBook
    {
        public string title;
        public string authorName;
        public Sprite genre;
        public Color color;
        UnityEvent events;

        public TempBook(string title, string author, Sprite genre, Color color)
        {
            this.title = title;
            this.authorName = author;
            this.genre = genre;
            this.color = color;
            events = null;
        }
        public TempBook(string title, string author, Sprite genre, Color color, UnityEvent ev)
        {
            this.title = title;
            this.authorName = author;
            this.genre = genre;
            this.color = color;
            events = ev;
        }
    }
}