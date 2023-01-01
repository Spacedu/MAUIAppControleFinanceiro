namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	private TransactionAdd _transactionAdd;
	private TransactionEdit _transactionEdit;
    public TransactionList(TransactionAdd transactionAdd, TransactionEdit transactionEdit)
	{
		this._transactionAdd = transactionAdd;
		this._transactionEdit = transactionEdit;

		InitializeComponent();
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
	{
		Navigation.PushModalAsync(_transactionAdd);
	}

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionEdit);
    }
}