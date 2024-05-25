using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using zba.intranet.modules.Models;

public class AdminController : Controller
{
    private readonly HttpClient _client;

    public AdminController()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:5000/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> AddQuestionAndAnswers(string questionText, bool isActive, List<SingleAnswerDto> answers)
    {
        var addQuestionDto = new { questionText = questionText, isActive = isActive };
        var content = new StringContent(JsonConvert.SerializeObject(new { addQuestionDto }), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Question/AddQuestion", content);
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var questionId = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseData)["id"];

            var addAnswerDto = new AddAnswerDto
            {
                QuestionId = questionId,
                SingleAnswerDto = answers
            };

            var answerContent = new StringContent(JsonConvert.SerializeObject(new { addAnswerDto }), Encoding.UTF8, "application/json");
            var answerResponse = await _client.PostAsync("api/Answer/AddAnswer", answerContent);

            if (answerResponse.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                var errorResponse = await answerResponse.Content.ReadAsStringAsync();
                return Json(new { success = false, error = errorResponse });
            }
        }

        return Json(new { success = false });
    }

    [HttpGet]
    public async Task<ActionResult> GetQuestions(int index = 1, int size = 10, string searchValue = "")
    {
        var response = await _client.GetAsync($"api/Question/GetAll?index={index}&size={size}&searchValue={searchValue}");
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return Content(responseData, "application/json");
        }

        return Json(null);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteQuestion(int id)
    {
        var deleteQuestionDto = new { id };
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(_client.BaseAddress, "api/Question/DeleteQuestion"),
            Content = new StringContent(JsonConvert.SerializeObject(new { deleteQuestionDto }), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseData)["isSuccess"];
            return Json(new { success = result });
        }

        return Json(new { success = false });
    }

}
