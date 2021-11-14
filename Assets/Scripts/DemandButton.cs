using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandButton : MonoBehaviour
{
    public Client client;

    public void Sell()
    {
        BlackMarket.Instance.Sell(client);
        Destroy(this.transform.parent.gameObject);
    }
}
