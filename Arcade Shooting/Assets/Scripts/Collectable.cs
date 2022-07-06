using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Collectable : MonoBehaviour
{

    [SerializeField]
    CollectableItem collectableItem;

    [SerializeField]
    AudioClip coinSound;

    AudioSource audioSource;
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 200 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TakeCoinCoroutine());
        }

    }
    IEnumerator TakeCoinCoroutine()
    {
        meshRenderer.enabled = false;
        audioSource.PlayOneShot(coinSound);
        yield return new WaitWhile(() => audioSource.isPlaying);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        meshRenderer.enabled = true;
    }
    public int getValue()
    {
        return collectableItem.Value;
    }
}
