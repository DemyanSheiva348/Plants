using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 4f;
    public GameObject Plant;
    private GameObject currentPlant; // Ссылка на последнее посаженное растение
    private bool growed = false;

    void Update()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(MoveX, 0, MoveY) * speed;

        if (Input.GetKeyDown(KeyCode.E)) // GetKeyDown, чтобы создать 1 объект за нажатие
        {
            currentPlant = Instantiate(Plant, transform.position, Quaternion.identity);
            currentPlant.GetComponent<Renderer>().material.color = Color.yellow;
            StartCoroutine(ChangeSizeAfterDelay(currentPlant, 10f));
        }

        // Проверяем нажатие R и удаляем объект, если он вырос
        if (growed && Input.GetKeyDown(KeyCode.R))
        {
            Destroy(currentPlant);
            growed = false; // Сбрасываем флаг после удаления
        }
    }

    IEnumerator ChangeSizeAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            obj.transform.localScale *= 5;
            obj.GetComponent<Renderer>().material.color = Color.green;
            growed = true;
        }
    }
}
