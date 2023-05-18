using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private GameObject dialogue;
    private GameObject character;
    private GameObject yourTurn;
    private GameObject options;
    private GameObject standings;
    private GameObject counter;
    private GameObject spacesLeft;
    private GameObject spinner;
    private GameObject turnTracker;
    private GameObject turnTrackerDrop;

    private GameObject optionA;
    private GameObject optionB;
    private GameObject optionC;
    private GameObject optionD;
    private GameObject optionE;

    private GameObject outcome1;
    private GameObject outcome2;
    private GameObject outcome3;
    private GameObject outcome4;
    private GameObject outcome5;
    private GameObject outcome6;
    private GameObject outcome7;
    private GameObject spinnerTitle;
    private GameObject spinnerTitleDrop;

    private BoardManager game;
    private PlayerTracker pt1;
    private PlayerTracker pt2;
    private PlayerTracker pt3;
    private PlayerTracker pt4;

    private int charsShown = 0;
    private string targetText = "";
    private Text dialogueText;
    private Text speakerText;
    private bool endOfChain;
    private string mostRecentAns;
    private List<string> choices;
    private List<string> spinnerChoices;
    private int spinnerLoc;
    private bool spinning;
    private int windDown;
    private int spinnerFrame;
    private Text yourTurnColor;
    private string mostRecentPrompt;
    private int counterMin;
    private int counterMax;
    private int counterStatus;

    // Start is called before the first frame update
    void Start() {
        game = FindObjectOfType<BoardManager>();

        dialogue = transform.Find("Dialogue").gameObject;
        character = transform.Find("Character").gameObject;
        yourTurn = transform.Find("Your Turn").gameObject;
        options = transform.Find("Options").gameObject;
        standings = transform.Find("Standings").gameObject;
        counter = transform.Find("Counter").gameObject;
        spacesLeft = transform.Find("Spaces Left").gameObject;
        spinner = transform.Find("Spinner").gameObject;
        turnTracker = standings.transform.Find("Turn").gameObject;
        turnTrackerDrop = turnTracker.transform.Find("Drop").gameObject;

        optionA = options.transform.Find("Option A").gameObject;
        optionB = options.transform.Find("Option B").gameObject;
        optionC = options.transform.Find("Option C").gameObject;
        optionD = options.transform.Find("Option D").gameObject;
        optionE = options.transform.Find("Option E").gameObject;

        outcome1 = spinner.transform.Find("Panel (1)").gameObject;
        outcome2 = spinner.transform.Find("Panel (2)").gameObject;
        outcome3 = spinner.transform.Find("Panel (3)").gameObject;
        outcome4 = spinner.transform.Find("Panel (4)").gameObject;
        outcome5 = spinner.transform.Find("Panel (5)").gameObject;
        outcome6 = spinner.transform.Find("Panel (6)").gameObject;
        outcome7 = spinner.transform.Find("Panel (7)").gameObject;
        spinnerTitle = spinner.transform.Find("Title").gameObject;
        spinnerTitleDrop = spinnerTitle.transform.Find("Drop").gameObject;

        yourTurnColor = yourTurn.transform.Find("Drop").gameObject.GetComponent<Text>();

        ToggleActivity(-1, -1, -1, -1, 1, -1, -1, -1);

        PlayerState[] players = game.state.GetPlayers();
        pt1 = standings.transform.Find("P1").gameObject.GetComponent<PlayerTracker>();
        pt2 = standings.transform.Find("P2").gameObject.GetComponent<PlayerTracker>();
        pt3 = standings.transform.Find("P3").gameObject.GetComponent<PlayerTracker>();
        pt4 = standings.transform.Find("P4").gameObject.GetComponent<PlayerTracker>();
        pt1.SetPlayer(players[0]);
        pt2.SetPlayer(players[1]);
        pt3.SetPlayer(players[2]);
        pt4.SetPlayer(players[3]);

        dialogueText = dialogue.GetComponentsInChildren<Text>()[0];
        speakerText = character.GetComponentsInChildren<Text>()[0];
        dialogueText.text = "";

        spinning = false;
        windDown = -1;
        spinnerFrame = 0;
        targetText = "";
    }

    // Update is called once per frame
    void Update() {
        turnTracker.GetComponent<Text>().text = game.state.getTurnText();
        turnTrackerDrop.GetComponent<Text>().text = game.state.getTurnText();

        if (dialogueText.text != targetText) {
            charsShown += 1;
            dialogueText.text = targetText.Substring(0, charsShown);
        } else {
            if (WaitForDialogueAnswer()) {
                if (endOfChain) {
                    ToggleActivity(-1, -1, -1, -1, 1, -1, -1, -1);
                }
            }
        }

        if (spinning) {
            if (spinnerFrame == 0) {
                windDown--;
                spinnerLoc++;
                if (spinnerLoc > 7) {
                    spinnerLoc = 1;
                }
                if (Input.GetKeyDown(KeyCode.Space) && windDown < 0) {
                    windDown = Random.Range(6, 9);
                } else if (windDown == 0) {
                    spinning = false;
                    mostRecentAns = spinnerChoices[spinnerLoc - 1];
                }
                if (windDown <= 3) {
                    spinnerFrame = 3;
                } else {
                    spinnerFrame = 1;
                }
            } else {
                spinnerFrame--;
            }
        }

        if (mostRecentPrompt == "Counter") {
            mostRecentAns = "" + counterStatus;
        }

        outcome1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 1 ? 1.0f : 0.5f);
        outcome2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 2 ? 1.0f : 0.5f);
        outcome3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 3 ? 1.0f : 0.5f);
        outcome4.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 4 ? 1.0f : 0.5f);
        outcome5.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 5 ? 1.0f : 0.5f);
        outcome6.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 6 ? 1.0f : 0.5f);
        outcome7.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, spinnerLoc == 7 ? 1.0f : 0.5f);
    }

    public void YourTurn(string character, Color color) {
        yourTurn.GetComponent<Text>().text = "Go, " + character + "!";
        mostRecentPrompt = "Splash Screen";
        yourTurnColor.color = color;
        ToggleActivity(-1, -1, 1, -1, 0, -1, -1, -1);
    }

    public void Dialogue(string targetText, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        mostRecentPrompt = "Dialogue";
        ToggleActivity(1, -1, -1, -1, 0, -1, 0, -1);
        this.targetText = targetText;
    }

    public void Dialogue(string speaker, string targetText, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        speakerText.text = speaker;
        mostRecentPrompt = "Dialogue";
        ToggleActivity(1, 1, -1, -1, 0, -1, 0, -1);
        this.targetText = targetText;
    }

    public void Dialogue(string targetText, List<string> choices, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        this.mostRecentAns = "";
        mostRecentPrompt = "Options";
        ToggleActivity(1, -1, -1, 1, 0, -1, 0, -1);
        this.choices = choices;
        this.choices.Reverse();
        SetOptions();
        this.targetText = targetText;
    }

    public void Dialogue(string speaker, string targetText, List<string> choices, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        speakerText.text = speaker;
        this.mostRecentAns = "";
        mostRecentPrompt = "Options";
        ToggleActivity(1, 1, -1, 1, 0, -1, 0, -1);
        this.choices = choices;
        this.choices.Reverse();
        SetOptions();
        this.targetText = targetText;
    }

    public void Spinner(string title, Color titleColor, List<string> options) {
        outcome1.GetComponentsInChildren<Text>()[0].text = options[0];
        outcome2.GetComponentsInChildren<Text>()[0].text = options[1];
        outcome3.GetComponentsInChildren<Text>()[0].text = options[2];
        outcome4.GetComponentsInChildren<Text>()[0].text = options[3];
        outcome5.GetComponentsInChildren<Text>()[0].text = options[4];
        outcome6.GetComponentsInChildren<Text>()[0].text = options[5];
        outcome7.GetComponentsInChildren<Text>()[0].text = options[6];
        spinnerTitle.GetComponent<Text>().text = title;
        spinnerTitle.GetComponent<Text>().color = titleColor;
        spinnerTitleDrop.GetComponent<Text>().text = title;
        mostRecentPrompt = "Spinner";
        spinnerChoices = options;
        spinnerLoc = Random.Range(1, 7);
        spinning = true;
        windDown = -1;
        ToggleActivity(-1, -1, -1, -1, 0, -1, -1, 1);
    }

    public void Counter(int min, int max, bool countStars) {
        counterMin = min;
        counterMax = max;
        counterStatus = counterMin;
        ToggleActivity(0, -1, -1, -1, 0, 1, -1, -1);
        mostRecentPrompt = "Counter";
    }

    public void IncrementCounter() {
        counterStatus += 1;
        if (counterStatus > counterMax) {
            counterStatus = counterMax;
        }
    }

    public void DecrementCounter() {
        counterStatus -= 1;
        if (counterStatus < counterMin) {
            counterStatus = counterMin;
        }
    }

    private void ToggleActivity(int d, int c, int y, int o, int s, int cc, int sl, int sp) {
        dialogue.SetActive(d == 0 ? dialogue.activeInHierarchy : d > 0);
        character.SetActive(c == 0 ? character.activeInHierarchy : c > 0);
        yourTurn.SetActive(y == 0 ? yourTurn.activeInHierarchy : y > 0);
        options.SetActive(o == 0 ? options.activeInHierarchy : o > 0);
        counter.SetActive(cc == 0 ? counter.activeInHierarchy : cc > 0);
        spacesLeft.SetActive(sl == 0 ? spacesLeft.activeInHierarchy : sl > 0);
        spinner.SetActive(sp == 0 ? spinner.activeInHierarchy : sp > 0);
        if (s > 0 && standings.transform.position.y == 200) {
            StartCoroutine(MoveStandings(true));
        } else if (s < 0 && standings.transform.position.y == 0) {
            StartCoroutine(MoveStandings(false));
        }
    }

    private IEnumerator MoveStandings(bool moveIn) {
        if (moveIn) {
            for (int i = 0; i <= 10; i++) {
                standings.transform.position = new Vector3(0, i * 20, 0);
                yield return new WaitForSeconds(0.05f);
            }
        } else {
            for (int i = 10; i >= 0; i--) {
                standings.transform.position = new Vector3(0, i * 20, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void SetOptions() {
        int c = choices.Count;
        optionA.GetComponentsInChildren<Text>()[0].text = choices[0];
        optionB.GetComponentsInChildren<Text>()[0].text = choices[1];
        if (c > 2) {
            optionC.GetComponentsInChildren<Text>()[0].text = choices[2];
            optionC.SetActive(true);
            if (c > 3) {
                optionD.GetComponentsInChildren<Text>()[0].text = choices[3];
                optionD.SetActive(true);
                if (c > 4) {    
                    optionE.GetComponentsInChildren<Text>()[0].text = choices[4];
                    optionE.SetActive(true);
                } else {
                    optionE.SetActive(false);
                }
            } else {
                optionD.SetActive(false);
                optionE.SetActive(false);
            }
        } else {
            optionC.SetActive(false);
            optionD.SetActive(false);
            optionE.SetActive(false);
        }
    }

    public void SetMoveCounterNumber(int i) {
        spacesLeft.GetComponent<Text>().text = "" + i;
    }

    public void MoveCounter(bool show) {
        spacesLeft.SetActive(show);
    }

    public bool WaitForDialogueAnswer() {
        if (mostRecentPrompt == "Spinner") {
            return !spinning;
        } else if (mostRecentPrompt == "Options") {
            return mostRecentAns != "";
        } else {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public string MostRecentDialogueAnswer() {
        return mostRecentAns;
    }

    public void OptionAClicked() {
        mostRecentAns = choices[0];
    }

    public void OptionBClicked() {
        mostRecentAns = choices[1];
    }

    public void OptionCClicked() {
        mostRecentAns = choices[2];
    }

    public void OptionDClicked() {
        mostRecentAns = choices[3];
    }

    public void OptionEClicked() {
        mostRecentAns = choices[4];
    }
}
