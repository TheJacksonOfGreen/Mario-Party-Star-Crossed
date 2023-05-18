using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameState {
    // GAME SETTINGS
    [SerializeField]
    protected int maxTurn;
    [SerializeField]
    protected int bonusStars;
    [SerializeField]
    protected bool halfwayThere;
    [SerializeField]
    protected bool hiddenBlocksPresent;
    [SerializeField]
    protected bool consolationCoins;
    [SerializeField]
    protected List<int> handicaps;
    [SerializeField]
    protected List<bool> minigames;

    // VISIBLE GAME DATA
    [SerializeField]
    protected int turnsCompleted;

    // INTERNAL GAME DATA
    [SerializeField]
    private string filename;
    // 0-27
    [SerializeField]
    protected List<int> mostRecentFFAs;
    // 0-13
    [SerializeField]
    protected List<int> mostRecent2v2s;
    // 0-11
    [SerializeField]
    protected List<int> mostRecent1v3s;
    // 0-9
    [SerializeField]
    protected List<int> mostRecentDuels;
    // 0-5
    [SerializeField]
    protected List<int> mostRecentBattles;
    // floor(<# of solid, non-start spaces on board> / 20) spots
    // 1-<# of solid, non-start spaces on board>
    // Empty if turnsCompleted < 3, or if not hiddenBlocks
    [SerializeField]
    protected List<int> hiddenBlocks;
    [SerializeField]
    protected int chanceTimeCount;
    // Start at -1
    [SerializeField]
    protected int mostRecentBattleMinigame;
    [SerializeField]
    protected int battleChance;
    [SerializeField]
    protected int suspendedForMG;

    // PLAYER STATES
    [SerializeField]
    protected PlayerState p1;
    [SerializeField]
    protected PlayerState p2;
    [SerializeField]
    protected PlayerState p3;
    [SerializeField]
    protected PlayerState p4;

    public GameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) {
        if (maxTurn % 5 != 0 || maxTurn < 10 || maxTurn > 50) {
            throw new System.ArgumentException("Max Turn Number Param is invalid");
        }
        this.maxTurn = maxTurn;
        if (bonusStars < 0 || bonusStars > 5) {
            throw new System.ArgumentException("Bonus Star Number Param is invalid");
        }
        this.bonusStars = bonusStars;
        this.halfwayThere = (maxTurn != 10) && halfwayThere;
        this.hiddenBlocksPresent = hiddenBlocksPresent;
        this.consolationCoins = consolationCoins;
        /*if (handicaps.Count != 4 || handicaps.ConvertAll<bool>(o => (o < 0 || o > 9)).Contains(false)) {
            throw new System.ArgumentException("Handicaps Param is invalid");
        }*/
        this.handicaps = handicaps;
        /*if (minigames.Count != 70) {
            throw new System.ArgumentException("Minigames Param is invalid");
        }*/
        this.minigames = minigames;
        this.turnsCompleted = 0;
        this.mostRecentFFAs = new List<int>();
        this.mostRecent2v2s = new List<int>();
        this.mostRecent1v3s = new List<int>();
        this.mostRecentDuels = new List<int>();
        this.mostRecentBattles = new List<int>();
        this.suspendedForMG = 0;
        if (hiddenBlocksPresent) {
            this.hiddenBlocks = new List<int>();
        }
        this.chanceTimeCount = 0;
        this.mostRecentBattleMinigame = -1;
        this.battleChance = 0;
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        this.p4 = p4;

        p1.setStars(this.handicaps[0]);
        p2.setStars(this.handicaps[1]);
        p3.setStars(this.handicaps[2]);
        p4.setStars(this.handicaps[3]);
    }

    public PlayerState[] GetStandings() {
        PlayerState[] states = new PlayerState[] { p1, p2, p3, p4 };
        List<PlayerState> standings = new List<PlayerState>();
        foreach (PlayerState state in states) {
            if (state.getPlacing() == 1) {
                standings.Add(state);
            }
        }
        foreach (PlayerState state in states) {
            if (state.getPlacing() == 2) {
                standings.Add(state);
            }
        }
        foreach (PlayerState state in states) {
            if (state.getPlacing() == 3) {
                standings.Add(state);
            }
        }
        foreach (PlayerState state in states) {
            if (state.getPlacing() == 4) {
                standings.Add(state);
            }
        }
        return standings.ToArray();
    }

    public PlayerState[] GetPlayers() {
        return new PlayerState[] { p1, p2, p3, p4 };
    }

    public int getTurnStatus() {
        if (turnsCompleted < 5 && maxTurn != 10) {
            return 0;
        } else if (turnsCompleted <= maxTurn / 2) {
            return 1;
        } else if (turnsCompleted > maxTurn / 2 && turnsCompleted < maxTurn - 5) {
            return 2;
        } else if (turnsCompleted == maxTurn - 1) {
            return 4;
        } else {
            return 3;
        }
    }

    public string getTurnText() {
        return ((this.turnsCompleted > 8) ? "Turn " : "Turn 0") + (this.turnsCompleted + 1) + "/" + this.maxTurn;
    }

    public int getTurnEvent() {
        if (turnsCompleted == 0) {
            return 1;
        } else if (turnsCompleted == maxTurn / 2 && halfwayThere) {
            return 2;
        } else if (turnsCompleted == maxTurn - 5) {
            return 3;
        } else if (turnsCompleted == maxTurn) {
            return 4;
        } else {
            return 0;
        }
    }

    public void turnCompleted() {
        turnsCompleted += 1;
    }

    public string SaveGame() {
        if (filename == null) {
            int n = 0;
            while (File.Exists("Assets/Data/savedgame_" + n)) {
                n += 1;
            }
            filename = "savedgame_" + n;
        }
        File.WriteAllText("Assets/Data/" + filename, JsonUtility.ToJson(this));
        return filename;
    }
}

