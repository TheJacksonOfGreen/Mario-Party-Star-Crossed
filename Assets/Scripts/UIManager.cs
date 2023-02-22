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

    private GameObject optionA;
    private GameObject optionB;
    private GameObject optionC;
    private GameObject optionD;
    private GameObject optionE;

    private int charsShown = 0;
    private string targetText = "";
    private Text dialogueText;
    private Text speakerText;
    private bool endOfChain;
    private string mostRecentAns;
    private List<string> choices;

    // Start is called before the first frame update
    void Start() {
        dialogue = transform.Find("Dialogue").gameObject;
        character = transform.Find("Character").gameObject;
        yourTurn = transform.Find("Your Turn").gameObject;
        options = transform.Find("Options").gameObject;
        standings = transform.Find("Standings").gameObject;

        optionA = options.transform.Find("Option A").gameObject;
        optionB = options.transform.Find("Option B").gameObject;
        optionC = options.transform.Find("Option C").gameObject;
        optionD = options.transform.Find("Option D").gameObject;
        optionE = options.transform.Find("Option E").gameObject;

        dialogueText = dialogue.GetComponentsInChildren<Text>()[0];
        speakerText = character.GetComponentsInChildren<Text>()[0];
        dialogueText.text = "";

        ToggleActivity(-1, -1, -1, -1, 1);
    }

    // Update is called once per frame
    void Update() {
        if (dialogueText.text != targetText) {
            charsShown += 1;
            dialogueText.text = targetText.Substring(0, charsShown);
        } else {
            if (WaitForDialogueAnswer()) {
                if (endOfChain) {
                    ToggleActivity(-1, -1, -1, -1, 1);
                }
            }
        }
    }

    public void Dialogue(string targetText, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        ToggleActivity(1, -1, -1, -1, -1);
        this.targetText = targetText;
    }

    public void Dialogue(string speaker, string targetText, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        speakerText.text = speaker;
        ToggleActivity(1, 1, -1, -1, -1);
        this.targetText = targetText;
    }

    public void Dialogue(string targetText, List<string> choices, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        ToggleActivity(1, -1, -1, 1, -1);
        this.mostRecentAns = "";
        this.choices = choices;
        this.choices.Reverse();
        SetOptions();
        this.targetText = targetText;
    }

    public void Dialogue(string speaker, string targetText, List<string> choices, bool endOfChain) {
        charsShown = 0;
        this.endOfChain = endOfChain;
        speakerText.text = speaker;
        ToggleActivity(1, 1, -1, 1, -1);
        this.mostRecentAns = "";
        this.choices = choices;
        this.choices.Reverse();
        SetOptions();
        this.targetText = targetText;
    }

    private void ToggleActivity(int d, int c, int y, int o, int s) {
        dialogue.SetActive(d == 0 ? dialogue.activeInHierarchy : d > 0);
        character.SetActive(c == 0 ? character.activeInHierarchy : c > 0);
        yourTurn.SetActive(y == 0 ? yourTurn.activeInHierarchy : y > 0);
        options.SetActive(o == 0 ? options.activeInHierarchy : o > 0);
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
        optionB.GetComponentsInChildren<Text>()[1].text = choices[1];
        if (c > 2) {
            optionC.GetComponentsInChildren<Text>()[2].text = choices[2];
            optionC.SetActive(true);
            if (c > 3) {
                optionD.GetComponentsInChildren<Text>()[3].text = choices[3];
                optionD.SetActive(true);
                if (c > 4) {    
                    optionE.GetComponentsInChildren<Text>()[4].text = choices[4];
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

    public bool WaitForDialogueAnswer() {
        return (charsShown >= targetText.Length) && ((options.activeInHierarchy && mostRecentAns != "") || Input.GetKeyDown(KeyCode.Space));
    }

    public string MostRecentDialogueAnswer() {
        return mostRecentAns;
    }

    public void OptionAClicked() {
        if (charsShown >= targetText.Length) {
            mostRecentAns = choices[0];
        }
    }

    public void OptionBClicked() {
        if (charsShown >= targetText.Length) {
            mostRecentAns = choices[1];
        }
    }

    public void OptionCClicked() {
        if (charsShown >= targetText.Length) {
            mostRecentAns = choices[2];
        }
    }

    public void OptionDClicked() {
        if (charsShown >= targetText.Length) {
            mostRecentAns = choices[3];
        }
    }

    public void OptionEClicked() {
        if (charsShown >= targetText.Length) {
            mostRecentAns = choices[4];
        }
    }
}
