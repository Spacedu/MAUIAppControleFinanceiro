using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
    /*
		* Publisher - Subscribers -> 
		* TransactionAdd -> Publisher > Cadastro (Mensagem > Transaction).
		* OK - Subscribers -> TransactionList (Receba o Transaction).
	*/
    private ITransactionRepository _repository;
    public TransactionList(ITransactionRepository repository)
	{
		this._repository = repository;

		InitializeComponent();

        Reload();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reload();
        });
	}

	private void Reload()
	{
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