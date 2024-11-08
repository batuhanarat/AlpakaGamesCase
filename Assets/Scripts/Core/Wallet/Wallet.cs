public interface IWallet
{
    public float Balance { get;  }
    public void Deposit(float amount);
    public bool TryWithdraw(float amount);
    public bool CanAfford(float amount);
    public void Initialize(WalletData data);
}

public class Wallet: IWallet
{
    private WalletData _walletData;
    public float Balance
    {
        get => _walletData.totalValue;
        private set => _walletData.totalValue = value;
    }
    public void Initialize(WalletData walletData)
    {
        _walletData = walletData;
    }
    public void Deposit(float amount)
    {
        Balance += amount;
        RaiseBalanceChangedEvent();
    }
    public bool TryWithdraw(float amount)
    {
        if(CanAfford(amount)) {
            Balance -= amount;
            RaiseBalanceChangedEvent();
            return true;
        }
        return false;
    }
    public bool CanAfford(float amount) {
        return Balance >= amount;
    }
    private void RaiseBalanceChangedEvent()
    {
        EventBus<BalanceChangedEvent>.Raise(new BalanceChangedEvent{
            newBalance = Balance});
    }

}


