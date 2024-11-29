using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // time and game progression
    [Min(1)] [SerializeField] private int day = 1;
    [SerializeField] private float time = 0;
    // amount of books the game will generate
    [Min(1)][SerializeField] private int nrBooks = 1;
    [Min(1)][SerializeField] private int nrRealBooks = 1;
    // number of correct and incorrect books the player has collected
    [Min(0)][SerializeField] private int nrCorrectBooks = 0;
    [Min(0)][SerializeField] private int nrIncorrectBooks = 0;
    // possible combination of books that will be generated
    [SerializeField] private string[] titles;
    [SerializeField] private string[] authorName;
    [SerializeField] private Image[] genres;
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
        
    }

    public int Score()
    {
        return 0;
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

    public void GameOver()
    {

    }
}