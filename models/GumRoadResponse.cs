using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Publisher.models
{
    public class GumRoadResponse
    {
        
            public bool success { get; set; }
            public int uses { get; set; }
            public Purchase purchase { get; set; }
        
        public class Card
        {
            public string visual { get; set; }
            public string type { get; set; }
            public string bin { get; set; }
            public int? expiry_month { get; set; }
            public int? expiry_year { get; set; }
        }
        public class Purchase
        {
            public string seller_id { get; set; }
            public string product_id { get; set; }
            public string product_name { get; set; }
            public string permalink { get; set; }
            public string product_permalink { get; set; }
            public string short_product_id { get; set; }
            public string email { get; set; }
            public decimal price { get; set; }
            public decimal gumroad_fee { get; set; }
            public string currency { get; set; }
            public int quantity { get; set; }
            public bool discover_fee_charged { get; set; }
            public bool can_contact { get; set; }
            public string referrer { get; set; }
            public Card card { get; set; }
            public int order_number { get; set; }
            public string sale_id { get; set; }
            public DateTime sale_timestamp { get; set; }
            public string license_key { get; set; }
            public string ip_country { get; set; }
            public bool is_gift_receiver_purchase { get; set; }
            public bool refunded { get; set; }
            public bool disputed { get; set; }
            public bool dispute_won { get; set; }
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public string variants { get; set; }
            public List<object> custom_fields { get; set; }
            public bool chargebacked { get; set; }
        }
    }
}