[System.Serializable]
public abstract class ClassicStarGameState: GameState {
    protected int starLocation;

    public ClassicStarGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        NewStarPos(new List<int>());
    }

    public void NewStarPos(List<int> blocked) {
        List<int> possibleLocs = new List<int>(StarLocationIDs());
        possibleLocs.RemoveAll(i => blocked.Contains(i));
        starLocation = possibleLocs[Random.Range(0, possibleLocs.Count - 1)];
    }

    public abstract List<int> StarLocationIDs();
}

[System.Serializable]
public class FantasticFactoryGameState: ClassicStarGameState {
    public FantasticFactoryGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {}

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class SunshineShorelineGameState: ClassicStarGameState {
    private bool westernSwapJunctionShortPath;
    private bool easternSwapJunctionShortPath;

    public SunshineShorelineGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.westernSwapJunctionShortPath = true;
        this.easternSwapJunctionShortPath = true;
    }

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class MarvelousMetroGameState: ClassicStarGameState {
    private int starPrice;
    private int nabbitPos;

    public MarvelousMetroGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.starPrice = 10;
        this.nabbitPos = -1;
    }

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class NuttyNebulaGameState: GameState {
    public NuttyNebulaGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {}
}

[System.Serializable]
public class TreasureTempleGameState: GameState {
    private List<int> grandTempleInvestments;
    private List<int> northernTempleInvestments;
    private List<int> centralTempleInvestments;
    private List<int> westernTempleInvestments;
    private List<int> easternTempleInvestments;
    private bool westernWhompBlocksInternal;
    private bool easternWhompBlocksInternal;
    private int ancientCountdown;

    public TreasureTempleGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.grandTempleInvestments = new List<int> { 0, 0, 0, 0 };
        this.northernTempleInvestments = new List<int> { 0, 0, 0, 0 };
        this.centralTempleInvestments = new List<int> { 0, 0, 0, 0 };
        this.westernTempleInvestments = new List<int> { 0, 0, 0, 0 };
        this.easternTempleInvestments = new List<int> { 0, 0, 0, 0 };
        this.westernWhompBlocksInternal = false;
        this.easternWhompBlocksInternal = false;
        this.ancientCountdown = 6;
    }
}

[System.Serializable]
public class FlowerFestivalGameState: GameState {
    private int floatLocationA;
    private int floatLocationB;
    private int floatLocationC;
    private List<string> senbonbikiPool;

    public FlowerFestivalGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        // TODO: Float Location
        this.senbonbikiPool = new List<string>() { "None", "None", "None", "Dueling Glove", "Bowser Suit", "Magic Mushroom", "10 Coins", "Star" };
    }
}

[System.Serializable]
public class TwistedTowersGameState: ClassicStarGameState {
    private int graveyardPortraitTarget;
    private int startingPortraitTarget;
    private int midlevelPortraitTarget;
    private int roofPortraitTarget;
    private int kamekPortraitTarget;
    // 0: Blue
    // 1: Red
    // 2: Lucky
    // 3: Duel
    // 4: Bowser
    // 5: Chance
    private static List<int> magicSpaceOdds = new List<int>() {0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5};
    private int magicSpaces;

