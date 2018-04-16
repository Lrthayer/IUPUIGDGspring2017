using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTextBox : MonoBehaviour {

	public TextAsset act_zero_file;
	public string[] act_zero_lines;
	private Text textInstance;
	private Image rightChar;
	private Image leftChar;
	public Sprite amelia_neutral;
	public Sprite volkov_neutral;
	public Sprite volkov_ponder;
	public Sprite haru_emote;

	protected int currentLine = 0;
	private int endline;

	public string text{
		set{
			textInstance.text = value;
		}
	}

	//Recieves the text componet from the text box game object
	void Awake (){
		textInstance = GetComponentInChildren<Text>();
	}

	void Start(){

		//References to the right and left character images
		rightChar = GameObject.Find ("RightCharacter").GetComponent<Image> ();
		leftChar = GameObject.Find ("LeftCharacter").GetComponent<Image> ();

        //Retrieves the text from the act's text fileand splits it by line into an array
        if (act_zero_file != null)
        {
            act_zero_lines = act_zero_file.text.Split('\n');
        }
        else {
            Debug.Log("Text Null");
        }

		//Set the endline to the length of the act's lenght
		endline = act_zero_lines.Length;
	}

	void Update() {

		text = act_zero_lines [currentLine];

		if (Input.GetKeyDown (KeyCode.Return)) {
			currentLine++;
		}

		//Resets the current line to zero when all the lines have been cycled through
		if (currentLine == endline) {
			currentLine = 0;
		}

		if (currentLine == 0) {
			ChangeLeftCharacter (2);
			ChangeRightCharacter (0);
		}else if (currentLine == 1) {
			ChangeLeftCharacter (2);
			ChangeRightCharacter (0);
		}else if (currentLine == 3) {
			ChangeLeftCharacter (3);
			ChangeRightCharacter (1);
		}else if (currentLine == 4) {
			ChangeLeftCharacter (1);
			ChangeRightCharacter (3);
		}
	}


	//Can change the character sprite based on a predetermined list of sprites
	void ChangeRightCharacter(int character){
		if (character == 0) {
			rightChar.sprite = amelia_neutral;
		}
		else if (character == 1) {
			rightChar.sprite = volkov_neutral;
		}
		else if (character == 2) {
			rightChar.sprite = volkov_ponder;

		}
		else if (character == 3) {
			rightChar.sprite = haru_emote;
		}
	}

	void ChangeLeftCharacter(int character){
		if (character == 0) {
			leftChar.sprite = amelia_neutral;
		}
		else if (character == 1) {
			leftChar.sprite = volkov_neutral;
		}
		else if (character == 2) {
			leftChar.sprite = volkov_ponder;

		}
		else if (character == 3) {
			leftChar.sprite = haru_emote;
		}
	}
}
