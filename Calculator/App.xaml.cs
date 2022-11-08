using Color = Microsoft.Maui.Graphics.Color;

namespace Calculator;

public partial class App : Application
{
    private Color colorwhite = Colors.White;
    private Color colorblack = Colors.Black;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

   
}
