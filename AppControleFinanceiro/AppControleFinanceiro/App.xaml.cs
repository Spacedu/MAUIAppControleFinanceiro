using AppControleFinanceiro.Views;

namespace AppControleFinanceiro;

public partial class App : Application
{
	public App(TransactionList listPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(listPage);
	}
}
