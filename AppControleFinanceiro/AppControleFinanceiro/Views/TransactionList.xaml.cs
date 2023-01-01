using AppControleFinanceiro.Repositories;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	private TransactionAdd _transactionAdd;
	private TransactionEdit _transactionEdit;
	private ITransactionRepository _repository;
    public TransactionList(TransactionAdd transactionAdd, TransactionEdit transactionEdit, ITransactionRepository repository)
	{
		this._transactionAdd = transactionAdd;
		this._transactionEdit = transactionEdit;
		this._repository = repository;

		InitializeComponent();

        CollectionViewTransactions.ItemsSource = _repository.GetAll();
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