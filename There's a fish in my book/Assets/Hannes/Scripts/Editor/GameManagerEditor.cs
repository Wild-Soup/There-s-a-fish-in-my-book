using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private SerializedProperty bookPrefab;
    private SerializedProperty fadeInpanel;
    private SerializedProperty objectiveText;
    private SerializedProperty librarienSpawnPosition;
    private SerializedProperty playerSpawnPosition;
    private SerializedProperty day;
    private SerializedProperty time;
    private SerializedProperty nrBooks;
    private SerializedProperty nrRealBooks;
    private SerializedProperty nrCorrectBooks;
    private SerializedProperty nrIncorrectBooks;
    private SerializedProperty titles;
    private SerializedProperty authorName;
    private SerializedProperty genres;
    private SerializedProperty colors;
    private SerializedProperty possibleEvents;
    private SerializedProperty gameOverPanel;
    private SerializedProperty generatedBooks;
    private SerializedProperty correctBooks;
    private SerializedProperty days;

    private bool timeDropDown;
    private bool dayInformation;

    private bool bookInfoDropDown;

    private bool currentStatDropDown;

    private bool bookStatDropDown;


    private void OnEnable()
    {
        bookPrefab = serializedObject.FindProperty("bookPrefab");
        objectiveText = serializedObject.FindProperty("objectiveText");
        librarienSpawnPosition = serializedObject.FindProperty("librarienSpawnPosition");
        playerSpawnPosition = serializedObject.FindProperty("playerSpawnPosition");
        day = serializedObject.FindProperty("day");
        days = serializedObject.FindProperty("days");
        time = serializedObject.FindProperty("time");
        nrBooks = serializedObject.FindProperty("nrBooks");
        nrRealBooks = serializedObject.FindProperty("nrRealBooks");
        nrCorrectBooks = serializedObject.FindProperty("nrCorrectBooks");
        nrIncorrectBooks = serializedObject.FindProperty("nrIncorrectBooks");
        titles = serializedObject.FindProperty("titles");
        authorName = serializedObject.FindProperty("authorName");
        genres = serializedObject.FindProperty("genres");
        colors = serializedObject.FindProperty("colors");
        possibleEvents = serializedObject.FindProperty("possibleEvents");
        gameOverPanel = serializedObject.FindProperty("gameOverPanel");
        generatedBooks = serializedObject.FindProperty("generatedBooks");
        correctBooks = serializedObject.FindProperty("correctBooks");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        if (bookPrefab.boxedValue == null)
            EditorGUILayout.HelpBox("This script needs a book prefab", MessageType.Error);
        EditorGUILayout.PropertyField(bookPrefab, new GUIContent("Book prefab"));

        EditorGUILayout.PropertyField(librarienSpawnPosition, new GUIContent("Librarian Spawn Pos"));
        EditorGUILayout.PropertyField(objectiveText, new GUIContent("Object Text"));
        EditorGUILayout.PropertyField(playerSpawnPosition, new GUIContent("Player Spawn Pos"));
        EditorGUILayout.PropertyField(gameOverPanel, new GUIContent("Game Over Panel"));

        timeDropDown = EditorGUILayout.Foldout(timeDropDown, "Progression");

        if (timeDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(day, new GUIContent("Current Day", "the day that the player is currently on, this will effect difficulty"));

            if (day.intValue <= 0)
                EditorGUILayout.HelpBox("The day should never be below 1", MessageType.Warning);

            EditorGUILayout.PropertyField(time, new GUIContent("Current Game Time", "the amount of time in seconds the player has to complete the objective"));

            float hours = Mathf.FloorToInt(time.floatValue / 3600f);
            float minutes = Mathf.FloorToInt(time.floatValue / 60f) - (hours * 60f);
            float seconds = Mathf.FloorToInt(time.floatValue) - (hours * 3600f) - (minutes * 60f);
            float milliseconds = Mathf.FloorToInt((time.floatValue - (hours * 3600f) - (minutes * 60f) - seconds) * 1000);


            EditorGUILayout.HelpBox($"Time left: h{hours} m{minutes} s{seconds}, ms{milliseconds}", MessageType.Info);

            EditorGUILayout.PropertyField(days, new GUIContent("Days", "Game information for every in game day"));

            EditorGUI.indentLevel--;
        }

        bookInfoDropDown = EditorGUILayout.Foldout(bookInfoDropDown, "Book Stats");

        if (bookInfoDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(generatedBooks, new GUIContent("Generated Books", "the books that have been generated"));

            EditorGUILayout.PropertyField(correctBooks, new GUIContent("Correct Books", "the books that have been selected to be collected"));

            EditorGUI.indentLevel--;
        }

        currentStatDropDown = EditorGUILayout.Foldout(currentStatDropDown, "Current Days Stats");

        if (currentStatDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(nrCorrectBooks, new GUIContent("Nr Correct Books", "Number of books collected that are correct"));

            if (nrCorrectBooks.intValue < 0)
                EditorGUILayout.HelpBox("if this value is below 0 it will not be possible to beat", MessageType.Warning);

            EditorGUILayout.PropertyField(nrIncorrectBooks, new GUIContent("Nr Incorrect Books", "Number of books collected that are incorrect"));

            if (nrIncorrectBooks.intValue < 0)
                EditorGUILayout.HelpBox("if this value is less than 0 something is wrong", MessageType.Warning);

            EditorGUI.indentLevel--;
        }

        bookStatDropDown = EditorGUILayout.Foldout(bookStatDropDown, "Possible Book Information");
        
        if (bookStatDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(titles, new GUIContent("Titles", "a list of titles that a book can generate with"));

            if (titles.arraySize < 1)
                EditorGUILayout.HelpBox("this array needs to be bigger than 0", MessageType.Warning);

            EditorGUILayout.PropertyField(authorName, new GUIContent("Author Names", "a list of author names that a book can generate with"));

            if (authorName.arraySize < 1)
                EditorGUILayout.HelpBox("this array needs to be bigger than 0", MessageType.Warning);

            EditorGUILayout.PropertyField(genres, new GUIContent("Genres", "a list of genres that book can generate with"));

            if (genres.arraySize < 1)
                EditorGUILayout.HelpBox("this array needs to be bigger than 0", MessageType.Warning);

            EditorGUILayout.PropertyField(colors, new GUIContent("Colors", "a list of colors that book can generate with"));

            if (colors.arraySize < 1)
                EditorGUILayout.HelpBox("this array needs to be bigger than 0", MessageType.Warning);

            EditorGUILayout.PropertyField(possibleEvents, new GUIContent("Events", "a list of events that book can generate with"));

            EditorGUI.indentLevel--;
        }

        GameManager instance = (GameManager)target;
        if (GUILayout.Button("Start Day") && Application.isPlaying)
            instance.StartDay();
        if (GUILayout.Button("End Day") && Application.isPlaying)
            instance.EndDay(true);

        serializedObject.ApplyModifiedProperties();
    }
}