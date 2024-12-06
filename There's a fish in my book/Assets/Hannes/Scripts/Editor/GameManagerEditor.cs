using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
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

    private bool timeDropDown;

    private bool bookInfoDropDown;

    private bool currentStatDropDown;

    private bool bookStatDropDown;


    private void OnEnable()
    {
        day = serializedObject.FindProperty("day");
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

            EditorGUI.indentLevel--;
        }

        bookInfoDropDown = EditorGUILayout.Foldout(bookInfoDropDown, "Book Stats");

        if (bookInfoDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(nrBooks, new GUIContent("Nr books", "The number of books that will generate this day"));

            if (nrBooks.intValue < 1)
                EditorGUILayout.HelpBox("if the number of books is below one the game will be impossible", MessageType.Error);

            EditorGUILayout.PropertyField(nrRealBooks, new GUIContent("Nr Real Books", "The number of books that are generated that will be collected"));

            if (nrRealBooks.intValue < 1)
                EditorGUILayout.HelpBox("if the number of real books is below one the game will be impossible", MessageType.Error);

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

            EditorGUILayout.PropertyField(generatedBooks, new GUIContent("Generated Books", "the books that have been generated"));

            EditorGUILayout.PropertyField(correctBooks, new GUIContent("Correct Books", "the books that have been selected to be collected"));

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

        serializedObject.ApplyModifiedProperties();
    }
}
