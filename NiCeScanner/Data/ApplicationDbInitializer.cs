using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using NiCeScanner.Models;

namespace NiCeScanner.Data
{
	public class ApplicationDbInitializer
	{
		public static void Seed(IApplicationBuilder appBuilder)
		{
			using (IServiceScope serviceScope = appBuilder.ApplicationServices.CreateScope())
			{
				ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				if (!context.Categories.Any())
				{
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
								},
								en = new
								{
									question = "Recover; Lowest circularity level: Primary business processes ensure that products are designed so that materials at the end of their lifespan can be safely incinerated with energy recovery?",
									tooltip = "For services: does the use of your services prevent products and materials from being incinerated with energy recovery?"
								}
							}),
							Category = c1,
							Weight = 0,
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
									question = "Recycling; In welke mate zijn de materialen in uw product gerecycled en recyclebaar, kunnen de materialen worden verwerkt tot grondstoffen met, bij voorkeur, dezelfde (hoogwaardige) of eventueel tot mindere (laagwaardige) kwaliteit dan de oorspronkelijke grondstof tot en wordt dit in de praktijk toegepast/geborgd?",
									tooltip = "Bij diensten: Zet je producten die uit materialen bestaan die gerecycled kunnen worden?"
								},
								en = new
								{
									question = "Recycling; To what extent are the materials in your product recycled and recyclable, can the materials be processed into raw materials with, preferably, the same (high-quality) or possibly lower (low-quality) quality than the original raw material, and is this applied/guaranteed in practice?",
									tooltip = "For services: Do you use products made from materials that can be recycled?"
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
								},
								en = new
								{
									question = "Repair, Refurbish, Remanufacture, and Repurpose; Primary business processes ensure that products are designed so that they can be efficiently repaired, refurbished, or updated? Is there a long warranty? Do you use refurbished parts/products yourself?",
									tooltip = "For services: repair café, maintenance contracts, refurbishing, using repairable products in services. Can you quickly deliver replacement parts for products already delivered to your customers? Do you still supply parts for products that have been discontinued? Do you invest in affordable maintenance for your customers so that the products in your product range last longer? Are returned products refurbished for resale? Do you sell products where the parts can be efficiently replaced?"
								}
							}),
							Category = c1,
							Weight = 2,
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
								},
								en = new
								{
									question = "Reuse: Primary business processes ensure that products are designed for a long lifespan, products and parts thereof are reused in the same function, and is this practice ensured by you (e.g., through deposit/repurchase scheme)?",
									tooltip = "For services: e.g., using used products. Are products used in your services reused multiple times? Ensuring the sharing and redistribution of old products."
								}
							}),
							Category = c1,
							Weight = 2,
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
								},
								en = new
								{
									question = "Reduce: Primary business processes ensure continuous attention to the reduction of raw materials and primary and critical materials, energy, and water in products and during the usage phase?",
									tooltip = "For services: Do you use products where efficient use of raw materials in production and usage phase has been considered? Maintenance of appliances/machines to keep them efficient."
								}
							}),
							Category = c1,
							Weight = 2,
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
									question = "Rethink: Primaire bedrijfsprocessen zorgen ervoor dat producten worden ontworpen vanuit circulariteitsprincipes, om zo lang mogelijk mee te gaan, voor standaardisatie en compatibiliteit, makkelijk onderhoud en reparatie, op te waarderen en aan te passen, demontage & recycling met end-of-life strategieën en alles met zo laag mogelijke negatieve impact?",
									tooltip = "Wordt uw product bijvoorbeeld via platforms aangeboden t.b.v. intensiever gebruik? Of kan deze multifunctioneel worden ingezet?"
								},
								en = new
								{
									question = "Rethink: Primary business processes ensure that products are designed based on circularity principles, to last as long as possible, for standardization and compatibility, easy maintenance and repair, upgradability and adaptability, disassembly & recycling with end-of-life strategies, and all with the lowest possible negative impact?",
									tooltip = "For example, is your product offered via platforms for more intensive use? Or can it be used multifunctionally?"
								}
							}),
							Category = c1,
							Weight = 3,
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
								},
								en = new
								{
									question = "Refuse: Do you prevent the use of raw materials by using or developing solutions that render products unnecessary, by foregoing the function or by delivering/using a radically different and more sustainable product?",
									tooltip = "For services: for example, developing a service that eliminates the need for new products, such as a diaper washing service. Hot water instead of chemical weed killer. Innovation to prevent material use."
								}
							}),
							Category = c1,
							Weight = 3,
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
								},
								en = new
								{
									question = "Primary business processes ensure that a logistics system is set up for the collection, sorting, and tracing of products, parts, and materials?",
									tooltip = "Understand what happens to products, what breaks first, when and why products are discarded, whether they are repaired, resold, modified, know where the materials in products are, etc."
								}
							}),
							Category = c1,
							Weight = 3,
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
								},
								en = new
								{
									question = "Do primary business processes ensure deliberate work with circular business models and revenue models?",
									tooltip = "There are many different circular business models and revenue models. For example: Performance model (contract based on pay-per-performance), Middleman model (recover, refurbish & resell or repair), Access model (providing access to product), classic sustainable model (long lifespan), and hybrid model (combination of long lifespan and sale of associated consumables)."
								}
							}),
							Category = c1,
							Weight = 3,
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
								},
								en = new
								{
									question = "A large part of the company's revenue comes from circular activities",
									tooltip = ""
								}
							}),
							Category = c1,
							Weight = 3,
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
								},
								en = new
								{
									question = "Primary business processes ensure that conducting an LCA (Life Cycle Assessment) or something similar is standard?",
									tooltip = ""
								}
							}),
							Category = c1,
							Weight = 2,
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
								},
								en = new
								{
									question = "Primary business processes are designed to minimize the loss of materials, energy, and water in production/business processes?",
									tooltip = "waste loss"
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
								},
								en = new
								{
									question = "Primary business processes are designed so that byproducts (materials, energy, water) from production/business processes are reused or recycled?",
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
									question = "Verleng je de levensduur van je materiële activa, zoals machines en gebouwen door gepland onderhoud en reparaties?",
									tooltip = "levensduur machines"
								},
								en = new
								{
									question = "Do you extend the lifespan of your tangible assets, such as machinery and buildings, through planned maintenance and repairs?",
									tooltip = "machine lifespan"
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
								},
								en = new
								{
									question = "Do you ensure minimal harmful emissions during production/business processes and logistics?",
									tooltip = "For example, sourcing locally, environmentally friendly production processes, efficient and sustainable transportation methods, avoiding unnecessary travel, etc."
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
								},
								en = new
								{
									question = "Do you use energy from renewable sources for business processes/production (solar, wind, water, geothermal)?",
									tooltip = "sustainable energy"
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
								},
								en = new
								{
									question = "Is no or circular packaging material used, and is the packaging material (cardboard, pallets) taken back for storage and transportation after delivery?",
									tooltip = "packaging"
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
									tooltip = "Samenwerking in het valoriseren van reststromen (materialen, warmte, etc.), het afnemen van en/of leveren aan andere organisaties. Maak je afspraken met je klanten over mogelijke terugname van geleverde producten na gebruik? (zoals statiegeld of een minimale prijs waarvoor je producten wilt terugkopen na gebruik)?"
								},
								en = new
								{
									question = "Is there systematic collaboration with stakeholders to close loops within and/or outside the chain?",
									tooltip = "Collaboration in valorizing byproducts (materials, heat, etc.), procuring from and/or supplying to other organizations. Do you make agreements with your customers about the possible return of delivered products after use? (such as deposit or a minimum price at which you want to buy back products after use)?"
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
								},
								en = new
								{
									question = "Do you collaborate structurally with other organizations in the chain to improve environmental and social impact?",
									tooltip = "Broad, coordination, structural vs incidental, Is there a chain coordinator present?"
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
								},
								en = new
								{
									question = "Do you set requirements for suppliers regarding circularity? (toxicity and recyclability of materials, recycled materials, working conditions, etc)",
									tooltip = "Do you gather information about the origin of materials and the conditions under which people extract these materials?"
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
									tooltip = "bv afstand, intenties, waarvoor worden producten gebruikt, wordt bewust nagedacht over samenwerken met circulaire bedrijven of oude economie bedrijven"
								},
								en = new
								{
									question = "Customer portfolio, do you set requirements for your customers/clients regarding circularity?",
									tooltip = "e.g., distance, intentions, what are the products used for, is there conscious consideration for collaborating with circular companies or traditional economy companies"
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
								},
								en = new
								{
									question = "Are customers transparently informed about the circular impact of the organization?",
									tooltip = "Transparent communication of what is going well, but also of what can be improved. Does the communication include material and energy consumption of products and services, the origin of materials and recyclability, and are the True Price and Total Cost of Ownership communicated?"
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
									tooltip = "Werk je samen zodat producten goed onderhouden en/of gerepareerd worden zodat producten langer efficiënt gebruikt worden? (onderhouds- en servicecontracten in samenwerking met andere marktpartijen) Werk je samen om product as a service of pay per performance, etc. te organiseren?"
								},
								en = new
								{
									question = "Establishing partnerships to take responsibility for products in the usage phase and beyond?",
									tooltip = "Do you collaborate to ensure that products are well-maintained and/or repaired so that they are used efficiently for a longer period? (maintenance and service contracts in collaboration with other market parties) Do you collaborate to organize product as a service or pay-per-performance, etc.?"
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
									tooltip = "Kennis delen waarmee de transitie kan worden versneld over producten, materialen en processen. Werkt u samen om inzichtelijk te maken waar in de keten de grootste milieu- en sociale impactverbetering te behalen is (bijv. aan de hand van LCA analyse)?"
								},
								en = new
								{
									question = "Knowledge sharing, inspiring, etc. with other organizations",
									tooltip = "Sharing knowledge to accelerate the transition regarding products, materials, and processes. Do you collaborate to identify where in the chain the greatest environmental and social impact improvement can be achieved (e.g., based on LCA analysis)?"
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
								},
								en = new
								{
									question = "Collaboration to influence government policies and regulations that hinder circularity?",
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
								},
								en = new
								{
									question = "Does the circular ambition and multiple value creation explicitly state in the organization's vision and mission?",
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
								},
								en = new
								{
									question = "To what extent has the organization translated the vision into concrete goals to achieve full circularity (by 2050 at the latest)?",
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
								},
								en = new
								{
									question = "To what extent does the organization have a strategy to achieve full circularity?",
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
								},
								en = new
								{
									question = "To what extent does the organization serve as a role model as a frontrunner in the sector when it comes to circularity?",
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
								},
								en = new
								{
									question = "To what extent does the organization transparently publish their circular and societal impact (both positive and negative impact) in, for example, an annual report?",
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
								},
								en = new
								{
									question = "Is the personnel policy explicitly focused on social aspects such as inclusivity and retention and development of work capacity?",
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
								},
								en = new
								{
									question = "Is the recruitment, training, and knowledge policy focused on acquiring and sharing knowledge and skills in the field of circular economy?",
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
								},
								en = new
								{
									question = "To what extent are buildings and premises used by the organization developed in a circular manner?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 3,
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
								},
								en = new
								{
									question = "To what extent is water used efficiently, rainwater harvested, water stored, and water purified?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 2,
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
								},
								en = new
								{
									question = "To what extent does the organization practice waste separation in its operations in offices, canteens, etc.?",
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
								},
								en = new
								{
									question = "To what extent are as few resources and office supplies used and consumed as possible? (paper, water, pens, etc.)",
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
								},
								en = new
								{
									question = "To what extent is office furniture kept in use for as long as possible and used as efficiently as possible? (laptops, office chairs, flexible workspaces, etc.)",
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
								},
								en = new
								{
									question = "To what extent is the procurement policy (besides production procurement) focused on circular and local sourcing? Think of office furniture and items, etc.",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 3,
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
								},
								en = new
								{
									question = "Is cleaning done in a sustainable manner?",
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
								},
								en = new
								{
									question = "To what extent are products in the canteen and catering sustainable and locally sourced, and are disposable items avoided?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 2,
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
								},
								en = new
								{
									question = "Does the organization generate sustainable energy?",
									tooltip = ""
								}
							}),
							Category = c5,
							Weight = 2,
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
								},
								en = new
								{
									question = "Does your organization use green energy?",
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
								},
								en = new
								{
									question = "To what extent is the organization actively implementing energy-saving measures across the organization to reduce its CO2 footprint?",
									tooltip = "Do you know how to calculate your organization's CO2 footprint? Are you aware of the costs associated with the current European CO2 price? Does your organization use tools to identify potential energy savings? Are you familiar with subsidy opportunities for energy savings?"
								}
							}),
							Category = c5,
							Weight = 2,
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
								},
								en = new
								{
									question = "Does your organization offset its CO2 emissions?",
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
									tooltip = "Heeft uw organisatie dienstauto’s? Kent uw bedrijf de woon-werk-trajecten van de medewerkers en het daarbij gebruikte vervoersmiddel? Gebruikt uw bedrijf openbaar vervoerskaarten? Schakelt uw bedrijf over van fossiel naar elektrisch vervoer? Kent uw bedrijf de CO2-voetafdruk van het (vlieg-)verkeer?"
								},
								en = new
								{
									question = "To what extent does the organization take measures to reduce the CO2 footprint of employee mobility?",
									tooltip = "Does your organization have company cars? Does your company know the commuting routes of its employees and the means of transportation used? Does your company use public transportation cards? Is your company transitioning from fossil fuel to electric vehicles? Does your company know the CO2 footprint of its (air) travel?"
								}
							}),
							Category = c5,
							Weight = 2,
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
								},
								en = new
								{
									question = "To what extent does the organization have a diverse workforce?",
									tooltip = "People with distance to the labor market, disabled people, individuals over 50, training programs, cultural backgrounds, gender"
								}
							}),
							Category = c6,
							Weight = 2,
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
									tooltip = "Informeren van werknemers over gezond en velig werken, gebruik van een antidiscriminatieprotocol, rekening houden met de balans tussen werk en privé, aandacht voor fysieke, sensorische (bv lawaai) en mentale belasting van werk, doelbewust beleid om verspilling en uitputting van medewerkers te voorkomen."
								},
								en = new
								{
									question = "To what extent are healthy and safe physical and mental working conditions maintained within the organization?",
									tooltip = "Informing employees about healthy and safe working, use of an anti-discrimination protocol, consideration for work-life balance, attention to physical, sensory (e.g. noise), and mental workloads, deliberate policy to prevent waste and exhaustion of employees."
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
									question = "In onze organisatie sprake van werkzekerheid voor de werknemers?",
									tooltip = "Niet onnodig werken met zzp-constructies en arbeidscontracten voor bepaalde tijd, enz."
								},
								en = new
								{
									question = "Does our organization provide job security for its employees?",
									tooltip = "Avoiding unnecessary use of self-employed (ZZP) constructions and fixed-term employment contracts, etc."
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
									question = "In onze organisatie zijn werknemers vrij om te beslissen hoe het werk wordt georganiseerd?",
									tooltip = "Thuiswerken, werktijden, enz."
								},
								en = new
								{
									question = "In our organization, do employees have the freedom to decide how the work is organized?",
									tooltip = "Remote work, working hours, etc."
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
									question = "In onze organisatie sluit de kennis, vaardigheden en interesse van werknemers erg goed aan bij het werk dat ze doen?",
									tooltip = "In onze organisatie vinden werknemers het werk interessant. In onze organisatie is het talent van de werknemer het uitgangspunt voor de uit te voeren taken."
								},
								en = new
								{
									question = "In our organization, do the knowledge, skills, and interests of employees align well with the work they do?",
									tooltip = "In our organization, employees find the work interesting. In our organization, the talent of the employee is the basis for the tasks to be performed."
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
									question = "Er worden concrete acties ingezet zodat werknemers zich betrokken voelen bij de organisatie?",
									tooltip = "Bedrijfsuitjes, werktevredenheidsenquêtes, personeelscyclus"
								},
								en = new
								{
									question = "Are concrete actions taken to make employees feel engaged with the organization?",
									tooltip = "Company outings, employee satisfaction surveys, personnel cycle"
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
									question = "In onze organisatie wordt de ontwikkeling van kennis en vaardigheden en persoonlijke ontwikkeling gestimuleerd?",
									tooltip = "Werknemers kunnen werken aan persoonlijke ontwikkeling. Er worden ook werknemers aangenomen die niet direct perfect voldoen aan de vacature-eisen. Er is tijd en ruimte om te groeien in een baan. Werknemersbelang staat voorop bij het aanbieden van scholing. Er is tijd en geld voor coaching van werknemers, ook bij onvoldoende functioneren."
								},
								en = new
								{
									question = "In our organization, is the development of knowledge, skills, and personal growth encouraged?",
									tooltip = "Employees can work on personal development. Employees are hired even if they don't perfectly match the job requirements. There is time and space for growth within a job. Employee welfare is a priority when offering training. Time and resources are allocated for employee coaching, even when performance is insufficient."
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
									question = "In hoeverre wordt in de hele keten rekening gehouden met sociale aspecten zoals slavernij, kinderarbeid, extreme armoede, buitensporige werktijden, etc.?",
									tooltip = "Er wordt niet alleen gekeken naar arbeidsvoorwaarden binnen het bedrijf, maar in de hele levenscyclus van de producten van de organisatie, naar aspecten zoals minimum aanvaardbaar loon (het duurzame loon op lange termijn, of het fatsoenlijke leefloon op korte termijn), kinderarbeid (dwangarbeid, niet in staat om naar school te gaan), extreme armoede (afgeleid van de absolute armoedegrens van de Wereldbank), buitensporige werktijden (dwangarbeid, onvrijwillig), veiligheid en gezondheid op het werk (gebaseerd op statistieken van de IAO)"
								},
								en = new
								{
									question = "To what extent are social aspects such as slavery, child labor, extreme poverty, excessive working hours, etc., taken into account throughout the entire chain?",
									tooltip = "Not only are labor conditions within the company considered, but throughout the entire lifecycle of the organization's products, aspects such as minimum acceptable wage (sustainable wage in the long term, or decent living wage in the short term), child labor (forced labor, inability to attend school), extreme poverty (derived from the World Bank's absolute poverty line), excessive working hours (forced labor, involuntary), workplace safety and health (based on ILO statistics)."
								}
							}),
							Category = c6,
							Weight = 2,
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
								},
								en = new
								{
									question = "To what extent do employees receive fair compensation and good secondary employment benefits?",
									tooltip = "imbalance, compared to industry peers, profit sharing for all employees"
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

				if (!context.Sectors.Any())
				{
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
				}

				context.SaveChanges();
			}
		}
	}
}
