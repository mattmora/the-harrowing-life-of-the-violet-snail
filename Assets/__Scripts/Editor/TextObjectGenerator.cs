using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEditor;

[ExecuteInEditMode]
public class TextObjectGenerator : MonoBehaviour
{
    [Tooltip("An existing object in the scene to use as the parent.")]
    public GameObject parent;
    [Tooltip("A name for the parent object to be created if an existing parent is not set.")]
    public string parentName = "==TEXT==";

    [TextArea]
    public string textBlock;

    [Tooltip("The prefab that will be generated. Any TMP_Text components in the main prefab object or its children will be modified.")]
    public GameObject textPrefab;

    [Tooltip("A string to find in the prefab TMP_Text components for the generated text to replace. Useful for using TMP tags. If empty or not found, the entire component text will be set to the generated text.")]
    public string interpolationString;

    public bool generateAsPrefabInstance = true;

    public bool activate;

    void OnValidate()
    {
        if (activate)
        {
            activate = false;

            // Strip and split the whole text
            // string strippedBlock = RemoveBetween(textBlock, "[", "]");
            string strippedBlock = textBlock;
            string[] texts = strippedBlock.Split('\n');

            if (texts.Length == 0) return;

            // Make a parent object for the text objects if one isn't set
            var par = parent == null ? new GameObject(parentName) : parent;

            // Generate text objects
            int objNum = 1;
            foreach (var t in texts)
            {
                string text = t.Trim();
                if (text == string.Empty) continue;
                GameObject go = PrefabUtility.InstantiatePrefab(textPrefab, par.transform) as GameObject;
                foreach (var textComponent in go.GetComponentsInChildren<TMP_Text>(true))
                {
                    if (textComponent.text.Contains(interpolationString))
                    {
                        textComponent.text = textComponent.text.Replace(interpolationString, text);
                    }
                    else textComponent.text = text;
                }
                go.name = objNum.ToString() + " " + text.Substring(0, text.Length > 15 ? 15 : text.Length);
                objNum++;
            }
            Debug.Log($"Generated {objNum-1} text objects as children to {parent.name} object.");
        }
    }

    public static string RemoveBetween(string original, string firstTag, string secondTag)
    {
        string pattern = firstTag + "(.*?)" + secondTag;
        Regex regex = new Regex(pattern);

        foreach (Match match in regex.Matches(original))
        {
            if (match.Value != string.Empty)
            {
                original = original.Replace(match.Value, string.Empty);
            }
        }
        return original;
    }
}