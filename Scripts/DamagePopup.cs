using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    Player player;
    [SerializeField]
    float textSpeed = 5;

    //get the tmpro
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        player = FindObjectOfType<Player>();
    }
    //puts the damage to text
    public void SetDamage(float damage)
    {
        textMesh.SetText(damage.ToString());
        Invoke("DestroyText", 1);
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }

    //moves the text up
    private void Update()
    {
        transform.position += new Vector3(0, textSpeed, 0) * Time.deltaTime;
        transform.LookAt(player.transform);
        transform.rotation = player.transform.rotation;
    }
}
