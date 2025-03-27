using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform hourhand;
    [SerializeField] private Transform minutehand;

    [SerializeField] private Transform librarienSpawnPosition;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private TextMeshProUGUI[] objectiveText;
    // the prefab for the books
    [SerializeField] private GameObject bookPrefab;
    // time and game progression
    [SerializeField] private int day = 1;
    public float time { get; private set; }
    [SerializeField] private Day[] days;
    // amount of books the game has generate
    [SerializeField] private Book[] generatedBooks;
    [SerializeField] private List<Book> correctBooks;
    [SerializeField] private List<Book> scannedBooks;
    // number of correct and incorrect books the player has collected
    [SerializeField] private int nrCorrectBooks = 0;
    [SerializeField] private int nrIncorrectBooks = 0;
    // possible combination of books that will be generated
    [SerializeField] private string[] titles;
    [SerializeField] private string[] authorName;
    [SerializeField] private Sprite[] genres;
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject[] possibleEvents;

    [SerializeField]private GameObject gameOverPanel = null;


    // Start is called before the first frame update
    void Start()
    {
        // makes this a singelton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(instance.gameObject);

        StartDay();
    }
    // Update is called once per frame
    void Update()
    {
        time += Mathf.Clamp(Time.deltaTime, 0f, 360f);

        hourhand.localRotation = Quaternion.Euler(0f, ((240f / 360f) * time) - 90f, 0f);
        minutehand.localRotation = Quaternion.Euler(0f, (360f * (time / 60f)), 0f);

        if (time >= 360f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public int Score
    {
        get
        {
            return 0;
        }
    }
    /// <summary> //
    /// Starts a new day
    /// </summary>
    public void StartDay()
    {
        time = 0f;


        nrCorrectBooks = 0;
        nrIncorrectBooks = 0;

        GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawnPosition.position;
        GameObject.FindGameObjectWithTag("Librarian").transform.position = librarienSpawnPosition.position;
        GameObject.FindGameObjectWithTag("Librarian").GetComponent<LibrarianAI>().ResetAnger();

        // destroys all books if they already exist
        for (int i = 0; i < generatedBooks.Length; i++)
            if (generatedBooks[i] != null)
                Destroy(generatedBooks[i].gameObject);

        generatedBooks = new Book[0];
        // generates books
        SetBooks();;

        List<GameObject> positions = GameObject.FindGameObjectsWithTag("BookPos").ToList();

        foreach (TextMeshProUGUI text in objectiveText)
            text.text = "";

        // places all the books around the library
        foreach (Book book in generatedBooks)
        {
            if (correctBooks.Contains(book))
            {
                objectiveText[correctBooks.FindIndex(x => x == book)].text += $"{book.title.text} by {book.author.text}";
                objectiveText[correctBooks.FindIndex(x => x == book)].color = Color.white;
            }

            // gets a random position
            int index = Random.Range(0, positions.Count);
            // sets the books position and rotation to the randomized position and rotation
            book.transform.position = positions[index].transform.position;
            book.transform.rotation = positions[index].transform.rotation;
            // removes the already used position from the list
            positions.RemoveAt(index);
        }
    }
    /// <summary>
    /// creates books for the day
    /// </summary>
    private void SetBooks()
    {
        // gets the amount of generated books that will appear during this day
        int booksCount = (2*(day - 1)) + 4;
        // gets the amount of generated books that will be correct during this day
        int mainBooksCount = (2 * (day - 1)) + 3;
        // gets the amount of generated books that will be traps during this day
        int trapBooksCount = 2 * (day - 1);
        // gets the amount of books that will be cloned during this day
        int cloneBooksCount = day;

        // creates a  list of all the books that will generate
        generatedBooks = new Book[booksCount + trapBooksCount + cloneBooksCount];
        correctBooks = new List<Book>();
        // a list contain unused titles
        List<string> availableTitles = new List<string>();
        // adds every title to the available titles
        availableTitles.AddRange(titles);
        for (int i = 0; i < booksCount; i++)
        {
            // fail safe for if all titles have been used
            if (availableTitles.Count == 0)
                break;
            generatedBooks[i] = Instantiate(bookPrefab).GetComponent<Book>();
            // creates the book
            generatedBooks[i].Initialize(availableTitles[Random.Range(0, availableTitles.Count)], // all possible titles
                authorName[Random.Range(0, authorName.Length)], // all possible author names
                genres[Random.Range(0, genres.Length)], // all possible generes
                colors[Random.Range(0, colors.Length)]); // all possible colors
            // make this book a main book if i is less than the amount of main books
            if (i < mainBooksCount)
                correctBooks.Add(generatedBooks[i]);
            // removes this books title from the available titles
            availableTitles.Remove(generatedBooks[i].title.text);
        }

        for (int i = 0; i < trapBooksCount; i++)
        {
            // fail safe for if all titles have been used
            if (availableTitles.Count == 0)
                break;
            generatedBooks[booksCount + i] = Instantiate(bookPrefab).GetComponent<Book>();
            // creates a trap book
            generatedBooks[booksCount + i].Initialize(availableTitles[Random.Range(0, availableTitles.Count)], // all possible titles
                authorName[Random.Range(0, authorName.Length)], // all possible author names
                genres[Random.Range(0, genres.Length)], // all possible generes
                colors[Random.Range(0, colors.Length)]); // all possible colors

            generatedBooks[booksCount + i].GetComponent<VFXManager>().SetVFX(possibleEvents[Random.Range(0, possibleEvents.Length)]);

            // removes this books title from the available titles
            availableTitles.Remove(generatedBooks[i].title.text);
        }

        for (int i = 0; i < cloneBooksCount; i++)
        {
            // clones the correct books
            generatedBooks[booksCount + trapBooksCount + i] = CloneBook(correctBooks[Random.Range(0, correctBooks.Count)]);
        }
    }
    /// <summary>
    /// takes a book scrambles the author and/or title
    /// </summary>
    /// <param name="original">the book that will be cloned</param>
    /// <returns>returns the new book</returns>
    private Book CloneBook(Book original)
    {
        List<List<char>> letterarray = new List<List<char>> { "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ1234567890 .,".ToCharArray().ToList(), "edgpatqnlijwmubofcyhkrvszxäåoHDGBFECATLXJNMQROPZIWYUKVSÄÅO0987654321-,.".ToCharArray().ToList() };
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
            while (changedIndex.Contains(currentIndex) && letterarray[0].Contains(author[currentIndex]));
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
        Book newBook = Instantiate(bookPrefab).GetComponent<Book>();
        newBook.Initialize(newTitle, newAuthor, original.genre, original.color.color);
        // returns the new cloned book
        return newBook;
    }
    public bool EndDay(bool overrride = false)
    {
        if (nrCorrectBooks == correctBooks.Count || overrride)
        {
            day++;
            StartCoroutine(FadeinFadeOut(1f));
            return true;
        }
        return false;
    }

    private IEnumerator FadeinFadeOut(float time)
    {
        Image panel = transform.GetChild(0).GetComponentInChildren<Image>();

        float time1 = 0;

        while (time1 < time)
        {
            panel.color = new Color(0f, 0f, 0f, time1 / time);

            time1 += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        ResetScene();
        yield return new WaitForSeconds(1f);

        time1 = 1;

        while (time1 > 0)
        {
            panel.color = new Color(0f, 0f, 0f, time1 / time);

            time1 -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

    }
    /// <summary>
    /// Scans a book and updates all the related values
    /// </summary>
    /// <param name="book">The book that will be scanned</param>
    /// <returns>if the book was a correct book or not</returns>
    public bool ScanBook(Book book)
    {
        if (scannedBooks.Contains(book))
            return false;

        scannedBooks.Add(book);

        if (correctBooks.Contains(book))
        {
            nrCorrectBooks++;
            objectiveText[correctBooks.FindIndex(x => x == book)].color = Color.green;
            return true;
        }

        nrIncorrectBooks++;
        return false;
    }

    public void ResetScene()
    {
        day = 1;
        MenuScripts.StartMainScene("Main Prototype Scene");
        StartDay();
    }

    public void ChangeTime(float amount)
    {
        time = Mathf.Min(time - amount, 360f);
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