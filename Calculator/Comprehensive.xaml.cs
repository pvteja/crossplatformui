using System.Diagnostics;


namespace Calculator;

public partial class Comprehensive : ContentPage
{

    public Comprehensive()
    {
        InitializeComponent();
        OnClear(this, null);

    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";

    Stack<String> stack = new Stack<String>();


    async void WarningHandler(object sender, EventArgs e)
    {
        await DisplayAlert("Limited Operators", "Developed only for operands /,*,-,+,(,) . Floating number not included in input. Available in output","OK");
        OnClear(this, null);
    }

    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0"))
        {
            this.resultText.Text ="";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;
    }

   async void OnParanthesisCheck(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        currentEntry = pressed;
      
        if ((this.resultText.Text == "0"))
        {
            this.resultText.Text ="";
         
        }
        this.resultText.Text += pressed;
       // await DisplayAlert("Alert",currentEntry,"OK");
       // await DisplayAlert("Alert", resultText.Text, "OK");

        if (currentEntry == ")")
        {
            if (stack.Count == 0 || stack.Pop() != "(")
            {
                await DisplayAlert("Alert", "Parenthesis Matching Error", "OK");
                OnClear(this, null);
            }
        }
        else
        {
            stack.Push(currentEntry);
        }

        currentEntry = string.Empty;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        // LockNumberValue(resultText.Text);

        // currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed;
        this.resultText.Text += pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            // currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        //firstNumber = 0;
        // secondNumber = 0;
        // currentState = 1;
        // decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
    }

    async void OnCalculate(object sender, EventArgs e)
    {
        if (stack.Count == 0)
        {
          //  await DisplayAlert("Alert", stack.Count.ToString(), "OK");
           // await DisplayAlert("Alert", resultText.Text.ToString(), "OK");

            float value = ExprCalculator.Calculate(resultText.Text);






        this.CurrentCalculation.Text = $"{resultText.Text}";
        this.resultText.Text = value.ToString();
        currentEntry = string.Empty;

        }
        else
        {
            await DisplayAlert("Alert", "Parenthesis Matching error", "OK");
            OnClear(this, null);

        }
    }


    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }


}
