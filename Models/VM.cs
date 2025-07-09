

namespace wennovation_hub.Models
{
    public class WennovatorApplicationVM
    {
        // Section 1: Business Information
        public string BusinessName { get; set; }
        public string Industry { get; set; }
        public string RegistrationNumber { get; set; }
        public string YearsOperation { get; set; }
        public int TeamSize { get; set; }
        public string Founders { get; set; }
        public string BusinessDescription { get; set; }
        public string ValueProposition { get; set; }
        public string Headquarters { get; set; }

        // Section 2: Financial Information
        public string RevenueModel { get; set; }
        public string AnnualRevenue { get; set; }
        public ProfitabilityStatus Profitability { get; set; }
        public string ProfitMargin { get; set; }
        public string FundingHistory { get; set; }
        public string FundingNeeds { get; set; }

        // Section 3: Support Needs
        public List<string> Documents { get; set; } = new List<string>();
        public string InvestmentChallenges { get; set; }
        public List<string> Trainings { get; set; } = new List<string>();
        public string AdditionalInfo { get; set; }
    }

    public enum ProfitabilityStatus
    {
        NotSelected,
        Yes,
        No
    }
    
    public class AdminDash
    {
        public List<Bookings> Bookings { get; set; }
        public List<Wennovator> Wennovators { get; set; }
    }
}