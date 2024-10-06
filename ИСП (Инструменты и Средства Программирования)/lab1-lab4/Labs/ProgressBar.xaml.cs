namespace Labs;

public partial class ProgressBar : ContentPage
{
	public ProgressBar()
	{
		InitializeComponent();
	}
    CancellationTokenSource cancellationToken;

    async void StartButton_Clicked(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(() => titleLabel.Text = "Counting");
        this.startButton.IsEnabled = false;
        this.cancelButton.IsEnabled = true;
        progressBar.Progress = 0;
        cancellationToken = new CancellationTokenSource();

        try
        {
            double integral = await Task.Run(() => CalculateIntegral(cancellationToken.Token));
            Dispatcher.Dispatch(() => titleLabel.Text = $"Result: {integral}");
            this.cancelButton.IsEnabled = false;
        }
        catch (OperationCanceledException)
        {
            Dispatcher.Dispatch(() => titleLabel.Text = "Canceled");
            this.progressBar.Progress = 0;
            this.percentageLabel.Text = "0%";
            this.cancelButton.IsEnabled = false;
        }
        finally
        {
            this.startButton.IsEnabled = true;
            this.cancelButton.IsEnabled = false;
        }
    }

    async Task<double> CalculateIntegral(CancellationToken cancellationToken)
    {
        double step = 0.000001;
        double start = 0;
        double end = 1;
        double result = 0;
        for (double x = start; x < end; x += step)
        {
            cancellationToken.ThrowIfCancellationRequested();
            for (int i = 0; i < 100; i++)
            {
                var temp = i * i;
            }
            double y = Math.Sin(x);
            result += y * step;
            Dispatcher.Dispatch(() => progressBar.Progress = Math.Round(x, 2));
            Dispatcher.Dispatch(() => percentageLabel.Text = $"{(int)(x * 100)}%");
        }
        Dispatcher.Dispatch(() => percentageLabel.Text = "100%");
        return result;
    }

    void CancelButton_Clicked(object sender, EventArgs e)
    {
        cancellationToken?.Cancel();
    }
}