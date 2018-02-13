using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoBossTrigger : MonoBehaviour {

    public GameObject wendigo;
    public GameObject wenidgoHealthBar;
    public GameObject playerCamera;
    public GameObject inputController;
    public Rigidbody player;
    public Rigidbody cameraStartPos;
    public ActivateFollowTarget startIceWall;
    public ActivateFollowTarget endIceWall;

    private Animator cameraAnim;
    private FollowTargetLerp cameraMove;
    private TakeDamage playerTakeDamage;
    private TakeDamage wendigoTakeDamage;
    private MoveInputReceiver moveInputReceiver;
    private SpellInputReceiver spellInputReceiver;
    private ComponentInputReceiver componentInputReceiver;
    private PlayerAbilities playerAbilities;

	// Use this for initialization
	void Awake () {
        wendigo.SetActive(false);
        wenidgoHealthBar.SetActive(false);
        cameraAnim = playerCamera.GetComponent<Animator>();
        cameraMove = playerCamera.GetComponent<FollowTargetLerp>();
        playerTakeDamage = player.GetComponent<TakeDamage>();
        wendigoTakeDamage = wendigo.GetComponent<TakeDamage>();
        moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
        spellInputReceiver = inputController.GetComponent<SpellInputReceiver>();
        componentInputReceiver = inputController.GetComponent<ComponentInputReceiver>();
        playerAbilities = player.GetComponent<PlayerAbilities>();
        cameraAnim.enabled = false;

	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(ActivateBoss ());
        }
    }

    IEnumerator ActivateBoss () {
        moveInputReceiver.enabled = false;
        spellInputReceiver.enabled = false;
        componentInputReceiver.enabled = false;
        playerAbilities.enabled = false;
        cameraMove.target = cameraStartPos;
        wendigo.SetActive(true);
        playerTakeDamage.enabled = false;
        wendigoTakeDamage.enabled = false;
        yield return new WaitForSeconds(0.5f);
        cameraMove.enabled = false;
        cameraAnim.enabled = true;
        cameraAnim.SetTrigger("WendigoBossStart");
        yield return new WaitForSeconds(6f);
        endIceWall.Activate();
        yield return new WaitForSeconds(3f);
        startIceWall.Activate();
        wenidgoHealthBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        wendigo.GetComponent<WendigoAI>().enabled = true;
        cameraMove.enabled = true;
        moveInputReceiver.enabled = true;
        spellInputReceiver.enabled = true;
        componentInputReceiver.enabled = true;
        playerAbilities.enabled = true;
        cameraAnim.enabled = false;
        cameraMove.target = player;
        playerTakeDamage.enabled = true;
        wendigoTakeDamage.enabled = true;
        Destroy(gameObject, 1f);
    }
}
