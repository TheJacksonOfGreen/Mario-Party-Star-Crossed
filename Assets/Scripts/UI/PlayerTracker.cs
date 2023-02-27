using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour {
    private PlayerState tracking;
    private BoardManager game;

    private Text coinText;
    private Text starText;
    private Text placingText;
    private Text placingColor;
    private RawImage avatarFrame;
    private RawImage avatarPic;
    private RawImage itemImg1;
    private RawImage itemImg2;
    private RawImage itemImg3;
    private RawImage shroomStamp;
    private Image backPanel;

    public List<Texture2D> items;
    public List<Texture2D> stamps;
    //public List<Texture2D> avatars;

    public void SetPlayer(PlayerState ps) {
        this.tracking = ps;
    }

    // Start is called before the first frame update
    void Start() {
        game = FindObjectOfType<BoardManager>();
        coinText = transform.Find("Coins").transform.Find("CoinCount").gameObject.GetComponent<Text>();
        starText = transform.Find("Stars").transform.Find("StarCount").gameObject.GetComponent<Text>();
        placingText = transform.Find("Placing").gameObject.GetComponent<Text>();
        placingColor = placingText.gameObject.transform.Find("Drop").gameObject.GetComponent<Text>();
        avatarFrame = transform.Find("Avatar").gameObject.GetComponent<RawImage>();
        avatarPic = avatarFrame.gameObject.transform.Find("Picture").gameObject.GetComponent<RawImage>();
        itemImg1 = transform.Find("Items").transform.Find("Item (1)").gameObject.GetComponent<RawImage>();
        itemImg2 = transform.Find("Items").transform.Find("Item (2)").gameObject.GetComponent<RawImage>();
        itemImg3 = transform.Find("Items").transform.Find("Item (3)").gameObject.GetComponent<RawImage>();
        shroomStamp = transform.Find("Movement").gameObject.GetComponent<RawImage>();
        backPanel = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        coinText.text = (tracking.getCoins() < 10 ? "0" : "") + tracking.getCoins();
        starText.text = (tracking.getStars() < 10 ? "0" : "") + tracking.getStars();
        switch (tracking.getTeam()) {
            case 1:
                backPanel.color = new Color(0.0f, 0.0f, 1.0f, 0.4f);
                break;
            case 2:
                backPanel.color = new Color(1.0f, 0.0f, 0.0f, 0.4f);
                break;
            default:
                backPanel.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
                break;
        }
        switch (tracking.getPlacing()) {
            case 1:
                placingText.text = "1st";
                placingColor.color = new Color(1.0f, 0.75f, 0.0f, 1.0f);
                break;
            case 2:
                placingText.text = "2nd";
                placingColor.color = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                break;
            case 3:
                placingText.text = "3rd";
                placingColor.color = new Color(0.67f, 0.4f, 0.0f, 1.0f);
                break;
            default:
                placingText.text = "4th";
                placingColor.color = new Color(0.67f, 0.44f, 1.0f, 1.0f);
                break;
        }
        //TODO: Implement Avatar Pictures
        if (tracking.getItems().Count >= 1) {
            itemImg1.texture = items[(int) tracking.getItems()[0]];
        } else {
            itemImg1.gameObject.SetActive(false);
        }
        if (tracking.getItems().Count >= 2) {
            itemImg2.texture = items[(int) tracking.getItems()[1]];
        } else {
            itemImg2.gameObject.SetActive(false);
        }
        if (tracking.getItems().Count >= 3) {
            itemImg3.texture = items[(int) tracking.getItems()[2]];
        } else {
            itemImg3.gameObject.SetActive(false);
        }
        shroomStamp.texture = stamps[tracking.getMovement()];
    }
}
