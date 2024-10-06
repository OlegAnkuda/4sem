using Labs.Services;
using NbrbAPI.Models;
namespace Labs;

public partial class Converter : ContentPage
{
    private IEnumerable<Rate> _rates;
    IRateService rateServ;
    private readonly List<string> _abbreviations = new List<string>
        {
            "RUB",
            "EUR",
            "USD",
            "CHF",
            "CNY",
            "GBP"
        };
    public Converter(IRateService service)
    {
        InitializeComponent();
        rateServ = service;
        this.datePick.MaximumDate = DateTime.Now.Date;
        this.curPick.ItemsSource = _abbreviations;
    }
    public async void SetRates(object sender, DateChangedEventArgs e)
    {
        _rates = await rateServ.GetRates(e.NewDate);
        this.rubLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "RUB").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "RUB").Cur_Scale).ToString();
        this.eurLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "EUR").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "EUR").Cur_Scale).ToString();
        this.usdLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "USD").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "USD").Cur_Scale).ToString();
        this.chfLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "CHF").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "CHF").Cur_Scale).ToString();
        this.cnyLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "CNY").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "CNY").Cur_Scale).ToString();
        this.gbpLabel.Text = (_rates.FirstOrDefault(r => r.Cur_Abbreviation == "GBP").Cur_OfficialRate /
            _rates.FirstOrDefault(r => r.Cur_Abbreviation == "GBP").Cur_Scale).ToString();
    }
    public void CurCalculate(object sender, TextChangedEventArgs e)
    {
        if (this.bynEntry != null && this.curPick.SelectedItem != null && _rates != null)
        {
            try
            {
                double.Parse(this.bynEntry.Text);
            }
            catch
            {
                this.resultEntry.Text = "Îøèáêà";
                return;
            }
            this.resultEntry.Text = (decimal.Parse(this.bynEntry.Text) /
                _rates.FirstOrDefault(r => r.Cur_Abbreviation == this.curPick.SelectedItem.ToString()).Cur_OfficialRate *
                _rates.FirstOrDefault(r => r.Cur_Abbreviation == this.curPick.SelectedItem.ToString()).Cur_Scale).ToString();
        }
    }
}