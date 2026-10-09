using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    public GameObject player;
    private float originalHealth;
    private GameObject border;
    private GameObject background;
    private GameObject bar;
    private GameObject display;
    private float originalHeight;
    private float originalWidth;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("Couldn't get the player instance");
            Destroy(this.gameObject);
        }

        border = GameObject.Find("HealthBarBorder");
        background = GameObject.Find("HealthBarBackground");
        bar = GameObject.Find("HealthBarFill");
        display = GameObject.Find("Display");

        originalHealth = player.GetComponent<PlayerShip>().health;
        originalHeight = bar.GetComponent<RectTransform>().sizeDelta.y;
        originalWidth = bar.GetComponent<RectTransform>().sizeDelta.x;

        //float screenHeight = Screen.height;
        //RectTransform picture = border.GetComponent<RectTransform>();
        //picture.transform.position = new Vector2(picture.transform.position.x, screenHeight);
    }

    void Update()
    {
        if (player != null)
        {
            RectTransform picture = bar.GetComponent<RectTransform>();
            float perc = (float)player.GetComponent<PlayerShip>().health / originalHealth;
            //Debug.Log(perc);
            picture.sizeDelta = new Vector2(perc * originalWidth, originalHeight);
            display.GetComponent<TextMeshProUGUI>().text = "Health: " + player.GetComponent<PlayerShip>().health + " / " + originalHealth;
        }
    }
}
