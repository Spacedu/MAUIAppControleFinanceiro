using AppControleFinanceiro.Models;
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
		var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;

		double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
		double expense = items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
		double balance = income - expense;

		LabelIncome.Text = income.ToString("C");
		LabelExpense.Text = expense.ToString("C");
		LabelBalance.Text = balance.ToString("C");
    }

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
	{
		
		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
	}

    private void TapGestureRecognizerTapped_To_TransactionEdit(object sender, TappedEventArgs e)
    {
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
		Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
		transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }
}