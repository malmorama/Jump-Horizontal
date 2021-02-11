using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [System.Serializable]
    public class LevelContent
    {
        public string methodName;
        public int randomNumberFrom;
        public int randomNumberTo;
        public string objRespawn;
        public string objOnTop;
    }

    public int levelIDHeader;
    public int randomNumberGeneratedFrom;
    public int randomNumberGeneratedTo;
    public List<LevelContent> levelcontent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverGeneratorMetod(int levelID, Collider2D collider)
    {
        if (levelID == levelIDHeader)
        {
            print(levelID + collider.gameObject.name);
        }
    }


    private void RespawnObjectWithObjectOnTop(Collider2D collision, string objectThatHitCollider, string objectBelowToRespawn, string objectAboveToRespawn)
    {
        //if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        //{
        //object below to inactivate and respawn
        collision.gameObject.SetActive(false);
        Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
        ObjectPooler.Instance.SpawnFromPool(objectBelowToRespawn, position, Quaternion.identity);

        //object above
        Vector2 position2 = new Vector2(position.x, position.y + 3);
        ObjectPooler.Instance.SpawnFromPool(objectAboveToRespawn, position2, Quaternion.identity);
        //}
    }

    private void RespawnAnotherObject(Collider2D collision, string objectThatHitCollider, string objectToRespawn)
    {
        //if (collision.gameObject.name.StartsWith(objectThatHitCollider))
        //{
        collision.gameObject.SetActive(false);
        Vector2 position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
        //newPlat = Instantiate(springPrefab, position, Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool(objectToRespawn, position, Quaternion.identity);
        //}
    }

    private void MoveObject(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector2(Camera.main.transform.position.x + (42 * Random.Range(1f, 1.1f)), Random.Range(-15f, 10f));
    }


}
