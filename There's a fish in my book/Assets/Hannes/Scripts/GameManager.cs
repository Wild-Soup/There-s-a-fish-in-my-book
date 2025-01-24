using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<Transform> availablePositions;
    // time and game progression
    [SerializeField] private int day = 1;
    [SerializeField] private float time = 0;
    [SerializeField] private Day[] days;
    // amount of books the game has generate
    [SerializeField] private Book[] generatedBooks;
    [SerializeField] private Book[] correctBooks;
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
        time += Time.deltaTime;
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
        SetBooks();
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

        // creates a  list of all the books that will generate
        generatedBooks = new Book[booksCount + trapBooksCount + cloneBooksCount];
        correctBooks = new Book[mainBooksCount];
        // a list contain unused titles
        List<string> availableTitles = new List<string>();
        // adds every title to the available titles
        foreach (string title in titles)
        {
            availableTitles.Add(title);
        }

        for (int i = 0; i < booksCount; i++)
        {
            // fail safe for if all titles have been used
            if (availableTitles.Count == 0)
                break;

            // creates the book
            generatedBooks[i] = new Book(availableTitles[Random.Range(0, availableTitles.Count)], // all possible titles
                authorName[Random.Range(0, authorName.Length)], // all possible author names
                genres[Random.Range(0, genres.Length)], // all possible generes
                colors[Random.Range(0, colors.Length)]); // all possible colors
            // make this book a main book if i is less than the amount of main books
            if (i < mainBooksCount)
                correctBooks[i] = generatedBooks[i];
            // removes this books title from the available titles
            availableTitles.Remove(generatedBooks[i].title.text);
        }

        for (int i = 0; i < trapBooksCount; i++)
        {
            // fail safe for if all titles have been used
            if (availableTitles.Count == 0)
                break;

            // creates a trap book
            generatedBooks[booksCount + i] = new Book(availableTitles[Random.Range(0, availableTitles.Count)],
                authorName[Random.Range(0, authorName.Length)],
                genres[Random.Range(0, genres.Length)],
                colors[Random.Range(0, colors.Length)],
                possibleEvents[Random.Range(0, possibleEvents.Length)]);

            // removes this books title from the available titles
            availableTitles.Remove(generatedBooks[i].title.text);
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
    private Book CloneBook(Book original)
    {
        List<List<char>> letterarray = new List<List<char>> { "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray().ToList(), "edgpatqnlijwmubofcyhkrvszxHDGBFECATLXJNMQROPZIWYUKVS ".ToCharArray().ToList() };
        // a list with all letters of the
        char[] title = original.title.text.ToCharArray();
        // a list with all letters of the author
        char[] author = original.author.text.ToCharArray();
        // how many lettters will be changed
        int changedLetters = 6 - day;

        for (int i = 0; i < changedLetters; i++)
        {
            // the index of letters that have changed
            int[] changedIndex = new int[changedLetters];

            // the index for the letter that will be changed
            int currentIndex = 0;

            // the currentIndex will be randomized once and then again if the given index has already been used
            do
            {
                currentIndex = Random.Range(0, title.Length + author.Length);
            }
            while (changedIndex.Contains(currentIndex));
            // sets the letter att the current index to a coresponding letter in the letterarray
            if (currentIndex >= title.Length)
                author[currentIndex - title.Length] = letterarray[1][letterarray[0].FindIndex(x => x == author[currentIndex - title.Length])];
            else
                title[currentIndex] = letterarray[1][letterarray[0].FindIndex(x => x == title[currentIndex])];
        }
        string newTitle = "";
        string newAuthor = "";
        // turns the list into a string
        foreach (char letter in title)
            newTitle += letter;
        // turns the list into a string
        foreach (char letter in author)
            newAuthor += letter;

        // returns the new cloned book
        return new Book(newTitle, newAuthor, original.genre, original.color.color);
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