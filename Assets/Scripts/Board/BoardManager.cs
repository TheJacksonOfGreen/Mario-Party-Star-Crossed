using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardManager : MonoBehaviour {
    [HideInInspector]
    public GameState state;
    public string fileToLoad;
    public Player p1;
    public Player p2;
    public Player p3;
    public Player p4;

    private int phase;
    private PlayerState[] players;
    private List<BoardSpace> registry;
    private int maxID;

    void Awake() {
        if (fileToLoad != "" && File.Exists("Assets/Data/" + fileToLoad)) {
            state = JsonUtility.FromJson<GameState>(File.ReadAllText("Assets/Data/" + fileToLoad));
        } else {
            state = new GameState(new PlayerState(0, 0), new PlayerState(0, 1), new PlayerState(0, 2), new PlayerState(0, 3), 10, 0, false, false, false, new List<int>() { 0, 0, 0, 0 }, new List<bool>() { false });
            fileToLoad = state.SaveGame();
        }
        registry = new List<BoardSpace>();
        foreach (BoardSpace space in GetComponentsInChildren<BoardSpace>()) {
            if (space.id > maxID) {
                maxID = space.id;
            }
            if (space.id != -1) {
                while (registry.Count <= space.id) {
                    registry.Add(null);
                }
                registry[space.id] = space;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        players = state.GetPlayers();
        p1.SetPlayer(players[0]);
        p2.SetPlayer(players[1]);
        p3.SetPlayer(players[2]);
        p4.SetPlayer(players[3]);
    }

    // Update is called once per frame
    void Update() {
        foreach (PlayerState pA in players) {
            pA.setPlacing(1);
            foreach (PlayerState pB in players) {
                if (pA != pB && (pB.getStars() > pA.getStars() || (pA.getStars() == pB.getStars() && pB.getCoins() > pA.getCoins()))) {
                    pA.setPlacing(pA.getPlacing() + 1);
                }
            }
        }

        // 0: Start-Of-Turn Event (Opening Ceremony, Halfway There, Last Five Turns)
        // 1: P1 Pre-Roll
        // 2: P1 Post-Roll
        // 3: P2 Pre-Roll
        // 4: P2 Post-Roll
        // 5: P3 Pre-Roll
        // 6: P3 Post-Roll
        // 7: P4 Pre-Roll
        // 8: P4 Post-Roll
        // 9: Minigame
        // 10: End-of-turn reset, save

        switch (phase) {
            case 0:
                switch (state.getTurnEvent()) {
                    case 1:
                        // TODO: Opening Ceremony
                        break;
                    case 2:
                        // TODO: Halfway There
                        break;
                    case 3:
                        // TODO: Last Five Turns
                        break;
                    case 4:
                        // TODO: Closing Ceremony
                        break;
                    default:
                        break;
                }
                phase = 1;
                break;
            case 1:
                p1.StartTurn();
                phase = 2;
                break;
            case 2:
                if (p1.TurnOver()) {
                    phase = 3;
                }
                break;
            case 3:
                p2.StartTurn();
                phase = 4;
                break;
            case 4:
                if (p2.TurnOver()) {
                    phase = 5;
                }
                break;
            case 5:
                p3.StartTurn();
                phase = 6;
                break;
            case 6:
                if (p3.TurnOver()) {
                    phase = 7;
                }
                break;
            case 7:
                p4.StartTurn();
                phase = 8;
                break;
            case 8:
                if (p4.TurnOver()) {
                    phase = 9;
                }
                break;
            case 9:
                // TODO: Implement Minigame Phase
                phase = 10;
                break;
            case 10:
                foreach (PlayerState ps in players) {
                    ps.setTeam(0);
                }
                state.turnCompleted();
                state.SaveGame();
                phase = 0;
                break;
            default:
                phase = 0;
                break;
        }
    }

    public BoardSpace GetSpaceFromID(int id) {
        return this.registry[id];
    }

    public BoardSpace GetRandomSpace() {
        return this.registry[Random.Range(1, this.registry.Count - 1)];
    }

    public int MyPlayerNumber(Player p) {
        if (p == p1) {
            return 1;
        } else if (p == p2) {
            return 2;
        } else if (p == p3) {
            return 3;
        } else if (p == p4) {
            return 4;
        } else {
            return 0;
        }
    }
}
