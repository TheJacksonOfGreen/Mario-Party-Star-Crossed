using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBlock : MonoBehaviour {
    public List<Material> faces;
    public float rotationSpeed;
    public List<ParticleSystem> particles;
    public bool magic;
    [Tooltip("Set to anything other than 0 to force a certain roll.")]
    public int debugRoll = 0;

    private int currentRoll;
    private int maxRoll;
    private int degree;
    private Renderer rend;
    private List<Transform> followers;
    private bool leader;
    [HideInInspector]
    public float comTimer;
    private int aiMagicRoll;

    // Start is called before the first frame update
    void Start() {
        maxRoll = faces.Count;
        currentRoll = Random.Range(1, maxRoll);
        rend = GetComponent<Renderer>();
        degree = 0;
        leader = true;
        followers = new List<Transform>();
        aiMagicRoll = 0;

        if (transform.parent.gameObject.GetComponent<Player>() != null) {
            transform.position = transform.parent.position + (Vector3.up * 3.0f);
            if (transform.parent.gameObject.GetComponent<Player>().state.getController() == 0) {
                comTimer = -8.5f;
            } else {
                comTimer = 0.0f;
                if (magic) {
                    aiMagicRoll = int.Parse(transform.parent.gameObject.GetComponent<Player>().brain.Prompt("Choose Magic Dice Roll", new List<string>(), 0));
                }
            }
        } else if (transform.parent.gameObject.GetComponent<DiceBlock>() != null) {
            leader = false;
            transform.parent.gameObject.GetComponent<DiceBlock>().AskForDirection(transform);
        }
    }

    // Update is called once per frame
    void Update() {
        comTimer += Time.deltaTime;
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime));
        if (magic) {
            if (aiMagicRoll != 0) {
                currentRoll = aiMagicRoll;
            } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D)) {
                currentRoll += 1;
                if (currentRoll > maxRoll) {
                    currentRoll = 1;
                }
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A)) {
                currentRoll -= 1;
                if (currentRoll < 1) {
                    currentRoll = maxRoll;
                }
            }
        } else {
            if (currentRoll == maxRoll) {
                currentRoll = 1;
            } else {
                currentRoll += 1;
            }
        }
        
        if (debugRoll != 0) {
            currentRoll = debugRoll;
        }
        rend.material = faces[currentRoll - 1];

        if (followers.Count > 0) {
            degree += 2;
            if (degree >= 360 / followers.Count) {
                degree = degree % (360 / followers.Count);
            }
            for (int f = 0; f < followers.Count; f++) {
                followers[f].position = (Quaternion.AngleAxis(degree + ((360 / followers.Count) * f), Vector3.up) * Vector3.forward * 1.5f) + transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || comTimer >= 1.5f) {
            comTimer = -99.9f;
            rend.enabled = false;
            foreach (ParticleSystem part in particles) {
                part.Play();
            }
            if (leader) {
                transform.parent.gameObject.GetComponent<Player>().SendRoll(currentRoll);
            } else {
                transform.parent.gameObject.GetComponent<DiceBlock>().PassAlongRoll(currentRoll);
            }
            Destroy(gameObject, 1.0f);
        }
    }

    public void AskForDirection(Transform t) {
        followers.Add(t);
        t.GetComponent<DiceBlock>().comTimer = this.comTimer;
    }

    public void PassAlongRoll(int i) {
        if (leader) {
            transform.parent.gameObject.GetComponent<Player>().SendRoll(i);
        }
    }
}
