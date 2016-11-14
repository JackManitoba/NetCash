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
                case "PinResetPanel":
                    {
                        p = new PinResetPanel();
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
