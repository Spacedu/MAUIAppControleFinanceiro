using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;
    private ITransactionRepository _repository;
    public TransactionEdit(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
    }
    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        if(transaction.Type == TransactionType.Income)
            RadioIncome.IsChecked = true;
        else
            RadioExpense.IsChecked = true;

        EntryName.Text = transaction.Name;
        DatePickerDate.Date = transaction.Date.Date;
        EntryValue.Text = transaction.Value.ToString();
    }
    private void TapGestureRecognizerTapped_ToClose(object sender, TappedEventArgs e)
    {
        KeyboardFixBugs.HideKeyboard();
        Navigation.PopModalAsync();
    }
    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
            return;

        SaveTransactionInDatabase();

        KeyboardFixBugs.HideKeyboard();
        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new Transaction()
        {
            Id = _transaction.Id,
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = Math.Abs(double.Parse(EntryValue.Text))
        };

        _repository.Update(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }
        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }
        double result;
        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }


        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }
        return valid;
    }
}