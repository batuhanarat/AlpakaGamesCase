using DependencyInjection;
using UnityEngine;

public class Provider : MonoBehaviour, IDependencyProvider
{

    [Provide]
    public RaycastBatchProcessor ProvideRaycastBatchProcessor(){
        return new RaycastBatchProcessor();
    }

    private Wallet _wallet;

    [Provide]
    public IWallet ProvideWallet() {
        return _wallet ?? new Wallet();
    }

}