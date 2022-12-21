using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOn : MonoBehaviour
{
    public GameObject prefabDamage;
    // Start is called before the first frame update
    public void DamageTxt()
    {
        GameObject inst = Instantiate(prefabDamage, this.transform);
    }
}
