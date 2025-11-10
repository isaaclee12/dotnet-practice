// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using LiteDB;

// // Models
// public enum CaseStatus { Open, Closed, Pending }

// public class LegalCase
// {
//     public int Id { get; set; }
//     public string CaseType { get; set; }
//     public CaseStatus Status { get; set; }
//     public int ClientId { get; set; }
// }

// // Repository mimic for LiteDB (just for seed/delete)
// public class CaseRepository
// {
//     private readonly ILiteCollection<LegalCase> _cases;
//     public CaseRepository(LiteDatabase db) => _cases = db.GetCollection<LegalCase>("cases");
//     public IEnumerable<LegalCase> GetAll() => _cases.FindAll();
//     public void Add(LegalCase c) => _cases.Insert(c);
//     public void DeleteAll() => _cases.DeleteAll();
// }

// class Program
// {
//     static async Task Main()
//     {
//         string baseUrl = "https://localhost:5116/cases"; // Change port if needed

//         // --- 1. Seed LiteDB ---
//         using var db = new LiteDatabase("LegalCases.db");
//         var repo = new CaseRepository(db);

//         Console.WriteLine("Deleting existing data...");
//         repo.DeleteAll();

//         Console.WriteLine("Seeding test data...");
//         repo.Add(new LegalCase { CaseType = "Civil", Status = CaseStatus.Open, ClientId = 1 });
//         repo.Add(new LegalCase { CaseType = "Criminal", Status = CaseStatus.Closed, ClientId = 2 });
//         repo.Add(new LegalCase { CaseType = "Civil", Status = CaseStatus.Pending, ClientId = 1 });

//         Console.WriteLine("Seed complete!");

//         // --- 2. Test API endpoints ---
//         using var client = new HttpClient();

//         // GET all
//         Console.WriteLine("\nGET all cases:");
//         var allCases = await client.GetFromJsonAsync<List<LegalCase>>(baseUrl);
//         allCases.ForEach(c => Console.WriteLine($"{c.Id}: {c.CaseType} ({c.Status}) - Client {c.ClientId}"));

//         // GET by ID (use first case)
//         if (allCases.Count > 0)
//         {
//             int id = allCases[0].Id;
//             Console.WriteLine($"\nGET case by ID ({id}):");
//             var caseById = await client.GetFromJsonAsync<LegalCase>($"{baseUrl}/{id}");
//             Console.WriteLine($"{caseById.Id}: {caseById.CaseType} ({caseById.Status}) - Client {caseById.ClientId}");
//         }

//         // POST a new case
//         var newCase = new LegalCase { CaseType = "Family", Status = CaseStatus.Open, ClientId = 3 };
//         Console.WriteLine("\nPOST new case:");
//         var postResponse = await client.PostAsJsonAsync(baseUrl, newCase);
//         Console.WriteLine($"POST status: {postResponse.StatusCode}");

//         // GET by ClientId (assuming route uses 'client/{clientId}' as discussed)
//         int clientIdToTest = 1;
//         Console.WriteLine($"\nGET cases for client {clientIdToTest}:");
//         var clientCases = await client.GetFromJsonAsync<List<LegalCase>>($"{baseUrl}/client/{clientIdToTest}");
//         clientCases.ForEach(c => Console.WriteLine($"{c.Id}: {c.CaseType} ({c.Status}) - Client {c.ClientId}"));

//         // PUT (update first case)
//         if (allCases.Count > 0)
//         {
//             var updateCase = allCases[0];
//             updateCase.Status = CaseStatus.Closed;
//             Console.WriteLine($"\nPUT update case {updateCase.Id}:");
//             var putResponse = await client.PutAsJsonAsync($"{baseUrl}/{updateCase.Id}", updateCase);
//             Console.WriteLine($"PUT status: {putResponse.StatusCode}");
//         }

//         // DELETE (delete last case)
//         if (allCases.Count > 0)
//         {
//             int deleteId = allCases[^1].Id;
//             Console.WriteLine($"\nDELETE case {deleteId}:");
//             var deleteResponse = await client.DeleteAsync($"{baseUrl}/{deleteId}");
//             Console.WriteLine($"DELETE status: {deleteResponse.StatusCode}");
//         }

//         Console.WriteLine("\nAll endpoint tests completed!");
//     }
// }
