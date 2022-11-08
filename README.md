# CALCULATOR 
## Assignment 1: CS-797R Cross Platform Mobile Development

Team Members:

1. Vedasree Kommindala - vedasree.kommindala@gmail.com - vedasree-kommindala
     
     
   * Created private GitHub repository "Calculator_Project" for the project.
   * Added Flyout Header to the existing basic Calculator project and given labels.
      
<br/>
   
 <img width="708" alt="image" src="https://user-images.githubusercontent.com/114097793/193737006-dfa413f6-26a7-4940-9b62-12bbe8bae723.png">
 
<br/>

  ```
   <?xml version = "1.0" encoding = "UTF-8" ?>
<Shell
    x:Class="Calculator.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:Calculator">
    <Shell.FlyoutHeader>
        <Grid HeightRequest="50" BackgroundColor="{DynamicResource fgColor}">
        </Grid>
    </Shell.FlyoutHeader>
    <FlyoutItem Title = "Home">
        <ShellContent
            ContentTemplate = "{DataTemplate local:MainPage}" />
    </FlyoutItem>
    <FlyoutItem Title = "Advanced Page">
        <ShellContent
            ContentTemplate = "{DataTemplate local:AddedFunc}" />
    </FlyoutItem>
    <FlyoutItem Title = "About">
        <ShellContent
            ContentTemplate = "{DataTemplate local:MainPage}" />
    </FlyoutItem>
    <MenuItem Text="Onlight" Clicked="LightTheme" />
    <MenuItem Text="OnDark" Clicked="DarkTheme" />
    <MenuItem Text="Green" Clicked="GreenTheme" />
    <MenuItem Text="Pink" Clicked="PinkTheme" />

</Shell>
  ```

   * Defined and added Green and Pink Themes to the Flyout header
 <br/>
  
  <img width="710" alt="image" src="https://user-images.githubusercontent.com/114097793/193740350-600c116f-06eb-487a-b6c8-b9cce0f317da.png">
 <br/>
  
  <img width="707" alt="image" src="https://user-images.githubusercontent.com/114097793/193740397-879f10b6-a549-404a-8416-6951c2f39eac.png">
  
  ```
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
  
  ```

    
    
  
      










2. Venkata Pruthvi Teja Pandugayala - pvteja1@gmail.com - pvteja
3. Praneeth Ranga - rangagufus2021@gmail.com - PraneethRanga


