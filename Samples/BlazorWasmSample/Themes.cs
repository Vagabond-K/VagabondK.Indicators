using MudBlazor;
using MudBlazor.Utilities;

namespace BlazorWasmSample
{
    public class Themes
    {
        public static MudTheme DefaultTheme { get; } = new()
        {
            PaletteDark = new()
            {
                GrayDefault = Colors.Grey.Default,
                GrayDark = Colors.Grey.Darken1,
                GrayDarker = Colors.Grey.Darken2,
                GrayLight = Colors.Grey.Lighten1,
                GrayLighter = Colors.Grey.Lighten2,

                HoverOpacity = 0.3,

                OverlayDark = new MudColor("#212121").SetAlpha(0.5).ToString(MudColorOutputFormats.RGBA),
                OverlayLight = new MudColor(Colors.Shades.White).SetAlpha(0.5).ToString(MudColorOutputFormats.RGBA),
                TableHover = new MudColor(Colors.Shades.Black).SetAlpha(0.2).ToString(MudColorOutputFormats.RGBA),

                White = Colors.Shades.White,

                Black = Colors.Shades.Black,
                Background = "#161616",
                BackgroundGrey = "#1f1f1f",
                Surface = "#1f1f1f",
                DrawerBackground = new MudColor("#1f1f1f").SetAlpha(0.6).ToString(MudColorOutputFormats.RGBA),
                DrawerText = "#e6e6e6",
                DrawerIcon = "#e6e6e6",
                AppbarBackground = new MudColor("#272727").SetAlpha(0.6).ToString(MudColorOutputFormats.RGBA),
                AppbarText = "#e6e6e6",
                TextPrimary = "#e6e6e6",
                TextSecondary = "#cccccc",
                ActionDefault = "#e6e6e6",
                ActionDisabled = "rgba(230,230,230, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                Divider = "rgba(255,255,255, 0.24)",
                DividerLight = "rgba(255,255,255, 0.12)",
                TableLines = "rgba(255,255,255, 0.24)",
                TableStriped = "rgba(255,255,255, 0.4)",
                LinesDefault = "rgba(255,255,255, 0.24)",
                LinesInputs = "rgba(255,255,255, 0.6)",
                TextDisabled = "rgba(255,255,255, 0.4)",

                ChipDefault = new MudColor(Colors.Shades.Black).SetAlpha(0.08).ToString(MudColorOutputFormats.RGBA),
                ChipDefaultHover = new MudColor(Colors.Shades.Black).SetAlpha(0.12).ToString(MudColorOutputFormats.RGBA),

                Primary = "#e6e6e6",
                PrimaryDarken = "#aaaaaa",
                PrimaryLighten = Colors.Shades.White,
                PrimaryContrastText = Colors.Shades.Black,

                Secondary = "#1ec7c7",
                SecondaryContrastText = Colors.Shades.White,

                Tertiary = "#776be7",
                TertiaryContrastText = Colors.Shades.White,

                Info = "#3299ff",
                InfoContrastText = Colors.Shades.White,

                Success = "#0bba83",
                SuccessContrastText = Colors.Shades.White,

                Warning = "#ffa800",
                WarningContrastText = Colors.Shades.White,

                Error = "#f64e62",
                ErrorContrastText = Colors.Shades.White,

                Dark = "#333333",
                DarkContrastText = Colors.Shades.White,
            },
            Typography = new()
            {
                Default = new() { FontFamily = new[] { "IBM Plex Sans KR", "sans-serif" }, },
                H1 = new() { FontWeight = 500 },
                H2 = new() { FontWeight = 500 },
                H3 = new() { FontWeight = 500 },
                H4 = new() { FontWeight = 500 },
                H5 = new() { FontWeight = 500 },
                H6 = new() { FontWeight = 500 },
                Body1 = new() { FontFamily = new[] { "Gowun Batang", "serif" } },
                Body2 = new() { FontFamily = new[] { "Gowun Batang", "serif" } },
                Subtitle1 = new() { FontFamily = new[] { "Gowun Batang", "serif" } },
                Subtitle2 = new() { FontFamily = new[] { "Gowun Batang", "serif" } },
            }
        };
    }
}
