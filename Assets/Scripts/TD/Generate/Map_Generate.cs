using System.Collections;
using UnityEngine;

public class Map_Generate : MonoBehaviour
{
    public float time;
    public int map_size;
    public int cell_size;
    public int trees_count;
    public int rocks_count;
    public int grass_count;
    public int moushrooms_count;
    public int generate_percent_done;
    public bool generate_done;

    public GameObject[] land;
    public GameObject[] mountains;
    public GameObject[] trees;
    public GameObject[] rocks;
    public GameObject[] moushrooms;
    public GameObject[] grass;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;
    public GameObject player9;
    public GameObject player10;

    public static Map_Generate Instance { get; set; } // To collect and send data from this script
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        generate_done = false;
        generate_percent_done = 0;
    }
    private void Start()
    {
        WordlGeneration();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;//General_Value.Instance.globaltime; 
        if (generate_done == false) GenerateDoneMessage();
    }
    public void WordlGeneration()
    {
        if (land[0] != null)
        {
            StartCoroutine(GroundGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found land object");
        if (mountains[0] != null)
        {
            StartCoroutine(MountainGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found mountains object");
        if (trees[0] != null)
        {
            StartCoroutine(TreesGeneration());
            StartCoroutine(TreesGeneration());
            StartCoroutine(TreesGeneration());
            StartCoroutine(TreesGeneration());
            StartCoroutine(TreesGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found trees object");
        if (grass[0] != null)
        {
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
            StartCoroutine(GrassGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found grass object");
        if (moushrooms[0] != null)
        {
            StartCoroutine(MoushroomGeneration());
            StartCoroutine(MoushroomGeneration());
            StartCoroutine(MoushroomGeneration());
            StartCoroutine(MoushroomGeneration());
            StartCoroutine(MoushroomGeneration());
            StartCoroutine(MoushroomGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found moushrooms object");
        if (rocks[0] != null)
        {
            StartCoroutine(RockGeneration());
        }
        else UnityEngine.Debug.LogWarning("Can't found rocks object");
    }
    public void GenerateDoneMessage()
    {
        if (generate_percent_done == 100)
        {
            generate_done = true;
            UnityEngine.Debug.LogWarning("World creating complete! Time needed: " + time);
        }
    }
    public IEnumerator GroundGeneration()
    {
        // Clamp map_size within a reasonable range
        if (map_size < 1) map_size = 1;
        if (map_size > 700) map_size = 700;

        Vector3 startPosition = Vector3.zero;  // The center of the grid at (0, 0, 0)

        // Calculate the offset to center the grid around (0, 0, 0)
        int halfSize = map_size / 2;

        // Loop through each position in a grid pattern around the center
        for (int x = -halfSize; x <= halfSize; x++)
        {
            for (int z = -halfSize; z <= halfSize; z++)
            {
                // Randomly pick a ground tile from the 'land' array and instantiate it at each grid position
                GameObject groundTile = Instantiate(land[Random.Range(0, land.Length)], new Vector3(startPosition.x + x, startPosition.y, startPosition.z + z), Quaternion.identity);
                groundTile.name = $"Land_{x}_{z}";  // Naming each tile uniquely based on its grid position
                groundTile.SetActive(true);
                // Optional: Yield every row to avoid freezing
                if (z % map_size == 0)
                    yield return new WaitForEndOfFrame();
            }
        }
        UnityEngine.Debug.LogWarning("Ground creating completed! Time passed: " + time);
        generate_done = true;
    }
    public IEnumerator TreesGeneration()
    {
        Vector3 position = new Vector3(0, 0, 0);
        position.x = 0;
        position.y = 0; //Y position
        position.z = 0;
        for (int i = 0; i < (map_size * trees_count); i++)
        {
            position.x = Random.Range(0, map_size * 21f);
            position.y = 2;
            position.z = Random.Range(0, map_size * 21f);
            GameObject tree = Instantiate(trees[Random.Range(0, trees.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            tree.name = "Tree_" + i;
            tree.transform.localScale = new Vector3(Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f));
            tree.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        generate_percent_done += 4;
        //UnityEngine.Debug.Log("Trees creating complete! Time needed: " + time);
    }
    public IEnumerator GrassGeneration()
    {
        Vector3 position = new Vector3(0, 0, 0);
        position.x = 0;
        position.y = 0; //Y position
        position.z = 0;
        for (int i = 0; i < (map_size * grass_count); i++)
        {
            position.x = Random.Range(0, map_size * 21f);
            position.y = 2.2f;
            position.z = Random.Range(0, map_size * 21f);
            GameObject flower = Instantiate(grass[Random.Range(0, grass.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            flower.name = "Tree_" + i;
            flower.transform.localScale = new Vector3(Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f));
            flower.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        generate_percent_done += 4;
        //UnityEngine.Debug.Log("Grass creating complete! Time needed: " + time);
    }
    public IEnumerator MoushroomGeneration()
    {
        Vector3 position = new Vector3(0, 0, 0);
        position.x = 0;
        position.y = 0; //Y position
        position.z = 0;
        for (int i = 0; i < (map_size * moushrooms_count); i++)
        {
            position.x = Random.Range(0, map_size * 21);
            position.y = 2.2f;
            position.z = Random.Range(0, map_size * 21);
            GameObject moushroom = Instantiate(moushrooms[Random.Range(0, moushrooms.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            moushroom.name = "Moushroom_" + i;
            moushroom.transform.localScale = new Vector3(Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f), Random.Range(1.0f, 2.0f));
            moushroom.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        generate_percent_done += 4;
        //UnityEngine.Debug.Log("Moushroom creating complete! Time needed: " + time);
    }
    public IEnumerator RockGeneration()
    {
        Vector3 position = new Vector3(0, 0, 0);
        position.x = 0;
        position.y = 0; //Y position
        position.z = 0;
        for (int z = 0; z < (map_size * rocks_count); z++)
        {
            position.x = Random.Range(0, map_size * 21);
            position.y = 2;
            position.z = Random.Range(0, map_size * 21);
            GameObject rock = Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            rock.name = "Rock_" + z;
            rock.transform.localScale = new Vector3(Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f));
            rock.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        generate_percent_done += 4;
        //UnityEngine.Debug.Log("Rock creating complete! Time needed: " + time);
    }
    public IEnumerator MountainGeneration()
    {
        Vector3 position = new Vector3(0, 0, 0);
        position.x = 0;
        position.y = 0; //Y position
        position.z = - 21;
        for (int i = 0; i < map_size; i++)
        {
            position.x += 21;
            position.y = 0;
            position.z = 0;
            GameObject mountain = Instantiate(mountains[Random.Range(0, mountains.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            mountain.name = "Mountain_" + i;
            mountain.gameObject.SetActive(true); 
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        for (int i = 0; i < map_size; i++)
        {
            position.x = map_size * 21;
            position.y = 0;
            position.z += 21 ;
            GameObject mountain = Instantiate(mountains[Random.Range(0, mountains.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            mountain.name = "Mountain_" + i;
            mountain.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        for (int i = 0; i < map_size; i++)
        {
            position.x -= 21;
            position.y = 0;
            position.z = map_size * 21; ;
            GameObject mountain = Instantiate(mountains[Random.Range(0, mountains.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            mountain.name = "Mountain_" + i;
            mountain.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        for (int i = 0; i < map_size; i++)
        {
            position.x = 0;
            position.y = 0;
            position.z -= 21;
            GameObject mountain = Instantiate(mountains[Random.Range(0, mountains.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
            mountain.name = "Mountain_" + i;
            mountain.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); // waiting for blocks to be installed
        }
        generate_percent_done += 4;
        //UnityEngine.Debug.Log("Mountain creating complete! Time needed: " + time);
    }
}