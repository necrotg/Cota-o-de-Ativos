namespace Cotação_de_Ativos.Models
{
    using System;
    using Newtonsoft.Json;

    public partial class ExternalQuotationResponse
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("requestedAt")]
        public DateTimeOffset RequestedAt { get; set; }

        [JsonProperty("took")]
        public string Took { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketChange")]
        public double RegularMarketChange { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public double RegularMarketChangePercent { get; set; }

        [JsonProperty("regularMarketTime")]
        public DateTimeOffset RegularMarketTime { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public long RegularMarketDayHigh { get; set; }

        [JsonProperty("regularMarketDayRange")]
        public string RegularMarketDayRange { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public long RegularMarketDayLow { get; set; }

        [JsonProperty("regularMarketVolume")]
        public long RegularMarketVolume { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public double RegularMarketPreviousClose { get; set; }

        [JsonProperty("regularMarketOpen")]
        public double RegularMarketOpen { get; set; }

        [JsonProperty("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public long FiftyTwoWeekLow { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public long FiftyTwoWeekHigh { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("usedInterval")]
        public string UsedInterval { get; set; }

        [JsonProperty("usedRange")]
        public string UsedRange { get; set; }

        [JsonProperty("historicalDataPrice")]
        public HistoricalDataPrice[] HistoricalDataPrice { get; set; }

        [JsonProperty("validRanges")]
        public string[] ValidRanges { get; set; }

        [JsonProperty("validIntervals")]
        public string[] ValidIntervals { get; set; }

        [JsonProperty("priceEarnings")]
        public double PriceEarnings { get; set; }

        [JsonProperty("earningsPerShare")]
        public double EarningsPerShare { get; set; }

        [JsonProperty("logourl")]
        public Uri Logourl { get; set; }
    }

    public partial class HistoricalDataPrice
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("adjustedClose")]
        public double AdjustedClose { get; set; }
    }
}
