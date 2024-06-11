using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class ScanFactory
{
    public static Scan CreateAndSubmit(ApplicationDbContext context)
    {
		Scan scan = new Scan()
		{	
			ContactName = "John Doe",
			ContactEmail = "john@doe.gmail.com",
			SectorId = new Random().Next(1, 5),
			Results = "[{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Arbeid\\\"},\\\"en\\\":{\\\"name\\\":\\\"Labor\\\"}}\",\"category_uuid\":\"87a1b56b-1f91-4176-9ba3-e74ac0f09060\",\"mean\":1.5},{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Productie en logistiek\\\"},\\\"en\\\":{\\\"name\\\":\\\"Production and logistics\\\"}}\",\"category_uuid\":\"dd9707f8-93d1-4803-b451-98b21865279f\",\"mean\":2.0},{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Ambitie\\\"},\\\"en\\\":{\\\"name\\\":\\\"Ambition\\\"}}\",\"category_uuid\":\"417806a9-4c79-44c5-9b60-08544e8deed0\",\"mean\":2.2},{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Ketensamenwerking\\\"},\\\"en\\\":{\\\"name\\\":\\\"Chain collaboration\\\"}}\",\"category_uuid\":\"c827d80b-0807-4deb-8f4d-145c7fe9ecfe\",\"mean\":2.6},{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Innovatie in grondstofgebruik\\\"},\\\"en\\\":{\\\"name\\\":\\\"Innovation in raw material use\\\"}}\",\"category_uuid\":\"ffad595d-985d-45c8-b92b-89cf41ac1502\",\"mean\":2.8666666666666667},{\"category_data\":\"{\\\"nl\\\":{\\\"name\\\":\\\"Facilitair\\\"},\\\"en\\\":{\\\"name\\\":\\\"Facility\\\"}}\",\"category_uuid\":\"d6b93472-c807-4484-b664-d594d6072924\",\"mean\":3.111111111111111}]",				CreatedAt = DateTime.Now,
		};
		context.Scans.Add(scan);
		context.SaveChanges();

		Answer a1 = new Answer() { QuestionId = 1, ScanId = scan.Id, Score = 1, Comment = "test" };
		Answer a2 = new Answer() { QuestionId = 2, ScanId = scan.Id, Score = 5, Comment = "test" };
		Answer a3 = new Answer() { QuestionId = 3, ScanId = scan.Id, Score = 2, Comment = "test" };
		Answer a4 = new Answer() { QuestionId = 4, ScanId = scan.Id, Score = 4, Comment = "test" };
		Answer a5 = new Answer() { QuestionId = 5, ScanId = scan.Id, Score = 3, Comment = "test" };
		Answer a6 = new Answer() { QuestionId = 6, ScanId = scan.Id, Score = 2, Comment = "test" };
		
		context.Answers.AddRange(a1, a2, a3, a4, a5, a6);
		context.SaveChanges();
		
		ScanCode scd1 = new ScanCode()
		{
			Code = Guid.NewGuid(),
			ScanId = scan.Id,
			CanEdit = false
		};
		
		ScanCode scd2 = new ScanCode()
		{
			Code = Guid.NewGuid(),
			ScanId = scan.Id,
			CanEdit = true
		};
		
		context.ScanCodes.AddRange(scd1, scd2);
		context.SaveChanges();
        
        return scan;
    }
}