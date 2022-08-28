using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;


public class IslandManager : MonoBehaviour
{
    [SerializeField] public int time;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject grabberPrefab;
    [SerializeField] private GameObject easterEggGrabberPrefab;

    //[SerializeField] private TextMeshProUGUI  coinText;
    //[SerializeField] private TextMeshProUGUI  repText; IN UILayer
    [SerializeField] private Light2D sun;
    [SerializeField] private GameObject playerLight;

    [SerializeField] private GameObject lights;
    //EVENT MANAGER
    private EventManager eventManager;


    private void Awake()
    {
        timeText.text = "Time: " + time;
        sun = GameObject.FindGameObjectWithTag("Light").GetComponent<Light2D>();
        //playerLight  = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        //playerLight = FindObjectOfType<Player>().GetComponentInChildren<Light2D>().gameObject;
        eventManager = FindObjectOfType<EventManager>();

        LightControl();
    }


    private void LockAllEvents()
    {
        Debug.Log("EVENTS ARE LOCKED!");
        foreach (Event @event in eventManager.allEvents)
        {
            if (!@event.isChallenge)
            {
                @event.LockEvent();
            }
        }
    }


    //ZNIŽOVANIE ČASU O LOWERBY
    public void LowerTime(int lowerBy)
    {
        time -= lowerBy;

        LightControl();
        //AK VYPRŠÍ TAK SPAWNI RUKU
        if (time <= 0)
        {
            time = 0;
            LockAllEvents();
            Invoke(nameof(StartGrabber), 20f);

        }

        timeText.text = "Time: " + time;
    }

    private void StartGrabber()
    {
        if (EventManager.IsInEvent)
        {
            Invoke(nameof(StartGrabber), 20f);
            return;
        }
        //GameObject grabber = Instantiate(grabberPrefab, transform.position, Quaternion.identity) as GameObject;
        //grabber.transform.position = new Vector3(grabber.transform.position.x, grabber.transform.position.y, 20);
        //Debug.Log(grabber.transform.position.z);
        Instantiate(grabberPrefab);
        Invoke(nameof(GrabberEasterEgg), 2f);
    }

    private void GrabberEasterEgg()
    {
        //TODO EASTER EGG
        Debug.Log(nameof(GrabberEasterEgg) + "!");
        Instantiate(easterEggGrabberPrefab);
        Invoke(nameof(StartBattle), 10f);
        
    }

    //MENENIE SVETIEL PODLA CASU NA MAPE
    private void LightControl()
    {
        sun.intensity = 0.2f + time * 0.2f;

        if (sun.intensity < 0.6)
        {
            lights.SetActive(true);
            //playerLight.SetActive(true);

        }
        else
        {
            lights.SetActive(false);
            //playerLight.SetActive(false);

        }
    }

    //SPUSTI SCÉNU BOJA
    public void StartBattle()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.isDead = false;
            player.isVictorious = false;
        }
        //Time.timeScale = 1;
        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
    }
}
