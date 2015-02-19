using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour {
	public delegate void ClickAction();
	//public static event ClickAction OnClicked;
	protected string thisString = "";
	private GUIStyle myStyle = new GUIStyle();
	private RectOffset rctOff;
	private int textWidth = Screen.width - 50;
	private int textHeight = 10;
	protected int textBoxTop = 30;
	protected GameObject m2;
	protected Vector3 m2Pos;
	protected bool stationary = false;
	protected Vector3 moveTo;
	protected Vector3 lastPosition;
	private float moveSpeed = 2f;
	protected List<inventory> inventory = new List<inventory>();
	protected Dictionary<string, string> inventoryOverlay = new Dictionary<string, string>();
	//protected Dictionary<string, GameObject> inventoryOverlay = new Dictionary<string, GameObject>();
	protected Text textMe;
	protected GameObject textBoxMe;

	// UI elements
	protected GameObject invBox;

	// keypad code setup
	public static int thisCode;
	public static string tappedCode;
	public static string numberTemp;
	public static bool keypadClosed = true;


	void Awake () {
		m2 = GameObject.Find("m2");
		lastPosition = moveTo = m2.transform.position;
		stationary = true;
		addInventoryItems ();
		getUIElements ();

		// to get all text children of a canvas
		//GameObject canvas = GameObject.Find("Canvas");
		//Text[] textValue = canvas.GetComponentsInChildren<Text>();
		//textValue[0].text = "hey";
		
		textBoxMe = GameObject.Find ("inventory_text");
		// to get specific text object by name
		textMe = textBoxMe.GetComponent<Text>();
		textMe.text = "";

		thisCode = -10;
		tappedCode = "-42";
		numberTemp = "";

	}
	
	protected virtual void Start() {
		// protected or public to use virtual (parent)
		//textBoxMe.SetActive(false);	// uncomment to hide by default
	}

	protected virtual void Update () {
		m2Pos = m2.transform.position;
		if (lastPosition != moveTo) {
			lastPosition = moveTo;
			m2.transform.LookAt(moveTo);
		}

		if (!stationary) {
			m2.transform.position = Vector3.Lerp(m2.transform.position, moveTo, Time.deltaTime * moveSpeed);
			//if(m2.transform.position == moveTo) { // OLD version caused a very small math error
			if(Vector3.Distance (m2.transform.position, moveTo) < .2f) {
				stationary = true;
				//Debug.Log ("FINAL: " + m2.transform.position + " ~~~ " + moveTo);
			}
		}
		//Debug.Log (stationary);
	}

	protected Vector3 getM2Pos(){
		return m2Pos;
	}
	
	protected void displayMessage(string displayString, int lineCount) {
		thisString = displayString;
		textBoxTop = 10 * lineCount;
		
		CancelInvoke();
		Invoke ("removeText", 5);
	}
	
	void removeText() {
		//string textBox = GameObject.Find("textBox").GetType;
		//Debug.Log (GameObject.Find("textBox").name);
		// try to get the dynamic label and fade out before nulling the value
		/*
		 while (fadeCanvasGroup.alpha < 1f) {
			fadeCanvasGroup.alpha += speed * Time.deltaTime;
		}
		*/
		
		thisString = "";
	}
	
	void OnGUI() {
		Font thisFont;
		//random fonts for testing
		//thisFont = Resources.Load<Font> ("LcdStd");
		//thisFont = Resources.Load<Font> ("PhoenixScript");
		//thisFont = Resources.Load<Font> ("MAGNETOB");
		thisFont = Resources.Load<Font> ("Japonesa_0");
		
		myStyle.font = thisFont;
		myStyle.fontSize = 20;
		myStyle.normal.textColor = Color.black;
		myStyle.alignment = TextAnchor.MiddleCenter;
		myStyle.wordWrap = true;
		myStyle.fixedWidth = textWidth;
		myStyle.fixedHeight = textHeight;
		myStyle.name = "textBox";
		
		rctOff = GUI.skin.customStyles[0].overflow;
		myStyle.overflow = rctOff;

		GUI.Label (new Rect (Screen.width / 2 - textWidth / 2, textBoxTop, 100, 30), thisString, myStyle);

		/*
		if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 30), "Unlock Door")) {
			if(OnClicked != null)
				OnClicked();
		}
		*/
	}

	void addInventoryItems() {
		//room 1 objects
		inventoryOverlay.Add ("bucket", "Bucket object in your inventory now");
		inventoryOverlay.Add ("pencil", "Pencil...mmmmmm...graphite");
		inventoryOverlay.Add ("notebook", "Notebook, ode to pulp");
		inventoryOverlay.Add ("bottle", "Bottle, not just for babies anymore");
		//room 2 objects

	}

	void getUIElements(){
		invBox = GameObject.Find ("inventory");

	}

}
