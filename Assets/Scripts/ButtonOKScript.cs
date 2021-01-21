using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOKScript : MonoBehaviour
{

    public Button button;
    public GameObject inputField;

    // Start is called before the first frame update
    void Start()
    {

        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        inputField = GameObject.Find("InputField");

    }

    void TaskOnClick()
    {
        SceneLoader.ready = true;
        SceneLoader.redNumber = Int32.Parse(inputField.GetComponent<InputField>().textComponent.text);
    }
}
