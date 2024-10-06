namespace Labs
{
    public partial class MainPage : ContentPage
    {
        int state = 1;
        string mathOperator;
        double firstNum, secondNum;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnClear(object sender, EventArgs e)
        {
            firstNum = 0;
            secondNum = 0;
            state = 1;
            this.result.Text = "0";
        }

        void OnPercent(object sender, EventArgs e)
        {
            if (secondNum == 0 || state != 2) { this.result.Text = "0"; return; }
            if (secondNum < 0) { this.result.Text = "Err"; return; }

            secondNum = secondNum / 100;
            this.result.Text = (firstNum * secondNum).ToString();
        }

        void OnBackspace(object sender, EventArgs e)
        {
            if (this.result.Text.Length > 0)
            {
                this.result.Text = this.result.Text.Remove(this.result.Text.Length - 1);
                if (this.result.Text.Length == 0)
                    this.result.Text = "0";
            }
        }

        void OnReverse(object sender, EventArgs e)
        {
            if (state == 2)
            {
                double.TryParse(this.result.Text, out secondNum);
                secondNum = 1 / secondNum;
                this.result.Text = secondNum.ToString();
            }
            else if (state == 1)
            {
                double.TryParse(this.result.Text, out firstNum);
                firstNum = 1 / firstNum;
                this.result.Text = firstNum.ToString();
            }
        }

        void OnSquarePower(object sender, EventArgs e)
        {
            if (state == 2)
            {
                double.TryParse(this.result.Text, out secondNum);
                secondNum = Math.Pow(secondNum, 2);
                this.result.Text = secondNum.ToString();
            }
            else if (state == 1)
            {
                double.TryParse(this.result.Text, out secondNum);
                firstNum = Math.Pow(firstNum, 2);
                this.result.Text = firstNum.ToString();
            }
        }

        void OnSquareRoot(object sender, EventArgs e)
        {
            if (state == 2)
            {
                if (secondNum < 0)
                {
                    this.result.Text = "Err";
                    return;
                }

                secondNum = Math.Sqrt(secondNum);
                this.result.Text = secondNum.ToString();
            }
            else if (state == 1)
            {
                if (firstNum < 0)
                {
                    this.result.Text = "Err";
                    return;
                }

                firstNum = Math.Sqrt(firstNum);
                this.result.Text = firstNum.ToString();
            }
        }

        void OnEquals(object sender, EventArgs e)
        {
            if (state == 2)
            {
                double result = 0;
                switch (mathOperator)
                {
                    case "÷":
                        if (secondNum == 0)
                        {
                            this.result.Text = "Err";
                            return;
                        }
                        result = firstNum / secondNum;
                        break;
                    case "×":
                        result = firstNum * secondNum;
                        break;
                    case "—":
                        result = firstNum - secondNum;
                        break;
                    case "+":
                        result = firstNum + secondNum;
                        break;
                    case "^":
                        result = Math.Pow(firstNum, secondNum);
                        break;
                }

                this.result.Text = result.ToString();
                firstNum = result;
                state = 1;
            }
        }

        void OnNumber(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnPressed = btn.Text;
            if (this.result.Text == "0" || state < 0)
            {
                this.result.Text = string.Empty;
                if (state < 0)
                {
                    state += -1;
                }
            }
            this.result.Text += btnPressed;
            double num;
            if (double.TryParse(this.result.Text, out num))
            {
                this.result.Text = num.ToString("N0");
                if (state == 1)
                {
                    firstNum = num;
                }
                else
                {
                    secondNum = num;
                }
            }
        }

        void onOperatorSelection(object sender, EventArgs e)
        {
            state = 2;
            this.result.Text = "0";
            Button button = (Button)sender;
            string buttonPressed = button.Text;
            mathOperator = buttonPressed;
        }

        void OnPlusMinus(object sender, EventArgs e)
        {
            if (state == 2)
            {
                double.TryParse(this.result.Text, out secondNum);
                secondNum = -secondNum;
                this.result.Text = secondNum.ToString();
            }
            else if (state == 1)
            {
                double.TryParse(this.result.Text, out firstNum);
                firstNum = -firstNum;
                this.result.Text = firstNum.ToString();
            }
        }

        void OnComma(object sender, EventArgs e)
        {
            if (!this.result.Text.Contains("."))
                this.result.Text += ".";
        }
    }

}
