using System.Globalization;
using System.Threading;
using System.Windows;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        CultureInfo pl = new CultureInfo("pl-PL");
        Thread.CurrentThread.CurrentCulture = pl;
        Thread.CurrentThread.CurrentUICulture = pl;

        base.OnStartup(e);
    }
}
