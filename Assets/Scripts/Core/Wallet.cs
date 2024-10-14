
using System;

public class Wallet
{
    private WalletData _walletData;
    private Action<int> onUpdateUI;

    public void AddToWallet(int value) {
        _walletData.totalValue += value;
        onUpdateUI(_walletData.totalValue);
    }

    public void TryMakeUpgrade(int upgradeValue) {
        if(_walletData.totalValue >= upgradeValue) {
            _walletData.totalValue -= upgradeValue;
        }
    }

    public Wallet(WalletData walletData, Action<int> OnUpdateUI) {
        _walletData = walletData;
        onUpdateUI = OnUpdateUI;
    }

    public int GetTotalGemCount() {
        return _walletData.totalValue;
    }
}