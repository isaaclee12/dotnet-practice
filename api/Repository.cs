using LiteDB;

// DB access / persistence logic
public class CaseRepository
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<LegalCase> _cases;

    public CaseRepository(string dbPath = "LegalCases.db")
    {
        _db = new LiteDatabase(dbPath);
        _cases = _db.GetCollection<LegalCase>("cases");
    }

    public IEnumerable<LegalCase> GetAll() => _cases.FindAll();
    public LegalCase GetById(int id) => _cases.FindById(id);
    public void Add(LegalCase legalCase) => _cases.Insert(legalCase);
    // NOTE: Does the below need an id?
    public void Update(LegalCase legalCase) => _cases.Update(legalCase);
    public void Delete(int id) => _cases.Delete(id);
}

public class ClientRepository
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<Client> _clients;

    public ClientRepository(string dbPath = "Clients.db")
    {
        _db = new LiteDatabase(dbPath);
        _clients = _db.GetCollection<Client>("clients");
    }

    public IEnumerable<Client> GetAll() => _clients.FindAll();
    public Client GetById(int id) => _clients.FindById(id);
    public void Add(Client client) => _clients.Insert(client);
    // NOTE: Does the below need an id?
    public void Update(Client client) => _clients.Update(client);
    public void Delete(int id) => _clients.Delete(id);
}
