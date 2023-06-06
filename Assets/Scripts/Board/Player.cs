using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerState state;
    private BoardSpace targetSpace;
    private int rollCount;
    private UIManager ui;
    private List<int> rolls;
    private bool usedItem;
    private Camera myCam;
    private bool takingTurn;
    public AIBrain brain;

    [Tooltip("Movement Speed of Player.")]
    public float moveSpeed = 1.0f;
    [Tooltip("The list of dice prefabs; Normal, Double, Triple, Magic, Poison, Bowser.")]
    public List<GameObject> dicePrefabs;
    [Tooltip("BoardManager of this board.")]
    public BoardManager game;

    // Start is called before the first frame update
    void Start() {
        ui = FindObjectOfType<UIManager>();
        game = FindObjectOfType<BoardManager>();
        myCam = GetComponentsInChildren<Camera>()[0];
        myCam.enabled = false;
        List<PlayerState> rivals = new List<PlayerState>() { game.p1.state, game.p2.state, game.p3.state, game.p4.state };
        rivals.Remove(state);
        if (state.getController() == 0) {
            brain = new PlayerBrain(state, rivals[0], rivals[1], rivals[2], game);
        } else {
            brain = new EasyBrain(state, rivals[0], rivals[1], rivals[2], game);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator TakeTurn() {
        ui.SetController(state.getController());
        ui.SetDecisionAI(brain);
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
                        case "Bowser Suit":
                            state.setMovement(5);
                            state.removeItem(BoardItem.BowserSuit);
                            break;
                        case "Poison Mushroom":
                            ui.Dialogue("Who Do You Want To Use It On?", new List<string>() { game.p1.state.charName(), game.p2.state.charName(), game.p3.state.charName(), game.p4.state.charName(), "Never Mind" }, true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            if (ui.MostRecentDialogueAnswer() == game.p1.state.charName()) {
                                game.p1.state.setMovement(4);
                                state.removeItem(BoardItem.PoisonMushroom);
                            } else if (ui.MostRecentDialogueAnswer() == game.p2.state.charName()) {
                                game.p2.state.setMovement(4);
                                state.removeItem(BoardItem.PoisonMushroom);
                            } else if (ui.MostRecentDialogueAnswer() == game.p3.state.charName()) {
                                game.p3.state.setMovement(4);
                                state.removeItem(BoardItem.PoisonMushroom);
                            } else if (ui.MostRecentDialogueAnswer() == game.p4.state.charName()) {
                                game.p4.state.setMovement(4);
                                state.removeItem(BoardItem.PoisonMushroom);
                            } else {
                                usedItem = false;
                            }
                            break;
                        case "Tweester Totem":
                            ui.Dialogue("Who Do You Want To Use It On?", new List<string>() { game.p1.state.charName(), game.p2.state.charName(), game.p3.state.charName(), game.p4.state.charName(), "Never Mind" }, true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            if (ui.MostRecentDialogueAnswer() == game.p1.state.charName()) {
                                game.p1.setNextSpace(game.GetRandomSpace(), true);
                                state.removeItem(BoardItem.TweesterTotem);
                            } else if (ui.MostRecentDialogueAnswer() == game.p2.state.charName()) {
                                game.p2.setNextSpace(game.GetRandomSpace(), true);
                                state.removeItem(BoardItem.TweesterTotem);
                            } else if (ui.MostRecentDialogueAnswer() == game.p3.state.charName()) {
                                game.p3.setNextSpace(game.GetRandomSpace(), true);
                                state.removeItem(BoardItem.TweesterTotem);
                            } else if (ui.MostRecentDialogueAnswer() == game.p4.state.charName()) {
                                game.p4.setNextSpace(game.GetRandomSpace(), true);
                                state.removeItem(BoardItem.TweesterTotem);
                            } else {
                                usedItem = false;
                            }
                            break;
                        case "Warp Pipe":
                            int swapWith = game.MyPlayerNumber(this) + Random.Range(1, 3);
                            if (swapWith > 4) {
                                swapWith -= 4;
                            }
                            int temp = state.getSpaceID();
                            switch (swapWith) {
                                case 1:
                                    setNextSpace(game.GetSpaceFromID(game.p1.state.getSpaceID()), true);
                                    game.p1.setNextSpace(game.GetSpaceFromID(temp), true);
                                    state.removeItem(BoardItem.WarpPipe);
                                    break;
                                case 2:
                                    setNextSpace(game.GetSpaceFromID(game.p2.state.getSpaceID()), true);
                                    game.p2.setNextSpace(game.GetSpaceFromID(temp), true);
                                    state.removeItem(BoardItem.WarpPipe);
                                    break;
                                case 3:
                                    setNextSpace(game.GetSpaceFromID(game.p3.state.getSpaceID()), true);
                                    game.p3.setNextSpace(game.GetSpaceFromID(temp), true);
                                    state.removeItem(BoardItem.WarpPipe);
                                    break;
                                case 4:
                                    setNextSpace(game.GetSpaceFromID(game.p4.state.getSpaceID()), true);
                                    game.p4.setNextSpace(game.GetSpaceFromID(temp), true);
                                    state.removeItem(BoardItem.WarpPipe);
                                    break;
                                default:
                                    usedItem = false;
                                    break;
                            }
                            break;
                        case "Plunder Chest":
                            List<string> theftChoices = new List<string>();
                            if (game.p1.state.getItems().Count > 0 && game.MyPlayerNumber(this) != 1) {
                                theftChoices.Add(game.p1.state.charName());
                            }
                            if (game.p2.state.getItems().Count > 0 && game.MyPlayerNumber(this) != 2) {
                                theftChoices.Add(game.p2.state.charName());
                            }
                            if (game.p3.state.getItems().Count > 0 && game.MyPlayerNumber(this) != 3) {
                                theftChoices.Add(game.p3.state.charName());
                            }
                            if (game.p4.state.getItems().Count > 0 && game.MyPlayerNumber(this) != 4) {
                                theftChoices.Add(game.p4.state.charName());
                            }
                            if (theftChoices.Count > 0) {
                                theftChoices.Add("Never Mind");
                                ui.Dialogue("Who Do You Want To Use It On?", theftChoices, true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            } else {
                                ui.Dialogue("Nobody has any items to steal.", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                usedItem = false;
                                break;
                            }
                            if (ui.MostRecentDialogueAnswer() == game.p1.state.charName()) {
                                state.removeItem(BoardItem.PlunderChest);
                                BoardItem stolenItem = game.p1.state.stealRandomItem();
                                state.addItem(stolenItem);
                                ui.Dialogue("You stole " + game.p1.state.charName() + "'s " + ItemSpace.itemNames[(int) stolenItem] + "!", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            } else if (ui.MostRecentDialogueAnswer() == game.p2.state.charName()) {
                                state.removeItem(BoardItem.PlunderChest);
                                BoardItem stolenItem = game.p2.state.stealRandomItem();
                                state.addItem(stolenItem);
                                ui.Dialogue("You stole " + game.p2.state.charName() + "'s " + ItemSpace.itemNames[(int) stolenItem] + "!", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            } else if (ui.MostRecentDialogueAnswer() == game.p3.state.charName()) {
                                state.removeItem(BoardItem.PlunderChest);
                                BoardItem stolenItem = game.p3.state.stealRandomItem();
                                state.addItem(stolenItem);
                                ui.Dialogue("You stole " + game.p3.state.charName() + "'s " + ItemSpace.itemNames[(int) stolenItem] + "!", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            } else if (ui.MostRecentDialogueAnswer() == game.p4.state.charName()) {
                                state.removeItem(BoardItem.PlunderChest);
                                BoardItem stolenItem = game.p4.state.stealRandomItem();
                                state.addItem(stolenItem);
                                ui.Dialogue("You stole " + game.p4.state.charName() + "'s " + ItemSpace.itemNames[(int) stolenItem] + "!", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            }
                            break;
                        case "Double Star Card":
                            ui.Dialogue("That won't have any effect right now.", true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            usedItem = false;
                            break;
                        case "Skeleton Key":
                            ui.Dialogue("That won't have any effect right now.", true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            usedItem = false;
                            break;
                        case "Gaddlight":
                            ui.Dialogue("That won't have any effect right now.", true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            usedItem = false;
                            break;
                        case "Chomp Treat":
                            ui.Dialogue("That won't have any effect right now.", true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            usedItem = false;
                            break;
                        case "Boo Bell":
                            state.removeItem(BoardItem.BooBell);
                            ui.Dialogue("Boo", "Eeeeeheeheehee! I just love causing trouble! I think I'll go steal a little something from one of your rivals!", false);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                            yield return new WaitForSeconds(0.1f);
                            bool stealingStars = false;
                            List<string> coinTheftChoices = new List<string>();
                            if (game.p1.state.getCoins() > 0 && game.MyPlayerNumber(this) != 1) {
                                coinTheftChoices.Add(game.p1.state.charName());
                            }
                            if (game.p2.state.getCoins() > 0 && game.MyPlayerNumber(this) != 2) {
                                coinTheftChoices.Add(game.p2.state.charName());
                            }
                            if (game.p3.state.getCoins() > 0 && game.MyPlayerNumber(this) != 3) {
                                coinTheftChoices.Add(game.p3.state.charName());
                            }
                            if (game.p4.state.getCoins() > 0 && game.MyPlayerNumber(this) != 4) {
                                coinTheftChoices.Add(game.p4.state.charName());
                            }
                            List<string> starTheftChoices = new List<string>();
                            if (game.p1.state.getStars() > 0 && game.MyPlayerNumber(this) != 1) {
                                coinTheftChoices.Add(game.p1.state.charName());
                            }
                            if (game.p2.state.getStars() > 0 && game.MyPlayerNumber(this) != 2) {
                                coinTheftChoices.Add(game.p2.state.charName());
                            }
                            if (game.p3.state.getStars() > 0 && game.MyPlayerNumber(this) != 3) {
                                coinTheftChoices.Add(game.p3.state.charName());
                            }
                            if (game.p4.state.getStars() > 0 && game.MyPlayerNumber(this) != 4) {
                                coinTheftChoices.Add(game.p4.state.charName());
                            }
                            if (state.getCoins() >= 40 && starTheftChoices.Count > 0) {
                                ui.Dialogue("Boo", "I'll steal Coins for free, but Stars will cost you 40 Coins!", new List<string>() {"I want Coins!", "I want Stars!", "No, Stealing is Wrong!"}, false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                                if (ui.MostRecentDialogueAnswer() == "I want Stars!") {
                                    stealingStars = true;
                                }
                                if (ui.MostRecentDialogueAnswer() != "No, Stealing is Wrong!") {
                                    if (stealingStars) {
                                        ui.Dialogue("Boo", "Alright, alright! Who should I take them from?", starTheftChoices, false);
                                    } else {
                                        ui.Dialogue("Boo", "Alright, alright! Who should I take them from?", coinTheftChoices, false);
                                    }
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                }
                            } else if (coinTheftChoices.Count > 0) {
                                coinTheftChoices.Add("No, Stealing is Wrong!");
                                ui.Dialogue("Boo", "Whose Coins do you want me to take?", coinTheftChoices, false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                            } else {
                                ui.Dialogue("Boo", "Well...I would if anyone had anything good to steal.", false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                            }
                            if (ui.MostRecentDialogueAnswer() == game.p1.state.charName()) {
                                ui.Dialogue("Boo", "Alright! Here I go!", false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                                if (game.p1.state.hasItem(BoardItem.Gaddlight)) {
                                    game.p1.state.removeItem(BoardItem.Gaddlight);
                                    ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else if (stealingStars) {
                                    state.changeCoins(-40);
                                    state.changeStars(1);
                                    game.p1.state.changeStars(-1);
                                    ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else {
                                    int coinsToSteal = Random.Range(game.p1.state.getCoins() / 4, 20);
                                    state.changeCoins(coinsToSteal);
                                    game.p1.state.changeCoins(-1 * coinsToSteal);
                                    ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                }
                            } else if (ui.MostRecentDialogueAnswer() == game.p2.state.charName()) {
                                ui.Dialogue("Boo", "Alright! Here I go!", false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                                if (game.p2.state.hasItem(BoardItem.Gaddlight)) {
                                    game.p2.state.removeItem(BoardItem.Gaddlight);
                                    ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else if (stealingStars) {
                                    state.changeCoins(-40);
                                    state.changeStars(1);
                                    game.p2.state.changeStars(-1);
                                    ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else {
                                    int coinsToSteal = Random.Range(game.p2.state.getCoins() / 4, 20);
                                    state.changeCoins(coinsToSteal);
                                    game.p2.state.changeCoins(-1 * coinsToSteal);
                                    ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                }
                            } else if (ui.MostRecentDialogueAnswer() == game.p3.state.charName()) {
                                ui.Dialogue("Boo", "Alright! Here I go!", false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                                if (game.p3.state.hasItem(BoardItem.Gaddlight)) {
                                    game.p3.state.removeItem(BoardItem.Gaddlight);
                                    ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else if (stealingStars) {
                                    state.changeCoins(-40);
                                    state.changeStars(1);
                                    game.p3.state.changeStars(-1);
                                    ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else {
                                    int coinsToSteal = Random.Range(game.p3.state.getCoins() / 4, 20);
                                    state.changeCoins(coinsToSteal);
                                    game.p3.state.changeCoins(-1 * coinsToSteal);
                                    ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                }
                            } else if (ui.MostRecentDialogueAnswer() == game.p4.state.charName()) {
                                ui.Dialogue("Boo", "Alright! Here I go!", false);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                                if (game.p4.state.hasItem(BoardItem.Gaddlight)) {
                                    game.p4.state.removeItem(BoardItem.Gaddlight);
                                    ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else if (stealingStars) {
                                    state.changeCoins(-40);
                                    state.changeStars(1);
                                    game.p4.state.changeStars(-1);
                                    ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                } else {
                                    int coinsToSteal = Random.Range(game.p4.state.getCoins() / 4, 20);
                                    state.changeCoins(coinsToSteal);
                                    game.p4.state.changeCoins(-1 * coinsToSteal);
                                    ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                    ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                    yield return new WaitForSeconds(0.1f);
                                }
                            } else {
                                ui.Dialogue("Boo", "Ugh! Laaaame!", true);
                                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                                yield return new WaitForSeconds(0.1f);
                            }
                            break;
                        default:
                            usedItem = false;
                            break;
                    }
                    if (usedItem) {
                        state.UsedItemStatTrigger();
                    }
                    break;
                /*
                case "View Board":
                    //TODO: Implement
                */
                default:
                    int mv = state.getMovement();
                    GameObject die = Instantiate(dicePrefabs[mv], transform);
                    rolls = new List<int>();
                    int targetRollCount = (mv == 1) ? 2 : ((mv == 2) ? 3 : ((mv == 5) ? 5 : 1));
                    yield return new WaitUntil(() => (rolls.Count >= targetRollCount));
                    for (int i = 0; i < targetRollCount; i++) {
                        this.rollCount += rolls[i];
                    }
                    ui.SetMoveCounterNumber(this.rollCount);
                    ui.MoveCounter(true);
                    setNextSpace(targetSpace.getNextSpaceInSequence(), false);
                    while (rollCount > 0) {
                        if (transform.position == targetSpace.transform.position) {
                            if (state.getMovement() == 5) {
                                if (game.MyPlayerNumber(this) != 1 && targetSpace.id == game.p1.state.getSpaceID()) {
                                    if (game.p1.state.getCoins() >= 20) {
                                        state.changeCoins(20);
                                        game.p1.state.changeCoins(-20);
                                    } else {
                                        state.changeCoins(game.p1.state.getCoins());
                                        game.p1.state.setCoins(0);
                                    }
                                }
                                if (game.MyPlayerNumber(this) != 2 && targetSpace.id == game.p2.state.getSpaceID()) {
                                    if (game.p2.state.getCoins() >= 20) {
                                        state.changeCoins(20);
                                        game.p2.state.changeCoins(-20);
                                    } else {
                                        state.changeCoins(game.p2.state.getCoins());
                                        game.p2.state.setCoins(0);
                                    }
                                } 
                                if (game.MyPlayerNumber(this) != 3 && targetSpace.id == game.p3.state.getSpaceID()) {
                                    if (game.p3.state.getCoins() >= 20) {
                                        state.changeCoins(20);
                                        game.p3.state.changeCoins(-20);
                                    } else {
                                        state.changeCoins(game.p3.state.getCoins());
                                        game.p3.state.setCoins(0);
                                    }
                                }
                                if (game.MyPlayerNumber(this) != 4 && targetSpace.id == game.p4.state.getSpaceID()) {
                                    if (game.p4.state.getCoins() >= 20) {
                                        state.changeCoins(20);
                                        game.p4.state.changeCoins(-20);
                                    } else {
                                        state.changeCoins(game.p4.state.getCoins());
                                        game.p4.state.setCoins(0);
                                    }
                                }
                            }
                            targetSpace.passHere(this);
                            yield return new WaitUntil(() => targetSpace.donePassingYet());
                            if (targetSpace.ableToLandHere()) {
                                rollCount -= 1;
                                state.MovedSpaceStatTrigger();
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
                    state.setMovement(0);
                    state.setSpace(targetSpace.id);
                    yield return new WaitUntil(() => targetSpace.doneLandingYet());
                    state.setSpace(targetSpace.id);
                    takingTurn = false;
                    myCam.enabled = false;
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

    public int GetCurrentRollCount() {
        return rollCount;
    }
}
