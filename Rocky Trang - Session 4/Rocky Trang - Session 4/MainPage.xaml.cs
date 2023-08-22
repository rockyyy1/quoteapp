using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Rocky_Trang___Session_4;

public partial class MainPage : ContentPage
{
	public static string fileName = "quoteFile.txt";
	public static List<Quotes> quoteList = new List<Quotes>();
	bool favourited = false;
	Quotes randomQuote;

	public MainPage()
	{
		InitializeComponent();
		load();
		displayRandomQuote();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		displayRandomQuote();
    }

    public static void save()
	{
		var localFolder = FileSystem.Current.AppDataDirectory;
		var filePath = Path.Combine(localFolder, fileName);
		String jsonString = JsonConvert.SerializeObject(quoteList);
        File.WriteAllText(filePath, jsonString);
	}

	void load()
	{
        var localFolder = FileSystem.Current.AppDataDirectory;
        var filePath = Path.Combine(localFolder, fileName);
		Debug.WriteLine("filepath:", filePath);
		String fileContents;
		try
		{
			fileContents = File.ReadAllText(filePath);
           
		}
		catch (Exception) 
		{
			Debug.WriteLine("Exception caught: No File found");
            Quotes sampleQuote = new Quotes("There's no place like home", "Dorothy", false);
            quoteList.Add(sampleQuote);
            save();
            return;
		}

        quoteList = JsonConvert.DeserializeObject<List<Quotes>>(fileContents);
        Debug.WriteLine(fileContents);
    }
	void displayRandomQuote()
	{
        Random random = new Random();
        int index = random.Next(quoteList.Count);
        randomQuote = quoteList[index];
        quoteLabel.Text = randomQuote.quote;
        authorLabel.Text = randomQuote.author;
		favourited = randomQuote.favourite;
        if (favourited)
        {
            favouriteButton.Source = "star_shaded.png";
        }
        else if (!favourited)
        {
            favouriteButton.Source = "star_unshaded.png";
        }
    }
    private async void randomQuotebutton_Clicked(object sender, EventArgs e)
    {
		Random random = new Random();
		if (quoteList == null)
		{
			quoteList = new List<Quotes>();
            Quotes sampleQuote = new Quotes("There's no place like home", "Dorothy", false);
            quoteList.Add(sampleQuote);
            save();
        }
		try
		{
			int index = random.Next(quoteList.Count);
			randomQuote = quoteList[index];
			quoteLabel.Text = randomQuote.quote;
			authorLabel.Text = randomQuote.author;
			favourited = randomQuote.favourite;
			if (favourited)
			{
				favouriteButton.Source = "star_shaded.png";
			}
			else if (!favourited)
			{
				favouriteButton.Source = "star_unshaded.png";
			}
		}

		catch (FileNotFoundException)
		{
			Debug.WriteLine("Exception caught: No File found");
			return;
		}
		catch (NullReferenceException)
		{
			await DisplayAlert("Error", "There are no quotes in the library", "OK");
			return;
		}
		catch ( Exception ex)
		{
			return;
		}
    }

	
    private async void saveQuoteButton_Clicked(object sender, EventArgs e)
    {
		if (quoteEntry.Text == null || authorEntry.Text == null || quoteEntry.Text == "" || authorEntry.Text == "") 
		{
            await DisplayAlert("Error", "One or more text fields were empty", "OK");
        }
		else 
		{
			Quotes newQuote = new Quotes(quoteEntry.Text, authorEntry.Text);
			quoteList.Add(newQuote);
			save();
			quoteEntry.Text = null;
			authorEntry.Text = null;
			favourited = false;
		} 
	}

    private void favouriteButton_Clicked(object sender, EventArgs e)
    {
		
		if (!favourited)
		{
            favouriteButton.Source = "star_shaded.png";
            favourited = true;
			randomQuote.favourite = true;
			this.favourited = true;
        }
        else if (favourited)
        {
            favouriteButton.Source = "star_unshaded.png";
            favourited = false;
			randomQuote.favourite = false;
			this.favourited = false;
        }
		save();
    }

    private void seeFavouritesButton_Clicked(object sender, EventArgs e)
    {
        FavouritesPage favouritesScreen = new FavouritesPage();
		foreach (Quotes quotes in quoteList)
		{
			if(quotes.favourite == true)
			{
				favouritesScreen.favouriteQuotes.Add(quotes);
            }
        }
		Navigation.PushModalAsync(favouritesScreen);
    }
}

