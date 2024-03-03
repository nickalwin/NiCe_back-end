using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using NiCeScanner.Models;

namespace NiCeScanner.Data
{
	public class ApplicationDbInitializer
	{
		public static void Seed(IApplicationBuilder appBuilder)
		{
			using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				if (!context.Categories.Any())
				{
					Category c1 = new Category()
					{
						Name = "Innovatie in grondstofgebruik",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					Category c2 = new Category()
					{
						Name = "Productie en logistiek",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					Category c3 = new Category()
					{
						Name = "Ketensamenwerking",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					Category c4 = new Category()
					{
						Name = "Ambitie",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					Category c5 = new Category()
					{
						Name = "Facilitair",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					Category c6 = new Category()
					{
						Name = "Arbeid",
						Show = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
					};

					context.Categories.AddRange(
						c1, c2, c3, c4, c5, c6
					);

					if (!context.Questions.Any())
					{
						Question q1 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Recover; Laagste niveau circulariteit: Primaire bedrijfsprocessen zorgen ervoor dat producten zo worden ontworpen dat materialen aan het einde van de levensduur veilig worden verbrand met terugwinning van energie?",
									tooltip = "Bij diensten: wordt dmv van uw diensten vermeden dat poducten en materialen niet meer veilig kunnen worden verbrand met terugwinning van energie?"
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q2 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Recycling;  In welke mate zijn de materialen in uw product gerecycled en recyclebaar, kunnen de materialen worden verwerkt tot grondstoffen met, bij voorkeur, dezelfde (hoogwaardige) of eventueel tot mindere (laagwaardige) kwaliteit dan de oorspronkelijke grondstof tot en wordt dit in de praktijk toegepast/geborgd?",
									tooltip = "Bij diensten: Zet je producten die uit materialen bestaan die gerecycled kunnen worden?"
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q3 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Repair, Refurbish, Remanufacture en Repurpose; Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen zodat ze op efficiënte wijze worden gerepareerd, opgeknapt of geupdated? Zit er lange garantie op? Maakt u zelf gebruik van opgeknapte onderdelen/producten?",
									tooltip = "Bij diensten: repair café, onderhoudscontracten, refurbishen, repareerbare producten inzetten bij diensten. Kun je vervangende onderdelen van reeds geleverde producten snel leveren aan je klanten? Lever je nog onderdelen van producten die uit de handel zijn genomen? Investeer je in betaalbaar onderhoud voor je klanten zodat de producten uit je productaanbod langer mee kunnen? Worden geretourneerde producten opgeknapt om opnieuw verkocht te worden (gerefurbished)? Verkoop je producten waarbij de onderdelen op een efficiënte wijze vervangen kunnen worden?"
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q4 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Reuse: Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen voor een lange levensduur, producten en onderdelen daarvan worden hergebruikt in dezelfde functie en wordt dit in de praktijk door u geborgd (bv door statiegeld/terugkooopregeling)",
									tooltip = "Bij diensten: bv gebruikte producten inzetten. Worden producten die bij uw diensten worden ingezet meerdere malen gebruikt? Zorgen voor delen en herverdelen van oude producten."
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q5 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Reduce: Primaire bedrijfsprocessen zorgen ervoor dat er continue aandacht is voor reductie van grondstoffen en primaire en kritieke grondstoffen, energie en water in producten en in de gebruiksfase?",
									tooltip = "Bij diensten: Zet je producten in waarbij nagedacht is over efficiënt grondstofgebruik in productie- en gebruiksfase? Onderhoud van apparaten/machines zodat ze zuinig blijven."
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q6 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Rethink: Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen vanuit cirulariteitsprincipes, om zo lang mogelijk mee te gaan, voor standaardisatie en compatibiliteit, makkelijk onderhoud en reparatie, op te waarderen en aan te passen, demontage & recycling met end-of-life strategiën en alles met zo laag mogelijke negatieve impact?",
									tooltip = "Wordt uw product bijvoorbeeld via platforms aangeboden t.b.v. intensiever gebruik? Of kan deze multifunctioneel worden ingezet?"
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q7 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Refuse: Voorkom je gebruik van grondstoffen, door oplossingen te gebruiken of ontwikkelen, waarmee producten overbodig worden, door van de functie af te zien of die met een radicaal ander en duurzamer product te leveren/gebruiken?",
									tooltip = "Bij diensten: bijvoorbeeld een dienst ontwikkelen waardoor geen nieuwe producten hoeven worden gemaakt, bv een dienst om luiers te wassen. Heet water ipv chemische onkruidverdelger. Innovatie waarmee materialen gebruik wordt voorkomen"
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q8 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Primaire bedrijfsprocessen zorgen ervoor dat er een logistiek systeem ingericht voor inzameling, sorteren en traceren van producten, onderdelen en materialen?",
									tooltip = "Weet wat er met producten gebeurt, wat gaat als eerste kapot, wanneer en waarom worden producten afgedankt, worden ze gerepareerd, doorverkocht, gemodificeerd, weet waar de materialen in producten zijn, etc."
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q9 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Primaire bedrijfsprocessen zorgen ervoor dat er doelbewust wordt gewerkt met circulaire business modellen en verdienmodellen?",
									tooltip = "Er zijn veel verschillende circulaire business modellen en verdienmodellen. Bijvoorbeeld: Prestatiemodel (contract obv pay-per-performance), Tussenbatermodel (terugwinnen, opknappen & doorverkopen of repareren), Toegangmodel (toegang verschappen to product), klassiek duurzaam model (lange levensduur) en hybride model (combinatie lange levensduur en verkoop van bijbehorende verbruiksproducten)."
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q10 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Een groot deel van de omzet van het bedrijf bestaat uit circulaire activiteiten",
									tooltip = ""
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q11 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Primaire bedrijfsprocessen zorgen ervoor dat uitvoeren van een LCA (LevensCyclus Analyse) of iets vergelijkbaars, standaard is?",
									tooltip = ""
								}
							}),
							Category = c1,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q12 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Primaire bedrijfsprocessen zijn zo ingericht dat zo min mogelijk materialen, energie en water verloren gaan in productie/bedrijfsprocessen?",
									tooltip = "afval uitval"
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};
						
						Question q13 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Primaire bedrijfsprocessen zijn zo ingericht dat reststromen (materialen, energie, water) uit productie/bedrijfsprocessen hergebruikt of gerecycled worden?",
									tooltip = ""
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q14 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Verleng je de levensduur van je materiele activa, zoals machines en gebouwen door gepland onderhoud en reparaties?",
									tooltip = "levensduur machines"
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q15 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Zorg je voor minimale schadelijke emissies tijdens productie/bedrijfsprocessen en logistiek?",
									tooltip = "Bv lokaal inkopen, milieuvriendelijke productieprocessen, efficiënte en duurzame transportmiddelen, niet voor niets rijden, etc"
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q16 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Gebruik je energie uit hernieuwbare bronnen voor bedrijfsprocessen/productie (zon, wind, water, aardwarmte)?",
									tooltip = "duurzame energie"
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q17 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Wordt geen of circulair verpakkingsmateriaal gebruikt en wordt het verpakkingsmateriaal (karton, pallets) voor opslag en vervoer teruggenomen na levering?",
									tooltip = "verpakkingen"
								}
							}),
							Category = c2,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q18 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Wordt structureel samengewerkt met stakeholders om kringlopen te sluiten binnen en/of buiten de keten?",
									tooltip = "Samenwerking in verwaarden van reststromen (materialen, warmte, etc) afnemen van en/of leveren aan andere organisaties. Maak je afspraken met je klanten over mogelijke terugname van geleverde producten na gebruik? (zoals statiegeld of een minimale prijs waarvoor je producten wilt terugkopen na gebruik)?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q19 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Werkt u structureel samen met andere organisaties in de keten aan impactverbetering op milieu en sociale aspecten?",
									tooltip = "Breed, regie, structureel vs incidenteel, Is keten coördinator aanwezig?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q20 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Stel je eisen aan leveranciers wb circulariteit? (toxiciteit en recyclebaarheid van materialen, gerecyclede materialen, arbeidsomstandigheden, etc)",
									tooltip = "Verzamel je de informatie over de herkomst van de materialen en de omstandigheden waaronder mensen deze materialen ontginnen?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q21 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Klantenportfolio, stel je eisen aan je klanten/opdrachtgevers wb circulariteit?",
									tooltip = "bv afstand, intenties, waarvoor worden producten gebruikt, wordt bewust nagedacht over samenwerken met circulaire bedrijven of oude econmie bedrijven"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q22 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Worden afnemers transparant geïnformeerd over de circulaire impact van de organisatie.",
									tooltip = "Transparante communicatie van wat goed gaat, maar ook van wat nog beter kan. Over materiaal- en energieverbruik van producten en diensten, de herkomst van materialen en recyclebaarheid, worden de True Price en Total Cost of Ownership gecommuniceerd?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q23 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Samenwerkingsverbanden aangaan om verantwoordelijkheid te nemen voor producten in de gebruiksfase en daarna?",
									tooltip = "Werk je samen zodat producten goed onderhouden en/of gerepareerd worden zodat producten langer efficiënt gebruikt worden? (onderhouds en servicecontracten in samenwerking met andere marktpartijen) Werk je samen om product as a service of pay per performance, etc te organiseren?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q24 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Kennisdeling, inspireren, etc met andere organisaties",
									tooltip = "Kennis delen waarmee de transitie kan worden versneld over producten, materialen en processen. Werkt u samen om inzichtelijk te maken waar in de keten de grootste milieu en sociale impactverbetering te behalen is (bv a.h.v. LCA analyse)?"
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q25 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "samenwerking om overheidsbeleid en wet- en regelgeving te beïnvloeden die circulariteit in de weg staan?",
									tooltip = ""
								}
							}),
							Category = c3,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q26 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Staat de circulaire ambitie en meervoudige waardecreatie expliciet in de visie en missie van de organisatie?",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q27 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre heeft de organisatie de visie vertaald naar concrete doelen om (uiterlijk in 2050) tot volledige circulariteit te komen?",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q28 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre heeft de organisatie een strategie om tot volledige circulariteit te komen?",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q29 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre heeft de organisatie een voorbeeldfunctie als koploper in de sector als het gaat om circulariteit?",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q30 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre publiceert de organisatie transparant over hun circulaire en maatschappelijke impact (zowel positieve als negatieve impact) in bijvoorbeeld een jaarverslag?",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q31 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Is het personeelsbeleid expliciet gericht op sociale aspecten zoals inclusiviteit en behoud en ontwikkeling van werkvermogen.",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q32 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Is het wervings-, scholings- en kennisbeleid gericht op kennis en kunde in huis halen en delen op gebied van circulaire economie.",
									tooltip = ""
								}
							}),
							Category = c4,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q33 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre zijn door organisatie gebruikte gebouwen en terreinen circulair ontwikkeld?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q34 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre wordt zuinig omgegaan met water, regenwater opgevangen, waterberging en water gezuiverd?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q35 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In welke mate doet de organisatie aan afvalscheiding in de bedrijfsvoering op kantoren en kantine e.d.?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q36 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In welke mate worden zo min mogelijk grondstoffen en kantoorartikelen ge- en ver-bruikt? (papier, water, pennen, etc.)",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q37 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In welke mate wordt kantoorinrichting zo lang mogelijk in gebruik gehouden en zo efficiënt mogelijk gebruikt? (laptops, bureaustoelen, flexibele werkplekken, etc.)",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q38 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre is het inkoopbeleid (naast inkoop productie) gericht op circulair en lokaal inkopen? Denk aan kantoorinrichting en artikelen, etc.",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q39 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Wordt op een duurzame manier schoongemaakt?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q40 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre zijn producten in de kantine en catering duurzaam en lokaal en worden wegwerpartikelen vermeden?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q41 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Wordt door de organisatie duurzame energie opgewekt?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q42 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Gebruikt uw organisatie groene stroom?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q43 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre is de organisatie organisatiebreed bezig met energiebesparende maatregelen en verlagen van CO2 voetafdruk?",
									tooltip = "Weet u hoe u de CO2-voetafdruk van uw organisatie kunt berekenen? Kent uw de kosten die ontstaan door de actuele Europese CO2-prijs? Gebruikt uw organisatie tools om mogelijke energiebesparing te ontdekken? Kent u subsidiemogelijkheden voor energiebesparingen?"
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q44 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Compenseert uw organisatie de CO2-uitstoot?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q45 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre treft de organisatie maatregelen om de CO-voetafdruk van de mobiliteit van werknemers te verkleinen?",
									tooltip = "Heeft uw organisatie dienstauto’s? Kent u bedrijf de woon-werk-trajecten van de medewerkers en het daarbij gebruikte vervoersmiddel? Openbaar vervoerskaarten in gebruik in het bedrijf? Omschakeling van fossiel naar elektrisch vervoer? Kent u de CO2-voetafdruk van het (vlieg-)verkeer van uw bedrijf?"
								}
							}),
							Category = c5,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q46 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre heeft de organisatie een divers personeelsbestand?",
									tooltip = "Mensen met afstand tot arbeidsmarkt, gehandicapten, 50+ ers, leertrajecten, culturele achtergronden, gender"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q47 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre is in de organisatie sprake van gezonde en veilige fysieke en mentale werkomstandigheden?",
									tooltip = "Informeren van werknemers over gezond en velig werken, gebruik van een antidiscriminatieprotocol, rekening houden met de balans. tussen werk en privé, aandacht voor fysieke, sensorische (bv lawaai) en mentale belasting van werk, doelbewust beleid om verspilling en uitputting van medewerkers te voorkomen."
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q48 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In onze organisatie sprake van werkzekerheid voor de werknemers",
									tooltip = "Niet onnodig werken met zzp constructies en arbeidscontracten voor bepaalde tijd, e.d."
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q49 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In onze organisatie zijn werknemers vrij om te beslissen hoe het werk wordt georganiseerd",
									tooltip = "Thuiswerken, werktijden bv"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q50 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In onze organisatie sluit de kennis, vaardigheden en interesse van werknemers erg goed aan bij het werk dat ze doen",
									tooltip = "In onze organisatie vinden werknemers het werk interessant, In onze organisatie is het talent van de werknemer uitgangspunt voor de uit te voeren taken"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q51 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "Er worden concrete acties ingezet zodat werknemers zich betrokken voelen bij de organisatie",
									tooltip = "Bedrijfsuitjes, werktevredenheidsenquetes, pe-cyclus"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q52 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In onze organisatie wordt de ontwikkeling van kennis en vaardigheden en persoonlijke ontwikkeling gestimuleerd",
									tooltip = "Werknemers kunnen werken aan persoonlijke ontwikkeling. Er worden ook werknemers aangenomen die niet direct perfect voldoen aan de vacture-eisen. Er is tijd en ruimte om te groeien in een baan. Werknemersbelang staat voorop bij het aanbieden van scholing. Er is tijd en geld voor coaching van werknemers, ook bij onvoldoende functioneren."
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q53 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre wordt in de hele keten rekening gehouden met sociale aspecten zoals slavernij, kinderarbeid, extreme armoede, buitensporige werktijden, etc.",
									tooltip = "Er wordt niet alleen gekeken naar arbeidsvoorwaarden binnen het bedrijf, maar in de hele levenscyclus van de producten van de organisatie, naar aspecten zoals minimum aanvaardbaar loon (het duurzame loon op lange termijn, of het fatsoenlijke leefloon op korte termijn), kinderarbeid (dwangarbeid, niet in staat om naar school te gaan), extreme armoede (afgeleid van de absolute armoedegrens van de Wereldbank), buitensporige werktijden (dwangarbeid, onvrijwillig), veiligheid en gezondheid op het werk (gebaseerd op statistieken van de IAO)"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
							CreatedAt = DateTime.Now
						};

						Question q54 = new Question()
						{
							Data = JsonConvert.SerializeObject(new
							{
								nl = new
								{
									question = "In hoeverre krijgen werknemers een eerlijke beloning en goede secundaire arbeidsvoorwaarden?",
									tooltip = "scheve verhoudingen, tov branchegenoten, winstuitkering alle werknemers"
								}
							}),
							Category = c6,
							Weight = 1,
							Statement = false,
							Show = true,
							Image = "https://placehold.co/600x400/EEE/31343C",
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
					}
				}

				context.SaveChanges();
			}
		}
	}
}
