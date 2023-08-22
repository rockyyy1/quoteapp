using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Rocky_Trang___Session_4;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage()
	{
		InitializeComponent();
	}
    public ObservableCollection<Quotes> favouriteQuotes = new ObservableCollection<Quotes>();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        favouriteQuoteList.ItemsSource = favouriteQuotes;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

    }


    private void backButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
        MainPage.save();
    }

    private void favouriteQuoteList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Quotes selectedQuote = favouriteQuotes[e.ItemIndex];
        favouriteQuotes.Remove(selectedQuote);
        int mainIndex = MainPage.quoteList.IndexOf(selectedQuote);
        MainPage.quoteList[mainIndex].favourite = false;
    }
}