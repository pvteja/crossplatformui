


using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Calculator;
public partial  class SimpleExercise :ContentPage
{
    private Color lightgray = Colors.LightGray;
    private Color lightgreen = Colors.LightGreen;
    private Color transparent = Colors.Transparent;
    private Color green = Colors.Green;
    private Color red = Colors.Red;
   // public Exercise exerciseListJson;
    int i=0;
    int correct = 0;
    public List<Exercise> exerciseList;


    public SimpleExercise()
    {
        GetExercise();
       InitializeComponent();





    }

    private void onFocusButton(object sender, EventArgs e)
    {
    
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }



    private async  void GetExercise()
    {

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("https://localhost:7058/Project2API");
        exerciseList= await response.Content.ReadFromJsonAsync<List<Exercise>>();
       Debug.WriteLine(exerciseList);
        Debug.WriteLine(exerciseList.GetType());

        //  exerciseList=listExe.ToArray();
        /*  try
          {
              // Online
               var message = await client.GetStringAsync("https://localhost:7058/Project2API");
              exerciseList=message;
              Debug.WriteLine(message);
              if (response.IsSuccessStatusCode)
              {
                  exerciseList = await response.Content.ReadFromJsonAsync<List<Exercise>>();

              }

              // Offline
              using var stream = await FileSystem.OpenAppPackageFileAsync("ExerciseData.json");
              using var reader = new StreamReader(stream);
              var contents = reader.ReadToEnd();
              exerciseList = JsonSerializer.Deserialize<List<Exercise>>(contents); 

              Debug.WriteLine("=================>", contents); 
          }
          catch (Exception ex)
          {
              Debug.WriteLine($"Unable to get execises: {ex.Message}");
              await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
          } */

    }


    private void DisplayGrid(int ind)
    {

        this.id.Text ="Question "+exerciseList[ind].id;
        this.question.Text="Q)"+((exerciseList[ind].question).Replace('*', 'x'));
        this.optionA.Text=exerciseList[ind].optionA;
        this.optionB.Text=exerciseList[ind].optionB;
        this.optionC.Text=exerciseList[ind].optionC;
        

    }

    private async void Option_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        Debug.WriteLine("Pressed"+pressed);
        Debug.WriteLine(exerciseList[i].answer.ToString());
        Debug.WriteLine(exerciseList[i].id+" , "+ exerciseList[i].question+" , "+exerciseList[i].optionA+" , "+exerciseList[i].optionB+" , "+exerciseList[i].optionC+" , "+exerciseList[i].answer);
        DisableOptions();
        if (pressed == exerciseList[i].answer)
        {
            button.BackgroundColor=lightgreen;
            this.result.IsVisible=true;
            this.result.Text="Correct";
            this.BackgroundImageSource = "celebrations2.gif";
            this.result.TextColor= lightgreen;
            correct=correct+1;
            await Task.Delay(2000);
            Next_Clicked(Next_Button,e);
        }
        else
        {
            button.BackgroundColor=lightgray;
            this.result.IsVisible=true;
            this.result.Text="Wrong";
            this.tryAgain.IsVisible=true;
            this.Next_Button.IsVisible=true;
            this.result.TextColor= lightgray;
        }
        this.score.Text="Score: "+correct+"/"+exerciseList.Count();


    }
    private void TryAgain_Clicked(object sender, EventArgs e)
    {
        EnableOptions();
        ButtonTransparent();
        this.result.Text="";
        this.tryAgain.IsVisible=false;
        this.Next_Button.IsVisible=false;
    }

    private void Start_Clicked(object sender, EventArgs e)
    {

        this.Start_Button.IsVisible=false;
        this.Question_Grid.IsVisible=true;


        //  Debug.WriteLine(exerciseList[0]);
        i=0;
        correct=0;
        DisplayGrid(i);
        this.score.Text="Score:0/"+exerciseList.Count();
    }
    private void DisableOptions()
    {
        this.optionA.IsEnabled=false;
        this.optionB.IsEnabled=false;
        this.optionC.IsEnabled=false;
    }
    private void EnableOptions()
    {
        this.optionA.IsEnabled=true;
        this.optionB.IsEnabled=true;
        this.optionC.IsEnabled=true;
    }

    private void ButtonTransparent()
    {
        this.optionA.BackgroundColor=transparent;
        this.optionB.BackgroundColor=transparent;
        this.optionC.BackgroundColor=transparent;
    }
    private void Next_Clicked(object sender, EventArgs e)
    {
        this.tryAgain.IsVisible=false;
        this.Next_Button.IsVisible=false;
        i=i+1;
        if (i==exerciseList.Count())
        {
            EndofExercise();
            return;
        }
        EnableOptions();
        
        ButtonTransparent();
        this.result.Text="";
        
        DisplayGrid(i);
        this.BackgroundImageSource="background.png";
    }
    private void EndofExercise()
    {
        this.Question_Grid.IsVisible=false;
        if (correct>6)
           this.BackgroundImageSource="celebrations2.gif";
        else
            this.BackgroundImageSource="background.png";
        this.finalScore.IsVisible=true;
        this.finalScore.Text="Final Score is :"+correct+"/"+exerciseList.Count();
        this.quitApp.IsVisible=true;
        this.StartAgain.IsVisible=true;
    }

    private async void StartAgain_Clicked(object sender, EventArgs e)
    {
        /* GetExercise();
         i=0;
         correct=0;
         this.quitApp.IsVisible=false;
         this.StartAgain.IsVisible=false;
         this.Question_Grid.IsVisible=true;
         this.BackgroundImageSource="background.png";
         this.finalScore.IsVisible=false;
         this.score.Text="Score: 0/10";
         EnableOptions();

         ButtonTransparent();
         this.result.Text="";
         Debug.WriteLine("i"+i);
         DisplayGrid(i); */
        bool answer = await DisplayAlert("Question?", "Would you like to Restart  the game", "Yes", "No");
        Debug.WriteLine("bool resart"+answer);
        if (answer)
        {
            GetExercise();
            this.Start_Button.IsVisible=true;
            this.Question_Grid.IsVisible=false;
            this.BackgroundImageSource="background.png";
            this.finalScore.IsVisible=false;
            EnableOptions();
            this.quitApp.IsVisible=false;
            this.StartAgain.IsVisible=false;
            ButtonTransparent();
            this.result.Text="";
            
        }


    }
    private async void  ShowStartAgain_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to Restart  the game", "Yes", "No");
        Debug.WriteLine("bool resart"+answer);
        if (answer) { 
            GetExercise();
            this.Start_Button.IsVisible=true;
            this.Question_Grid.IsVisible=false;
            this.tryAgain.IsVisible=false;
            this.Next_Button.IsVisible=false;
            EnableOptions();
            ButtonTransparent();
            this.result.Text="";
        }
    }

    private async void quitApp_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to Quit  the game", "Yes", "No");

        if(answer)
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);

    }

}