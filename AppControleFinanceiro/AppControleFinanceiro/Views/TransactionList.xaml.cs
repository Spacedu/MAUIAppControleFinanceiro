using AppControleFinanceiro.Repositories;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	private ITransactionRepository _repository;
    public TransactionList(ITransactionRepository repository)
	{
		this._repository = repository;

		InitializeComponent();

        CollectionViewTransactions.ItemsSource = _repository.GetAll();
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
	{
		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
		Navigation.PushModalAsync(transactionAdd);
	}

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
		var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }
}