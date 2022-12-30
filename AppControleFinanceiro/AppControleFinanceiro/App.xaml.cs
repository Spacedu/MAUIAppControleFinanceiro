using AppControleFinanceiro.Views;

namespace AppControleFinanceiro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new TransactionList();
	}
}
