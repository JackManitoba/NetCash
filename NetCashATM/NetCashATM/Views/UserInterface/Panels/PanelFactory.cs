
namespace NetCashATM.UserInterface.Panels
{
    public class PanelFactory
    {
        public ATMPanel GetPanel(string identifier)
        {
            ATMPanel p = null;
            switch (identifier)
            {
                case "BalancePanel":
                    {
                        p = new BalancePanel();
                        break;
                    }
                case "DepositPanel":
                    {
                        p = new DepositPanel();
                        break;
                    }
                case "MainPanel":
                    {
                        p = new MainPanel();
                        break;
                    }
                case "PrintInfo":
                    {
                        p = new PrintInfo();
                        break;
                    }
                case "PinPanel":
                    {
                        p = new PinPanel();
                        break;
                    }
                case "WithdrawalPanelError1":
                    {
                        p = new WithdrawalPanelATMError();
                        break;
                    }
                case "WithdrawalPanelError2":
                    {
                        p = new WithrawalPanelATMFundsAvailableError();
                        break;
                    }
                case "PinResetPanel":
                    {
                        p = new PinResetPanel();
                        break;
                    }
                case "CardCancelledPanel":
                    {
                        p = new CardCancelledPanel();
                        break;
                    }
                case "DepositErrorPanel":
                    {
                        p = new DepositErrorPanel();
                        break;
                    }
                case "PinRetryPanel":
                    {
                        p = new PinRetryPanel();
                        break;
                    }
                case "WithdrawalPanel":
                    {
                        p = new WithdrawalPanel();
                        break;
                    }
                case "LogoutPanel":
                    {
                        p = new LogoutPanel();
                        break;
                    }
            }

            return p;
        }
    }
}
