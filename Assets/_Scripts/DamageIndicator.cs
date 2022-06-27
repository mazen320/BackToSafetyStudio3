using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public float lifeTime = 2f;
    public float minDist;
    public float maxDist;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        initialPosition = transform.position;
        float distance = Random.Range(minDist, maxDist);//Distance the number will travel after spawning.
        targetPosition = initialPosition + (Quaternion.Euler(0, 0, direction) * new Vector3(distance, distance, 0f));//Numbers rotate a bit.
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float fraction = lifeTime / 2f;
        if(timer > lifeTime)
        {
            Destroy(this.gameObject);
        }
        else if(timer > fraction)
        {
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        }

        transform.position = Vector3.Lerp(initialPosition, targetPosition, Mathf.Sin(timer / lifeTime));//Numbers move.
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));//Numbers rotate.
    }
    public void SetDamageText(float damage)
    {
        text.text = damage.ToString();
    }
}
