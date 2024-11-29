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
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        timeDropDown = EditorGUILayout.Foldout(timeDropDown, "Progression");

        if (timeDropDown)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(day, new GUIContent("Current Day", "the day that the player is currently on, this will effect difficulty"));

            EditorGUILayout.PropertyField(time, new GUIContent("Current Game Time", "the amount of time in seconds the player has to complete the objective"));

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