    public TwistedTowersGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.magicSpaces = -1;
        this.shufflePortraits();
        this.randomizeMagicSpaces();
    }

    public void randomizeMagicSpaces() {
        int neo = magicSpaceOdds[Random.Range(0, magicSpaceOdds.Count - 1)];
        if (neo == magicSpaces) {
            neo += Random.Range(1, 4);
            if (neo >= 6) {
                neo -= 5;
            }
        }
        this.magicSpaces = neo;
    }

    public void shufflePortraits() {
        List<int> locsAvailable = new List<int>() {0, 1, 2, 3, 4};
        this.graveyardPortraitTarget = locsAvailable[Random.Range(0, locsAvailable.Count - 1)];
        locsAvailable.Remove(this.graveyardPortraitTarget);
        this.startingPortraitTarget = locsAvailable[Random.Range(0, locsAvailable.Count - 1)];
        locsAvailable.Remove(this.startingPortraitTarget);
        this.midlevelPortraitTarget = locsAvailable[Random.Range(0, locsAvailable.Count - 1)];
        locsAvailable.Remove(this.midlevelPortraitTarget);
        this.roofPortraitTarget = locsAvailable[Random.Range(0, locsAvailable.Count - 1)];
        locsAvailable.Remove(this.roofPortraitTarget);
        this.kamekPortraitTarget = locsAvailable[Random.Range(0, locsAvailable.Count - 1)];
        locsAvailable.Remove(this.kamekPortraitTarget);

        if (this.graveyardPortraitTarget == 0) {
            locsAvailable.Add(0);
            this.graveyardPortraitTarget = -1;
        }
        if (this.startingPortraitTarget == 0) {
            locsAvailable.Add(0);
            this.startingPortraitTarget = -1;
        }
        if (this.midlevelPortraitTarget == 1) {
            locsAvailable.Add(1);
            this.midlevelPortraitTarget = -1;
        }
        if (this.roofPortraitTarget == 2 || roofPortraitTarget == 3) {
            locsAvailable.Add(roofPortraitTarget);
            this.roofPortraitTarget = -1;
        }
        if (this.kamekPortraitTarget == 4) {
            locsAvailable.Add(4);
            this.kamekPortraitTarget = -1;
        }
        if (locsAvailable.Count == 1) {
            if (locsAvailable[0] == 4) {
                locsAvailable.Add(this.roofPortraitTarget);
                this.roofPortraitTarget = -1;
            } else {
                locsAvailable.Add(this.kamekPortraitTarget);
                this.kamekPortraitTarget = -1;
            }
        }

        if (this.graveyardPortraitTarget == -1) {
            this.graveyardPortraitTarget = locsAvailable[locsAvailable.Count - 1];
        }
        if (this.startingPortraitTarget == -1) {
            this.startingPortraitTarget = locsAvailable[locsAvailable.Count - 1];
        }
        if (this.midlevelPortraitTarget == -1) {
            this.midlevelPortraitTarget = locsAvailable[locsAvailable.Count - 1];
        }
        if (this.roofPortraitTarget == -1) {
            this.roofPortraitTarget = locsAvailable[locsAvailable.Count - 1];
        }
        if (this.kamekPortraitTarget == -1) {
            this.kamekPortraitTarget = locsAvailable[locsAvailable.Count - 1];
        }
    }

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class CalamitousCliffsGameState: GameState {
    private bool starAvailable;

    public CalamitousCliffsGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.starAvailable = true;
    }
}

[System.Serializable]
public class FutureDreamGameState: ClassicStarGameState {
    public FutureDreamGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {}

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class CreepyCavernGameState: ClassicStarGameState {
    private bool whompKingBlocksEast;
    // 0: Mushroom
    // 1: Poison Mushroom
    // 2: Golden Drink
    // 3: Skeleton Key
    private int kingDesire;

    public CreepyCavernGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {
        this.whompKingBlocksEast = true;
        this.kingDesire = Random.Range(0, 3);
    }

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}

[System.Serializable]
public class SnowflakeLakeGameState: GameState {
    public SnowflakeLakeGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {}
}

[System.Serializable]
public class GreedyGalaGameState: ClassicStarGameState {
    public GreedyGalaGameState(PlayerState p1, PlayerState p2, PlayerState p3, PlayerState p4, int maxTurn, int bonusStars, bool halfwayThere, bool hiddenBlocksPresent, bool consolationCoins, List<int> handicaps, List<bool> minigames) : base(p1, p2, p3, p4, maxTurn, bonusStars, halfwayThere, hiddenBlocksPresent, consolationCoins, handicaps, minigames) {}

    public override List<int> StarLocationIDs() {
        // TODO: Star Locations
        return new List<int>() {0};
    }
}
