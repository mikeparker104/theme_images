namespace ThemeImagesSample;

public sealed class ThemeManager
{
	const string ThemePreferencesKey = "selected_theme";

	public static ThemeManager Shared { get; } = new ThemeManager();

    readonly object _lockObj = new();

    public readonly List<Theme> Themes = new()
	{
        new Theme(nameof(RedTheme), new RedTheme()),
        new Theme(nameof(GreenTheme), new GreenTheme()),
        new Theme(nameof(BlueTheme), new BlueTheme())
    };

    public Theme SelectedTheme { get; private set; }

    bool _initialized = false;

    ThemeManager()
    {
        SelectedTheme ??= Themes.First();
    }

    internal void Init()
    {
        if (_initialized)
            return;

        lock (_lockObj)
        {
            if (_initialized)
                return;

            if (Application.Current is not Application application)
                throw new Exception($"The {nameof(ThemeManager)} {nameof(Init)} method was called before the application has been fully initialized");

            var theme = Preferences.Get(ThemePreferencesKey, nameof(RedTheme));

            if (Themes.Any(i => i.Key == theme))
                SelectedTheme = Themes.Single(x => x.Key == theme);

            if (application.Resources.MergedDictionaries.Contains(SelectedTheme.Resources))
                application.Resources.MergedDictionaries.Remove(SelectedTheme.Resources);

            application.Resources.MergedDictionaries.Add(SelectedTheme.Resources);

            _initialized = true;
        }
    }

    public void SetTheme(Theme theme)
    {
        Init();

        if (Application.Current is not Application application)
            throw new Exception($"The {nameof(ThemeManager)} {nameof(SetTheme)} method was called before the application has been fully initialized");

        if (!Themes.Contains(theme))
            throw new Exception($"The specified theme, {nameof(theme)}, is not supported");

        if (application.Resources.MergedDictionaries.Contains(SelectedTheme.Resources))
            application.Resources.MergedDictionaries.Remove(SelectedTheme.Resources);

        application.Resources.MergedDictionaries.Add(theme.Resources);

        SelectedTheme = theme;
        Preferences.Set(ThemePreferencesKey, theme.Key);
    }
}