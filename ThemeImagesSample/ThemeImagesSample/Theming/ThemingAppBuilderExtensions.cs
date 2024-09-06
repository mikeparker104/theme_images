using Microsoft.Maui.LifecycleEvents;

namespace ThemeImagesSample;

public static class ThemingAppBuilderExtensions
{
	public static MauiAppBuilder UseTheming(this MauiAppBuilder builder)
	{
		builder.ConfigureLifecycleEvents(lifecycle =>
		{
#if IOS || MACCATALYST
			lifecycle.AddiOS(ios =>
			{
				ios.FinishedLaunching((app, options) =>
				{
                    ThemeManager.Shared.Init();
                    return true;
				});
			});
#elif ANDROID
			lifecycle.AddAndroid(android => android.OnCreate((activity, bundle) => ThemeManager.Shared.Init()));
#elif WINDOWS
			lifecycle.AddWindows(windows => windows.OnLaunched((app, args) =>  => ThemeManager.Shared.Init()));
#endif
		});

		return builder;
	}
}