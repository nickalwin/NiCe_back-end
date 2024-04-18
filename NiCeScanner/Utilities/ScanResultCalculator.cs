using Newtonsoft.Json;
using NiCeScanner.Models;
using NiCeScanner.Resources.Request.Scan;

namespace NiCeScanner.Utilities
{
	/// <summary>
	/// 	Provides methods to calculate and serialize scan results.
	/// </summary>
	internal static class ScanResultCalculator
	{
		/// <summary>
		/// 	Calculates the weighted means of the scan results for each category.
		/// </summary>
		/// <param name="answers">The collection of answer elements.</param>
		/// <param name="questionDictionary">The dictionary of questions.</param>
		/// <returns>A dictionary containing the category UUIDs and their corresponding weighted means.</returns>
		internal static Dictionary<Guid, double> CalculateResults(IEnumerable<AnswerElement> answers, Dictionary<Guid, Question> questionDictionary)
		{
			Dictionary<Guid, double> totalWeightedScores = new Dictionary<Guid, double>();
			Dictionary<Guid, double> totalWeights = new Dictionary<Guid, double>();

			foreach (var answer in answers)
			{
				Guid questionUuid = answer.Question_uuid;
				Guid categoryUuid = answer.Category_uuid;

				short score = answer.Answer;

				if (score == 0|| score == -1) // not applicable || i dont know, TODO, to be asked about how the calculation should work
				{
					continue;
				}

				short weight = questionDictionary[questionUuid].Weight;

				double weightedScore = score * weight;

				if (totalWeightedScores.ContainsKey(categoryUuid))
				{
					totalWeightedScores[categoryUuid] += weightedScore;
					totalWeights[categoryUuid] += weight;
				}
				else
				{
					totalWeightedScores[categoryUuid] = weightedScore;
					totalWeights[categoryUuid] = weight;
				}
			}

			Dictionary<Guid, double> categoryWeightedMeans = new Dictionary<Guid, double>();
			foreach (var categoryUuid in totalWeightedScores.Keys)
			{
				categoryWeightedMeans[categoryUuid] = totalWeightedScores[categoryUuid] / totalWeights[categoryUuid];
			}

			return categoryWeightedMeans;
		}

		internal static Dictionary<Guid, double> CalculateResults(Scan scan)
		{
			Dictionary<Guid, double> totalWeightedScores = new Dictionary<Guid, double>();
			Dictionary<Guid, double> totalWeights = new Dictionary<Guid, double>();

			foreach (var answer in scan.Answers)
			{
				Guid categoryUuid = answer.Question.Category.Uuid;

				short score = answer.Score;

				if (score == 0 || score == -1) // not applicable || i dont know, TODO, to be asked about how the calculation should work
				{
					continue;
				}

				short weight = answer.Question.Weight;

				double weightedScore = score * weight;

				if (totalWeightedScores.ContainsKey(categoryUuid))
				{
					totalWeightedScores[categoryUuid] += weightedScore;
					totalWeights[categoryUuid] += weight;
				}
				else
				{
					totalWeightedScores[categoryUuid] = weightedScore;
					totalWeights[categoryUuid] = weight;
				}
			}

			Dictionary<Guid, double> categoryWeightedMeans = new Dictionary<Guid, double>();
			foreach (var categoryUuid in totalWeightedScores.Keys)
			{
				categoryWeightedMeans[categoryUuid] = totalWeightedScores[categoryUuid] / totalWeights[categoryUuid];
			}

			return categoryWeightedMeans;
		}

		/// <summary>
		///		Serializes the category weighted means into a JSON string.
		/// </summary>
		/// <param name="categoryWeightedMeans">The dictionary of category UUIDs and their corresponding weighted means.</param>
		/// <param name="categories">The list of categories.</param>
		/// <returns>A JSON string representing the category results.</returns>
		internal static string SerializeResults(Dictionary<Guid, double> categoryWeightedMeans, List<Category> categories)
		{
			var categoryResults = (
				from c in categories
				join m in categoryWeightedMeans on c.Uuid equals m.Key
				select new
				{
					category_data = c.Data,
					category_uuid = c.Uuid,
					mean = m.Value
				}
			)
			.OrderBy(c => c.mean)
			.ToList();

			return JsonConvert.SerializeObject(categoryResults);
		}
	}

}
