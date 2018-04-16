using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EditCharacterName : EditTextBox {

    public TextAsset act_zero_character_file;
    public string[] act_zero_character_lines;
    private Text CharTextInstance;

    protected int CharCurrentLine = 0;
    private int CharEndline;

    public string CharText
    {
        set
        {
            CharTextInstance.text = value;
        }
    }

    //Recieves the text componet from the text box game object
    void Awake()
    {
        CharTextInstance = GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start () {

        //Retrieves the text from the act's text fileand splits it by line into an array
        if (act_zero_character_file != null)
        {
            act_zero_character_lines = act_zero_character_file.text.Split('\n');
        }

        //Set the endline to the length of the act's lenght
        CharEndline = act_zero_character_lines.Length;

    }
	
	// Update is called once per frame
	void Update () {

        CharText = act_zero_character_lines[CharCurrentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CharCurrentLine++;
        }

        //Resets the current line to zero when all the lines have been cycled through
        if (CharCurrentLine == CharEndline)
        {
            CharCurrentLine = 0;
        }

    }
}
