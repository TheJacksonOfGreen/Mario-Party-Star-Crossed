using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSpace : MonoBehaviour {
    [Tooltip("The Space directly after this one in the board layout.")]
    public BoardSpace next;
    [Tooltip("This space's internal ID Number.")]
    public int id;

    protected UIManager ui;
    protected BoardManager game;
    protected bool doneLanding;
    protected bool donePassing;
    protected int blueChance;
    protected bool canLandHere;

    // Start is called before the first frame update
    void Start() {
        this.blueChance = 75;
        this.canLandHere = true;
        ui = FindObjectOfType<UIManager>();
        game = FindObjectOfType<BoardManager>();
        this.setup();
        donePassing = true;
        doneLanding = true;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public virtual void setup() {}

    public void passHere(Player p) {
        StartCoroutine(pass(p));
    }

    public void landHere(Player p) {
        StartCoroutine(setMGTeam(p));
        StartCoroutine(land(p));
    }

    public bool ableToLandHere() {
        return canLandHere;
    }

    public virtual BoardSpace getNextSpaceInSequence() {
        return this.next;
    }

    public IEnumerator setMGTeam(Player p) {
        if (blueChance == 100) {
            p.state.setTeam(1);
            yield return null;
        } else if (blueChance == 0) {
            p.state.setTeam(2);
            yield return null;
        } else {
            p.state.setTeam(1);
            yield return new WaitForSeconds(0.1f);
            p.state.setTeam(2);
            yield return new WaitForSeconds(0.1f);
            p.state.setTeam(1);
            yield return new WaitForSeconds(0.1f);
            p.state.setTeam(2);
            yield return new WaitForSeconds(0.1f);
            p.state.setTeam(1);
            yield return new WaitForSeconds(0.1f);
            p.state.setTeam(2);
            yield return new WaitForSeconds(0.1f);
            if (Random.Range(1, 100) <= blueChance) {
                p.state.setTeam(1);
            }
        } 
    }

    public virtual IEnumerator pass(Player p) {
        donePassing = true;
        yield return null;
    }

    public virtual IEnumerator land(Player p) {
        doneLanding = true;
        yield return null;
    }

    public bool doneLandingYet() {
        return doneLanding;
    }

    public bool donePassingYet() {
        return donePassing;
    }

    public IEnumerator GivePlayerItem(Player p, BoardItem i, bool endOfChain) {
        bool fullPockets = !p.state.addItem(i);
        ui.Dialogue("You got a " + ItemSpace.itemNames[(int) i] + "!", !fullPockets && endOfChain);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        if (fullPockets) {
            List<string> playerItems = new List<string>();
            foreach (BoardItem b in p.state.getItems()) {
                playerItems.Add(ItemSpace.itemNames[(int) b]);
            }
            playerItems.Add(ItemSpace.itemNames[(int) i]);
            ui.Dialogue("You are carrying too many items. Pick one to throw out.", playerItems, false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            string trash = ui.MostRecentDialogueAnswer();
            yield return new WaitForSeconds(0.1f);
            BoardItem throwingOut = (BoardItem) ItemSpace.itemNames.IndexOf(trash);
            ui.Dialogue("You discarded " + trash + ".", endOfChain);
            p.state.removeItem(throwingOut);
            p.state.addItem(i);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual int SpacesToStar(bool withKey) {
        return next.SpacesToStar(withKey) + (this.canLandHere ? 1 : 0);
    }

    public virtual int PlayersInRange(int range) {
        int playersHere = 0;
        if (id == game.p1.state.getSpaceID()) {
            playersHere += 1;
        }
        if (id == game.p2.state.getSpaceID()) {
            playersHere += 1;
        } 
        if (id == game.p3.state.getSpaceID()) {
            playersHere += 1;
        }
        if (id == game.p4.state.getSpaceID()) {
            playersHere += 1;
        }
        if (range == 0 && this.canLandHere) {
            return playersHere;
        } else {
            return next.PlayersInRange(range - (this.canLandHere ? 1 : 0)) + playersHere;
        }
    }

    public abstract int AIValue(PlayerState state, List<PlayerState> rivals);

    public virtual int CumulativeValue(PlayerState state, List<PlayerState> rivals, int roll) {
        if (canLandHere) {
            if (roll == 0) {
                return AIValue(state, rivals) - (state.getCoins() >= 20 ? SpacesToStar(state.getItems().Contains(BoardItem.SkeletonKey)) : 0);
            } else {
                return next.CumulativeValue(state, rivals, roll - 1);
            }
        } else {
            return AIValue(state, rivals) + next.CumulativeValue(state, rivals, roll);
        }
    }

    public bool CanLandHere() {
        return canLandHere;
    }
}
