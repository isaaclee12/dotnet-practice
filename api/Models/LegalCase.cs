public enum CaseStatus {
    Filed,
    Open,
    Closed,
    Appealed,
}

public class LegalCase
{
    public int Id {get; set;}
    public string CaseType {get; set;} // Civil, Criminal, etc.
    public string LawyerType {get; set;} // TODO: options like "defendant" "prosecution"
    public CaseStatus Status {get; set;} // Filed, Open, Closed, Appealed, etc.
    public DateTime FilingDate {get; set;} // When the lawsuit was filed
    public DateTime AppealDate {get; set;}
    public DateTime CloseDate {get; set;}

    public int ClientId {get; set;}
    public Client Client {get; set;}
}

public class Client {
    public string Name {get; set;}
    public List<LegalCase> Cases {get; set;} = new List<LegalCase>();
} 
