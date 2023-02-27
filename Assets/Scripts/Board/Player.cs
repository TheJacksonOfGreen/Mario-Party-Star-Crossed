using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerState state;
    private BoardSpace targetSpace;
    private int rollCount;
    private UIManager ui;
    private int turnPhase;
    private List<int> rolls;
    private bool usedItem;
    private Camera myCam;
    private bool takingTurn;

    [Tooltip("Movement Speed of Player.")]
    public float moveSpeed = 1.0f;
    [Tooltip("The list of dice prefabs; Normal, Double, Triple, Magic, Poison, Bowser.")]
    public List<GameObject> dicePrefabs;
    [Tooltip("The Board Manager of this board.")]
    public BoardManager game;

    // Start is called before the first frame update
    void Start() {
        ui = FindObjectOfType<UIManager>();
        this.turnPhase = 0;
        myCam = GetComponentsInChildren<Camera>()[0];
        myCam.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator TakeTurn() {
        myCam.enabled = true;
        ui.YourTurn(state.charName(), state.charColor());
        usedItem = false;
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        while (takingTurn) {
            if (usedItem || state.getItems().Count == 0) {
                ui.Dialogue("What will you do?", new List<string>() { "Roll Dice", "View Board" }, true);
            } else {
                ui.Dialogue("What will you do?", new List<string>() { "Roll Dice", "Use Item", "View Board" }, true);
            }
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            switch (ui.MostRecentDialogueAnswer()) {
                case "Use Item":
                    List<string> itemOptions = new List<string>();
                    foreach (BoardItem b in state.getItems()) {
                        itemOptions.Add(ItemSpace.itemNames[(int) b]);
                    }
                    itemOptions.Add("Never Mind");
                    ui.Dialogue("Which Item Will You Use?", itemOptions, true);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    usedItem = true;
                    switch (ui.MostRecentDialogueAnswer()) {
                        case "Mushroom":
                            state.setMovement(1);
                            state.removeItem(BoardItem.Mushroom);
                            break;
                        case "Golden Mushroom":
                            state.setMovement(2);
                            state.removeItem(BoardItem.GoldenMushroom);
                            break;
                        case "Magic Mushroom":
                            state.setMovement(3);
                            state.removeItem(BoardItem.MagicMushroom);
                            break;
                        case "Poison Mushroom":
                            state.setMovement(4);
                            state.removeItem(BoardItem.PoisonMushroom);
                            break;
                        case "Bowser Suit":
                            state.setMovement(5);
                            state.removeItem(BoardItem.BowserSuit);
                            break;
                        default:
                            usedItem = false;
                            break;
                    }
                    break;
                /*
                case "View Board":
                    //TODO: Implement
                */
                default:
                    int mv = state.getMovement();
                    Instantiate(dicePrefabs[mv], transform);
                    rolls = new List<int>();
                    int targetRollCount = mv == 1 ? 2 : (mv == 2 ? 3 : (mv == 5 ? 5 : 1));
                    yield return new WaitUntil(() => (rolls.Count >= targetRollCount));
                    for (int i = 0; i < targetRollCount; i++) {
                        this.rollCount += rolls[i];
                    }
                    Debug.Log("Rolled a " + rollCount + "!");
                    ui.SetMoveCounterNumber(this.rollCount);
                    ui.MoveCounter(true);
                    targetSpace = targetSpace.getNextSpaceInSequence();
                    while (rollCount >= 0) {
                        if (transform.position == targetSpace.transform.position) {
                            targetSpace.passHere(this);
                            yield return new WaitUntil(() => targetSpace.donePassingYet());
                            if (targetSpace.ableToLandHere()) {
                                rollCount -= 1;
                            }
                            if (rollCount != 0) {
                                targetSpace = targetSpace.getNextSpaceInSequence();
                            }
                        } else {
                            transform.position = Vector3.MoveTowards(transform.position, targetSpace.transform.position, moveSpeed * Time.deltaTime);
                            yield return null;
                        }
                        ui.SetMoveCounterNumber(this.rollCount);
                        ui.MoveCounter(true);
                    }
                    targetSpace.landHere(this);
                    yield return new WaitUntil(() => targetSpace.doneLandingYet());
                    takingTurn = false;
                    break;
            }
        }
    }

    public void SetPlayer(PlayerState ps) {
        this.state = ps;
        this.setNextSpace(game.GetSpaceFromID(ps.getSpaceID()), true);
    }

    public void setNextSpace(BoardSpace b, bool instant) {
        this.targetSpace = b;
        if (instant) {
            transform.position = b.transform.position;
        }
    }

    public void StartTurn() {
        takingTurn = true;
        StartCoroutine(TakeTurn());
    }

    public bool TurnOver() {
        return !takingTurn;
    }

    public void SendRoll(int r) {
        rolls.Add(r);
    }
}
