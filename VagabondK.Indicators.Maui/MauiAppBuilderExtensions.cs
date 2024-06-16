using Microsoft.Maui.Hosting;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// MauiAppBuilder를 위한 확장 메서드 모음입니다. 
    /// </summary>
    public static class MauiAppBuilderExtensions
    {
        /// <summary>
        /// VagabondK.Indicators에서 제공하는 기능을 사용하려면 본 메서드를 호출합니다.
        /// </summary>
        /// <param name="builder">A builder for .NET MAUI cross-platform applications and services.</param>
        /// <returns>MauiAppBuilder</returns>
        public static MauiAppBuilder UseVagabondIndicators(this MauiAppBuilder builder)
            => builder.ConfigureMauiHandlers(handlers =>
            {
                handlers
                .AddHandler<DigitalTextPresenter, DigitalTextPresenterHandler>()
                .AddHandler<DigitalNumberPresenter, DigitalNumberPresenterHandler>();
            });
    }
}
