namespace ThemeImagesSample;

public partial class MainPage : ContentPage
{
	int _themeIndex = -1;

	public MainPage()
	{
		InitializeComponent();
    }

    void OnSwitchThemeClicked(object sender, EventArgs e)
	{
        if (_themeIndex == -1)
            _themeIndex = ThemeManager.Shared.Themes.ToList().IndexOf(ThemeManager.Shared.SelectedTheme);

        _themeIndex++;

        if (_themeIndex >= ThemeManager.Shared.Themes.Count)
            _themeIndex = 0;

        var selectedTheme = ThemeManager.Shared.Themes[_themeIndex];
        ThemeManager.Shared.SetTheme(selectedTheme);
        SemanticScreenReader.Announce($"Theme {ThemeManager.Shared.SelectedTheme.Key} set");
    }
}