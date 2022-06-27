namespace Server.Models.PolygonModels
{
    public class Address
    {
        public string address1 { get; set; } = null!;
        public string city { get; set; } = null!;
        public string postal_code { get; set; } = null!;
        public string state { get; set; } = null!;
    }

    public class Branding
    {
        public string icon_url { get; set; } = null!;
        public string logo_url { get; set; } = null!;
    }

    public class Results
    {
        public bool active { get; set; }
        public Address address { get; set; } = null!;
        public Branding? branding { get; set; } = null!;
        public string currency_name { get; set; } = null!;
        public string description { get; set; } = null!;
        public string homepage_url { get; set; } = null!;
        public string list_date { get; set; } = null!;
        public string locale { get; set; } = null!;
        public string market { get; set; } = null!;
        public string name { get; set; } = null!;
        public string phone_number { get; set; } = null!;
        public string primary_exchange { get; set; } = null!;
        public string share_class_figi { get; set; } = null!;
        public long share_class_shares_outstanding { get; set; }
        public string sic_code { get; set; } = null!;
        public string sic_description { get; set; } = null!;
        public string ticker { get; set; } = null!;
        public int total_employees { get; set; }
    }

    public class TickerInfo
    {
        public string request_id { get; set; } = null!;
        public Results results { get; set; } = null!;
        public string status { get; set; } = null!;
    }

}