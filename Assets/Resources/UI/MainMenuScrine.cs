using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class MainMenuScrine : MonoBehaviour
{
    [SerializeField] private UIDocument _Document;
    [SerializeField] private StyleSheet _styleSheet;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return null;
        var root = _Document.rootVisualElement;
        
        root.Clear();
        
        root.styleSheets.Add(_styleSheet);

        var container = new VisualElement();
        container.AddToClassList("container");
        
        var menuContainer = new VisualElement();
        menuContainer.AddToClassList("menuContainer");

        var nameText = new TextElement();
        nameText.AddToClassList("nameText");
        nameText.text = "Game"; 

        var startButton = new Button();
        startButton.AddToClassList("startButton");
        startButton.text = "Start";
        
        var settingsButton = new Button();
        settingsButton.AddToClassList("settingButton");
        settingsButton.text = "Settings";
        
        

        var quitButton = new Button();
        quitButton.AddToClassList("quitButton");
        quitButton.text = "Quit";

        var settingsContainer = new VisualElement();
        settingsContainer.AddToClassList("settingsContainer");
        
        
        
        root.Add(menuContainer);
        menuContainer.Add(container);
        menuContainer.Add(settingsContainer);
        container.Add(nameText);
        container.Add(startButton);
        container.Add(settingsButton);
        container.Add(quitButton);
        
        
    }
    void OpenSettings()
    {
        
     
    }
    
}
