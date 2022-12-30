namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
	{
		Navigation.PushModalAsync(new TransactionAdd());
	}

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new TransactionEdit());
    }
}