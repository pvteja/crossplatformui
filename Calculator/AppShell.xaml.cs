
namespace Calculator;

public partial class AppShell : Shell
{
    private Color colorwhite = Colors.White;
    private Color colorblack = Colors.Black;
    private Color colorpapaya = Colors.PapayaWhip;
    private Color colorolive = Colors.OliveDrab;
    private Color colorpink = Colors.DeepPink;
    private Color colorkhaki = Colors.Lavender;
    public AppShell()
    {
        InitializeComponent(); 

    }

    public void LightTheme(object sender, EventArgs e)
    {
        Resources["fgColor"] = colorblack;
        Resources["bgColor"] = colorwhite;
    }

    public void DarkTheme(object sender, EventArgs e)
    {
        Resources["fgColor"] = colorwhite;
        Resources["bgColor"] = colorblack;
    }
    public void GreenTheme(object sender, EventArgs e)
    {
        Resources["bgColor"] = colorpapaya;
        Resources["fgColor"] = colorolive;
    }
    public void PinkTheme(object sender, EventArgs e)
    {
        Resources["fgColor"] = colorpink;
        Resources["bgColor"] = colorkhaki;
    }

}
