namespace Calculator;

public partial class AddedFunc : ContentPage
{

    public AddedFunc()
    {
        InitializeComponent();
        OnClear(this, null);

    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";
    int n = 0;

    Stack<String> stack = new Stack<String>();


    async void OnParenthesis(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;



        this.CurrentCalculation.Text += pressed;
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
    void OnSelectNumber(object sender, EventArgs e)
    {
        if (currentState == -1)
        {
            this.CurrentCalculation.Text="";
        }
        Button button = (Button)sender;
        string pressed = button.Text;
        this.CurrentCalculation.Text += pressed;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {

            this.resultText.Text = "";

            if (currentState < 0)
            {
                currentState *= -1;
            }
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;
    }

    async void OnSelectOperator(object sender, EventArgs e)
    {
        n = n + 1;
        if (n < 2)
        {
            LockNumberValue(resultText.Text);

            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            this.CurrentCalculation.Text += pressed;


            mathOperator = pressed;
        }
        else
        {
            await DisplayAlert("Alert", " Performs operations on only two operands", "OK");
            OnClear(this, null);
        }
        currentEntry=string.Empty;

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

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
        CurrentCalculation.Text = "";
        stack.Clear();
        n = 0;
    }

    async void OnCalculate(object sender, EventArgs e)
    {
        if (stack.Count == 0)
        {


            if (currentState == 2)
            {



                LockNumberValue(resultText.Text);

                double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

                this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

                this.resultText.Text = result.ToString();
                firstNumber = result;
                secondNumber = 0;
                currentState = -1;
                stack.Clear();
                n = 0;

            }
        }
        else
        {
            await DisplayAlert("Alert", "Parenthesis Matching Error", "OK");
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

    void OnSqrt(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            double result = Calculator.Calculate(firstNumber, 0, "Sqrt");

            this.CurrentCalculation.Text = $"Sqrt {firstNumber}";

            this.resultText.Text = result.ToString();
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;
            stack.Clear();
            currentEntry=string.Empty;


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
