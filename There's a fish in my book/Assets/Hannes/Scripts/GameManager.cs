using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // time and game progression
    [SerializeField] private int day = 1;
    [SerializeField] private float time = 0;
    [SerializeField] private Day[] days;
    // amount of books the game has generate
    [SerializeField] private TempBook[] generatedBooks;
    [SerializeField] private TempBook[] correctBooks;
    // number of correct and incorrect books the player has collected
    [SerializeField] private int nrCorrectBooks = 0;
    [SerializeField] private int nrIncorrectBooks = 0;
    // possible combination of books that will be generated
    [SerializeField] private string[] titles;
    [SerializeField] private string[] authorName;
    [SerializeField] private Sprite[] genres;
    [SerializeField] private Color[] colors;
    [SerializeField] private UnityEvent[] possibleEvents;

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
    /// <summary>
    /// creates books for the day
    /// </summary>
    private void SetBooks()
    {
        // gets the amount of generated books that will appear during this day
        int booksCount = Random.Range(days[day - 1].minBookCount, days[day - 1].maxBookCount);
        // gets the amount of generated books that will be correct during this day
        int mainBooksCount = Random.Range(days[day - 1].minCorrectCount, days[day - 1].maxCorrectCount);
        // gets the amount of generated books that will be traps during this day
        int trapBooksCount = Random.Range(days[day - 1].minTrapCount, days[day - 1].maxTrapCount);
        // gets the amount of books that will be cloned during this day
        int cloneBooksCount = Random.Range(days[day - 1].minTrapCount, days[day - 1].maxTrapCount);

        generatedBooks = new TempBook[booksCount + trapBooksCount + cloneBooksCount];
        correctBooks = new TempBook[mainBooksCount];

        for (int i = 0; i < booksCount; i++)
        {
            // creates the book
            generatedBooks[i] = new TempBook(titles[Random.Range(0, titles.Length)],
                authorName[Random.Range(0, authorName.Length)],
                genres[Random.Range(0, genres.Length)],
                colors[Random.Range(0, colors.Length)]);
            // make this book a main book if i is less than the amount of main books
            if (i < mainBooksCount)
                correctBooks[i] = generatedBooks[i];
        }

        for (int i = 0; i < trapBooksCount; i++)
        {
            // creates a trap book
            generatedBooks[booksCount + i] = new TempBook(titles[Random.Range(0, titles.Length)],
                authorName[Random.Range(0, authorName.Length)],
                genres[Random.Range(0, genres.Length)],
                colors[Random.Range(0, colors.Length)],
                possibleEvents[Random.Range(0, possibleEvents.Length)]);
        }

        for (int i = 0; i < cloneBooksCount; i++)
        {
            // clones the correct books
            generatedBooks[booksCount + trapBooksCount + i] = CloneBook(correctBooks[Random.Range(0, correctBooks.Length)]);
        }
    }
    /// <summary>
    /// takes a book scrambles the author and/or title
    /// </summary>
    /// <param name="original">the book that will be cloned</param>
    /// <returns>returns the new book</returns>
    private TempBook CloneBook(TempBook original)
    {
        return null;
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
    [System.Serializable]
    public struct Day
    {
        public int minBookCount;
        public int maxBookCount;

        public int minCorrectCount;
        public int maxCorrectCount;

        public int minTrapCount;
        public int maxTrapCount;

        public int minClones;
        public int maxClones;
    }
}