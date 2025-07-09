
namespace wennovation_hub.Models
{
    public class Wennovator
    {
        public Guid Id { get; set; }
        public string? ApplicationId { get; set; }
        public string? Email { get; set; }
        public string? BusinessName { get; set; }
        public string? Industry { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? YearsOperation { get; set; }
        public int TeamSize { get; set; }
        public string? Founders { get; set; }
        public string? BusinessDescription { get; set; }
        public string? ValueProposition { get; set; }
        public string? Headquarters { get; set; }
        public string? RevenueModel { get; set; }
        public string? AnnualRevenue { get; set; }
        public string? Profitability { get; set; }
        public string? ProfitMargin { get; set; }
        public string? FundingHistory { get; set; }
        public string? FundingNeeds { get; set; }
        public string? Documents { get; set; }
        public string? InvestmentChallenges { get; set; }
        public string? Trainings { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}