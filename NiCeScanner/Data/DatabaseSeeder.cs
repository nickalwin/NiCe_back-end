using Newtonsoft.Json;
using NiCeScanner.Models;
using Microsoft.AspNetCore.Identity;

namespace NiCeScanner.Data
{
	public class DatabaseSeeder
	{
		public static async Task SeedUser(string name, string email, string password, string roleStr, ApplicationDbContext dbContext)
		{
			var roles = new List<string> { "Admin", "Manager", "Researcher", "Student", "Member" };

			foreach (var role in roles)
			{
				await dbContext.Roles.AddAsync(new IdentityRole(role));
			}

			await dbContext.SaveChangesAsync();

			var user = new IdentityUser { 
				UserName = email, 
				Email = email, 
				EmailConfirmed = true,
				NormalizedEmail = email.ToUpper(),
				NormalizedUserName = email.ToUpper(),
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
				PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, password)
			};
			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();

			var adminRole = dbContext.Roles.FirstOrDefault(r => r.Name == roleStr);
			await dbContext.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRole.Id });
			await dbContext.SaveChangesAsync();
		}
		public static async void Seed(ApplicationDbContext context, string imageFolder)
		{
			if (context.Users.Any())
				return;

			var roles = new List<string> { "Admin", "Manager", "Researcher", "Student", "Member" };

			foreach (var role in roles)
			{
				await context.Roles.AddAsync(new IdentityRole(role));
			}

			await context.SaveChangesAsync();

			await SeedUser("Admin", "admin@admin.com", "Admin@123", "Admin", context);
			await SeedUser("Manager", "manager@manager.com", "Manager@123", "Manager", context);
			await SeedUser("Researcher", "researcher@researcher.com", "Researcher@123", "Researcher", context);
			await SeedUser("Student", "student@student.com", "Student@123", "Student", context);
			await SeedUser("Member", "member@member.com", "Member@123", "Member", context);

			Category c1 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Innovatie in grondstofgebruik",
					},
					en = new
					{
						name = "Innovation in raw material use",
					}
				}),
				Color = "lichtroze",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			Category c2 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Productie en logistiek",
					},
					en = new
					{
						name = "Production and logistics",
					}
				}),
				Color = "blauw",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			Category c3 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Ketensamenwerking",
					},
					en = new
					{
						name = "Chain collaboration",
					}
				}),
				Color = "goud",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			Category c4 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Ambitie",
					},
					en = new
					{
						name = "Ambition",
					}
				}),
				Color = "appelgroen",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			Category c5 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Facilitair",
					},
					en = new
					{
						name = "Facility",
					}
				}),
				Color = "lichtblauw",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			Category c6 = new Category()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Arbeid",
					},
					en = new
					{
						name = "Labor",
					}
				}),
				Color = "donkerroze",
				Show = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			context.Categories.AddRange(
				c1, c2, c3, c4, c5, c6
			);

			await context.SaveChangesAsync();
			
			Link l1 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c1,
				CreatedAt = DateTime.Now
			};

			Link l2 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c1,
				CreatedAt = DateTime.Now
			};

			Link l3 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c2,
				CreatedAt = DateTime.Now
			};

			Link l4 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c2,
				CreatedAt = DateTime.Now
			};

			Link l5 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c3,
				CreatedAt = DateTime.Now
			};

			Link l6 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c3,
				CreatedAt = DateTime.Now
			};

			Link l7 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c4,
				CreatedAt = DateTime.Now
			};

			Link l8 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c4,
				CreatedAt = DateTime.Now
			};

			Link l9 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c5,
				CreatedAt = DateTime.Now
			};

			Link l10 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c5,
				CreatedAt = DateTime.Now
			};

			Link l11 = new Link()
			{
				Name = "Quickscan Circulaire Businessmodellen",
				Href = "https://circulairemaakindustrie.nl/documenten/quickscan-circulaire-businessmodellen",
				Category = c6,
				CreatedAt = DateTime.Now
			};

			Link l12 = new Link()
			{
				Name = "Self Assessment Tool Milieu Impact (Excel-bestand)",
				Href = "https://circulairemaakindustrie.nl/documenten/self-assessment-tool-milieu-impact",
				Category = c6,
				CreatedAt = DateTime.Now
			};

			context.Links.AddRange(
				l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11, l12
			);

			await context.SaveChangesAsync();

			ImageModel img1 = new ImageModel()
			{
				FileName = "Innovation_1.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_1.jpg"),
			};

			ImageModel img2 = new ImageModel()
			{
				FileName = "Innovation_2.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_2.jpg"),
			};

			ImageModel img3 = new ImageModel()
			{
				FileName = "Innovation_3.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_3.jpg"),
			};

			ImageModel img4 = new ImageModel()
			{
				FileName = "Innovation_4.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_4.jpg"),
			};

			ImageModel img5 = new ImageModel()
			{
				FileName = "Innovation_5.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_5.png"),
			};

			ImageModel img6 = new ImageModel()
			{
				FileName = "Innovation_6.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_6.jpg"),
			};

			ImageModel img7 = new ImageModel()
			{
				FileName = "Innovation_7.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_7.jpg"),
			};

			ImageModel img8 = new ImageModel()
			{
				FileName = "Innovation_8.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_8.jpg"),
			};

			ImageModel img9 = new ImageModel()
			{
				FileName = "Innovation_9.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_9.png"),
			};

			ImageModel img10 = new ImageModel()
			{
				FileName = "Innovation_10.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_10.jpg"),
			};

			ImageModel img11 = new ImageModel()
			{
				FileName = "Innovation_11.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Innovation/Innovation_11.jpeg"),
			};

			ImageModel img12 = new ImageModel()
			{
				FileName = "Productie_1.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_1.jpg"),
			};

			ImageModel img13 = new ImageModel()
			{
				FileName = "Productie_2.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_2.jpeg"),
			};

			ImageModel img14 = new ImageModel()
			{
				FileName = "Productie_3.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_3.jpeg"),
			};

			ImageModel img15 = new ImageModel()
			{
				FileName = "Productie_4.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_4.jpg"),
			};
			
			ImageModel img16 = new ImageModel()
			{
				FileName = "Productie_5.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_5.png"),
			};

			ImageModel img17 = new ImageModel()
			{
				FileName = "Productie_6.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Productie/Productie_6.png"),
			};

			ImageModel img18 = new ImageModel()
			{
				FileName = "Chain_1.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_1.jpg"),
			};

			ImageModel img19 = new ImageModel()
			{
				FileName = "Chain_2.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_2.png"),
			};

			ImageModel img20 = new ImageModel()
			{
				FileName = "Chain_3.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_3.png"),
			};

			ImageModel img21 = new ImageModel()
			{
				FileName = "Chain_4.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_4.jpeg"),
			};

			ImageModel img22 = new ImageModel()
			{
				FileName = "Chain_5.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_5.jpg"),
			};

			ImageModel img23 = new ImageModel()
			{
				FileName = "Chain_6.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_6.png"),
			};

			ImageModel img24 = new ImageModel()
			{
				FileName = "Chain_7.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_7.jpeg"),
			};

			ImageModel img25 = new ImageModel()
			{
				FileName = "Chain_8.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Chain/Chain_8.png"),
			};

			ImageModel img26 = new ImageModel()
			{
				FileName = "Ambitie_1.png",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_1.png"),
			};

			ImageModel img27 = new ImageModel()
			{
				FileName = "Ambitie_2.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_2.jpg"),
			};
			
			ImageModel img28 = new ImageModel()
			{
				FileName = "Ambitie_3.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_3.jpg"),
			};

			ImageModel img29 = new ImageModel()
			{
				FileName = "Ambitie_4.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_4.jpeg"),
			};

			ImageModel img30 = new ImageModel()
			{
				FileName = "Ambitie_5.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_5.jpg"),
			};

			ImageModel img31 = new ImageModel()
			{
				FileName = "Ambitie_6.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Ambitie/Ambitie_6.jpg"),
			};

			// ImageModel img32 = new ImageModel()
			// {
			// 	FileName = "Ambitie_7.jpg",
			// 	ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Ambitie_7.jpg"),
			// };

			ImageModel img33 = new ImageModel()
			{
				FileName = "Facility_1.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_1.jpg"),
			};

			ImageModel img34 = new ImageModel()
			{
				FileName = "Facility_2.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_2.jpg"),
			};
			ImageModel img35 = new ImageModel()
			{
				FileName = "Facility_3.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_3.jpg"),
			};

			ImageModel img36 = new ImageModel()
			{
				FileName = "Facility_4.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_4.jpg"),
			};

			ImageModel img37 = new ImageModel()
			{
				FileName = "Facility_5.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_5.jpg"),
			};

			ImageModel img38 = new ImageModel()
			{
				FileName = "Facility_6.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_6.jpeg"),
			};

			ImageModel img39 = new ImageModel()
			{
				FileName = "Facility_7.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_7.jpg"),
			};

			ImageModel img40 = new ImageModel()
			{
				FileName = "Facility_8.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_8.jpeg"),
			};

			ImageModel img41 = new ImageModel()
			{
				FileName = "Facility_9.jpeg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_9.jpeg"),
			};

			ImageModel img42 = new ImageModel()
			{
				FileName = "Facility_10.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_10.jpg"),
			};

			ImageModel img43 = new ImageModel()
			{
				FileName = "Facility_11.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_11.jpg"),
			};

			ImageModel img44 = new ImageModel()
			{
				FileName = "Facility_12.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_12.jpg"),
			};

			ImageModel img45 = new ImageModel()
			{
				FileName = "Facility_13.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Facility/Facility_13.jpg"),
			};

			ImageModel img46 = new ImageModel()
			{
				FileName = "Labor_1.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_1.jpg"),
			};

			ImageModel img47 = new ImageModel()
			{
				FileName = "Labor_2.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_2.jpg"),
			};

			ImageModel img48 = new ImageModel()
			{
				FileName = "Labor_3.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_3.jpg"),
			};

			ImageModel img49 = new ImageModel()
			{
				FileName = "Labor_4.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_4.jpg"),
			};

			// ImageModel img50 = new ImageModel()
			// {
			// 	FileName = "Labor_5.jpg",
			// 	ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_5.jpg"),
			// };

			ImageModel img51 = new ImageModel()
			{
				FileName = "Labor_6.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_6.jpg"),
			};

			ImageModel img52 = new ImageModel()
			{
				FileName = "Labor_7.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_7.jpg"),
			};

			ImageModel img53 = new ImageModel()
			{
				FileName = "Labor_8.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_8.jpg"),
			};

			ImageModel img54 = new ImageModel()
			{
				FileName = "Labor_9.jpg",
				ImageData = System.IO.File.ReadAllBytes(imageFolder + "Labor/Labor_9.jpg"),
			};

			context.Images.AddRange(
				img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16, img17,
				img18, img19, img20, img21, img22, img23, img24, img25, img26, img27, img28, img29, img30, img31,
				/*img32,*/ img33, img34, img35, img36, img37, img38, img39, img40, img41, img42, img43, img44, img45, img46,
				img47, img48, img49, /*img50,*/ img51, img52, img53, img54
			);

			await context.SaveChangesAsync();

			Question q1 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Energieniveau",
						question = "Recover; Laagste niveau circulariteit: Primaire bedrijfsprocessen zorgen ervoor dat producten zo worden ontworpen dat materialen aan het einde van de levensduur veilig worden verbrand met terugwinning van energie?",
						tooltip = "Bij diensten: wordt dmv van uw diensten vermeden dat poducten en materialen niet meer veilig kunnen worden verbrand met terugwinning van energie?"
					},
					en = new
					{
						header = "Energy level",
						question = "Recover; Lowest circularity level: Primary business processes ensure that products are designed so that materials at the end of their lifespan can be safely incinerated with energy recovery?",
						tooltip = "For services: does the use of your services prevent products and materials from being incinerated with energy recovery?"
					}
				}),
				Category = c1,
				Weight = 0,
				Statement = false,
				Show = true,
				ImageId = img1.Id,
				CreatedAt = DateTime.Now
			};

			Question q2 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Materiaalniveau",
						question = "Recycling; In welke mate zijn de materialen in uw product gerecycled en recyclebaar, kunnen de materialen worden verwerkt tot grondstoffen met, bij voorkeur, dezelfde (hoogwaardige) of eventueel tot mindere (laagwaardige) kwaliteit dan de oorspronkelijke grondstof tot en wordt dit in de praktijk toegepast/geborgd?",
						tooltip = "Bij diensten: Zet je producten die uit materialen bestaan die gerecycled kunnen worden?"
					},
					en = new
					{
						header = "Material level",
						question = "Recycling; To what extent are the materials in your product recycled and recyclable, can the materials be processed into raw materials with, preferably, the same (high-quality) or possibly lower (low-quality) quality than the original raw material, and is this applied/guaranteed in practice?",
						tooltip = "For services: Do you use products made from materials that can be recycled?"
					}
				}),

				Category = c1,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img2.Id,
				CreatedAt = DateTime.Now
			};

			Question q3 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Component/onderdeelniveau",
						question = "Repair, Refurbish, Remanufacture en Repurpose; Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen zodat ze op efficiënte wijze worden gerepareerd, opgeknapt of geupdated? Zit er lange garantie op? Maakt u zelf gebruik van opgeknapte onderdelen/producten?",
						tooltip = "Bij diensten: repair café, onderhoudscontracten, refurbishen, repareerbare producten inzetten bij diensten. Kun je vervangende onderdelen van reeds geleverde producten snel leveren aan je klanten? Lever je nog onderdelen van producten die uit de handel zijn genomen? Investeer je in betaalbaar onderhoud voor je klanten zodat de producten uit je productaanbod langer mee kunnen? Worden geretourneerde producten opgeknapt om opnieuw verkocht te worden (gerefurbished)? Verkoop je producten waarbij de onderdelen op een efficiënte wijze vervangen kunnen worden?"
					},
					en = new
					{
						header = "Component/part level",
						question = "Repair, Refurbish, Remanufacture, and Repurpose; Primary business processes ensure that products are designed so that they can be efficiently repaired, refurbished, or updated? Is there a long warranty? Do you use refurbished parts/products yourself?",
						tooltip = "For services: repair café, maintenance contracts, refurbishing, using repairable products in services. Can you quickly deliver replacement parts for products already delivered to your customers? Do you still supply parts for products that have been discontinued? Do you invest in affordable maintenance for your customers so that the products in your product range last longer? Are returned products refurbished for resale? Do you sell products where the parts can be efficiently replaced?"
					}
				}),
				Category = c1,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img3.Id,
				CreatedAt = DateTime.Now
			};

			Question q4 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Productniveau",
						question = "Reuse: Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen voor een lange levensduur, producten en onderdelen daarvan worden hergebruikt in dezelfde functie en wordt dit in de praktijk door u geborgd (bv door statiegeld/terugkooopregeling)",
						tooltip = "Bij diensten: bv gebruikte producten inzetten. Worden producten die bij uw diensten worden ingezet meerdere malen gebruikt? Zorgen voor delen en herverdelen van oude producten."
					},
					en = new
					{
						header = "Product level",
						question = "Reuse: Primary business processes ensure that products are designed for a long lifespan, products and parts thereof are reused in the same function, and is this practice ensured by you (e.g., through deposit/repurchase scheme)?",
						tooltip = "For services: e.g., using used products. Are products used in your services reused multiple times? Ensuring the sharing and redistribution of old products."
					}
				}),
				Category = c1,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img4.Id,
				CreatedAt = DateTime.Now
			};

			Question q5 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Proces en ontwerpniveau",
						question = "Reduce: Primaire bedrijfsprocessen zorgen ervoor dat er continue aandacht is voor reductie van grondstoffen en primaire en kritieke grondstoffen, energie en water in producten en in de gebruiksfase?",
						tooltip = "Bij diensten: Zet je producten in waarbij nagedacht is over efficiënt grondstofgebruik in productie- en gebruiksfase? Onderhoud van apparaten/machines zodat ze zuinig blijven."
					},
					en = new
					{
						header = "Process and design level",
						question = "Reduce: Primary business processes ensure continuous attention to the reduction of raw materials and primary and critical materials, energy, and water in products and during the usage phase?",
						tooltip = "For services: Do you use products where efficient use of raw materials in production and usage phase has been considered? Maintenance of appliances/machines to keep them efficient."
					}
				}),
				Category = c1,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img5.Id,
				CreatedAt = DateTime.Now
			};

			Question q6 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Proces en ontwerpniveau",
						question = "Rethink: Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen vanuit circulariteitsprincipes, om zo lang mogelijk mee te gaan, voor standaardisatie en compatibiliteit, makkelijk onderhoud en reparatie, op te waarderen en aan te passen, demontage & recycling met end-of-life strategieën en alles met zo laag mogelijke negatieve impact?",
						tooltip = "Wordt uw product bijvoorbeeld via platforms aangeboden t.b.v. intensiever gebruik? Of kan deze multifunctioneel worden ingezet?"
					},
					en = new
					{
						header = "Process and design level",
						question = "Rethink: Primary business processes ensure that products are designed based on circularity principles, to last as long as possible, for standardization and compatibility, easy maintenance and repair, upgradability and adaptability, disassembly & recycling with end-of-life strategies, and all with the lowest possible negative impact?",
						tooltip = "For example, is your product offered via platforms for more intensive use? Or can it be used multifunctionally?"
					}
				}),
				Category = c1,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img6.Id,
				CreatedAt = DateTime.Now
			};

			Question q7 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Proces en ontwerpniveau",
						question = "Refuse: Voorkom je gebruik van grondstoffen, door oplossingen te gebruiken of ontwikkelen, waarmee producten overbodig worden, door van de functie af te zien of die met een radicaal ander en duurzamer product te leveren/gebruiken?",
						tooltip = "Bij diensten: bijvoorbeeld een dienst ontwikkelen waardoor geen nieuwe producten hoeven worden gemaakt, bv een dienst om luiers te wassen. Heet water ipv chemische onkruidverdelger. Innovatie waarmee materialen gebruik wordt voorkomen"
					},
					en = new
					{
						header = "Process and design level",
						question = "Refuse: Do you prevent the use of raw materials by using or developing solutions that render products unnecessary, by foregoing the function or by delivering/using a radically different and more sustainable product?",
						tooltip = "For services: for example, developing a service that eliminates the need for new products, such as a diaper washing service. Hot water instead of chemical weed killer. Innovation to prevent material use."
					}
				}),
				Category = c1,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img7.Id,
				CreatedAt = DateTime.Now
			};

			Question q8 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Systeemniveau",
						question = "Primaire bedrijfsprocessen zorgen ervoor dat er een logistiek systeem ingericht voor inzameling, sorteren en traceren van producten, onderdelen en materialen?",
						tooltip = "Weet wat er met producten gebeurt, wat gaat als eerste kapot, wanneer en waarom worden producten afgedankt, worden ze gerepareerd, doorverkocht, gemodificeerd, weet waar de materialen in producten zijn, etc."
					},
					en = new
					{
						header = "System level",
						question = "Primary business processes ensure that a logistics system is set up for the collection, sorting, and tracing of products, parts, and materials?",
						tooltip = "Understand what happens to products, what breaks first, when and why products are discarded, whether they are repaired, resold, modified, know where the materials in products are, etc."
					}
				}),
				Category = c1,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img8.Id,
				CreatedAt = DateTime.Now
			};

			Question q9 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Systeemniveau",
						question = "Primaire bedrijfsprocessen zorgen ervoor dat er doelbewust wordt gewerkt met circulaire business modellen en verdienmodellen?",
						tooltip = "Er zijn veel verschillende circulaire business modellen en verdienmodellen. Bijvoorbeeld: Prestatiemodel (contract obv pay-per-performance), Tussenbatermodel (terugwinnen, opknappen & doorverkopen of repareren), Toegangmodel (toegang verschappen to product), klassiek duurzaam model (lange levensduur) en hybride model (combinatie lange levensduur en verkoop van bijbehorende verbruiksproducten)."
					},
					en = new
					{
						header = "System level",
						question = "Do primary business processes ensure deliberate work with circular business models and revenue models?",
						tooltip = "There are many different circular business models and revenue models. For example: Performance model (contract based on pay-per-performance), Middleman model (recover, refurbish & resell or repair), Access model (providing access to product), classic sustainable model (long lifespan), and hybrid model (combination of long lifespan and sale of associated consumables)."
					}
				}),
				Category = c1,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img9.Id,
				CreatedAt = DateTime.Now
			};

			Question q10 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Systeemniveau",
						question = "Een groot deel van de omzet van het bedrijf bestaat uit circulaire activiteiten",
						tooltip = ""
					},
					en = new
					{
						header = "System level",
						question = "A large part of the company's revenue comes from circular activities",
						tooltip = ""
					}
				}),
				Category = c1,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img10.Id,
				CreatedAt = DateTime.Now
			};

			Question q11 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Systeemniveau",
						question = "Primaire bedrijfsprocessen zorgen ervoor dat uitvoeren van een LCA (LevensCyclus Analyse) of iets vergelijkbaars, standaard is?",
						tooltip = ""
					},
					en = new
					{
						header = "System level",
						question = "Primary business processes ensure that conducting an LCA (Life Cycle Assessment) or something similar is standard?",
						tooltip = ""
					}
				}),
				Category = c1,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img11.Id,
				CreatedAt = DateTime.Now
			};

			Question q12 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Afval uitval",
						question = "Primaire bedrijfsprocessen zijn zo ingericht dat zo min mogelijk materialen, energie en water verloren gaan in productie/bedrijfsprocessen?",
						tooltip = "afval uitval"
					},
					en = new
					{
						header = "Waste loss",
						question = "Primary business processes are designed to minimize the loss of materials, energy, and water in production/business processes?",
						tooltip = "waste loss"
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img12.Id,
				CreatedAt = DateTime.Now
			};
			
			Question q13 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Reststromen",
						question = "Primaire bedrijfsprocessen zijn zo ingericht dat reststromen (materialen, energie, water) uit productie/bedrijfsprocessen hergebruikt of gerecycled worden?",
						tooltip = ""
					},
					en = new
					{
						header = "Byproducts",
						question = "Primary business processes are designed so that byproducts (materials, energy, water) from production/business processes are reused or recycled?",
						tooltip = ""
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img13.Id,
				CreatedAt = DateTime.Now
			};

			Question q14 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Levensduur machines",
						question = "Verleng je de levensduur van je materiële activa, zoals machines en gebouwen door gepland onderhoud en reparaties?",
						tooltip = "levensduur machines"
					},
					en = new
					{
						header = "Lifespan machines",
						question = "Do you extend the lifespan of your tangible assets, such as machinery and buildings, through planned maintenance and repairs?",
						tooltip = "machine lifespan"
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img14.Id,
				CreatedAt = DateTime.Now
			};

			Question q15 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Efficiënte en milieuvriendelijke productieprocessen",
						question = "Zorg je voor minimale schadelijke emissies tijdens productie/bedrijfsprocessen en logistiek?",
						tooltip = "Bv lokaal inkopen, milieuvriendelijke productieprocessen, efficiënte en duurzame transportmiddelen, niet voor niets rijden, etc"
					},
					en = new
					{
						header = "Efficient and environmentally friendly production processes",
						question = "Do you ensure minimal harmful emissions during production/business processes and logistics?",
						tooltip = "For example, sourcing locally, environmentally friendly production processes, efficient and sustainable transportation methods, avoiding unnecessary travel, etc."
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img15.Id,
				CreatedAt = DateTime.Now
			};

			Question q16 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Duurzame energie",
						question = "Gebruik je energie uit hernieuwbare bronnen voor bedrijfsprocessen/productie (zon, wind, water, aardwarmte)?",
						tooltip = "duurzame energie"
					},
					en = new
					{
						header = "Sustainable energy",
						question = "Do you use energy from renewable sources for business processes/production (solar, wind, water, geothermal)?",
						tooltip = "sustainable energy"
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img16.Id,
				CreatedAt = DateTime.Now
			};

			Question q17 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Verpakkingen",
						question = "Wordt geen of circulair verpakkingsmateriaal gebruikt en wordt het verpakkingsmateriaal (karton, pallets) voor opslag en vervoer teruggenomen na levering?",
						tooltip = "verpakkingen"
					},
					en = new
					{
						header = "Packaging",
						question = "Is no or circular packaging material used, and is the packaging material (cardboard, pallets) taken back for storage and transportation after delivery?",
						tooltip = "packaging"
					}
				}),
				Category = c2,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img17.Id,
				CreatedAt = DateTime.Now
			};

			Question q18 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Kringlopen sluiten",
						question = "Wordt structureel samengewerkt met stakeholders om kringlopen te sluiten binnen en/of buiten de keten?",
						tooltip = "Samenwerking in het valoriseren van reststromen (materialen, warmte, etc.), het afnemen van en/of leveren aan andere organisaties. Maak je afspraken met je klanten over mogelijke terugname van geleverde producten na gebruik? (zoals statiegeld of een minimale prijs waarvoor je producten wilt terugkopen na gebruik)?"
					},
					en = new
					{
						header = "Closing loops",
						question = "Is there systematic collaboration with stakeholders to close loops within and/or outside the chain?",
						tooltip = "Collaboration in valorizing byproducts (materials, heat, etc.), procuring from and/or supplying to other organizations. Do you make agreements with your customers about the possible return of delivered products after use? (such as deposit or a minimum price at which you want to buy back products after use)?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img18.Id,
				CreatedAt = DateTime.Now
			};

			Question q19 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Impactverbetering",
						question = "Werkt u structureel samen met andere organisaties in de keten aan impactverbetering op milieu en sociale aspecten?",
						tooltip = "Breed, regie, structureel vs incidenteel, Is keten coördinator aanwezig?"
					},
					en = new
					{
						header = "Impact improvement",
						question = "Do you collaborate structurally with other organizations in the chain to improve environmental and social impact?",
						tooltip = "Broad, coordination, structural vs incidental, Is there a chain coordinator present?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img19.Id,
				CreatedAt = DateTime.Now
			};

			Question q20 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Eisen aan leveranciers",
						question = "Stel je eisen aan leveranciers wb circulariteit? (toxiciteit en recyclebaarheid van materialen, gerecyclede materialen, arbeidsomstandigheden, etc)",
						tooltip = "Verzamel je de informatie over de herkomst van de materialen en de omstandigheden waaronder mensen deze materialen ontginnen?"
					},
					en = new
					{
						header = "Requirements for suppliers",
						question = "Do you set requirements for suppliers regarding circularity? (toxicity and recyclability of materials, recycled materials, working conditions, etc)",
						tooltip = "Do you gather information about the origin of materials and the conditions under which people extract these materials?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img20.Id,
				CreatedAt = DateTime.Now
			};

			Question q21 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Eisen aan leveranciers",
						question = "Klantenportfolio, stel je eisen aan je klanten/opdrachtgevers wb circulariteit?",
						tooltip = "bv afstand, intenties, waarvoor worden producten gebruikt, wordt bewust nagedacht over samenwerken met circulaire bedrijven of oude economie bedrijven"
					},
					en = new
					{
						header = "Requirements for suppliers",
						question = "Customer portfolio, do you set requirements for your customers/clients regarding circularity?",
						tooltip = "e.g., distance, intentions, what are the products used for, is there conscious consideration for collaborating with circular companies or traditional economy companies"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img21.Id,
				CreatedAt = DateTime.Now
			};

			Question q22 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Informeren van afnemers",
						question = "Worden afnemers transparant geïnformeerd over de circulaire impact van de organisatie.",
						tooltip = "Transparante communicatie van wat goed gaat, maar ook van wat nog beter kan. Over materiaal- en energieverbruik van producten en diensten, de herkomst van materialen en recyclebaarheid, worden de True Price en Total Cost of Ownership gecommuniceerd?"
					},
					en = new
					{
						header = "Informing customers",
						question = "Are customers transparently informed about the circular impact of the organization?",
						tooltip = "Transparent communication of what is going well, but also of what can be improved. Does the communication include material and energy consumption of products and services, the origin of materials and recyclability, and are the True Price and Total Cost of Ownership communicated?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img22.Id,
				CreatedAt = DateTime.Now
			};

			Question q23 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Verantwoordelijkheid gebruiksfase",
						question = "Samenwerkingsverbanden aangaan om verantwoordelijkheid te nemen voor producten in de gebruiksfase en daarna?",
						tooltip = "Werk je samen zodat producten goed onderhouden en/of gerepareerd worden zodat producten langer efficiënt gebruikt worden? (onderhouds- en servicecontracten in samenwerking met andere marktpartijen) Werk je samen om product as a service of pay per performance, etc. te organiseren?"
					},
					en = new
					{
						header = "Responsibility usage phase",
						question = "Establishing partnerships to take responsibility for products in the usage phase and beyond?",
						tooltip = "Do you collaborate to ensure that products are well-maintained and/or repaired so that they are used efficiently for a longer period? (maintenance and service contracts in collaboration with other market parties) Do you collaborate to organize product as a service or pay-per-performance, etc.?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img23.Id,
				CreatedAt = DateTime.Now
			};

			Question q24 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Kennisdeling",
						question = "Kennisdeling, inspireren, etc met andere organisaties",
						tooltip = "Kennis delen waarmee de transitie kan worden versneld over producten, materialen en processen. Werkt u samen om inzichtelijk te maken waar in de keten de grootste milieu- en sociale impactverbetering te behalen is (bijv. aan de hand van LCA analyse)?"
					},
					en = new
					{
						header = "Knowledge sharing",
						question = "Knowledge sharing, inspiring, etc. with other organizations",
						tooltip = "Sharing knowledge to accelerate the transition regarding products, materials, and processes. Do you collaborate to identify where in the chain the greatest environmental and social impact improvement can be achieved (e.g., based on LCA analysis)?"
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img24.Id,
				CreatedAt = DateTime.Now
			};

			Question q25 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Overheidsbeleid",
						question = "samenwerking om overheidsbeleid en wet- en regelgeving te beïnvloeden die circulariteit in de weg staan?",
						tooltip = ""
					},
					en = new
					{
						header = "Government policy",
						question = "Collaboration to influence government policies and regulations that hinder circularity?",
						tooltip = ""
					}
				}),
				Category = c3,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img25.Id,
				CreatedAt = DateTime.Now
			};

			Question q26 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Visie en missie",
						question = "Staat de circulaire ambitie en meervoudige waardecreatie expliciet in de visie en missie van de organisatie?",
						tooltip = ""
					},
					en = new
					{
						header = "Vision and mission",
						question = "Does the circular ambition and multiple value creation explicitly state in the organization's vision and mission?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img27.Id,
				CreatedAt = DateTime.Now
			};

			Question q27 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Concrete doelen",
						question = "In hoeverre heeft de organisatie de visie vertaald naar concrete doelen om (uiterlijk in 2050) tot volledige circulariteit te komen?",
						tooltip = ""
					},
					en = new
					{
						header = "Concrete goals",
						question = "To what extent has the organization translated the vision into concrete goals to achieve full circularity (by 2050 at the latest)?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img27.Id,
				CreatedAt = DateTime.Now
			};

			Question q28 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Strategie",
						question = "In hoeverre heeft de organisatie een strategie om tot volledige circulariteit te komen?",
						tooltip = ""
					},
					en = new
					{
						header = "Strategy",
						question = "To what extent does the organization have a strategy to achieve full circularity?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img28.Id,
				CreatedAt = DateTime.Now
			};

			Question q29 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Voorbeeldfunctie",
						question = "In hoeverre heeft de organisatie een voorbeeldfunctie als koploper in de sector als het gaat om circulariteit?",
						tooltip = ""
					},
					en = new
					{
						header = "Role model",
						question = "To what extent does the organization serve as a role model as a frontrunner in the sector when it comes to circularity?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img29.Id,
				CreatedAt = DateTime.Now
			};

			Question q30 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Transparantie",
						question = "In hoeverre publiceert de organisatie transparant over hun circulaire en maatschappelijke impact (zowel positieve als negatieve impact) in bijvoorbeeld een jaarverslag?",
						tooltip = ""
					},
					en = new
					{
						header = "Transparency",
						question = "To what extent does the organization transparently publish their circular and societal impact (both positive and negative impact) in, for example, an annual report?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img30.Id,
				CreatedAt = DateTime.Now
			};

			Question q31 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Personeelsbeleid",
						question = "Is het personeelsbeleid expliciet gericht op sociale aspecten zoals inclusiviteit en behoud en ontwikkeling van werkvermogen.",
						tooltip = ""
					},
					en = new
					{
						header = "Personnel policy",
						question = "Is the personnel policy explicitly focused on social aspects such as inclusivity and retention and development of work capacity?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img31.Id,
				CreatedAt = DateTime.Now
			};

			Question q32 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Wervings-, scholings- en kennisbeleid",
						question = "Is het wervings-, scholings- en kennisbeleid gericht op kennis en kunde in huis halen en delen op gebied van circulaire economie.",
						tooltip = ""
					},
					en = new
					{
						header = "Recruitment, training, and knowledge policy",
						question = "Is the recruitment, training, and knowledge policy focused on acquiring and sharing knowledge and skills in the field of circular economy?",
						tooltip = ""
					}
				}),
				Category = c4,
				Weight = 1,
				Statement = false,
				Show = true,
				// ImageId = img32.Id,
				CreatedAt = DateTime.Now
			};

			Question q33 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Huisvesting",
						question = "In hoeverre zijn door organisatie gebruikte gebouwen en terreinen circulair ontwikkeld?",
						tooltip = ""
					},
					en = new
					{
						header = "Housing",
						question = "To what extent are buildings and premises used by the organization developed in a circular manner?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img33.Id,
				CreatedAt = DateTime.Now
			};

			Question q34 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Waterverbruik",
						question = "In hoeverre wordt zuinig omgegaan met water, regenwater opgevangen, waterberging en water gezuiverd?",
						tooltip = ""
					},
					en = new
					{
						header = "Water consumption",
						question = "To what extent is water used efficiently, rainwater harvested, water stored, and water purified?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img34.Id,
				CreatedAt = DateTime.Now
			};

			Question q35 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Afvalscheiding",
						question = "In welke mate doet de organisatie aan afvalscheiding in de bedrijfsvoering op kantoren en kantine e.d.?",
						tooltip = ""
					},
					en = new
					{
						header = "Waste separation",
						question = "To what extent does the organization practice waste separation in its operations in offices, canteens, etc.?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img35.Id,
				CreatedAt = DateTime.Now
			};

			Question q36 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Grondstoffen",
						question = "In welke mate worden zo min mogelijk grondstoffen en kantoorartikelen ge- en ver-bruikt? (papier, water, pennen, etc.)",
						tooltip = ""
					},
					en = new
					{
						header = "Raw materials",
						question = "To what extent are as few resources and office supplies used and consumed as possible? (paper, water, pens, etc.)",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img36.Id,
				CreatedAt = DateTime.Now
			};

			Question q37 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Inrichting",
						question = "In welke mate wordt kantoorinrichting zo lang mogelijk in gebruik gehouden en zo efficiënt mogelijk gebruikt? (laptops, bureaustoelen, flexibele werkplekken, etc.)",
						tooltip = ""
					},
					en = new
					{
						header = "Furnishing",
						question = "To what extent is office furniture kept in use for as long as possible and used as efficiently as possible? (laptops, office chairs, flexible workspaces, etc.)",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img37.Id,
				CreatedAt = DateTime.Now
			};

			Question q38 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Inkoopbeleid, circulair inkopen, lokaal inkopen",
						question = "In hoeverre is het inkoopbeleid (naast inkoop productie) gericht op circulair en lokaal inkopen? Denk aan kantoorinrichting en artikelen, etc.",
						tooltip = ""
					},
					en = new
					{
						header = "Procurement policy, circular procurement, local procurement",
						question = "To what extent is the procurement policy (besides production procurement) focused on circular and local sourcing? Think of office furniture and items, etc.",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 3,
				Statement = false,
				Show = true,
				ImageId = img38.Id,
				CreatedAt = DateTime.Now
			};

			Question q39 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Schoonmaak",
						question = "Wordt op een duurzame manier schoongemaakt?",
						tooltip = ""
					},
					en = new
					{
						header = "Cleaning",
						question = "Is cleaning done in a sustainable manner?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img39.Id,
				CreatedAt = DateTime.Now
			};

			Question q40 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Catering",
						question = "In hoeverre zijn producten in de kantine en catering duurzaam en lokaal en worden wegwerpartikelen vermeden?",
						tooltip = ""
					},
					en = new
					{
						header = "Catering",
						question = "To what extent are products in the canteen and catering sustainable and locally sourced, and are disposable items avoided?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img40.Id,
				CreatedAt = DateTime.Now
			};

			Question q41 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Duurzame energie",
						question = "Wordt door de organisatie duurzame energie opgewekt?",
						tooltip = ""
					},
					en = new
					{
						header = "Sustainable energy",
						question = "Does the organization generate sustainable energy?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img41.Id,
				CreatedAt = DateTime.Now
			};

			Question q42 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Energiegebruik",
						question = "Gebruikt uw organisatie groene stroom?",
						tooltip = ""
					},
					en = new
					{
						header = "Energy use",
						question = "Does your organization use green energy?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img42.Id,
				CreatedAt = DateTime.Now
			};

			Question q43 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Energiegebruik",
						question = "In hoeverre is de organisatie organisatiebreed bezig met energiebesparende maatregelen en verlagen van CO2 voetafdruk?",
						tooltip = "Weet u hoe u de CO2-voetafdruk van uw organisatie kunt berekenen? Kent uw de kosten die ontstaan door de actuele Europese CO2-prijs? Gebruikt uw organisatie tools om mogelijke energiebesparing te ontdekken? Kent u subsidiemogelijkheden voor energiebesparingen?"
					},
					en = new
					{
						header = "Energy use",
						question = "To what extent is the organization actively implementing energy-saving measures across the organization to reduce its CO2 footprint?",
						tooltip = "Do you know how to calculate your organization's CO2 footprint? Are you aware of the costs associated with the current European CO2 price? Does your organization use tools to identify potential energy savings? Are you familiar with subsidy opportunities for energy savings?"
					}
				}),
				Category = c5,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img43.Id,
				CreatedAt = DateTime.Now
			};

			Question q44 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Energiegebruik",
						question = "Compenseert uw organisatie de CO2-uitstoot?",
						tooltip = ""
					},
					en = new
					{
						header = "Energy use",
						question = "Does your organization offset its CO2 emissions?",
						tooltip = ""
					}
				}),
				Category = c5,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img44.Id,
				CreatedAt = DateTime.Now
			};

			Question q45 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Mobiliteit werknemers",
						question = "In hoeverre treft de organisatie maatregelen om de CO-voetafdruk van de mobiliteit van werknemers te verkleinen?",
						tooltip = "Heeft uw organisatie dienstauto’s? Kent uw bedrijf de woon-werk-trajecten van de medewerkers en het daarbij gebruikte vervoersmiddel? Gebruikt uw bedrijf openbaar vervoerskaarten? Schakelt uw bedrijf over van fossiel naar elektrisch vervoer? Kent uw bedrijf de CO2-voetafdruk van het (vlieg-)verkeer?"
					},
					en = new
					{
						header = "Employee mobility",
						question = "To what extent does the organization take measures to reduce the CO2 footprint of employee mobility?",
						tooltip = "Does your organization have company cars? Does your company know the commuting routes of its employees and the means of transportation used? Does your company use public transportation cards? Is your company transitioning from fossil fuel to electric vehicles? Does your company know the CO2 footprint of its (air) travel?"
					}
				}),
				Category = c5,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img45.Id,
				CreatedAt = DateTime.Now
			};

			Question q46 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Diversiteit",
						question = "In hoeverre heeft de organisatie een divers personeelsbestand?",
						tooltip = "Mensen met afstand tot arbeidsmarkt, gehandicapten, 50+ ers, leertrajecten, culturele achtergronden, gender"
					},
					en = new
					{
						header = "Diversity",
						question = "To what extent does the organization have a diverse workforce?",
						tooltip = "People with distance to the labor market, disabled people, individuals over 50, training programs, cultural backgrounds, gender"
					}
				}),
				Category = c6,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img46.Id,
				CreatedAt = DateTime.Now
			};

			Question q47 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Fysieke en mentale veiligheid",
						question = "In hoeverre is in de organisatie sprake van gezonde en veilige fysieke en mentale werkomstandigheden?",
						tooltip = "Informeren van werknemers over gezond en velig werken, gebruik van een antidiscriminatieprotocol, rekening houden met de balans tussen werk en privé, aandacht voor fysieke, sensorische (bv lawaai) en mentale belasting van werk, doelbewust beleid om verspilling en uitputting van medewerkers te voorkomen."
					},
					en = new
					{
						header = "Physical and mental safety",
						question = "To what extent are healthy and safe physical and mental working conditions maintained within the organization?",
						tooltip = "Informing employees about healthy and safe working, use of an anti-discrimination protocol, consideration for work-life balance, attention to physical, sensory (e.g. noise), and mental workloads, deliberate policy to prevent waste and exhaustion of employees."
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img47.Id,
				CreatedAt = DateTime.Now
			};

			Question q48 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Werkzekerheid",
						question = "In onze organisatie sprake van werkzekerheid voor de werknemers?",
						tooltip = "Niet onnodig werken met zzp-constructies en arbeidscontracten voor bepaalde tijd, enz."
					},
					en = new
					{
						header = "Job security",
						question = "Does our organization provide job security for its employees?",
						tooltip = "Avoiding unnecessary use of self-employed (ZZP) constructions and fixed-term employment contracts, etc."
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img48.Id,
				CreatedAt = DateTime.Now
			};

			Question q49 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Autonomie",
						question = "In onze organisatie zijn werknemers vrij om te beslissen hoe het werk wordt georganiseerd?",
						tooltip = "Thuiswerken, werktijden, enz."
					},
					en = new
					{
						header = "Autonomy",
						question = "In our organization, do employees have the freedom to decide how the work is organized?",
						tooltip = "Remote work, working hours, etc."
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img49.Id,
				CreatedAt = DateTime.Now
			};

			Question q50 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Competentie",
						question = "In onze organisatie sluit de kennis, vaardigheden en interesse van werknemers erg goed aan bij het werk dat ze doen?",
						tooltip = "In onze organisatie vinden werknemers het werk interessant. In onze organisatie is het talent van de werknemer het uitgangspunt voor de uit te voeren taken."
					},
					en = new
					{
						header = "Competence",
						question = "In our organization, do the knowledge, skills, and interests of employees align well with the work they do?",
						tooltip = "In our organization, employees find the work interesting. In our organization, the talent of the employee is the basis for the tasks to be performed."
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				// ImageId = img50.Id,
				CreatedAt = DateTime.Now
			};

			Question q51 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Betrokkenheid",
						question = "Er worden concrete acties ingezet zodat werknemers zich betrokken voelen bij de organisatie?",
						tooltip = "Bedrijfsuitjes, werktevredenheidsenquêtes, personeelscyclus"
					},
					en = new
					{
						header = "Engagement",
						question = "Are concrete actions taken to make employees feel engaged with the organization?",
						tooltip = "Company outings, employee satisfaction surveys, personnel cycle"
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img51.Id,
				CreatedAt = DateTime.Now
			};

			Question q52 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Ontwikkeling",
						question = "In onze organisatie wordt de ontwikkeling van kennis en vaardigheden en persoonlijke ontwikkeling gestimuleerd?",
						tooltip = "Werknemers kunnen werken aan persoonlijke ontwikkeling. Er worden ook werknemers aangenomen die niet direct perfect voldoen aan de vacature-eisen. Er is tijd en ruimte om te groeien in een baan. Werknemersbelang staat voorop bij het aanbieden van scholing. Er is tijd en geld voor coaching van werknemers, ook bij onvoldoende functioneren."
					},
					en = new
					{
						header = "Development",
						question = "In our organization, is the development of knowledge, skills, and personal growth encouraged?",
						tooltip = "Employees can work on personal development. Employees are hired even if they don't perfectly match the job requirements. There is time and space for growth within a job. Employee welfare is a priority when offering training. Time and resources are allocated for employee coaching, even when performance is insufficient."
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img52.Id,
				CreatedAt = DateTime.Now
			};

			Question q53 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Arbeid in externe keten",
						question = "In hoeverre wordt in de hele keten rekening gehouden met sociale aspecten zoals slavernij, kinderarbeid, extreme armoede, buitensporige werktijden, etc.?",
						tooltip = "Er wordt niet alleen gekeken naar arbeidsvoorwaarden binnen het bedrijf, maar in de hele levenscyclus van de producten van de organisatie, naar aspecten zoals minimum aanvaardbaar loon (het duurzame loon op lange termijn, of het fatsoenlijke leefloon op korte termijn), kinderarbeid (dwangarbeid, niet in staat om naar school te gaan), extreme armoede (afgeleid van de absolute armoedegrens van de Wereldbank), buitensporige werktijden (dwangarbeid, onvrijwillig), veiligheid en gezondheid op het werk (gebaseerd op statistieken van de IAO)"
					},
					en = new
					{
						header = "Labor in external chain",
						question = "To what extent are social aspects such as slavery, child labor, extreme poverty, excessive working hours, etc., taken into account throughout the entire chain?",
						tooltip = "Not only are labor conditions within the company considered, but throughout the entire lifecycle of the organization's products, aspects such as minimum acceptable wage (sustainable wage in the long term, or decent living wage in the short term), child labor (forced labor, inability to attend school), extreme poverty (derived from the World Bank's absolute poverty line), excessive working hours (forced labor, involuntary), workplace safety and health (based on ILO statistics)."
					}
				}),
				Category = c6,
				Weight = 2,
				Statement = false,
				Show = true,
				ImageId = img53.Id,
				CreatedAt = DateTime.Now
			};

			Question q54 = new Question()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						header = "Beloning",
						question = "In hoeverre krijgen werknemers een eerlijke beloning en goede secundaire arbeidsvoorwaarden?",
						tooltip = "scheve verhoudingen, tov branchegenoten, winstuitkering alle werknemers"
					},
					en = new
					{
						header = "Reward",
						question = "To what extent do employees receive fair compensation and good secondary employment benefits?",
						tooltip = "imbalance, compared to industry peers, profit sharing for all employees"
					}
				}),
				Category = c6,
				Weight = 1,
				Statement = false,
				Show = true,
				ImageId = img54.Id,
				CreatedAt = DateTime.Now
			};

			context.Questions.AddRange(
				q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, 
				q11, q12, q13, q14, q15, q16, q17, q18, q19, q20,
				q21, q22, q23, q24, q25, q26, q27, q28, q29, q30,
				q31, q32, q33, q34, q35, q36, q37, q38, q39, q40,
				q41, q42, q43, q44, q45, q46, q47, q48, q49, q50,
				q51, q52, q53, q54
			);

			await context.SaveChangesAsync();

			Advice aq1 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Kijk tijdens het ontwerpen van producten dat producten op het eind van hun levensfase in ieder geval veilig verbrand kunnen worden. Wat voor materialen gebruikt u voor uw producten en welke stoffen komen er vrij bij het verbranden van dit materiaal?",
					},
					en = new
					{
						data = "When designing products, ensure that products can at least be safely incinerated at the end of their life cycle. What materials do you use for your products and what substances are released when this material is burned?",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q1.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq2 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat de producten die u produceert recyclebaar zijn en ook gerecycled kunnen worden. Dit kunt u doen door uw product zo veel mogelijk van hetzelfde materiaal te maken en dus niet een product te maken met bijvoorbeeld 3 verschillende soorten plastic. U kunt ook nog een stap verder gaan en uw verkochte producten aan het eind van hun leven zelf in te zamelen.",
					},
					en = new
					{
						data = "Make sure that the products you produce are recyclable and can also be recycled. You can do this by making your product as much as possible from the same material and therefore not making a product with, for example, 3 different types of plastic. You can also go one step further and collect your sold products yourself at the end of their lives.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q2.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq3 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat producten op aan manier worden ontworpen zodat ze kunnen worden gerepareerd, opgeknapt en bijgewerkt. Dit kunt u doen door bijvoorbeeld zelf reparatie services aan te bieden of de garantie op producten te verhogen. Ook kan u kijken naar hoe makkelijk het product in en uit elkaar kan worden gehaald.",
					},
					en = new
					{
						data = "Ensure products are designed in a way that they can be repaired, refurbished and updated. You can do this, for example, by offering repair services yourself or increasing the warranty on products. You can also look at how easy the product can be put together and taken apart.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q3.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq4 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat uw producten worden ontworpen voor een lange levensduur. Dit kunt u doen door bijvoorbeeld statiegeld, een terugkoop regeling of biedt uw product aan als een service. In het laatste geval blijft het product van u en huurt u het product aan klanten. Ook kunt u promoten om het product te hergebruiken.",
					},
					en = new
					{
						data = "Make sure your products are designed for a long lifespan. You can do this, for example, through a deposit, a buyback scheme or offering your product as a service. In the latter case, the product remains yours and you rent the product to customers. You can also promote reusing the product.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q4.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq5 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat u zo min mogelijk (kritieke) grondstoffen, water en energie gebruikt. Als u kritieke grondstoffen gebruikt, kunt u deze dan verplaatsen door niet kritieke grondstoffen? Is het mogelijk om het product kleiner aan te bieden? Kunt u wat besparen met een andere verpakking? De grondstoffen scanner geeft inzichten in welke grondstoffen in de toekomst schaars worden.",
					},
					en = new
					{
						data = "Make sure you use as little (critical) raw materials, water and energy as possible. If you use critical resources, can you replace them with non-critical resources? Is it possible to offer the product in a smaller size? Can you save some money with different packaging? The raw materials scanner provides insights into which raw materials will become scarce in the future.",
					}
				}),
				AdditionalLink = "https://www.grondstoffenscanner.nl",
				AdditionalLinkName = "Grondstoffenscanner",
				QuestionId = q5.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq6 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat producten worden ontworpen vanuit circulariteitsprincipe. Kijk eens naar verschillende circulaire businessmodellen via de scan die hierboven staat.",
					},
					en = new
					{
						data = "Ensure that products are designed based on the circularity principle. Take a look at different circular business models via the scan above.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q6.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq7 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Bedenk oplossingen waardoor producten overbodig worden of bedenk een duurzamere versie van een product. De e-reader zorgde er bijvoorbeeld voor dat er minder boeken geprint hoefde te worden.",
					},
					en = new
					{
						data = "Come up with solutions that make products redundant or come up with a more sustainable version of a product. For example, the e-reader meant that fewer books had to be printed.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q7.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq8 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Richt een logistiek systeem in voor het inzamelen, sorteren en traceren van producten, onderdelen en materialen. Zo komt u er achter waar u minder circulair bezig bent en dus kunt verbeteren.",
					},
					en = new
					{
						data = "Set up a logistics system for collecting, sorting and tracing products, parts and materials. This way you will find out where you are less circular and can therefore improve.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q8.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq9 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat producten worden ontworpen vanuit circulariteitsprincipe. Kijk eens naar verschillende circulaire businessmodellen via de scan die hierboven staat.",
					},
					en = new
					{
						data = "Ensure that products are designed based on the circularity principle. Take a look at different circular business models via the scan above.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q9.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq10 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Kijk eens naar hoe groot gedeelte van uw omzet bestaat uit circulaire activiteiten. Hoe kunt u dit percentage vergroten?",
					},
					en = new
					{
						data = "Take a look at how much of your turnover consists of circular activities. How can you increase this percentage?",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q10.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};
			
			Advice aq11 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Voer een Levens Cyclus Analyse uit over uw producten. Met een Life Cycle Analyse kijkt u naar de impact van de volledige levenscyclus van de producten of services. U kunt hier bijvoorbeeld de ISO 14000 richtlijn voor gebruiken.",
					},
					en = new
					{
						data = "Perform a Life Cycle Analysis on your products. With a Life Cycle Analysis you look at the impact of the complete life cycle of the products or services. For example, you can use the ISO 14000 guideline for this.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q11.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq12 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat u bedrijfsprocessen zo inricht dat materialen, energie en water zo min mogelijk verloren gaan. Een eerste stap hiervoor kan zijn het inzichtelijk maken waar energie, water en materialen nu verloren gaan in het proces.",
					},
					en = new
					{
						data = "Make sure that you organize business processes in such a way that materials, energy and water are lost as little as possible. A first step in this regard can be to gain insight into where energy, water and materials are lost in the process.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q12.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq13 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Daarna kunt u gaan kijken naar hoe u dit dan gaat verbeteren, kunt u het verloren materiaal hergebruiken of recyclen?",
					},
					en = new
					{
						data = "You can then look at how you are going to improve this, can you reuse or recycle the lost material?",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q13.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq14 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat u onderhoud en reparaties inplant voor gebouwen en machines zodat ze langer meegaan.",
					},
					en = new
					{
						data = "Make sure you schedule maintenance and repairs for buildings and machines to ensure they last longer.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q14.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq15 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat u weet welke emissies er tijdens productieprocessen en logistiek vrijkomen en probeer schadelijk emissies te reduceren. Fabrieken kunnen dit bijvoorbeeld doen door cyclonen, Aero cyclonen, bezink kamers of elektrostatische stofvangers. Door voldoende lucht toe te voegen aan verbranding zorg je voor een volledige verbranding waar er minder gevaarlijke stoffen vrijkomen. In logistiek kun je emissies voorkomen door op groene energie te rijden.",
					},
					en = new
					{
						data = "Make sure you know what emissions are released during production processes and logistics and try to reduce harmful emissions. Factories can do this, for example, through cyclones, Aero cyclones, settling chambers or electrostatic precipitators. By adding sufficient air to combustion you ensure complete combustion where fewer hazardous substances are released. In logistics you can prevent emissions by driving on green energy.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q15.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq16 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat u energie uit hernieuwbare bronnen gebruikt voor productie en logistiek.",
					},
					en = new
					{
						data = "Make sure you use energy from renewable sources for production and logistics.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q16.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq17 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Verminder waar mogelijk verpakkingsmateriaal. Heeft u ter bescherming wel verpakkingsmateriaal nodig koop dan circulair verpakkingsmateriaal in.",
					},
					en = new
					{
						data = "Reduce packaging materials where possible. If you need packaging material for protection, purchase circular packaging material.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q17.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq18 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga meer samenwerken met andere stakeholders om samen kringlopen te sluiten.  Dit is natuurlijk makkelijker gezegd dan gedaan. Begin met het inzichtelijk maken door middel van een stakeholder analyse welke partijen u allemaal nodig heeft voor een circulair bedrijfsproces.",
					},
					en = new
					{
						data = "Collaborate more with other stakeholders to close cycles together. This is of course easier said than done. Start by providing insight into which parties you need for a circular business process through a stakeholder analysis.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q18.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq19 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga structureel samenwerken met andere organisaties in de keten aan impactverbetering op milieu en sociale aspecten. Dit kunt u bijvoorbeeld doen door dit onderwerp onder de aandacht te brengen bij een branchevereniging.",
					},
					en = new
					{
						data = "Structurally collaborate with other organizations in the chain to improve impact on the environment and social aspects. You can do this, for example, by bringing this subject to the attention of a trade association.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q19.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq20 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Stel circulariteit eisen aan leveranciers met betrekking tot circulariteit. Een begin stap hierin zou zijn de eisen op papier zetten. Hierin kunt u overheidsinstanties als voorbeeld nemen. Ze moeten vanwege beleid veel circulair inkopen en hebben hier dan vaak ook een beleidsplan voor.",
					},
					en = new
					{
						data = "Set circularity requirements for suppliers with regard to circularity. A starting step in this would be to put the requirements on paper. You can use government agencies as an example. Due to policy, they have to make a lot of circular purchasing and often have a policy plan for this.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q20.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq21 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Stel circulaire-eisen aan klanten en opdrachtgevers. Dit is makkelijker gezegd dan gedaan en klinkt wellicht spannend. Dit kan al worden gedaan door klanten en opdrachtgevers te vragen wat ze aan circulaire economie doen.",
					},
					en = new
					{
						data = "Set circularity requirements for suppliers with regard to circularity. A starting step in this would be to put the requirements on paper. You can use government agencies as an example. Due to policy, they have to make a lot of circular purchasing and often have a policy plan for this.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q21.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq22 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Informeer uw afnemers transparant over uw circulaire impact. Stap een hierin is uw circulaire impact inzichtelijk maken. Dit zou u bijvoorbeeld kunnen doen door middel van de Corporate Sustainable Reporting Directive (CSRD) en daarvan European Sustainability Reporting Standard (ESRS) Environment 5 Circulaire economie.",
					},
					en = new
					{
						data = "Inform your customers transparently about your circular impact. Step one is to gain insight into your circular impact. You could do this, for example, through the Corporate Sustainable Reporting Directive (CSRD) and its European Sustainability Reporting Standard (ESRS) Environment 5 Circular economy.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q22.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq23 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga samenwerking aan om verantwoordelijkheid te nemen voor producten in de gebruiksfase en daarna. Zoek concurrerende bedrijven die circulaire economie ook belangrijk vinden. Bedenk samen hoe u verantwoordelijkheid kan nemen voor producten na de gebruikersfase.",
					},
					en = new
					{
						data = "Collaborate to take responsibility for products in the use phase and beyond. Find competitive companies that also consider the circular economy important. Consider together how you can take responsibility for products after the user phase.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q23.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq24 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Deel de kennis die u heeft opgenomen met andere door bijvoorbeeld naar een conferentie te gaan.",
					},
					en = new
					{
						data = "Share the knowledge you have absorbed with others, for example by going to a conference.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q24.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq25 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Werk samen met andere om overheidsbeleid en wet en regelgevingen te beïnvloeden die circulariteit in de weg staan. Dit kan bijvoorbeeld door het onderwerp onder de aandacht van de branchevereniging te brengen.",
					},
					en = new
					{
						data = "Work with others to influence government policies and laws and regulations that hinder circularity. This can be done, for example, by bringing the subject to the attention of the trade association.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q25.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq26 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Formuleer een circulaire ambitie gericht op meervoudige waarde creatie en benoem deze expliciet in de missie en visie van de organisatie.",
					},
					en = new
					{
						data = "Formulate a circular ambition aimed at multiple value creation and state this explicitly in the mission and vision of the organization.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q26.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq27 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Vertaal uw circulaire visie om uiterlijk in 2050 tot een volledig circulaire bedrijfsvoering te komen, naar concrete doelen. Hiervoor kunt u een work breakdown structuur gebruiken.",
					},
					en = new
					{
						data = "Translate your circular vision to achieve fully circular business operations by 2050 at the latest into concrete goals. You can use a work breakdown structure for this.",
					}
				}),
				AdditionalLink = "https://asana.com/pl/resources/work-breakdown-structure",
				AdditionalLinkName = "Work breakdown structure",
				QuestionId = q27.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq28 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Maak gebaseerd op uw circulaire doelen een strategie om tot volledige circulariteit te komen.",
					},
					en = new
					{
						data = "Create a strategy to achieve full circularity based on your circular goals.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q28.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			// Advice aq29 = new Advice()
			// {
			// 	Data = JsonConvert.SerializeObject(new
			// 	{
			// 		nl = new
			// 		{
			// 			data = "",
			// 		},
			// 		en = new
			// 		{
			// 			data = "",
			// 		}
			// 	}),
			// 	AdditionalLink = null,
			// 	AdditionalLinkName = null,
			// 	QuestionId = q29.Id,
			// 	Condition = 2,
			// 	CreatedAt = DateTime.Now
			// };

			// Advice aq30 = new Advice()
			// {
			// 	Data = JsonConvert.SerializeObject(new
			// 	{
			// 		nl = new
			// 		{
			// 			data = "",
			// 		},
			// 		en = new
			// 		{
			// 			data = "",
			// 		}
			// 	}),
			// 	AdditionalLink = null,
			// 	AdditionalLinkName = null,
			// 	QuestionId = q30.Id,
			// 	Condition = 2,
			// 	CreatedAt = DateTime.Now
			// };

			// Advice aq31 = new Advice()
			// {
			// 	Data = JsonConvert.SerializeObject(new
			// 	{
			// 		nl = new
			// 		{
			// 			data = "",
			// 		},
			// 		en = new
			// 		{
			// 			data = "",
			// 		}
			// 	}),
			// 	AdditionalLink = null,
			// 	AdditionalLinkName = null,
			// 	QuestionId = q31.Id,
			// 	Condition = 2,
			// 	CreatedAt = DateTime.Now
			// };

			// Advice aq32 = new Advice()
			// {
			// 	Data = JsonConvert.SerializeObject(new
			// 	{
			// 		nl = new
			// 		{
			// 			data = "",
			// 		},
			// 		en = new
			// 		{
			// 			data = "",
			// 		}
			// 	}),
			// 	AdditionalLink = null,
			// 	AdditionalLinkName = null,
			// 	QuestionId = q32.Id,
			// 	Condition = 2,
			// 	CreatedAt = DateTime.Now
			// };

			
			// Advice aq33 = new Advice()
			// {
			// 	Data = JsonConvert.SerializeObject(new
			// 	{
			// 		nl = new
			// 		{
			// 			data = "",
			// 		},
			// 		en = new
			// 		{
			// 			data = "",
			// 		}
			// 	}),
			// 	AdditionalLink = null,
			// 	AdditionalLinkName = null,
			// 	QuestionId = q33.Id,
			// 	Condition = 2,
			// 	CreatedAt = DateTime.Now
			// };

			Advice aq34 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga zuiniger om met water. Dit kunt u doen door bijvoorbeeld het opvangen van regenwater. Regenwater kunt u verder zuiveren of op zichzelf gebruiken om bijvoorbeeld het toilet door te spoelen.",
					},
					en = new
					{
						data = "Use water more sparingly. You can do this, for example, by collecting rainwater. You can further purify rainwater or use it on its own, for example to flush the toilet.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q34.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq35 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg ervoor dat afval scheiden in uw eigen pand makkelijk wordt gemaakt. Dit kunt u bijvoorbeeld doen door gescheiden afvalbakken neer te zetten.",
					},
					en = new
					{
						data = "Make sure that waste separation is made easy in your own building. You can do this, for example, by placing separate waste bins.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q35.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq36 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga zuinig om met kantoorartikelen zoals papier, water en pennen, kies tijdens het inkopen voor grondstoffen die lang meegaan en recyclebaar zijn. Kijk bijvoorbeeld eens naar schriften waarbij je inkt kan uitwissen.",
					},
					en = new
					{
						data = "Use office supplies such as paper, water and pens sparingly, and when purchasing, choose raw materials that last a long time and are recyclable. For example, look at notebooks where you can erase ink.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q36.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq37 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Maak zo lang mogelijk gebruik van laptops, bureaustoelen, tafels etc. Als ze wel kapot zijn kunt u er altijd voor kiezen om ze eerst te repareren. Hiernaast helpt het inzetten van flexwerkplekken met het verminderen van verbruik.",
					},
					en = new
					{
						data = "Use laptops, office chairs, tables, etc. for as long as possible. If they are broken, you can always choose to repair them first. In addition, the use of flexible workplaces helps to reduce consumption.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q37.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq38 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Koop spullen lokaal en circulair in. Kijk eens welke bedrijven er in de buurt zitten. Dit is ook nog eens goed voor de lokale economie. Als een stap hierboven op kunt u kijken naar circulair en lokale inkoop. Welke circulaire bedrijven zitten er in de buurt? Hier kunt u een gratis e-learning circulair inkopen volgen.",
					},
					en = new
					{
						data = "Buy things locally and circularly. Take a look at which companies are nearby. This is also good for the local economy. As a step above, you can look at circular and local purchasing. Which circular companies are nearby?",
					}
				}),
				AdditionalLink = "https://elearning.ikwilcirculairinkopen.nl/nl",
				AdditionalLinkName = "E-learning Circulair inkopen",
				QuestionId = q38.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq39 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Maak duurzaam schoon. Dit kun je doen door bijvoorbeeld duurzame schoonmaakspullen te gebruiken en niet te veel heet water te gebruiken.",
					},
					en = new
					{
						data = "Clean sustainably. You can do this, for example, by using sustainable cleaning supplies and not using too much hot water.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q39.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq40 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Zorg voor een duurzame catering. Dit kunt u doen door van lokale ondernemers in te kopen en zo min mogelijk wegwerpartikelen te gebruiken. Ook het aanbieden van vegetarische of veganistische opties kan hieraan bijdragen.",
					},
					en = new
					{
						data = "Provide sustainable catering. You can do this by purchasing from local entrepreneurs and using as few disposable items as possible. Offering vegetarian or vegan options can also contribute to this.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q40.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq41 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Maak gebruik van duurzame energie bijvoorbeeld door middel van een duurzame energieprovider of door zelf duurzame energie op te wekken.",
					},
					en = new
					{
						data = "Use sustainable energy, for example through a sustainable energy provider or by generating sustainable energy yourself.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q41.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq42 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Ga organisatie breed aan de slag met het besparen van energie en het verlagen van uw voetafdruk.",
					},
					en = new
					{
						data = "Work across the organization to save energy and reduce your footprint.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q42.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq43 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Het is natuurlijk belangrijk om weinig CO2 uit te stoten, maar het is ook mogelijk om CO2 te compenseren wanneer u het wel uitstoot. Dit kan via verschillende wegen. U kunt bijvoorbeeld bomen gaan planten met uw werknemers (Tree Nation) of u kunt gemakkelijk online een bedrag betalen via bijvoorbeeld Climate Neutral Group. Daarbij is voorkomen altijd beter dan compenseren.",
					},
					en = new
					{
						data = "It is of course important to emit little CO2, but it is also possible to compensate for CO2 when you do emit it. This can be done through various ways. For example, you can plant trees with your employees (Tree Nation) or you can easily pay an amount online via, for example, Climate Neutral Group. Prevention is always better than compensation.",
					}
				}),
				AdditionalLink = "https://tree-nation.com",
				AdditionalLinkName = "Tree nation",
				QuestionId = q43.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq44 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Hierbij kunt u bijvoorbeeld denken aan de CO 2 afdruk van de mobiliteit van werknemers.",
					},
					en = new
					{
						data = "For example, consider the CO2 footprint of employee mobility.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q44.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq45 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Werk aan een divers personeelsbestand. Probeer bij een nieuwe sollicitatie niet te kijken naar leeftijd, etniciteit of überhaupt foto's. Zo zorgt u voor gelijkere kansen. Of ga nog een stap verder en maak gebruik van open hiring net zoals Greyston Bakery.",
					},
					en = new
					{
						data = "Build a diverse workforce. When applying for a new job, try not to look at age, ethnicity or photos at all. This way you ensure more equal opportunities. Or go one step further and use open hiring just like Greyston Bakery.",
					}
				}),
				AdditionalLink = "https://www.acf.hhs.gov/sites/default/files/documents/opre/Pathways-Case-Study-Greyston.pdf",
				AdditionalLinkName = "Greyston Bakery's Open Hiring Model",
				QuestionId = q45.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq46 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Werk aan een gezonde en veilige fysieke en mentale werkomstandigheden. De fysieke werkomstandigheden zijn wellicht directer zichtbaar en erg belangrijk. Zorg er bijvoorbeeld voor dat werknemers op tijd pauze kunnen nemen, het niet te warm of koud wordt in uw pand etc. Lees hier meer over op de site van het RIVM.",
					},
					en = new
					{
						data = "Work on healthy and safe physical and mental working conditions. The physical working conditions are perhaps more immediately visible and very important. For example, ensure that employees can take a break on time, it does not get too hot or cold in your building, etc. Read more about this on the RIVM website.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q46.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq47 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Werk aan de werkzekerheid voor werknemers. Kun je bijvoorbeeld meer vaste contracten aanbieden?",
					},
					en = new
					{
						data = "Work on job security for employees. For example, can you offer more permanent contracts?",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q47.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			Advice aq48 = new Advice()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						data = "Geef werknemers meer vrijheid om te beslissen hoe werk wordt georganiseerd. Luister meer naar werknemers. Zet processen in stand om werknemers waardoor je naar werknemers moet luisteren. Zet bijvoorbeeld een ideeën box in een pauze ruimte neer, waar werknemers anoniem ideeën in kunnen stoppen.",
					},
					en = new
					{
						data = "Give employees more freedom to decide how work is organized. Listen more to employees. Establish processes around employees that require you to listen to employees. For example, place an ideas box in a break room where employees can anonymously put ideas in.",
					}
				}),
				AdditionalLink = null,
				AdditionalLinkName = null,
				QuestionId = q48.Id,
				Condition = 2,
				CreatedAt = DateTime.Now
			};

			context.Advices.AddRange(
				aq1, aq2, aq3, aq4, aq5, aq6, aq7, aq8, aq9, aq10, aq11, aq12, aq13, aq14, aq15, 
				aq16, aq17, aq18, aq19, aq20, aq21, aq22, aq23, aq24, aq25, aq26, aq27, aq28,
				aq34, aq35, aq36, aq37, aq38, aq39, aq40, aq41, aq42, aq43, aq44, aq45, aq46, aq47, aq48
			);

			await context.SaveChangesAsync();

			Sector s1 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Agrarisch",
					},
					en = new
					{
						name = "Agricultural",
					}
				}),
			};

			Sector s2 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Bouw",
					},
					en = new
					{
						name = "Build",
					}
				}),
			};

			Sector s3 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Detailhandel",
					},
					en = new
					{
						name = "Retail",
					}
				}),
			};

			Sector s4 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Energie",
					},
					en = new
					{
						name = "Energy",
					}
				}),
			};

			Sector s5 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Financiële dienstverlening",
					},
					en = new
					{
						name = "Financial services",
					}
				}),
			};

			Sector s6 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Gezondheidszorg",
					},
					en = new
					{
						name = "Healthcare",
					}
				}),
			};

			Sector s7 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Horeca",
					},
					en = new
					{
						name = "Catering industry",
					}
				}),
			};

			Sector s8 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "ICT",
					},
					en = new
					{
						name = "IT",
					}
				}),
			};

			Sector s9 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Industrie",
					},
					en = new
					{
						name = "Industry",
					}
				}),
			};

			Sector s10 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Landbouw",
					},
					en = new
					{
						name = "Agriculture",
					}
				}),
			};

			Sector s11 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Onderwijs",
					},
					en = new
					{
						name = "Education",
					}
				}),
			};

			Sector s12 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Overheid",
					},
					en = new
					{
						name = "Government",
					}
				}),
			};

			Sector s13 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Transport",
					},
					en = new
					{
						name = "Transport",
					}
				}),
			};

			Sector s14 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Vastgoed",
					},
					en = new
					{
						name = "Property",
					}
				}),
			};

			Sector s15 = new Sector()
			{
				Data = JsonConvert.SerializeObject(new
				{
					nl = new
					{
						name = "Zakelijke dienstverlening",
					},
					en = new
					{
						name = "Business services",
					}
				}),
			};

			context.Sectors.AddRange(
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			);

			await context.SaveChangesAsync();
		}
	}
}
